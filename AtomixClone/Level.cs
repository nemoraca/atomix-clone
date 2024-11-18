using System;

namespace AtomixClone
{
    public class Level
    {
        public BoolArray[] Tiles { get; }

        public string MoleculeUp { get; set; }

        public string MoleculeDown { get; set; }

        public byte[,] AtomPositions { get; set; }      // First atom plays special role in checking the solution.

        public byte[] Solution { get; set; }

        public byte[] SpiralCentre { get; set; }

        public Level(params BoolArray[] tiles)
        {
            if (tiles.Length != 13)
                throw new ArgumentException("Every level must have 13 rows.");
            Tiles = tiles;
        }
    }
}
