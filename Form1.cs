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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private void label2_Click(object sender, EventArgs e)
        {
            if (currentLevel != null)
            {
                //Level listener
                modifyPropertyButton.Visible = true;

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

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && currentLevel != null)
            {
                // On clique droit sur un pattern
                contextMenuPattern.Items[0].Visible = true;
                contextMenuPattern.Show(levelName, e.Location);
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

                refreshPatternList();
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

        private void patternList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            modifyPropertyButton.Visible = true;
            //Listener de la patternList
            if (patternList.SelectedNode.Parent == null)
            {
                //On a sélectionné un pattern
                firstPropertiesName.Visible = true;
                firstPropertiesName.Text = "Nom du pattern : ";
                firstPropertiesContent.Visible = true;
                firstPropertiesContent.Text = patternList.SelectedNode.Text;

                secondPropertiesName.Visible = true;
                secondPropertiesName.Text = "Ordre : ";
                secondPropertiesContent.Visible = true;
                secondPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedNode.Index).getOrder().ToString();

                thirdPropertiesName.Visible = true;
                thirdPropertiesName.Text = "Est aléatoire : ";
                thirdPropertiesContent.Visible = true;
                bool isRandom = currentLevel.getPattern(patternList.SelectedNode.Index).getIsRandom();
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
                fourthPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedNode.Index).getPatternWaves().Count.ToString();
            }
            else
            {
                // On a sélectionné une wave
                thirdPropertiesContent.Visible = false;
                thirdPropertiesName.Visible = false;
                fourthPropertiesContent.Visible = false;
                fourthPropertiesName.Visible = false;

                firstPropertiesName.Visible = true;
                firstPropertiesName.Text = "Durée : ";
                firstPropertiesContent.Visible = true;
                firstPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index).getDuration().ToString();

                secondPropertiesName.Visible = true;
                secondPropertiesName.Text = "Nombre d'ennemis : ";
                secondPropertiesContent.Visible = true;
                secondPropertiesContent.Text = currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index).getEnemyList().Count.ToString();
            }
        }
        
        private void patternList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            patternList.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right && e.Node.Parent == null)
            {
                // On clique droit sur un pattern
                contextMenuPattern.Items[1].Visible = true;
                contextMenuPattern.Items[2].Visible = true;
                contextMenuPattern.Show(patternList, e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuPattern.Items[3].Visible = true;
                contextMenuPattern.Show(patternList, e.Location);
            }
        }

        private void patternList_MouseClick(object sender, MouseEventArgs e)
        {
            //On regarde si on clique autre part que sur un node
            TreeViewHitTestInfo hitTestInfo = patternList.HitTest(e.Location);
            if (e.Button == MouseButtons.Right &&  hitTestInfo.Node == null)
            {
                contextMenuPattern.Items[0].Visible = true;
                contextMenuPattern.Show(patternList, e.Location);
            }
        }

        private void contextMenuPattern_Closing(object sender, CancelEventArgs e)
        {
            for (int i=0; i< contextMenuPattern.Items.Count; i++)
            {
                contextMenuPattern.Items[i].Visible = false;
            }
        }

        private void contexMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if (item.Text == "Ajouter une vague")
            {
                if (patternList.SelectedNode.Parent == null)
                {
                    currentLevel.getPattern(patternList.SelectedNode.Index).addWave(new Wave());
                }
                else
                {
                    currentLevel.getPattern(patternList.SelectedNode.Parent.Index).addWave(new Wave());
                }
            }
            else if (item.Text == "Ajouter un pattern")
            {
                currentLevel.addPattern(new Pattern());
            }
            else if (item.Text == "Supprimer la vague")
            {
                currentLevel.getPattern(patternList.SelectedNode.Parent.Index).removeWave(patternList.SelectedNode.Index);
            }
            else if (item.Text == "Supprimer le pattern")
            {
                currentLevel.removePattern(patternList.SelectedNode.Index);
            }
            refreshPatternList();
           
        }

        private void refreshPatternList()
        {
            //On refresh la liste
            patternList.Nodes.Clear();
            List<String> patternNames;
            patternNames = currentLevel.getPatternNames();
            for (int i = 0; i < patternNames.Count; i++)
            {
                patternList.Nodes.Add(patternNames[i]);
            }
            List<Pattern> patterns = currentLevel.getPatterns();
            for (int i = 0; i < patterns.Count; i++)
            {
                List<Wave> waveList = patterns[i].getPatternWaves();
                for (int j = 0; j < waveList.Count; j++)
                {
                    patternList.Nodes[i].Nodes.Add("Vague " + (j + 1));
                }
            }
        }
    }
}
