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
using System.Xml.Serialization;

namespace PiouMaker
{
    public partial class Form1 : Form
    {
        Level currentLevel;
        XMLManager xmlManager;
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
            if (currentLevel != null)
            {
                //Level listener
                firstPropertiesName.Visible = true;
                firstPropertiesName.Text = "Nom du niveau : ";
                firstPropertiesContent.Visible = true;
                firstPropertiesContent.Text = currentLevel.getLevelName();

                secondPropertiesName.Visible = true;
                secondPropertiesName.Text = "Est infini : ";
                secondPropertiesContent.Visible = true;
                bool isInfinite = currentLevel.getIsInfinite();
                if (isInfinite)
                {
                    secondPropertiesContent.Text = "vrai";
                }
                else
                {
                    secondPropertiesContent.Text = "faux";
                }


                thirdPropertiesName.Visible = true;
                thirdPropertiesName.Text = "Est aléatoire : ";
                thirdPropertiesContent.Visible = true;
                bool isRandom = currentLevel.getIsRandom();
                if (isRandom)
                {
                    thirdPropertiesContent.Text = "vrai";
                }
                else
                {
                    thirdPropertiesContent.Text = "faux";
                }

                fourthPropertiesName.Visible = true;
                fourthPropertiesName.Text = "Nombre de pattern : ";
                fourthPropertiesContent.Visible = true;
                fourthPropertiesContent.Text = currentLevel.getPatterns().Count.ToString();
            }
        }

        private void openLevelButton_Click(object sender, EventArgs e)
        {
            //open a .piou file
            openFileDialog1.Filter = "Piou file | *.piou";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentLevel = new Level();
                currentLevel.setFilePath(openFileDialog1.FileName);
                // cut pour avoir le nom du fichier sans le path
                string[] names = currentLevel.getFilePath().Split(new char[] { '\\' });

                levelName.Text = names[names.Length - 1];
                currentLevel.setLevelName(levelName.Text);

                //on enleve les buttons pour charger un niveau
                openLevelButton.Visible = false;
                openLevelButton.Enabled = false;
                createLevelButton.Enabled = false;
                createLevelButton.Visible = false;
                //on afficher la liste des patterns
                patternList.Visible = true;

                Stream fileStream = openFileDialog1.OpenFile();
                xmlManager = new XMLManager(currentLevel, fileStream);

                List<String> patternNames;
                patternNames = currentLevel.getPatternNames();
                for (int i = 0; i < patternNames.Count; i++)
                {
                    patternList.Items.Add(patternNames[i]);
                }
                fileStream.Close();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Truc de la validation
            xmlManager.saveLevel(currentLevel);
        }

        private void createLevelButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog createLevelDialog = new SaveFileDialog();
            createLevelDialog.Filter = "Piou file | *.piou";
            createLevelDialog.RestoreDirectory = true;

            if (createLevelDialog.ShowDialog() == DialogResult.OK)
            {
                currentLevel = new Level();
                currentLevel.setFilePath(createLevelDialog.FileName);
                // cut pour avoir le nom du fichier sans le path
                string[] names = currentLevel.getFilePath().Split(new char[] { '\\' });

                levelName.Text = names[names.Length - 1];
                currentLevel.setLevelName(levelName.Text);

                //on enleve les buttons pour charger un niveau
                openLevelButton.Visible = false;
                openLevelButton.Enabled = false;
                createLevelButton.Enabled = false;
                createLevelButton.Visible = false;
                //on afficher la liste des patterns
                patternList.Visible = true;
            }
        }

        private void patternList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Listener de la patternList
            if (patternList.SelectedItem.ToString() != "-1")
            {
                firstPropertiesName.Visible = true;
                firstPropertiesName.Text = "Nom du pattern : ";
                firstPropertiesContent.Visible = true;
                firstPropertiesContent.Text = patternList.SelectedItem.ToString();

               secondPropertiesName.Visible = true;
               secondPropertiesName.Text = "Ordre : ";
               secondPropertiesContent.Visible = true;
               secondPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedIndex).getOrder().ToString();

                thirdPropertiesName.Visible = true;
                thirdPropertiesName.Text = "Est aléatoire : ";
                thirdPropertiesContent.Visible = true;
                bool isRandom = currentLevel.getPattern(patternList.SelectedIndex).getIsRandom();
                if (isRandom)
                {
                    thirdPropertiesContent.Text = "vrai";
                }
                else
                {
                    thirdPropertiesContent.Text = "faux";
                }
                

               fourthPropertiesName.Visible = true;
               fourthPropertiesName.Text = "Nombre de vagues : ";
               fourthPropertiesContent.Visible = true;
               fourthPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedIndex).getPatternWaves().Count.ToString();
            }
        }
    }
}
