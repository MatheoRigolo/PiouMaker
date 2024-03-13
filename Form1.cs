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
        Enemy? selectedEnemy;

        //utile pour drag and drop
        List<PictureBox> enemyPicures = new List<PictureBox>();
        PictureBox selectedEnemyBox;
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
            gamePanel.Controls.Add(gameBackground);
            modifyPropertyButton.Visible = true;

            selectedEnemy = null;
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
                    gamePanel.Controls[gamePanel.Controls.Count-1].BringToFront();
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
                refreshPatternList();
            }
            else if (item.Text == "Ajouter un pattern")
            {
                currentLevel.addPattern(new Pattern());
                refreshPatternList();
            }
            else if (item.Text == "Supprimer la vague")
            {
                currentLevel.getPattern(patternList.SelectedNode.Parent.Index).removeWave(patternList.SelectedNode.Index);
                refreshPatternList();
            }
            else if (item.Text == "Supprimer le pattern")
            {
                currentLevel.removePattern(patternList.SelectedNode.Index);
                refreshPatternList();
            }
            else if (item.Text == "Supprimer l'ennemi")
            {
                //On supprime l'ennemi
                if (selectedEnemyBox != null)
                {
                    int index = enemyPicures.IndexOf(selectedEnemyBox);
                    waveSelected.removeEnemy(index);
                    enemyPicures.Remove(selectedEnemyBox);
                    selectedEnemyBox.Dispose();
                    selectedEnemy = null;
                }
            }
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
            if (!isLevelProperties && patternList.SelectedNode != null && patternList.SelectedNode.Parent == null && selectedEnemy == null)
            {
                //On a sélectionné un pattern

                //On ajoute les nom du pattern
                addProperty("Nom du pattern :", patternList.SelectedNode.Text);

                addProperty("Ordre :", currentLevel.getPattern(patternList.SelectedNode.Index).getOrder().ToString());

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
            else if(!isLevelProperties && patternList.SelectedNode != null && patternList.SelectedNode.Parent != null && selectedEnemy == null)
            {
                // Proprétés d'une wave
                addProperty("Durée :", waveSelected.getDuration().ToString());

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
                addProperty("Nom du niveau :", currentLevel.getLevelName());

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
            else if(selectedEnemy != null)
            {
                // proprétés d'un ennemi

                PropertyView property1 = new PropertyView();
                property1.setPanelString("Type d'ennemi :");
                ComboBox comboBox1 = new ComboBox();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.Items.Add("roaming enemy");
                comboBox1.Items.Add("shooting enemy");
                comboBox1.Items.Add("rusher");
                comboBox1.Items.Add("bomber");
                switch(selectedEnemy.getEnemyType())
                {
                    case "roamingEnemy":
                        comboBox1.SelectedIndex = 0;
                        break;
                    case "shootingEnemy":
                        comboBox1.SelectedIndex = 1;
                        break;
                    case "rusher":
                        comboBox1.SelectedIndex = 2;
                        break;
                    case "bomber":
                        comboBox1.SelectedIndex = 3;
                        break;
                }
                property1.setControl(comboBox1);
                property1.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property1);

                addProperty("Délai d'apparition :", selectedEnemy.getSpawnTime().ToString());

                PropertyView property3 = new PropertyView();
                property3.setPanelString("Auto Aim :");
                ComboBox comboBox3 = new ComboBox();
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox3.Items.Add("Faux");
                comboBox3.Items.Add("Vrai");
                if (selectedEnemy.AutoAim)
                {
                    comboBox3.SelectedIndex = 1;
                }
                else
                {
                    comboBox3.SelectedIndex = 0;
                }
                property3.setControl(comboBox3);
                property3.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property3);

                addProperty("Dégats :", selectedEnemy.Damage.ToString());
                addProperty("Dégats par balle :", selectedEnemy.DamagePerBullet.ToString());
                addProperty("Vitesse d'attaque :", selectedEnemy.AttackSpeed.ToString());
                addProperty("Vitesse des balles :", selectedEnemy.BulletSpeed.ToString());
                addProperty("Points de vie :", selectedEnemy.Health.ToString());
                addProperty("Score gagné :", selectedEnemy.ScoreGived.ToString());
                addProperty("Vitesse de déplacement :", selectedEnemy.MoveSpeed.ToString());

                PropertyView property2 = new PropertyView();
                property2.setPanelString("Côté d'apparition :");
                ComboBox comboBox2 = new ComboBox();
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.Items.Add("haut");
                comboBox2.Items.Add("bas");
                comboBox2.Items.Add("droite");
                comboBox2.Items.Add("gauche");
                switch (selectedEnemy.ApparitionDirection)
                {
                    case "haut":
                        comboBox2.SelectedIndex = 0;
                        break;
                    case "bas":
                        comboBox2.SelectedIndex = 1;
                        break;
                    case "droite":
                        comboBox2.SelectedIndex = 2;
                        break;
                    case "gauche":
                        comboBox2.SelectedIndex = 3;
                        break;
                }
                property2.setControl(comboBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add(property2);
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
                    refreshPatternList();
                }
                else if (properties.Count == 2 && properties[1].getLabel().Text == "Nombre d'ennemis :")
                {
                    //on a les propriétés d'une wave
                    currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index).setDuration(properties[0].getControl().Text);
                    refreshPatternList();
                }
                else if (properties.Count > 0 && selectedEnemy != null)
                {
                    // On modifie les proprétés d'un ennemi
                    string typeText = properties[0].getControl().Text;
                    string enemyType = "";
                    switch (typeText)
                    {
                        case "roaming enemy":
                            enemyType = "roamingEnemy";
                            break;
                        case "shooting enemy":
                            enemyType = "shootingEnemy";
                            break;
                        case "rusher":
                            enemyType = typeText;
                            break;
                        case "bomber":
                            enemyType = typeText; ;
                            break;
                    }
                    selectedEnemy.setEnemyType(enemyType);

                    selectedEnemy.setSpawnTime(int.Parse(properties[1].getControl().Text));

                    switch (properties[2].getControl().Text)
                    {
                        case "Vrai":
                            selectedEnemy.AutoAim = true;
                            break;
                        case "Faux":
                            selectedEnemy.AutoAim = false;
                            break;
                    }

                    selectedEnemy.Damage = int.Parse(properties[3].getControl().Text);
                    selectedEnemy.DamagePerBullet = int.Parse(properties[4].getControl().Text);
                    selectedEnemy.AttackSpeed = float.Parse(properties[5].getControl().Text);
                    selectedEnemy.BulletSpeed = float.Parse(properties[6].getControl().Text);
                    selectedEnemy.Health = int.Parse(properties[7].getControl().Text);
                    selectedEnemy.ScoreGived = int.Parse(properties[8].getControl().Text);
                    selectedEnemy.MoveSpeed = float.Parse(properties[9].getControl().Text);

                    selectedEnemy.ApparitionDirection = properties[10].getControl().Text;
                }
                refreshProperties();
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

                var pictureBoxMoved = (PictureBox)sender;
                //selectedEnemy = waveSelected.getEnemy(enemyPicures.IndexOf(pictureBoxMoved));
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
            gamePanel.Controls[gamePanel.Controls.Count - 1].BringToFront();

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
                selectedEnemyBox = sender as PictureBox;
                // On clique droit sur un pattern
                contextMenuPattern.Items[4].Visible = true;
                contextMenuPattern.Show(sender as PictureBox, e.Location);
            }
            else if(e.Button == MouseButtons.Left)
            {
                var c = sender as PictureBox;
                if (null == c) return;
                int index = enemyPicures.IndexOf(c);
                if (selectedEnemy == null || index != waveSelected.getEnemyList().IndexOf(selectedEnemy))
                {
                    selectedEnemy = waveSelected.getEnemy(index);
                    // On affiche les propriétés de l'ennemi
                    refreshProperties();
                }
            }
        }

        private void addProperty(string propertyName, string baseValue)
        {
            PropertyView property = new PropertyView();
            property.setPanelString(propertyName);
            TextBox textBox = new TextBox();
            textBox.Text = baseValue;
            property.setControl(textBox);
            property.setPos(propertiesPanel.DisplayRectangle, properties.Count);
            properties.Add(property);
        }
    }
}
