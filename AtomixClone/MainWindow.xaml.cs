using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Forms.Integration;

namespace AtomixClone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double TileWidthHeight { get; } = 61;
        private sbyte[,] Spiral { get; }
        private Level[] Levels { get; }
        private Border[,] Tiles { get; }
        private byte[,] TileContents { get; }
        private Border[,] InfoTiles { get; }
        private Array LevelWallMapping { get; }
        private CircularArray<Direction> CircularArray { get; }
        private Shape SelectionOutline { get; set; }
        private Brush[] SelectionCircleColours { get; }
        private byte AnimatedByte { get; set; }
        private byte AnimatedCircleColour { get; set; }
        private byte CurrentLevel { get; set; }
        private bool LevelInitialised { get; set; }
        private bool LevelSolved { get; set; }
        private byte NumberOfAtoms { get; set; }
        private byte Selected { get; set; }
        private Direction Moving { get; set; }
        private Queue<Direction> NextMoves { get; set; }
        private FrameworkElement BoxElement { get; set; }
        private FrameworkElement ChildBoxElement { get; set; }
        private FrameworkElement SelectedElement { get; set; }
        private FrameworkElement ChildSelectedElement { get; set; }
        private int BoxPositionX { get; set; }
        private int BoxPositionY { get; set; }
        private int AtomPositionX { get; set; }
        private int AtomPositionY { get; set; }
        private int LowerBoxBound { get; set; }
        private int UpperBoxBound { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Spiral = GameData.Spiral;

            Levels = GameData.Levels;

            SelectionCircleColours = new Brush[]
            {
                (Brush)App.Current.Resources["Gradient0"],
                (Brush)App.Current.Resources["Gradient1"],
                (Brush)App.Current.Resources["Gradient2"],
                (Brush)App.Current.Resources["Gradient3"]
            };

            CompositionTarget.Rendering += Rendering;

            Tiles = new Border[13, 16];
            TileContents = new byte[13, 16];
            InfoTiles = new Border[7, 7];

            Type thisType = this.GetType();
            for (byte i = 0; i < Tiles.GetLength(0); ++i)
                for (byte j = 0; j < Tiles.GetLength(1); ++j)
                    Tiles[i, j] = (Border)thisType.GetField(string.Format("border_{0:d2}_{1:d2}", i, j), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            for (byte i = 0; i < InfoTiles.GetLength(0); ++i)
                for (byte j = 0; j < InfoTiles.GetLength(1); ++j)
                    InfoTiles[i, j] = (Border)thisType.GetField(string.Format("border_{0:d}_{1:d}", i, j), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

            NextMoves = new Queue<Direction>(2);
            // We shall use this circular array to detect long pressed arrow keys.
            CircularArray = new CircularArray<Direction>(3);
            LevelWallMapping = (Array)App.Current.Resources["LevelWallMapping"];
        }

        private void Rendering(object sender, EventArgs args)
        {
            if (!LevelInitialised || LevelSolved) return;

            AnimateByte();

            if (Selected == 255)
                AnimateBox();
            else
                AnimateAtom();
        }

        void AnimateByte()
        {
            ++AnimatedByte;
            if (Selected == 255) return;
            byte b = (byte)((AnimatedByte / 8) % 4);
            if (AnimatedCircleColour != b)
            {
                AnimatedCircleColour = b;
                SelectionOutline.Stroke = SelectionCircleColours[AnimatedCircleColour];
            }
        }

        private void AnimateBox()
        {
            if (Moving != Direction.None)
            {
                double x = (double)ChildBoxElement.Tag;
                double y = (double)BoxElement.Tag;

                if (BoxPositionX == x && BoxPositionY == y)
                {
                    Moving = Direction.None;
                }
                else
                {
                    switch (Moving)
                    {
                        case Direction.Left:
                            ChildBoxElement.Tag = x - (x % TileWidthHeight == 11 ? 11 : 10);
                            break;
                        case Direction.Right:
                            ChildBoxElement.Tag = x + (x % TileWidthHeight == 50 ? 11 : 10);
                            break;
                        case Direction.Top:
                            BoxElement.Tag = y - (y % TileWidthHeight == 11 ? 11 : 10);
                            break;
                        case Direction.Bottom:
                            BoxElement.Tag = y + (y % TileWidthHeight == 50 ? 11 : 10);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Moving == Direction.None)
            {
                if (NextMoves.Count > 0)
                {
                    Moving = NextMoves.Dequeue();

                    switch (Moving)
                    {
                        case Direction.Left:
                            if (BoxPositionX < Math.Max(TileWidthHeight, LowerBoxBound + TileWidthHeight)) goto default;
                            BoxPositionX -= (byte)TileWidthHeight;
                            break;
                        case Direction.Right:
                            if (BoxPositionX > Math.Min(854, UpperBoxBound - TileWidthHeight)) goto default;
                            BoxPositionX += (byte)TileWidthHeight;
                            break;
                        case Direction.Top:
                            if (BoxPositionY < Math.Max(TileWidthHeight, LowerBoxBound + TileWidthHeight)) goto default;
                            BoxPositionY -= (byte)TileWidthHeight;
                            break;
                        case Direction.Bottom:
                            if (BoxPositionY > Math.Min(671, UpperBoxBound - TileWidthHeight)) goto default;
                            BoxPositionY += (byte)TileWidthHeight;
                            break;
                        default:
                            Moving = Direction.None;
                            break;
                    }
                }
            }
        }

        private void AnimateAtom()
        {
            if (Moving != Direction.None)
            {
                double x = (double)ChildSelectedElement.Tag;
                double y = (double)SelectedElement.Tag;

                if (AtomPositionX == x && AtomPositionY == y)
                {
                    Moving = Direction.None;
                    TileContents[AtomPositionY / (byte)TileWidthHeight, AtomPositionX / (byte)TileWidthHeight] = Selected;
                    if (CheckSolution())
                    {
                        AnimatedByte = 0;
                        LevelFinale();
                    }
                }
                else
                {
                    switch (Moving)
                    {
                        case Direction.Left:
                            ChildSelectedElement.Tag = x - (x % TileWidthHeight == 7 ? 7 : 6);
                            break;
                        case Direction.Right:
                            ChildSelectedElement.Tag = x + (x % TileWidthHeight == 54 ? 7 : 6);
                            break;
                        case Direction.Top:
                            SelectedElement.Tag = y - (y % TileWidthHeight == 7 ? 7 : 6);
                            break;
                        case Direction.Bottom:
                            SelectedElement.Tag = y + (y % TileWidthHeight == 54 ? 7 : 6);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Moving == Direction.None)
            {
                if (NextMoves.Count > 0)
                {
                    Moving = NextMoves.Dequeue();
                    AtomPositionX = Convert.ToInt32(ChildSelectedElement.Tag);
                    AtomPositionY = Convert.ToInt32(SelectedElement.Tag);
                    int i = AtomPositionY / (byte)TileWidthHeight;
                    int j = AtomPositionX / (byte)TileWidthHeight;
                    byte d = 0;

                    switch (Moving)
                    {
                        case Direction.Left:
                            while (TileContents[i, j - (d + 1)] == 0) ++d;
                            if (d == 0) goto default;
                            AtomPositionX -= d * (byte)TileWidthHeight;
                            break;
                        case Direction.Right:
                            while (TileContents[i, j + (d + 1)] == 0) ++d;
                            if (d == 0) goto default;
                            AtomPositionX += d * (byte)TileWidthHeight;
                            break;
                        case Direction.Top:
                            while (TileContents[i - (d + 1), j] == 0) ++d;
                            if (d == 0) goto default;
                            AtomPositionY -= d * (byte)TileWidthHeight;
                            break;
                        case Direction.Bottom:
                            while (TileContents[i + (d + 1), j] == 0) ++d;
                            if (d == 0) goto default;
                            AtomPositionY += d * (byte)TileWidthHeight;
                            break;
                        default:
                            Moving = Direction.None;
                            break;
                    }

                    if (Moving != Direction.None)
                        TileContents[i, j] = 0;
                }
            }
        }

        private bool CheckSolution()
        {
            Level level = Levels[CurrentLevel];
            Border element = (Border)gameCanvas.Children[1];
            Grid childElement = (Grid)element.Child;
            int x = Convert.ToInt32(childElement.Tag);
            int y = Convert.ToInt32(element.Tag);
            byte i = (byte)(y / TileWidthHeight);
            byte j = (byte)(x / TileWidthHeight);
            byte p = (byte)TileContents.GetLength(0);
            byte q = (byte)TileContents.GetLength(1);
            byte k, l, index, value;
            for (byte n = 0; n < level.Solution.Length; ++n)
            {
                k = (byte)(i + Spiral[n, 0]);
                l = (byte)(j + Spiral[n, 1]);
                if (k < 0 || k >= p || l < 0 || l >= q)
                {
                    value = 0;
                }
                else
                {
                    index = TileContents[k, l];
                    if (index == 0 || index == 255)
                        value = 0;
                    else
                        value = level.AtomPositions[index - 1, 0];
                }

                if (level.Solution[n] != value) return false;
            }

            return true;
        }

        private async void LevelFinale()
        {
            LevelSolved = true;
            modalBorder.IsHitTestVisible = true;
            ((Grid)ChildSelectedElement).Children.RemoveAt(1);
            Selected = 255;
            await Task.Delay(500);
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
            timer.Tick += (object s, EventArgs e) =>
            {
                gameCanvas.Children.RemoveAt(1);
                if (gameCanvas.Children.Count == 2)
                {
                    timer.Stop();
                    LevelInitialised = false;
                    LevelSolved = false;
                    NextLevel();
                }
                else
                {
                    AtomDisappears();
                }
            };

            AtomDisappears();
            timer.Start();
        }

        private void AtomDisappears()
        {
            Border b;
            Grid g;
            b = (Border)gameCanvas.Children[1];
            g = (Grid)b.Child;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AtomixClone.Images.finale.gif");
            System.Drawing.Image gifImage = System.Drawing.Image.FromStream(stream);
            g.Children.RemoveAt(0);
            g.Children.Add(new WindowsFormsHost()
            {
                Width = TileWidthHeight - 10,
                Height = TileWidthHeight - 10,
                Child = new System.Windows.Forms.PictureBox()
                {
                    Width = (int)TileWidthHeight - 10,
                    Height = (int)TileWidthHeight - 10,
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                    Image = gifImage
                }
            });
        }

        private void NextLevel()
        {
            ColorAnimation colourAnimation = new ColorAnimation() { From = Colors.Transparent, To = Colors.Black };
            colourAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            colourAnimation.FillBehavior = FillBehavior.HoldEnd;
            colourAnimation.Completed += (object s, EventArgs e) =>
            {
                ++CurrentLevel;
                if (CurrentLevel == Levels.Length)
                {
                    this.Title = "Game complete";
                    return;
                }
                ClearLevel();
                Thread.Sleep(500);
                modalBorder.BeginAnimation(Border.TagProperty, null);
                StartButton_Click(null, null);
            };

            modalBorder.BeginAnimation(Border.TagProperty, colourAnimation);
        }

        private void ClearLevel()
        {
            moleculeUp.ClearValue(TextBlock.TextProperty);
            moleculeDown.ClearValue(TextBlock.TextProperty);
            Level level = Levels[CurrentLevel - 1];
            ((IList)TileContents).Clear();
            for (byte i = 0; i < Tiles.GetLength(0); ++i)
                for (byte j = 0; j < Tiles.GetLength(1); ++j)
                    if (level.Tiles[i][j]) Tiles[i, j].ClearValue(Border.TagProperty);
            gameCanvas.Children.RemoveAt(1);

            byte p = level.SpiralCentre[0];
            byte q = level.SpiralCentre[1];
            for (byte n = 0; n < level.Solution.Length; ++n)
            {
                if (level.Solution[n] == 0) continue;
                InfoTiles[p + Spiral[n, 0], q + Spiral[n, 1]].Child = null;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs args)
        {
            Title = string.Format("Atomix - Level {0}", CurrentLevel + 1);
            modalBorder.Child = null;
            InitialiseLevel();
            modalBorder.Tag = Colors.Transparent;
            modalBorder.IsHitTestVisible = false;
        }

        private void InitialiseLevel()
        {
            Level level = Levels[CurrentLevel];
            NumberOfAtoms = (byte)(level.AtomPositions.GetLength(0) - 1);
            if (!string.IsNullOrEmpty(level.MoleculeUp))
            {
                moleculeUp.Text = level.MoleculeUp;
                moleculeDown.Text = level.MoleculeDown;
                SelectionOutline = (Ellipse)App.Current.Resources["SelectionCircle"];
            }
            else
            {
                moleculeUp.Text = "BONUS";
                moleculeDown.Text = "STAGE";
                SelectionOutline = (Rectangle)App.Current.Resources["SelectionRect"];
            }

            AnimatedByte = 255;
            AnimatedCircleColour = 255; // 255 means that no colour is selected.
            Selected = 255;  // 255 means that no atom is selected.
            Moving = Direction.None;    // Nothing is moving.
            NextMoves.Clear();
            CircularArray.Clear();

            App.Current.Resources["WallLevel"] = App.Current.Resources[string.Format("WallLevel{0}", LevelWallMapping.GetValue(CurrentLevel))];

            for (byte i = 0; i < Tiles.GetLength(0); ++i)
                for (byte j = 0; j < Tiles.GetLength(1); ++j)
                    if (level.Tiles[i][j])
                    {
                        Tiles[i, j].Tag = null;
                        TileContents[i, j] = 255;
                    }

            Border border;
            for (byte i = 0; i < level.AtomPositions.GetLength(0); ++i)
            {
                border = new Border
                {
                    Width = TileWidthHeight,
                    Height = TileWidthHeight,
                    ClipToBounds = true,
                    // Tag must be initialised with a double value. If not, binding in AtomBorderStyle will not work.
                    Tag = TileWidthHeight * level.AtomPositions[i, 1],
                    Child = new Grid()
                    {
                        Tag = TileWidthHeight * level.AtomPositions[i, 2]
                    }
                };

                if (i != NumberOfAtoms)
                {
                    border.Style = (Style)Application.Current.Resources["AtomBorderStyle"];
                    ((Grid)border.Child).Children.Add(new AtomElement((Atoms)level.AtomPositions[i, 0]));
                    TileContents[level.AtomPositions[i, 1], level.AtomPositions[i, 2]] = (byte)(i + 1);
                }
                else
                {
                    border.Style = (Style)Application.Current.Resources["BoxBorderStyle"];
                    BoxElement = border;
                    ChildBoxElement = (FrameworkElement)border.Child;
                    BoxPositionX = Convert.ToInt32(ChildBoxElement.Tag);
                    BoxPositionY = Convert.ToInt32(BoxElement.Tag);
                }
                gameCanvas.Children.Add(border);
            }

            byte p = level.SpiralCentre[0];
            byte q = level.SpiralCentre[1];
            for (byte n = 0; n < level.Solution.Length; ++n)
            {
                if (level.Solution[n] == 0) continue;
                InfoTiles[p + Spiral[n, 0], q + Spiral[n, 1]].Child = new AtomElement((Atoms)level.Solution[n]);
            }

            LevelInitialised = true;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs args)
        {
            if (!LevelInitialised || LevelSolved) return;

            UpperBoxBound = int.MaxValue;
            LowerBoxBound = int.MinValue;

            byte key = (byte)args.Key;

            if (args.Key == Key.Space || args.Key == Key.Enter || args.Key == Key.Return)
            {
                if (Moving != Direction.None) return;

                Border element;
                Grid childElement;

                if (Selected != 255)
                {
                    element = (Border)gameCanvas.Children[Selected];
                    childElement = (Grid)element.Child;
                    Selected = 255;
                    SelectedElement = ChildSelectedElement = null;
                    BoxPositionX = Convert.ToInt32(childElement.Tag);
                    BoxPositionY = Convert.ToInt32(element.Tag);
                    BoxElement.Tag = (double)BoxPositionY;
                    ChildBoxElement.Tag = (double)BoxPositionX;
                    BoxElement.ClearValue(Border.BorderBrushProperty);
                    childElement.Children.RemoveAt(1);
                    return;
                }

                byte b = TileContents[BoxPositionY / (byte)TileWidthHeight, BoxPositionX / (byte)TileWidthHeight];
                if (b != 0 && b != 255)
                {
                    Selected = b;
                    SelectedElement = (FrameworkElement)gameCanvas.Children[Selected];
                    ChildSelectedElement = ((Border)SelectedElement).Child as FrameworkElement;
                    ((Border)BoxElement).BorderBrush = Brushes.Transparent;
                    ((Grid)ChildSelectedElement).Children.Add(SelectionOutline);
                }

                return;
            }

            if (23 <= key && key <= 26)
            {
                Direction direction = (Direction)(key - 22);
                int count = CircularArray.Where(d => d == Direction.None).Select(d => d).Count();
                CircularArray.Add(direction);
                // We detected long pressed arrow key (or first short pressed key).
                if (count < 2) NextMoves.Clear();
                // We want to prevent enquing more than two moves.
                if (NextMoves.Count < 2) NextMoves.Enqueue(direction);
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs args)
        {
            if (!LevelInitialised || LevelSolved) return;

            CircularArray.Add(Direction.None);

            if (Selected != 255) return;

            // If we detected long pressed and released arrow key.
            if (CircularArray.Where(d => d == Direction.None).Select(d => d).Count() < 2)
            {
                int tag = Convert.ToInt32(BoxElement.Tag);
                int childTag = Convert.ToInt32(ChildBoxElement.Tag);
                byte tagRest = (byte)(tag % TileWidthHeight);
                byte childTagRest = (byte)(childTag % TileWidthHeight);

                switch (Moving)
                {
                    case Direction.Left:
                        LowerBoxBound = childTagRest >= 30 ? childTag - childTagRest : childTag - childTagRest - (byte)TileWidthHeight;
                        break;
                    case Direction.Right:
                        UpperBoxBound = childTagRest <= 30 ? childTag - childTagRest + (byte)TileWidthHeight : childTag - childTagRest + 122;
                        break;
                    case Direction.Top:
                        LowerBoxBound = tagRest >= 30 ? tag - tagRest : tag - tagRest - (byte)TileWidthHeight;
                        break;
                    case Direction.Bottom:
                        UpperBoxBound = childTagRest <= 30 ? tag - tagRest + (byte)TileWidthHeight : tag - tagRest + 122;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
