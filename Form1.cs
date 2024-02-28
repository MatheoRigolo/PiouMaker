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
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace PiouMaker
{
    public partial class Form1 : Form
    {
        Level currentLevel;
        XMLManager xmlManager;
        List<PropertyView> properties;
        Wave waveSelected;

        //utile pour drag and drop
        List<PictureBox> enemyPicures = new List<PictureBox>();
        PictureBox selectedEnemy;
        bool isANewEnemy;
        bool isDragging = false;
        Point originalImageLocation;

        public Form1()
        {
            InitializeComponent();

            // Ajoutez des éléments à la ListView
            ListViewItem item1 = new ListViewItem();
            item1.ImageIndex = 0; // Indice de l'image dans l'ImageList
            ListViewItem item2 = new ListViewItem();
            item2.ImageIndex = 1;
            ListViewItem item3 = new ListViewItem();
            item3.ImageIndex = 2;

            // Ajoutez les éléments à la collection Items du ListView
            listView1.Items.Add(item1);
            listView1.Items.Add(item2);
            listView1.Items.Add(item3);

            initProperties();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (currentLevel != null)
            {
                refreshProperties(true);
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
                string[] autre = names[names.Length - 1].Split(".piou");
                levelName.Text = autre[autre.Length - 2];
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
            string message = "Fichier correctement modifié";
            string title = "Validation";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
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
                string[] autre = names[names.Length - 1].Split(".piou");
                levelName.Text = autre[autre.Length - 2];
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
            properties.Clear();
            gamePanel.Controls.Clear();
            modifyPropertyButton.Visible = true;
            //Listener de la patternList
            if (patternList.SelectedNode.Parent == null)
            {
                //On a sélectionné un pattern

                //On enlève le panel du milieu : plus aucune vague n'est sélectionnée
                gamePanel.Visible = false;
            }
            else
            {
                // On a sélectionné une wave
                waveSelected = currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index);
                enemyPicures.Clear();

                //On affiche le preview du jeu au milieu
                gamePanel.Visible = true;

                //Il faut afficher les ennemis
                for (int i=0; i<waveSelected.getEnemyList().Count; i++)
                {
                    //Afficher l'ennemi
                    PictureBox enemybox = new PictureBox();
                    enemybox.BackColor = Color.Transparent;
                    enemybox.Image = enemies.Images[1];
                    enemybox.SizeMode = PictureBoxSizeMode.AutoSize;
                    enemybox.Location = new Point((int)(gamePanel.Width * (waveSelected.getEnemy(i).getPos().X / 100f) - (float)enemybox.Width / 2f), (int)(gamePanel.Height * (waveSelected.getEnemy(i).getPos().Y / 100f) - (float)enemybox.Height / 2f));
                    enemybox.Cursor = Cursors.Hand;

                    // On ajoute les controleurs pour les bouger
                    enemybox.MouseDown += PictureBox1_MouseDown;
                    enemybox.MouseMove += PictureBox1_MouseMove;
                    enemybox.MouseUp += PictureBox1_MouseUp;
                    enemybox.MouseClick += enemy_MouseClick;

                    enemyPicures.Add(enemybox);
                    gamePanel.Controls.Add(enemybox);
                }
            }
            refreshProperties();
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
            if (e.Button == MouseButtons.Right && hitTestInfo.Node == null)
            {
                contextMenuPattern.Items[0].Visible = true;
                contextMenuPattern.Show(patternList, e.Location);
            }
        }

        private void contextMenuPattern_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < contextMenuPattern.Items.Count; i++)
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
            else if (item.Text == "Supprimer l'ennemi")
            {
                //On supprime l'ennemi
                if (selectedEnemy != null)
                {
                    int index = enemyPicures.IndexOf(selectedEnemy);
                    waveSelected.removeEnemy(index);
                    enemyPicures.Remove(selectedEnemy);
                    selectedEnemy.Dispose();
                }
            }
            refreshPatternList();
            refreshProperties();

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

        private void refreshProperties(bool isLevelProperties = false)
        {
            properties.Clear();
            //Listener de la patternList
            if (!isLevelProperties && patternList.SelectedNode != null && patternList.SelectedNode.Parent == null)
            {
                //On a sélectionné un pattern

                //On ajoute les nom du pattern
                PropertyView property1 = new PropertyView();
                property1.setPanelString("Nom du pattern :");
                TextBox textBox1 = new TextBox();
                textBox1.Text = patternList.SelectedNode.Text;
                property1.setControl(textBox1);
                property1.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property1);

                PropertyView property2 = new PropertyView();
                property2.setPanelString("Ordre :");
                TextBox textBox2 = new TextBox();
                textBox2.Text = currentLevel.getPattern(patternList.SelectedNode.Index).getOrder().ToString();
                property2.setControl(textBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property2);

                PropertyView property3 = new PropertyView();
                property3.setPanelString("Est aléatoire :");
                ComboBox comboBox3 = new ComboBox();
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox3.Items.Add("vrai");
                comboBox3.Items.Add("faux");
                bool isRandom = currentLevel.getPattern(patternList.SelectedNode.Index).getIsRandom();
                if (isRandom)
                {
                    comboBox3.SelectedIndex = 0;
                }
                else
                {
                    comboBox3.SelectedIndex = 1;
                }
                property3.setControl(comboBox3);
                property3.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property3);

                PropertyView property4 = new PropertyView();
                property4.setPanelString("Nombre de vagues :");
                TextBox textBox4 = new TextBox();
                textBox4.Enabled = false;
                textBox4.Text = currentLevel.getPattern(patternList.SelectedNode.Index).getPatternWaves().Count.ToString();
                property4.setControl(textBox4);
                property4.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property4);
            }
            else if(!isLevelProperties && patternList.SelectedNode != null && patternList.SelectedNode.Parent != null)
            {

                PropertyView property1 = new PropertyView();
                property1.setPanelString("Durée :");
                TextBox textBox1 = new TextBox();
                textBox1.Text = waveSelected.getDuration().ToString();
                property1.setControl(textBox1);
                property1.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property1);

                PropertyView property2 = new PropertyView();
                property2.setPanelString("Nombre d'ennemis :");
                TextBox textBox2 = new TextBox();
                textBox2.Enabled = false;
                textBox2.Text = waveSelected.getEnemyList().Count.ToString();
                property2.setControl(textBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property2);
            }
            else if (isLevelProperties)
            {
                // Propriétés du niveau

                //On ajoute les nom du niveau
                PropertyView property1 = new PropertyView();
                property1.setPanelString("Nom du niveau :");
                TextBox textBox1 = new TextBox();
                textBox1.Text = currentLevel.getLevelName();
                property1.setControl(textBox1);
                property1.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property1);

                //on ajoute la propriétée "infinie" du niveau
                PropertyView property2 = new PropertyView();
                property2.setPanelString("Est infini :");
                ComboBox comboBox2 = new ComboBox();
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.Items.Add("vrai");
                comboBox2.Items.Add("faux");
                bool isInfinite = currentLevel.getIsInfinite();
                if (isInfinite)
                {
                    comboBox2.SelectedIndex = 0;
                }
                else
                {
                    comboBox2.SelectedIndex = 1;
                }
                property2.setControl(comboBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property2);


                //on ajoute la propriétée "random" du niveau
                PropertyView property3 = new PropertyView();
                property3.setPanelString("Est aléatoire : ");
                ComboBox comboBox3 = new ComboBox();
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox3.Items.Add("vrai");
                comboBox3.Items.Add("faux");
                bool isRandom = currentLevel.getIsRandom();
                if (isRandom)
                {
                    comboBox3.SelectedIndex = 0;
                }
                else
                {
                    comboBox3.SelectedIndex = 1;
                }
                property3.setControl(comboBox3);
                property3.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property3);

                //On ajoute le nombre de pattern
                PropertyView property4 = new PropertyView();
                property4.setPanelString("Nombre de pattern :");
                TextBox textBox4 = new TextBox();
                textBox4.Enabled = false;
                textBox4.Text = currentLevel.getPatterns().Count.ToString();
                property4.setControl(textBox4);
                property4.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property4);
            }

            propertiesPanel.Controls.Clear();
            for (int i = 0; i < properties.Count; i++)
            {
                propertiesPanel.Controls.Add(properties[i].getLabel());
                propertiesPanel.Controls.Add(properties[i].getControl());
            }
        }

        private void initProperties()
        {
            properties = new List<PropertyView>();
        }

        private void modifyPropertyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (properties.Count == 4 && properties[0].getLabel().Text == "Nom du niveau :")
                {
                    //on a les propriétés d'un niveau
                    currentLevel.updateLevelWStrings(properties[0].getControl().Text, properties[1].getControl().Text, properties[2].getControl().Text);
                }
                else if (properties.Count == 4 && properties[0].getLabel().Text == "Nom du pattern :")
                {
                    //on a les propriétés d'un pattern
                    currentLevel.getPattern(patternList.SelectedNode.Index).updatePatternWString(properties[0].getControl().Text, properties[1].getControl().Text, properties[2].getControl().Text);
                }
                else if (properties.Count == 2 && properties[1].getLabel().Text == "Nombre d'ennemis :")
                {
                    //on a les propriétés d'une wave
                    currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index).setDuration(properties[0].getControl().Text);
                }
                refreshProperties();
                refreshPatternList();
            }
            catch (Exception ex)
            {
                //Pas cool
                string message = "Erreur dans la saisie des données";
                string title = "Erreur";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                originalImageLocation = e.Location;
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var pictureBoxMoved = (PictureBox)sender;
                if (!isDragging || null == pictureBoxMoved) return;
                pictureBoxMoved.Left += e.X - originalImageLocation.X;
                pictureBoxMoved.Top += e.Y - originalImageLocation.Y;
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = sender as PictureBox;
                if (null == c) return;
                isDragging = false;
                // on change la position
                int index = enemyPicures.IndexOf(c);
                int X = (int)((float)(c.Location.X + c.Size.Width / 2) / (float)gamePanel.Size.Width * 100);
                int Y = (int)((float)(c.Location.Y + c.Size.Width / 2) / (float)gamePanel.Size.Height * 100);
                waveSelected.getEnemyList()[index].setPos(X, Y);
            }
        }

        private void gamePanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                // Autorisez le glisser-déposer en copie
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void gamePanel_DragDrop(object sender, DragEventArgs e)
        {
            // Récupérez la position du curseur dans le Panel
            Point panelLocation = gamePanel.PointToClient(new Point(e.X, e.Y));

            // Récupérez l'élément glissé (ListViewItem)
            ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            // Créez une PictureBox pour afficher l'image
            PictureBox pictureBox = new PictureBox
            {
                Image = enemies.Images[draggedItem.ImageIndex],
                Location = panelLocation,
                SizeMode = PictureBoxSizeMode.AutoSize,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
        };

            // On ajoute les controleurs pour les bouger
            pictureBox.MouseDown += PictureBox1_MouseDown;
            pictureBox.MouseMove += PictureBox1_MouseMove;
            pictureBox.MouseUp += PictureBox1_MouseUp;
            pictureBox.MouseClick += enemy_MouseClick;


            // Ajoutez la PictureBox au Panel
            enemyPicures.Add(pictureBox);
            Enemy newEnemy = new Enemy();
            int X = (int)((float)(panelLocation.X + pictureBox.Size.Width / 2) / (float)gamePanel.Size.Width * 100);
            int Y = (int)((float)(panelLocation.Y + pictureBox.Size.Width / 2) / (float)gamePanel.Size.Height * 100);
            newEnemy.setPos(X, Y);
            waveSelected.addEnemy(newEnemy);
            gamePanel.Controls.Add(pictureBox);

            refreshProperties();
        }

        private void ListView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Commencez le glisser-déposer lorsque l'élément est glissé
            listView1.DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void enemy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && currentLevel != null)
            {
                selectedEnemy = sender as PictureBox;
                // On clique droit sur un pattern
                contextMenuPattern.Items[4].Visible = true;
                contextMenuPattern.Show(sender as PictureBox, e.Location);
            }
        }
    }
}
