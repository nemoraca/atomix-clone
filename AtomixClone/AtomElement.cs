using System.Windows;
using System.Windows.Media;

namespace AtomixClone
{
    public class AtomElement : FrameworkElement
    {
        public Atoms Atom { get; set; }

        public AtomElement(Atoms atom)
        {
            Atom = atom;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Brush brush;
            byte coord = (byte)(MainWindow.TileWidthHeight / 2);
            Point centre = new Point(coord, coord);
            Pen grayPen = (Pen)Application.Current.Resources["GrayPen"];
            Pen transparentPen = (Pen)Application.Current.Resources["TransparentPen"];

            switch (Atom)
            {
                case Atoms.H_w:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, coord));
                    break;
                case Atoms.H_nw:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, 0));
                    break;
                case Atoms.H_n:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(coord, 0));
                    break;
                case Atoms.H_ne:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, 0));
                    break;
                case Atoms.H_e:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, coord));
                    break;
                case Atoms.H_se:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, MainWindow.TileWidthHeight));
                    break;
                case Atoms.H_s:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(coord, MainWindow.TileWidthHeight));
                    break;
                case Atoms.H_sw:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, MainWindow.TileWidthHeight));
                    break;
                case Atoms.O_w_e:
                    brush = (Brush)Application.Current.Resources["BrushO"];
                    drawingContext.DrawLine(grayPen, new Point(0, coord), new Point(MainWindow.TileWidthHeight, coord));
                    break;
                case Atoms.O_n_s:
                    brush = (Brush)Application.Current.Resources["BrushO"];
                    drawingContext.DrawLine(grayPen, new Point(coord, 0), new Point(coord, MainWindow.TileWidthHeight));
                    break;
                case Atoms.O_w_w:
                    brush = (Brush)Application.Current.Resources["BrushO"];
                    drawingContext.DrawLine(grayPen, new Point(coord, coord - 5), new Point(0, coord - 5));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord + 5), new Point(0, coord + 5));
                    break;
                case Atoms.C_w_n_e_s:
                    brush = (Brush)Application.Current.Resources["BrushC"];
                    drawingContext.DrawLine(grayPen, new Point(0, coord), new Point(MainWindow.TileWidthHeight, coord));
                    drawingContext.DrawLine(grayPen, new Point(coord, 0), new Point(coord, MainWindow.TileWidthHeight));
                    break;
                case Atoms.C_nw_e_e_sw:
                    brush = (Brush)Application.Current.Resources["BrushC"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, 0));
                    drawingContext.DrawLine(grayPen, centre, new Point(0, MainWindow.TileWidthHeight));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord - 5), new Point(MainWindow.TileWidthHeight, coord - 5));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord + 5), new Point(MainWindow.TileWidthHeight, coord + 5));
                    break;
                case Atoms.C_w_w_ne_se:
                    brush = (Brush)Application.Current.Resources["BrushC"];
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, 0));
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, MainWindow.TileWidthHeight));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord - 5), new Point(0, coord - 5));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord + 5), new Point(0, coord + 5));
                    break;
                case Atoms.C_w_n_e_e:
                    brush = (Brush)Application.Current.Resources["BrushC"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, coord));
                    drawingContext.DrawLine(grayPen, centre, new Point(coord, 0));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord - 5), new Point(MainWindow.TileWidthHeight, coord - 5));
                    drawingContext.DrawLine(grayPen, new Point(coord, coord + 5), new Point(MainWindow.TileWidthHeight, coord + 5));
                    break;
                default:
                    brush = (Brush)App.Current.Resources[Atom.ToString()];
                    break;
            }

            if (brush is ImageBrush)
                drawingContext.DrawRectangle(brush, transparentPen, new Rect(0, 0, MainWindow.TileWidthHeight, MainWindow.TileWidthHeight));
            else
                drawingContext.DrawEllipse(brush, transparentPen, centre, coord * 2 / 3, coord * 2 / 3);
        }
    }
}
