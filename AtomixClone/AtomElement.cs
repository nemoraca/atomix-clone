﻿using System.Windows;
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

            switch (Atom)
            {
                case Atoms.H_w:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(0, coord));
                    break;
                case Atoms.H_e:
                    brush = (Brush)Application.Current.Resources["BrushH"];
                    drawingContext.DrawLine(grayPen, centre, new Point(MainWindow.TileWidthHeight, coord));
                    break;
                case Atoms.O_w_e:
                    brush = (Brush)Application.Current.Resources["BrushO"];
                    drawingContext.DrawLine(grayPen, new Point(0, coord), new Point(MainWindow.TileWidthHeight, coord));
                    break;
                default:
                    brush = null;
                    break;
            }

            drawingContext.DrawEllipse(brush, (Pen)Application.Current.Resources["TransparentPen"], centre, coord * 2 / 3, coord * 2 / 3);
        }
    }
}