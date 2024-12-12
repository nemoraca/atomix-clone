namespace AtomixClone
{
    public static class GameData
    {
        public static sbyte[,] Spiral {  get; } = new sbyte[,]
        {
            {0, 0}, {0, -1}, {-1, -1}, {-1, 0}, {-1, 1}, {0, 1}, {1, 1}, {1, 0}, {1, -1}, {1, -2}, {0, -2}, {-1, -2}, {-2, -2}, {-2, -1},
            {-2, 0}, {-2, 1}, {-2, 2}, {-1, 2}, {0, 2}, {1, 2}, {2, 2}, {2, 1}, {2, 0}, {2, -1}, {2, -2}, {2, -3}, {1, -3}, {0, -3},
            {-1, -3}, {-2, -3}, {-3, -3}, {-3, -2}, {-3, -1}, {-3, 0}, {-3, 1}, {-3, 2}, {-3, 3}, {-2, 3}, {-1, 3}, {0, 3}, {1, 3}, {2, 3},
            {3, 3}, {3, 2}, {3, 1}, {3, 0}, {3, -1}, {3, -2}, {3, -3}, {3, -4}, {2, -4}, {1, -4}, {0, -4}, {-1, -4}, {-2, -4}, {-3, -4},
            {-4, -4}, {-4, -3}, {-4, -2}, {-4, -1}, {-4, 0}, {-4, 1}, {-4, 2}, {-4, 3}, {-4, 4}, {-3, 4}, {-2, 4}, {-1, 4}, {0, 4}, {1, 4},
            {2, 4}, {3, 4}, {4, 4}, {4, 3}, {4, 2}, {4, 1}, {4, 0}, {4, -1}, {4, -2}, {4, -3}, {4, -4}, {4, -5}, {3, -5}, {2, -5}, {1, -5},
            {0, -5}, {-1, -5}, {-2, -5}, {-3, -5}, {-4, -5}, {-5, -5}, {-5, -4}, {-5, -3}, {-5, -2}, {-5, -1}, {-5, 0}, {-5, 1}, {-5, 2},
            {-5, 3}, {-5, 4}, {-5, 5}, {-4, 5}, {-3, 5}, {-2, 5}, {-1, 5}, {0, 5}, {1, 5}, {2, 5}, {3, 5}, {4, 5}, {5, 5}, {5, 4}, {5, 3},
            {5, 2}, {5, 1}, {5, 0}, {5,  -1}, {5, -2}, {5, -3}, {5, -4}, {5, -5}, {5, -6}, {4, -6}, {3, -6}, {2, -6}, {1, -6}, {0, -6},
            {-1, -6}, {-2, -6}, {-3, -6}, {-4, -6}, {-5, -6}, {-6, -6}, {-6, -5}, {-6, -4}, {-6, -3}, {-6, -2}, {-6, -1}, {-6, 0}, {-6, 1},
            {-6, 2}, {-6, 3}, {-6, 4}, {-6, 5}, {-6, 6}, {-5, 6}, {-4, 6}, {-3, 6}, {-2, 6}, {-1, 6}, {0, 6}, {1, 6}, {2, 6}, {3, 6},
            {4, 6}, {5, 6}, {6, 6}, {6, 5}, {6, 4}, {6, 3}, {6, 2}, {6, 1}, {6, 0}, {6, -1}, {6, -2}, {6, -3}, {6, -4}, {6, -5}, {6, -6}
        };

        public static Level[] Levels { get; } = new Level[]
        {
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
            },
            new Level(
                new BoolArray(0b00001111, 0b11111100),
                new BoolArray(0b00001000, 0b00000100),
                new BoolArray(0b01111000, 0b00010100),
                new BoolArray(0b01000000, 0b01010100),
                new BoolArray(0b01011000, 0b01000100),
                new BoolArray(0b01000001, 0b01110100),
                new BoolArray(0b01000011, 0b00000100),
                new BoolArray(0b01111000, 0b01110100),
                new BoolArray(0b00001010, 0b01010100),
                new BoolArray(0b00001001, 0b01010100),
                new BoolArray(0b00001000, 0b01011100),
                new BoolArray(0b00001111, 0b11000000),
                new BoolArray(0b00000000, 0b00000000)
            )
            {
                MoleculeUp = "MOLECULE",
                MoleculeDown = "METHYL PROPANOL",
                AtomPositions = new byte[,]
                {
                    {(byte)Atoms.O_n_s, 1, 9},
                    {(byte)Atoms.H_n, 4, 8},
                    {(byte)Atoms.C_w_n_e_s, 3, 10},
                    {(byte)Atoms.C_w_n_e_s, 6, 6},
                    {(byte)Atoms.C_w_n_e_s, 7, 10},
                    {(byte)Atoms.H_n, 4, 10},
                    {(byte)Atoms.H_n, 5, 9},
                    {(byte)Atoms.H_e, 2, 5},
                    {(byte)Atoms.H_s, 1, 4},
                    {(byte)Atoms.C_nw_n_ne_s, 10, 10},
                    {(byte)Atoms.H_s, 4, 5},
                    {(byte)Atoms.H_w, 9, 7},
                    {(byte)Atoms.H_se, 6, 3},
                    {(byte)Atoms.H_s, 8, 8},
                    {(byte)Atoms.H_sw, 6, 12},
                    {(byte)Atoms.None, 1, 3}
                },
                Solution = new byte[] {(byte)Atoms.O_n_s, (byte)Atoms.H_n, (byte)Atoms.C_w_n_e_s, (byte)Atoms.C_w_n_e_s, (byte)Atoms.C_w_n_e_s,
                    (byte)Atoms.H_n, 0, (byte)Atoms.H_n, 0, 0, 0, (byte)Atoms.H_e, 0, (byte)Atoms.H_s, (byte)Atoms.C_nw_n_ne_s, (byte)Atoms.H_s,
                    0, (byte)Atoms.H_w, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_se, (byte)Atoms.H_s, (byte)Atoms.H_sw},
                SpiralCentre = new byte[] {4, 3}
            },
            new Level(
                new BoolArray(0b00000011, 0b11111000),
                new BoolArray(0b00111110, 0b10001000),
                new BoolArray(0b00100000, 0b00101000),
                new BoolArray(0b00101000, 0b00101000),
                new BoolArray(0b00100000, 0b00001000),
                new BoolArray(0b00100010, 0b01001000),
                new BoolArray(0b00101001, 0b00111000),
                new BoolArray(0b00100000, 0b00100000),
                new BoolArray(0b00101001, 0b10111000),
                new BoolArray(0b00100000, 0b00001000),
                new BoolArray(0b00111110, 0b01001000),
                new BoolArray(0b00000011, 0b00001000),
                new BoolArray(0b00000001, 0b11111000)
            )
            {
                MoleculeUp = "MOLECULE",
                MoleculeDown = "PROPANTRIOL",
                AtomPositions = new byte[,]
                {
                    {(byte)Atoms.H_n, 5, 8},
                    {(byte)Atoms.H_e, 5, 5},
                    {(byte)Atoms.C_w_n_e_s, 2, 8},
                    {(byte)Atoms.O_w_e, 4, 9},
                    {(byte)Atoms.H_e, 7, 10},
                    {(byte)Atoms.C_w_n_e_s, 4, 5},
                    {(byte)Atoms.O_w_e, 6, 7},
                    {(byte)Atoms.H_w, 4, 12},
                    {(byte)Atoms.H_w, 9, 8},
                    {(byte)Atoms.H_e, 9, 6},
                    {(byte)Atoms.C_w_n_e_s, 8, 6},
                    {(byte)Atoms.O_w_e, 8, 10},
                    {(byte)Atoms.H_w, 9, 12},
                    {(byte)Atoms.H_s, 1, 6},
                    {(byte)Atoms.None, 1, 4}
                },
                Solution = new byte[] {(byte)Atoms.H_n, 0, (byte)Atoms.H_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.O_w_e, 0, 0, 0, 0, 0, 0, 0, 0,
                    (byte)Atoms.H_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.O_w_e, (byte)Atoms.H_w, (byte)Atoms.H_w, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, (byte)Atoms.H_e, (byte)Atoms.C_w_n_e_s, (byte)Atoms.O_w_e, (byte)Atoms.H_w, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)Atoms.H_s},
                SpiralCentre = new byte[] {5, 3}
            }
        };
    }
}
