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
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //TEMP C POUR LES TEST
            currentLevel.addPattern(new Pattern("ouoi oiuoi"));



            //Truc de la validation
            XmlWriter xmlWriter = XmlWriter.Create(currentLevel.getFilePath());
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("level");
            if (currentLevel.getIsRandom())
            {
                xmlWriter.WriteAttributeString("isRandom", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isRandom", "0");
            }
            if (currentLevel.getIsInfinite())
            {
                xmlWriter.WriteAttributeString("isInfinite", "1");
            }
            else
            {
                xmlWriter.WriteAttributeString("isInfinite", "0");
            }

            for (int i=0; i < currentLevel.getPatterns().Count; i++)
            {
                Pattern currentPattern = currentLevel.getPatterns()[i];
                xmlWriter.WriteStartElement("pattern");
                xmlWriter.WriteAttributeString("name", currentPattern.getPatternName());
                if (currentPattern.getDuration() != -1)
                {
                    xmlWriter.WriteAttributeString("duration", currentPattern.getDuration().ToString());
                }
                if (currentPattern.getIsRandom())
                {
                    xmlWriter.WriteAttributeString("isRandom", "1");
                }
                else
                {
                    xmlWriter.WriteAttributeString("isRandom", "0");
                }
                if (currentPattern.getOrder() != -1)
                {
                    xmlWriter.WriteAttributeString("order", currentPattern.getOrder().ToString());
                }
                for (int j=0; j < currentPattern.getPatternWaves().Count; j++)
                {
                    Wave currentWave = currentPattern.getPatternWaves()[j];
                    xmlWriter.WriteStartElement("wave");
                    if (currentWave.getDuration() != -1)
                    {
                        xmlWriter.WriteAttributeString("duration", currentWave.getDuration().ToString());
                    }
                    for (int u=0; u<currentWave.getEnemyList().Count; u++)
                    {
                        Enemy currentEnemy = currentWave.getEnemyList()[u];
                        xmlWriter.WriteStartElement("enemy");
                        if (currentEnemy.getSpawnTime() != -1)
                        {
                            xmlWriter.WriteAttributeString("spawnTime", currentEnemy.getSpawnTime().ToString());
                        }
                        xmlWriter.WriteAttributeString("type", currentEnemy.getEnemyType());
                        string posString = currentEnemy.getPos().X + ";" + currentEnemy.getPos().Y;
                        xmlWriter.WriteAttributeString("pos", posString);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
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
    }
}
