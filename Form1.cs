using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

namespace PiouMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*
            ImageList imageList = new ImageList();
            int imageSize = listView1.ClientSize.Height;
            foreach (Image enemyImage in enemies.Images)
            {
                Image resizedImage = ResizeImage(enemyImage, imageSize);
                imageList.Images.Add(resizedImage);
            }
            */
            // Ajoutez des éléments à la ListView
            ListViewItem item1 = new ListViewItem();
            item1.ImageIndex = 0; // Indice de l'image dans l'ImageList
            ListViewItem item2 = new ListViewItem();
            item2.ImageIndex = 1;

            // Ajoutez les éléments à la collection Items du ListView
            listView1.Items.Add(item1);
            listView1.Items.Add(item2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void openLevelButton_Click(object sender, EventArgs e)
        {
            //open a .piou file
            openFileDialog1.Filter = "Piou file | *.piou";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileContent = string.Empty;

                string filePath = openFileDialog1.FileName;
                // cut pour avoir le nom du fichier sans le path
                string[] names = filePath.Split(new char[] { '\\' });

                levelName.Text = names[names.Length - 1];
                //on enleve les buttons pour charger un niveau
                openLevelButton.Visible = false;
                openLevelButton.Enabled = false;
                createLevelButton.Enabled = false;
                createLevelButton.Visible = false;
                //on afficher la liste des patterns
                patternList.Visible = true;

                Stream fileStream = openFileDialog1.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                XmlTextReader XMLreader = new XmlTextReader(filePath);

                while (XMLreader.Read())
                {
                    // Do some work here on the data.
                    Trace.WriteLine(XMLreader.Name);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Truc de la validation
        }
    }
}
