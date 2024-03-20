using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PiouMaker
{
    internal class DirectionCross
    {
        public Point pos = new Point(0,0);
        int crossWidth = 10;
        int crossHeight = 10;

        public DirectionCross(Point position)
        {
            pos = position;
        }

        public void dessiner(Graphics g)
        {
            // Définissez les propriétés du crayon pour la croix
            Pen pen = new Pen(Color.Green, 2);
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

            // Dessinez la croix horizontale
            g.DrawLine(pen, pos.X - crossWidth, pos.Y, pos.X + crossWidth, pos.Y);

                // Dessinez la croix verticale
                g.DrawLine(pen, pos.X, pos.Y - crossHeight, pos.X, pos.Y + crossHeight);

            // Libérez les ressources du crayon
            pen.Dispose();
        }

        public void setPos(Point pointParam)
        {
            pos = pointParam;
        }

        public void setPos(int x, int y)
        {
            pos = new Point(x, y);
        }

        public void setCrossWidth(int crossWidthParam)
        {
            crossWidth = crossWidthParam;
        }

        public void setCrossHeight(int crossHeightParam)
        {
            crossHeight = crossHeightParam;
        }
    }
}
