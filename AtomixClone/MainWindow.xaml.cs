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

            Spiral = new sbyte[,]
            {
                {0, 0}, {0, -1}, {-1, -1}, {-1, 0}, {-1, 1}, {0, 1}, {1, 1}, {1, 0}, {1, -1}, {1, -2}, {0, -2}, {-1, -2},
                {-2, -2}, {-2, -1}, {-2, 0}, {-2, 1}, {-2, 2}, {-1, 2}, {0, 2}, {1, 2}, {2, 2}, {2, 1}, {2, 0}, {2, -1}, {2, -2},
                {2, -3}, {1, -3}, {0, -3}, {-1, -3}, {-2, -3}, {-3, -3}, {-3, -2}, {-3, -1}, {-3, 0}, {-3, 1}, {-3, 2}, {-3, 3},
                {-2, 3}, {-1, 3}, {0, 3}, {1, 3}, {2, 3}, {3, 3}, {3, 2}, {3, 1}, {3, 0}, {3, -1}, {3, -2}, {3, -3}, {3, -4},
                {2, -4}, {1, -4}, {0, -4}, {-1, -4}, {-2, -4}, {-3, -4}, {-4, -4}, {-4, -3}, {-4, -2}, {-4, -1}, {-4, 0}, {-4, 1},
                {-4, 2}, {-4, 3}, {-4, 4}, {-3, 4}, {-2, 4}, {-1, 4}, {0, 4}, {1, 4}, {2, 4}, {3, 4}, {4, 4}, {4, 3}, {4, 2},
                {4, 1}, {4, 0}, {4, -1}, {4, -2}, {4, -3}, {4, -4}, {4, -5}, {3, -5}, {2, -5}, {1, -5}, {0, -5}, {-1, -5}, {-2, -5},
                {-3, -5}, {-4, -5}, {-5, -5}, {-5, -4}, {-5, -3}, {-5, -2}, {-5, -1}, {-5, 0}, {-5, 1}, {-5, 2}, {-5, 3}, {-5, 4},
                {-5, 5}, {-4, 5}, {-3, 5}, {-2, 5}, {-1, 5}, {0, 5}, {1, 5}, {2, 5}, {3, 5}, {4, 5}, {5, 5}, {5, 4}, {5, 3}, {5, 2},
                {5, 1}, {5, 0}, {5,  -1}, {5, -2}, {5, -3}, {5, -4}, {5, -5}, {5, -6}, {4, -6}, {3, -6}, {2, -6 }, {1, -6}, {0, -6},
                {-1, -6}, {-2, -6}, {-3, -6}, {-4, -6}, {-5, -6}, {-6, -6}, {-6, -5}, {-6, -4}, {-6, -3}, {-6, -2}, {-6, -1}, {-6, 0},
                {-6, 1}, {-6, 2}, {-6, 3}, {-6, 4}, {-6, 5}, {-6, 6}, {-5, 6}, {-4, 6}, {-3, 6}, {-2, 6}, {-1, 6}, {0, 6}, {1, 6},
                {2, 6}, {3, 6}, {4, 6}, {5, 6}, {6, 6}, {6, 5}, {6, 4}, {6, 3}, {6, 2}, {6, 1}, {6, 0}, {6, -1}, {6, -2}, {6, -3},
                {6, -4}, {6, -5}, {6, -6}
            };

            Levels = new Level[] {
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b11111000),
                    new BoolArray(0b00000000, 0b10001000),
                    new BoolArray(0b00011111, 0b10001000),
                    new BoolArray(0b00010000, 0b01001000),
                    new BoolArray(0b00110000, 0b00101000),
                    new BoolArray(0b00101101, 0b00101000),
                    new BoolArray(0b00100101, 0b00001000),
                    new BoolArray(0b00100100, 0b10111000),
                    new BoolArray(0b00100000, 0b00010000),
                    new BoolArray(0b00111111, 0b11110000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "WATER",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_w_e, 7, 11},
                        {(byte)Atoms.H_e, 8, 6},
                        {(byte)Atoms.H_w, 3, 6},
                        {(byte)Atoms.None, 2, 4}
                    },
                    Solution = new byte[] {(byte)Atoms.O_w_e, (byte)Atoms.H_e, 0, 0, 0, (byte)Atoms.H_w},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00111110),
                    new BoolArray(0b00000000, 0b00100010),
                    new BoolArray(0b01111111, 0b11100010),
                    new BoolArray(0b01000000, 0b10101010),
                    new BoolArray(0b01001000, 0b10101010),
                    new BoolArray(0b01001111, 0b10010010),
                    new BoolArray(0b01000010, 0b00000010),
                    new BoolArray(0b01000010, 0b00000010),
                    new BoolArray(0b01000000, 0b11111010),
                    new BoolArray(0b01111111, 0b00100010),
                    new BoolArray(0b00000001, 0b11111110),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "METHANE",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_n_e_s, 5, 6},
                        {(byte)Atoms.H_e, 6, 3},
                        {(byte)Atoms.H_s, 5, 10},
                        {(byte)Atoms.H_w, 9, 9},
                        {(byte)Atoms.H_n, 10, 4},
                        {(byte)Atoms.None, 2, 2}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_n_e_s, (byte)Atoms.H_e, 0, (byte)Atoms.H_s, 0, (byte)Atoms.H_w, 0, (byte)Atoms.H_n},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00111100),
                    new BoolArray(0b00000000, 0b00100100),
                    new BoolArray(0b00000000, 0b00100100),
                    new BoolArray(0b00000000, 0b00100100),
                    new BoolArray(0b00011111, 0b11100100),
                    new BoolArray(0b01110100, 0b01000100),
                    new BoolArray(0b01000100, 0b00010100),
                    new BoolArray(0b01000100, 0b00000100),
                    new BoolArray(0b01010100, 0b11100100),
                    new BoolArray(0b01000000, 0b00000100),
                    new BoolArray(0b01010010, 0b00000100),
                    new BoolArray(0b01010000, 0b11111100),
                    new BoolArray(0b01111111, 0b10000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "MATHANOL",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_w_e, 8, 8},
                        {(byte)Atoms.C_w_n_e_s, 7, 11},
                        {(byte)Atoms.H_s, 3, 4},
                        {(byte)Atoms.H_w, 10, 10},
                        {(byte)Atoms.H_n, 10, 7},
                        {(byte)Atoms.H_e, 8, 4},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.O_w_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, 0, 0, (byte)Atoms.H_w, 0, 0, (byte)Atoms.H_n, 0, (byte)Atoms.H_e},
                    SpiralCentre = new byte[] {3, 4}
                },
                new Level(
                    new BoolArray(0b00000011, 0b11111000),
                    new BoolArray(0b00000010, 0b00101000),
                    new BoolArray(0b00000010, 0b10101000),
                    new BoolArray(0b00000010, 0b00101000),
                    new BoolArray(0b00000010, 0b00101000),
                    new BoolArray(0b00111111, 0b10001000),
                    new BoolArray(0b00100000, 0b00001000),
                    new BoolArray(0b00100000, 0b11001000),
                    new BoolArray(0b00111100, 0b00001000),
                    new BoolArray(0b00100000, 0b10011000),
                    new BoolArray(0b00100010, 0b10001000),
                    new BoolArray(0b00111000, 0b10001000),
                    new BoolArray(0b00001111, 0b11111000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "ETHENE",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_w_ne_se, 7, 5},
                        {(byte)Atoms.C_nw_e_e_sw, 7, 8},
                        {(byte)Atoms.H_sw, 7, 10},
                        {(byte)Atoms.H_nw, 11, 6},
                        {(byte)Atoms.H_ne, 2, 6},
                        {(byte)Atoms.H_se, 9, 8},
                        {(byte)Atoms.None, 1, 4}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_w_ne_se, (byte)Atoms.C_nw_e_e_sw, 0, 0, (byte)Atoms.H_sw, 0, (byte)Atoms.H_nw, 0, 0, (byte)Atoms.H_ne, 0, (byte)Atoms.H_se},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000111, 0b10000000),
                    new BoolArray(0b00000100, 0b11111000),
                    new BoolArray(0b00000100, 0b00001000),
                    new BoolArray(0b01111111, 0b10001000),
                    new BoolArray(0b01000100, 0b00001000),
                    new BoolArray(0b01000110, 0b01111000),
                    new BoolArray(0b01000100, 0b01001110),
                    new BoolArray(0b01000000, 0b01000010),
                    new BoolArray(0b01111111, 0b01001010),
                    new BoolArray(0b01000000, 0b00000010),
                    new BoolArray(0b01001100, 0b00100010),
                    new BoolArray(0b01000000, 0b00100010),
                    new BoolArray(0b01111111, 0b11111110)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "PROPENE",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_n_e_e, 4, 4},
                        {(byte)Atoms.C_w_n_e_s, 6, 9},
                        {(byte)Atoms.H_s, 3, 6},
                        {(byte)Atoms.H_s, 7, 11},
                        {(byte)Atoms.C_w_w_ne_se, 5, 12},
                        {(byte)Atoms.H_n, 11, 10},
                        {(byte)Atoms.H_e, 6, 4},
                        {(byte)Atoms.H_sw, 2, 9},
                        {(byte)Atoms.H_nw, 11, 2},
                        {(byte)Atoms.None, 1, 8}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_n_e_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, (byte)Atoms.H_s, 0, (byte)Atoms.C_w_w_ne_se, 0, 0,
                        (byte)Atoms.H_n, 0, (byte)Atoms.H_e, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_sw, 0, (byte)Atoms.H_nw},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.Bottle4, 6, 9},
                        {(byte)Atoms.Bottle3, 4, 7},
                        {(byte)Atoms.Bottle0, 6, 6},
                        {(byte)Atoms.Bottle1, 4, 9},
                        {(byte)Atoms.Bottle2, 7, 8},
                        {(byte)Atoms.Bottle5, 7, 6},
                        {(byte)Atoms.Bottle7, 5, 8},
                        {(byte)Atoms.Bottle6, 5, 7},
                        {(byte)Atoms.None, 4, 6}
                    },
                    Solution = new byte[] {(byte)Atoms.Bottle4, (byte)Atoms.Bottle3, (byte)Atoms.Bottle0, (byte)Atoms.Bottle1,
                        (byte)Atoms.Bottle2, (byte)Atoms.Bottle5, 0, (byte)Atoms.Bottle7, (byte)Atoms.Bottle6},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00011111, 0b11111100),
                    new BoolArray(0b00010000, 0b00000100),
                    new BoolArray(0b00010001, 0b00110100),
                    new BoolArray(0b00010010, 0b00000100),
                    new BoolArray(0b00010010, 0b01000100),
                    new BoolArray(0b00010010, 0b01010100),
                    new BoolArray(0b01111000, 0b00000100),
                    new BoolArray(0b01000010, 0b01111100),
                    new BoolArray(0b01001010, 0b00001000),
                    new BoolArray(0b01000000, 0b01101100),
                    new BoolArray(0b01111100, 0b00000100),
                    new BoolArray(0b00000101, 0b00000100),
                    new BoolArray(0b00000111, 0b11111100)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "ETHANOL",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_w_e, 8, 7},
                        {(byte)Atoms.C_w_n_e_s, 3, 8},
                        {(byte)Atoms.H_s, 1, 5},
                        {(byte)Atoms.H_w, 5, 5},
                        {(byte)Atoms.H_n, 10, 8},
                        {(byte)Atoms.H_n, 11, 6},
                        {(byte)Atoms.C_w_n_e_s, 5, 10},
                        {(byte)Atoms.H_s, 9, 11},
                        {(byte)Atoms.H_e, 9, 4},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.O_w_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, 0, 0, (byte)Atoms.H_w, 0, 0, (byte)Atoms.H_n,
                        (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_e},
                    SpiralCentre = new byte[] {3, 4}
                },
                new Level(
                    new BoolArray(0b00011111, 0b11111100),
                    new BoolArray(0b00010001, 0b00000100),
                    new BoolArray(0b00010001, 0b00000100),
                    new BoolArray(0b00010000, 0b00100100),
                    new BoolArray(0b00110111, 0b10100100),
                    new BoolArray(0b00100100, 0b10100100),
                    new BoolArray(0b00100100, 0b00110100),
                    new BoolArray(0b00100100, 0b00000100),
                    new BoolArray(0b00100110, 0b00100100),
                    new BoolArray(0b00100000, 0b01100100),
                    new BoolArray(0b00111000, 0b01000100),
                    new BoolArray(0b00100010, 0b01000100),
                    new BoolArray(0b00111111, 0b11111100)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "PROPANOL",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_n_s, 3, 8},
                        {(byte)Atoms.H_n, 4, 6},
                        {(byte)Atoms.C_w_n_e_s, 5, 11},
                        {(byte)Atoms.C_w_n_e_s, 8, 4},
                        {(byte)Atoms.C_w_n_e_s, 10, 9},
                        {(byte)Atoms.H_n, 5, 9},
                        {(byte)Atoms.H_n, 9, 9},
                        {(byte)Atoms.H_e, 10, 5},
                        {(byte)Atoms.H_s, 1, 10},
                        {(byte)Atoms.H_s, 4, 4},
                        {(byte)Atoms.H_s, 7, 11},
                        {(byte)Atoms.H_w, 9, 11},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.O_n_s, (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.C_w_n_e_s, (byte)Atoms.C_w_n_e_s,
                        (byte)Atoms.H_n, 0, (byte)Atoms.H_n, 0, 0, 0, (byte)Atoms.H_e, 0, (byte)Atoms.H_s, (byte)Atoms.H_s, (byte)Atoms.H_s, 0, (byte)Atoms.H_w},
                    SpiralCentre = new byte[] {4, 3}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00011111, 0b11111000),
                    new BoolArray(0b00010000, 0b00001000),
                    new BoolArray(0b00010101, 0b11001000),
                    new BoolArray(0b00010100, 0b00001000),
                    new BoolArray(0b00010000, 0b01101000),
                    new BoolArray(0b00010100, 0b00001000),
                    new BoolArray(0b00010101, 0b11101000),
                    new BoolArray(0b00010000, 0b00001000),
                    new BoolArray(0b00011111, 0b00101000),
                    new BoolArray(0b00000001, 0b00101000),
                    new BoolArray(0b00000001, 0b11111000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "ETHANAL",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_n_e_e, 8, 7},
                        {(byte)Atoms.C_w_n_e_s, 5, 9},
                        {(byte)Atoms.H_s, 4, 9},
                        {(byte)Atoms.H_s, 6, 11},
                        {(byte)Atoms.O_w_w, 2, 10},
                        {(byte)Atoms.H_n, 6, 8},
                        {(byte)Atoms.H_e, 6, 5},
                        {(byte)Atoms.None, 2, 4}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_n_e_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s,
                        (byte)Atoms.H_s, 0, (byte)Atoms.O_w_w, 0, 0, (byte)Atoms.H_n, 0, (byte)Atoms.H_e},
                    SpiralCentre = new byte[] {3, 4}
                },
                new Level(
                    new BoolArray(0b01111111, 0b11111100),
                    new BoolArray(0b01000000, 0b10000100),
                    new BoolArray(0b01110010, 0b00100100),
                    new BoolArray(0b01000000, 0b00000100),
                    new BoolArray(0b01000111, 0b11111100),
                    new BoolArray(0b01000100, 0b00010000),
                    new BoolArray(0b01000000, 0b10010000),
                    new BoolArray(0b01010000, 0b00011100),
                    new BoolArray(0b01010000, 0b01000100),
                    new BoolArray(0b01000011, 0b10000100),
                    new BoolArray(0b01000010, 0b00000100),
                    new BoolArray(0b01111111, 0b11111100),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "PROPANON",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_e_s_s, 7, 10},
                        {(byte)Atoms.C_w_n_e_s, 7, 8},
                        {(byte)Atoms.H_s, 3, 9},
                        {(byte)Atoms.H_s, 5, 11},
                        {(byte)Atoms.C_w_n_e_s, 5, 12},
                        {(byte)Atoms.H_n, 1, 5},
                        {(byte)Atoms.O_n_n, 8, 11},
                        {(byte)Atoms.H_n, 3, 5},
                        {(byte)Atoms.H_e, 9, 6},
                        {(byte)Atoms.H_w, 2, 11},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_e_s_s, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, 0, (byte)Atoms.H_s, (byte)Atoms.C_w_n_e_s,
                        (byte)Atoms.H_n, (byte)Atoms.O_n_n, (byte)Atoms.H_n, 0, (byte)Atoms.H_e, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_w},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b01111000, 0b00000000),
                    new BoolArray(0b01001110, 0b11111100),
                    new BoolArray(0b01000010, 0b10000100),
                    new BoolArray(0b01010011, 0b10000100),
                    new BoolArray(0b01000000, 0b00110100),
                    new BoolArray(0b01111000, 0b00000100),
                    new BoolArray(0b01000011, 0b00111100),
                    new BoolArray(0b01010000, 0b00000100),
                    new BoolArray(0b01101001, 0b01001100),
                    new BoolArray(0b01000000, 0b01000100),
                    new BoolArray(0b01010100, 0b00001100),
                    new BoolArray(0b01000101, 0b00000100),
                    new BoolArray(0b01111111, 0b11111100)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "METHANE SOURNESS",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_w_e, 8, 12},
                        {(byte)Atoms.C_w_n_n_e, 6, 6},
                        {(byte)Atoms.O_s_s, 7, 8},
                        {(byte)Atoms.H_w, 10, 8},
                        {(byte)Atoms.H_e, 6, 7},
                        {(byte)Atoms.None, 2, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.O_w_e, (byte)Atoms.C_w_n_n_e, (byte)Atoms.O_s_s, 0, 0, (byte)Atoms.H_w, 0, 0, 0, 0, (byte)Atoms.H_e},
                    SpiralCentre = new byte[] {3, 4}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.X, 4, 9},
                        {(byte)Atoms.T_w, 6, 6},
                        {(byte)Atoms.L_4, 7, 7},
                        {(byte)Atoms.T_s, 4, 7},
                        {(byte)Atoms.L_3, 6, 9},
                        {(byte)Atoms.T_e, 5, 7},
                        {(byte)Atoms.L_2, 6, 8},
                        {(byte)Atoms.T_n, 7, 9},
                        {(byte)Atoms.L_1, 4, 6},
                        {(byte)Atoms.None, 4, 6}
                    },
                    Solution = new byte[] {(byte)Atoms.X, (byte)Atoms.T_w, (byte)Atoms.L_4, (byte)Atoms.T_s,
                        (byte)Atoms.L_3, (byte)Atoms.T_e, (byte)Atoms.L_2, (byte)Atoms.T_n, (byte)Atoms.L_1},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000111, 0b11110000),
                    new BoolArray(0b00011100, 0b00010000),
                    new BoolArray(0b00010000, 0b10010000),
                    new BoolArray(0b00011110, 0b01011000),
                    new BoolArray(0b00010000, 0b01001000),
                    new BoolArray(0b00010001, 0b10001000),
                    new BoolArray(0b00010010, 0b00001000),
                    new BoolArray(0b00010000, 0b01001000),
                    new BoolArray(0b00010101, 0b00011000),
                    new BoolArray(0b00010001, 0b11110000),
                    new BoolArray(0b00011111, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "ETHANE SOURNESS",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_n_n_e, 7, 10},
                        {(byte)Atoms.C_w_n_e_s, 6, 6},
                        {(byte)Atoms.H_s, 2, 7},
                        {(byte)Atoms.O_s_s, 3, 9},
                        {(byte)Atoms.O_w_e, 9, 9},
                        {(byte)Atoms.H_n, 8, 5},
                        {(byte)Atoms.H_e, 6, 9},
                        {(byte)Atoms.H_w, 3, 6},
                        {(byte)Atoms.None, 2, 5}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_n_n_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, (byte)Atoms.O_s_s, 0, (byte)Atoms.O_w_e,
                        0, 0, (byte)Atoms.H_n, 0, (byte)Atoms.H_e, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_w},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b01111111, 0b11111110),
                    new BoolArray(0b01000000, 0b10000010),
                    new BoolArray(0b01001100, 0b10110010),
                    new BoolArray(0b01000000, 0b10000010),
                    new BoolArray(0b01001110, 0b11101010),
                    new BoolArray(0b01000010, 0b10001010),
                    new BoolArray(0b01010000, 0b10000010),
                    new BoolArray(0b01000000, 0b00000010),
                    new BoolArray(0b01011101, 0b10111010),
                    new BoolArray(0b01000000, 0b00000010),
                    new BoolArray(0b01000010, 0b00000010),
                    new BoolArray(0b01111111, 0b11111110),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "TRANSBUTENE",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_w_ne_se, 5, 12},
                        {(byte)Atoms.C_nw_e_e_sw, 9, 3},
                        {(byte)Atoms.C_nw_ne_se_sw, 3, 8},
                        {(byte)Atoms.H_nw, 3, 4},
                        {(byte)Atoms.C_nw_ne_se_sw, 5, 4},
                        {(byte)Atoms.H_se, 1, 11},
                        {(byte)Atoms.H_se, 2, 6},
                        {(byte)Atoms.H_sw, 3, 13},
                        {(byte)Atoms.H_nw, 3, 10},
                        {(byte)Atoms.H_nw, 10, 10},
                        {(byte)Atoms.H_ne, 6, 2},
                        {(byte)Atoms.H_se, 10, 12},
                        {(byte)Atoms.None, 1, 2}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_w_ne_se, (byte)Atoms.C_nw_e_e_sw, 0, 0, (byte)Atoms.C_nw_ne_se_sw, 0, (byte)Atoms.H_nw,
                        0, 0, (byte)Atoms.C_nw_ne_se_sw, 0, (byte)Atoms.H_se, 0, 0, (byte)Atoms.H_se, 0, (byte)Atoms.H_sw, 0, (byte)Atoms.H_nw,
                        0, 0, 0, 0, (byte)Atoms.H_nw, 0, (byte)Atoms.H_ne, 0, (byte)Atoms.H_se},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b01111111, 0b11111100),
                    new BoolArray(0b01000000, 0b00000100),
                    new BoolArray(0b01000001, 0b00000100),
                    new BoolArray(0b01001101, 0b00101100),
                    new BoolArray(0b01000000, 0b00100100),
                    new BoolArray(0b01001111, 0b10110100),
                    new BoolArray(0b01000000, 0b00100100),
                    new BoolArray(0b01000010, 0b00000100),
                    new BoolArray(0b01010000, 0b10110100),
                    new BoolArray(0b01000000, 0b00000100),
                    new BoolArray(0b01000010, 0b00000100),
                    new BoolArray(0b01111111, 0b11111100),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "CISBUTENE",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.C_w_w_ne_se, 4, 4},
                        {(byte)Atoms.C_nw_e_e_sw, 3, 9},
                        {(byte)Atoms.H_sw, 6, 3},
                        {(byte)Atoms.C_nw_ne_se_sw, 9, 12},
                        {(byte)Atoms.C_nw_ne_se_sw, 10, 3},
                        {(byte)Atoms.H_se, 2, 7},
                        {(byte)Atoms.H_sw, 7, 13},
                        {(byte)Atoms.H_nw, 4, 10},
                        {(byte)Atoms.H_ne, 1, 12},
                        {(byte)Atoms.H_nw, 5, 13},
                        {(byte)Atoms.H_ne, 2, 5},
                        {(byte)Atoms.H_se, 3, 12},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.C_w_w_ne_se, (byte)Atoms.C_nw_e_e_sw, 0, 0, (byte)Atoms.H_sw, 0, (byte)Atoms.C_nw_ne_se_sw,
                        0, 0, (byte)Atoms.C_nw_ne_se_sw, 0, (byte)Atoms.H_se, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_sw, 0, (byte)Atoms.H_nw, 0, (byte)Atoms.H_ne,
                        (byte)Atoms.H_nw, 0, (byte)Atoms.H_ne, 0, (byte)Atoms.H_se},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b00000001, 0b11111100),
                    new BoolArray(0b00000001, 0b00000100),
                    new BoolArray(0b00000001, 0b00100100),
                    new BoolArray(0b00000001, 0b00000100),
                    new BoolArray(0b00111111, 0b00100100),
                    new BoolArray(0b00100000, 0b00100100),
                    new BoolArray(0b00100001, 0b10100100),
                    new BoolArray(0b00100000, 0b00000100),
                    new BoolArray(0b00111100, 0b00100100),
                    new BoolArray(0b00100000, 0b10100100),
                    new BoolArray(0b00100111, 0b10100100),
                    new BoolArray(0b00100000, 0b00000100),
                    new BoolArray(0b00111111, 0b11111100)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "DIMETHYL ETHER",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_w_e, 5, 10},
                        {(byte)Atoms.C_w_n_e_s, 3, 7},
                        {(byte)Atoms.H_s, 7, 11},
                        {(byte)Atoms.H_s, 9, 10},
                        {(byte)Atoms.C_w_n_e_s, 11, 4},
                        {(byte)Atoms.H_n, 3, 5},
                        {(byte)Atoms.H_n, 11, 7},
                        {(byte)Atoms.H_e, 4, 3},
                        {(byte)Atoms.H_w, 10, 12},
                        {(byte)Atoms.None, 1, 3}
                    },
                    Solution = new byte[] {(byte)Atoms.O_w_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, 0, (byte)Atoms.H_s, (byte)Atoms.C_w_n_e_s,
                        (byte)Atoms.H_n, 0, (byte)Atoms.H_n, 0, (byte)Atoms.H_e, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_w},
                    SpiralCentre = new byte[] {3, 3}
                },
                new Level(
                    new BoolArray(0b01111111, 0b11111110),
                    new BoolArray(0b01000000, 0b00000010),
                    new BoolArray(0b01000111, 0b10010010),
                    new BoolArray(0b01000000, 0b00010010),
                    new BoolArray(0b01011000, 0b00000010),
                    new BoolArray(0b01000100, 0b00110010),
                    new BoolArray(0b01000100, 0b10000010),
                    new BoolArray(0b01111100, 0b00000010),
                    new BoolArray(0b01000000, 0b10011110),
                    new BoolArray(0b01000100, 0b10010000),
                    new BoolArray(0b01000000, 0b10010000),
                    new BoolArray(0b01111111, 0b11110000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    MoleculeUp = "MOLECULE",
                    MoleculeDown = "BUTANOL",
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.O_n_s, 3, 13},
                        {(byte)Atoms.H_n, 2, 12},
                        {(byte)Atoms.C_w_n_e_s, 2, 6},
                        {(byte)Atoms.C_w_n_e_s, 7, 3},
                        {(byte)Atoms.H_w, 4, 13},
                        {(byte)Atoms.H_n, 7, 7},
                        {(byte)Atoms.H_n, 8, 12},
                        {(byte)Atoms.C_w_n_e_s, 8, 8},
                        {(byte)Atoms.H_s, 1, 8},
                        {(byte)Atoms.H_s, 2, 2},
                        {(byte)Atoms.H_s, 6, 12},
                        {(byte)Atoms.H_n, 10, 5},
                        {(byte)Atoms.C_w_n_e_s, 9, 9},
                        {(byte)Atoms.H_s, 10, 12},
                        {(byte)Atoms.H_e, 5, 3},
                        {(byte)Atoms.None, 1, 2}
                    },
                    Solution = new byte[] {(byte)Atoms.O_n_s, (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_w,
                        0, 0, (byte)Atoms.H_n, 0, 0, (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s, (byte)Atoms.H_s, (byte)Atoms.H_s,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.H_s,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_e},
                    SpiralCentre = new byte[] {4, 4}
                },
                new Level(
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000100, 0b00100000),
                    new BoolArray(0b00000111, 0b11100000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000),
                    new BoolArray(0b00000000, 0b00000000)
                )
                {
                    AtomPositions = new byte[,]
                    {
                        {(byte)Atoms.Bottle4, 6, 9},
                        {(byte)Atoms.Bottle3, 7, 6},
                        {(byte)Atoms.Bottle0, 7, 9},
                        {(byte)Atoms.Bottle1, 5, 6},
                        {(byte)Atoms.Bottle2, 4, 8},
                        {(byte)Atoms.Bottle5, 5, 8},
                        {(byte)Atoms.Bottle7, 4, 6},
                        {(byte)Atoms.Bottle6, 7, 7},
                        {(byte)Atoms.None, 4, 6}
                    },
                    Solution = new byte[] {(byte)Atoms.Bottle4, (byte)Atoms.Bottle3, (byte)Atoms.Bottle0, (byte)Atoms.Bottle1,
                        (byte)Atoms.Bottle2, (byte)Atoms.Bottle5, 0, (byte)Atoms.Bottle7, (byte)Atoms.Bottle6},
                    SpiralCentre = new byte[] {3, 3}
                }
            };

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
