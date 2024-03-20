using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiouMaker
{
    internal class EnemyPictureBox : PictureBox
    {
        public float Angle = 0f;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Appel de la méthode de dessin de la classe de base
            //base.OnPaint(e);

            // Sauvegarde de la transformation actuelle
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2); // Déplacer le point d'origine au centre de la PictureBox
            e.Graphics.RotateTransform(Angle); // Rotation de l'angle spécifié
            e.Graphics.TranslateTransform(-this.Width / 2, -this.Height / 2); // Déplacer le point d'origine à son emplacement d'origine
            base.OnPaint(e);
            // Dessin de l'image avec la rotation
            //e.Graphics.DrawImage(this.Image, new Point(0, 0)); // Dessiner l'image à partir du coin supérieur gauche
        }
    }
}
