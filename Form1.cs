using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using PiouMaker.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace PiouMaker
{
    public partial class Form1 : Form
    {
        Level? currentLevel;
        XMLManager xmlManager;
        Dictionary<string, PropertyView> properties;

        Pattern? selectedPattern;
        Wave? selectedWave;
        Enemy? selectedEnemy;

        //utile pour drag and drop
        List<EnemyPictureBox> enemyPicures = new List<EnemyPictureBox>();
        EnemyPictureBox selectedEnemyBox;
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
            typeof(Panel).InvokeMember("DoubleBuffered",
   BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
   null, gamePanel, new object[] { true });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (currentLevel != null)
            {
                selectedPattern = null;
                selectedWave = null;
                selectedEnemy = null;
                gamePanel.Visible = false;
                //modifyPropertyButton.Visible = true;
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
            Level? savedLevel = currentLevel;
            //open a .piou file
            openFileDialog1.Filter = "Piou file | *.piou";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gamePanel.Visible = false;

                currentLevel = new Level();
                currentLevel.setFilePath(openFileDialog1.FileName);
                // cut pour avoir le nom du fichier sans le path
                string[] names = currentLevel.getFilePath().Split(new char[] { '\\' });
                string[] autre = names[names.Length - 1].Split(".piou");
                string name = autre[autre.Length - 2];
                currentLevel.setLevelName(name);

                xmlManager = new XMLManager(currentLevel);
                try
                {
                    xmlManager.initLevel();

                    levelName.Text = name;
                    //on enleve les buttons pour charger un niveau
                    openLevelButton.Visible = false;
                    createLevelButton.Visible = false;
                    //on afficher la liste des patterns
                    patternList.Visible = true;

                    refreshPatternList();
                }
                catch (Exception ex)
                {
                    currentLevel = savedLevel;
                    string message = "Fichier de niveau non valide : " + ex.Message;
                    string title = "Erreur";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Truc de la validation

            if (currentLevel == null)
            {
                string message = "Pas de niveau sélectionné";
                string title = "Attention";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
            // savoir si le niveau est faisable (minimum 1 pattern et pas de patterns sans vagues)
            // on autorise les vagues sans ennemis puisque ce la peut etre une vague de boss
            else if (currentLevel.getPatterns().Count == 0)
            {
                string message = "Niveau non valide : pas de pattern";
                string title = "Erreur";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
            }
            else // tout va bien (pour l'instant)
            {
                bool errorInWaves = false;
                bool errorInOrders = false;
                List<int> orders = new List<int>();
                foreach (Pattern pattern in currentLevel.getPatterns())
                {
                    if (pattern.getPatternWaves().Count == 0)
                    {
                        errorInWaves = true;
                    }
                    if (orders.Contains(pattern.getOrder()) && pattern.getOrder() != -1)
                    {
                        errorInOrders = true;
                    }
                    else
                    {
                        orders.Add(pattern.getOrder());
                    }
                }
                if (errorInWaves)
                {
                    string message2 = "Niveau non valide : au moins un pattern n'a pas de vagues d'ennemis";
                    string title2 = "Erreur";
                    MessageBoxButtons buttons2 = MessageBoxButtons.OK;
                    DialogResult result2 = MessageBox.Show(message2, title2, buttons2, MessageBoxIcon.Error);
                }
                else if (errorInOrders)
                {
                    string message2 = "Niveau non valide : Ordres des patterns incohérents";
                    string title2 = "Erreur";
                    MessageBoxButtons buttons2 = MessageBoxButtons.OK;
                    DialogResult result2 = MessageBox.Show(message2, title2, buttons2, MessageBoxIcon.Error);
                }
                else
                {
                    xmlManager.saveLevel();
                    string message = "Fichier correctement modifié";
                    string title = "Validation";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                }
            }
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
                createLevelButton.Visible = false;
                //on afficher la liste des patterns
                patternList.Visible = true;

                xmlManager = new XMLManager(currentLevel);
                xmlManager.saveLevel();
            }
        }

        private void patternList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            properties.Clear();
            gamePanel.Controls.Clear();

            gamePanel.Controls.Add(gameBackground);
            //modifyPropertyButton.Visible = true;

            selectedEnemy = null;
            //Listener de la patternList
            if (patternList.SelectedNode.Parent == null)
            {
                selectedWave = null;
                selectedEnemy = null;
                //On a sélectionné un pattern
                selectedPattern = currentLevel.getPattern(patternList.SelectedNode.Index);
                //On enlève le panel du milieu : plus aucune vague n'est sélectionnée
                gamePanel.Visible = false;
            }
            else
            {
                // On a sélectionné une wave
                selectedPattern = currentLevel.getPattern(patternList.SelectedNode.Parent.Index);
                selectedWave = selectedPattern.getWave(patternList.SelectedNode.Index);
                enemyPicures.Clear();

                //On affiche le preview du jeu au milieu
                gamePanel.Visible = true;

                //Il faut afficher les ennemis
                for (int i = 0; i < selectedWave.EnemyList.Count; i++)
                {
                    //Afficher l'ennemi
                    EnemyPictureBox enemybox = new EnemyPictureBox();
                    switch (selectedWave.EnemyList[i].EnemyType)
                    {
                        case "bomber":
                            enemybox.Image = enemies.Images["bomberImage"];
                            break;
                        case "shootingEnemy":
                            enemybox.Image = enemies.Images["shootingEnemyImage"];
                            break;
                        default:
                            enemybox.Image = enemies.Images["roamingEnemyImage"];
                            break;
                    }
                    enemybox.SizeMode = PictureBoxSizeMode.AutoSize;
                    enemybox.Location = new Point((int)(gamePanel.Width * (selectedWave.getEnemy(i).getPos().X / 100f) - (float)enemybox.Width / 2f), (int)(gamePanel.Height * (selectedWave.getEnemy(i).getPos().Y / 100f) - (float)enemybox.Height / 2f));
                    enemybox.Cursor = Cursors.Hand;
                    enemybox.BackColor = Color.Transparent;

                    if (selectedWave.EnemyList[i].EnemyType != "bomber")
                    {
                        switch (selectedWave.EnemyList[i].ApparitionDirection)
                        {
                            case "gauche":
                                enemybox.Angle = 180;
                                break;
                            case "haut":
                                enemybox.Angle = 270f;
                                break;
                            case "bas":
                                enemybox.Angle = 90f;
                                break;
                        }
                    }
                    else
                    {
                        enemybox.Angle = 0f;
                    }


                    // On ajoute les controleurs pour les bouger
                    enemybox.MouseDown += PictureBox1_MouseDown;
                    enemybox.MouseMove += PictureBox1_MouseMove;
                    enemybox.MouseUp += PictureBox1_MouseUp;
                    enemybox.MouseClick += enemy_MouseClick;

                    enemyPicures.Add(enemybox);
                    gamePanel.Controls.Add(enemybox);
                    gamePanel.Controls[gamePanel.Controls.Count - 1].BringToFront();
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
                gamePanel.Visible = false;
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
                    selectedWave.removeEnemy(index);
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
                if (selectedPattern != null && selectedPattern.getPatternName() == patternNames[i])
                {
                    patternList.SelectedNode = patternList.Nodes[i];
                    patternList.SelectedNode.Nodes.Clear();
                }
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
                addProperty("Nom du pattern :", patternList.SelectedNode.Text, "patternName");

                addProperty("Ordre :", currentLevel.getPattern(patternList.SelectedNode.Index).getOrder().ToString(), "order");

                PropertyView property3 = new PropertyView();
                property3.setPanelString("Est aléatoire :");
                ComboBox comboBox3 = new ComboBox();
                comboBox3.SelectedIndexChanged += comboBox_SelectedIndexChanged;
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
                properties.Add("isRandomPattern", property3);

                PropertyView property4 = new PropertyView();
                property4.setPanelString("Nombre de vagues :");
                TextBox textBox4 = new TextBox();
                textBox4.Enabled = false;
                textBox4.Text = currentLevel.getPattern(patternList.SelectedNode.Index).getPatternWaves().Count.ToString();
                property4.setControl(textBox4);
                property4.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add("numberOfWaves", property4);
            }
            else if (!isLevelProperties && patternList.SelectedNode != null && patternList.SelectedNode.Parent != null && selectedEnemy == null)
            {
                // Proprétés d'une wave
                addProperty("Durée :", selectedWave.getDuration().ToString(), "duration");

                PropertyView property2 = new PropertyView();
                property2.setPanelString("Nombre d'ennemis :");
                TextBox textBox2 = new TextBox();
                textBox2.Enabled = false;
                textBox2.Text = selectedWave.EnemyList.Count.ToString();
                property2.setControl(textBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add("numberOfEnnemies", property2);
            }
            else if (isLevelProperties)
            {
                // Propriétés du niveau

                //On ajoute les nom du niveau
                addProperty("Nom du niveau :", currentLevel.getLevelName(), "levelName");

                //on ajoute la propriétée "infinie" du niveau
                PropertyView property2 = new PropertyView();
                property2.setPanelString("Est infini :");
                ComboBox comboBox2 = new ComboBox();
                comboBox2.SelectedIndexChanged += comboBox_SelectedIndexChanged;
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
                properties.Add("isInfinite", property2);


                //on ajoute la propriétée "random" du niveau
                PropertyView property3 = new PropertyView();
                property3.setPanelString("Est aléatoire : ");
                ComboBox comboBox3 = new ComboBox();
                comboBox3.SelectedIndexChanged += comboBox_SelectedIndexChanged;
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
                properties.Add("isRandomLevel", property3);

                //On ajoute le nombre de pattern
                PropertyView property4 = new PropertyView();
                property4.setPanelString("Nombre de pattern :");
                TextBox textBox4 = new TextBox();
                textBox4.Enabled = false;
                textBox4.Text = currentLevel.getPatterns().Count.ToString();
                property4.setControl(textBox4);
                property4.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add("numberOfPatterns", property4);
            }
            else if (selectedEnemy != null)
            {
                // proprétés d'un ennemi

                // Propriétés communes à tous les ennemis

                PropertyView property1 = new PropertyView();
                property1.setPanelString("Type d'ennemi :");
                ComboBox comboBox1 = new ComboBox();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.Items.Add("roaming enemy");
                comboBox1.Items.Add("shooting enemy");
                comboBox1.Items.Add("rusher");
                comboBox1.Items.Add("bomber");
                switch (selectedEnemy.EnemyType)
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
                comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
                property1.setControl(comboBox1);
                property1.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add("enemyType", property1);

                addProperty("Délai d'apparition :", selectedEnemy.SpawnTime.ToString(System.Globalization.CultureInfo.InvariantCulture), "spawnTime");
                addProperty("Dégats :", selectedEnemy.Damage.ToString(), "damage");
                addProperty("Points de vie :", selectedEnemy.Health.ToString(), "health");
                addProperty("Score gagné :", selectedEnemy.ScoreGived.ToString(), "scoreGived");
                addProperty("Vitesse de déplacement :", selectedEnemy.MoveSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture), "moveSpeed");
                addProperty("xp donnée :", selectedEnemy.XpGived.ToString(), "xpGived");


                // Propriétés des shootings et des bomber

                if (selectedEnemy.EnemyType == "shootingEnemy" || selectedEnemy.EnemyType == "bomber")
                {
                    addProperty("Dégats par balle :", selectedEnemy.DamagePerBullet.ToString(), "damagePerBullet");
                    addProperty("Vitesse d'attaque :", selectedEnemy.AttackSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture), "attackSpeed");
                    addProperty("Vitesse des balles :", selectedEnemy.BulletSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture), "bulletSpeed");
                }

                // Proprétés des roaming/rusher/shooting

                if (selectedEnemy.EnemyType == "roamingEnemy" || selectedEnemy.EnemyType == "rusher" || selectedEnemy.EnemyType == "shootingEnemy")
                {
                    PropertyView property3 = new PropertyView();
                    property3.setPanelString("Auto Aim :");
                    ComboBox comboBox3 = new ComboBox();
                    comboBox3.SelectedIndexChanged += comboBox_SelectedIndexChanged;
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
                    properties.Add("autoAim", property3);

                    // Must set direction ? Apparit que si autoAim = false

                    if (!selectedEnemy.AutoAim)
                    {
                        PropertyView propertyDirection = new PropertyView();
                        propertyDirection.setPanelString("Saisir la direction ?");
                        ComboBox comboBoxDirection = new ComboBox();
                        comboBoxDirection.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboBoxDirection.Items.Add("non");
                        comboBoxDirection.Items.Add("oui");
                        if (selectedEnemy.MustSetDirection)
                        {
                            comboBoxDirection.SelectedIndex = 1;
                        }
                        else
                        {
                            comboBoxDirection.SelectedIndex = 0;
                        }
                        comboBoxDirection.SelectedIndexChanged += mustSetDirection_SelectedIndexChanged;
                        propertyDirection.setControl(comboBoxDirection);
                        propertyDirection.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                        properties.Add("mustSetDirection", propertyDirection);
                    }
                }

                PropertyView property2 = new PropertyView();
                property2.setPanelString("Côté d'apparition :");
                ComboBox comboBox2 = new ComboBox();
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.Items.Add("droite");
                comboBox2.Items.Add("gauche");
                if (selectedEnemy.EnemyType != "bomber")
                {
                    comboBox2.Items.Add("haut");
                    comboBox2.Items.Add("bas");
                }
                switch (selectedEnemy.ApparitionDirection)
                {
                    case "droite":
                        comboBox2.SelectedIndex = 0;
                        break;
                    case "gauche":
                        comboBox2.SelectedIndex = 1;
                        break;
                    case "haut":
                        comboBox2.SelectedIndex = 2;
                        break;
                    case "bas":
                        comboBox2.SelectedIndex = 3;
                        break;
                }
                comboBox2.SelectedIndexChanged += ApparitionSide_SelectedIndexChanged;
                property2.setControl(comboBox2);
                property2.setPos(propertiesPanel.DisplayRectangle, properties.Count);
                properties.Add("apparitionSide", property2);
            }

            propertiesPanel.Controls.Clear();

            var val = properties.Keys.ToList();
            foreach (var key in val)
            {
                propertiesPanel.Controls.Add(properties[key].getLabel());
                propertiesPanel.Controls.Add(properties[key].getControl());
            }
        }



        private void initProperties()
        {
            properties = new Dictionary<string, PropertyView>();
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
                var c = sender as EnemyPictureBox;
                if (null == c) return;
                isDragging = false;
                // on change la position
                int index = enemyPicures.IndexOf(c);
                int X = (int)((float)(c.Location.X + c.Size.Width / 2) / (float)gamePanel.Size.Width * 100);
                int Y = (int)((float)(c.Location.Y + c.Size.Height / 2) / (float)gamePanel.Size.Height * 100);
                selectedWave.EnemyList[index].setPos(X, Y);
            }
        }

        private void cross_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var c = sender as PictureBox;
                if (null == c) return;
                isDragging = false;
                // on change la direction de l'ennemi
                int X = (int)((float)(c.Location.X + c.Size.Width / 2) / (float)gamePanel.Size.Width * 100);
                int Y = (int)((float)(c.Location.Y + c.Size.Height / 2) / (float)gamePanel.Size.Height * 100);
                selectedEnemy.Direction = new Point(X, Y);
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
            EnemyPictureBox pictureBox = new EnemyPictureBox
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
            switch (draggedItem.ImageIndex)
            {
                case 1:
                    newEnemy.EnemyType = "bomber";
                    break;
                case 2:
                    newEnemy.EnemyType = "shootingEnemy";
                    break;
                default:
                    newEnemy.EnemyType = "roamingEnemy";
                    break;
            }
            selectedWave.addEnemy(newEnemy);
            gamePanel.Controls.Add(pictureBox);
            gamePanel.Controls[gamePanel.Controls.Count - 1].BringToFront();

            //crossPictureBox.Visible = false;

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
                selectedEnemyBox = sender as EnemyPictureBox;
                // On clique droit sur un pattern
                contextMenuPattern.Items[4].Visible = true;
                contextMenuPattern.Show(sender as PictureBox, e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                var c = sender as EnemyPictureBox;
                if (null == c) return;
                int index = enemyPicures.IndexOf(c);
                if (selectedEnemy == null || index != selectedWave.EnemyList.IndexOf(selectedEnemy))
                {
                    selectedEnemy = selectedWave.getEnemy(index);
                    // On affiche les propriétés de l'ennemi
                    refreshProperties();

                    if (selectedEnemy.MustSetDirection)
                    {
                        // On affiche la cross
                        crossPictureBox.Visible = true;
                        crossPictureBox.Location = new Point((int)((float)(selectedEnemy.Direction.X) / 100f * (float)gamePanel.Width), (int)((float)(selectedEnemy.Direction.Y) / 100f * (float)gamePanel.Height));
                        gamePanel.Controls.Add(crossPictureBox);
                        gamePanel.Controls[gamePanel.Controls.Count - 1].BringToFront();

                    }
                    else
                    {
                        crossPictureBox.Visible = false;
                    }
                }
            }
        }

        private void addProperty(string propertyName, string baseValue, string statLinked)
        {
            PropertyView property = new PropertyView();
            property.setPanelString(propertyName);
            TextBox textBox = new TextBox();
            textBox.Leave += textBox_Leave;
            textBox.Text = baseValue;
            property.setControl(textBox);
            property.setPos(propertiesPanel.DisplayRectangle, properties.Count);
            properties.Add(statLinked, property);
        }
        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (selectedEnemy != null)
            {
                int index = selectedWave.EnemyList.IndexOf(selectedEnemy);
                string newType = properties["enemyType"].getControl().Text;
                switch (newType)
                {
                    case "bomber":
                        selectedEnemy.EnemyType = "bomber";
                        if (selectedEnemy.ApparitionDirection == "haut" || selectedEnemy.ApparitionDirection == "bas")
                        {
                            selectedEnemy.ApparitionDirection = "droite";
                        }
                        enemyPicures[index].Angle = 0f;
                        enemyPicures[index].Image = enemies.Images["bomberImage"];
                        break;
                    case "shooting enemy":
                        enemyPicures[index].Image = enemies.Images["shootingEnemyImage"];
                        selectedEnemy.EnemyType = "shootingEnemy";
                        break;
                    case "roaming enemy":
                        enemyPicures[index].Image = enemies.Images["roamingEnemyImage"];
                        selectedEnemy.EnemyType = "roamingEnemy";
                        break;
                    case "rusher":
                        enemyPicures[index].Image = enemies.Images["roamingEnemyImage"];
                        selectedEnemy.EnemyType = "rusher";
                        break;
                    default:
                        enemyPicures[index].Image = enemies.Images["roamingEnemyImage"];
                        break;
                }
                refreshProperties();
            }
        }

        private void ouvrirUnNiveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openLevelButton.Visible == false)
            {
                Point originalLocation = openLevelButton.Location;
                openLevelButton.Location = new Point(-openLevelButton.Width, -openLevelButton.Height);
                openLevelButton.Visible = true;
                // Déclencher l'événement click
                openLevelButton.PerformClick();
                // Restaurer la position d'origine du bouton
                openLevelButton.Visible = false;
                openLevelButton.Location = originalLocation;
            }
            else
            {
                openLevelButton.PerformClick();
            }
        }

        private void créerUnNiveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createLevelButton.Visible == false)
            {
                Point originalLocation = createLevelButton.Location;
                createLevelButton.Location = new Point(-openLevelButton.Width, -openLevelButton.Height);
                createLevelButton.Visible = true;
                // Déclencher l'événement click
                createLevelButton.PerformClick();
                // Restaurer la position d'origine du bouton
                createLevelButton.Visible = false;
                createLevelButton.Location = originalLocation;
            }
            else
            {
                createLevelButton.PerformClick();
            }
        }

        private void enregistrerLeNiveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveButton.PerformClick();
        }

        private void mustSetDirection_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (selectedEnemy != null)
            {
                selectedEnemy.MustSetDirection = properties["mustSetDirection"].getControl().Text == "oui";
                if (selectedEnemy.MustSetDirection)
                {
                    crossPictureBox.Visible = true;
                }
                else
                {
                    crossPictureBox.Visible = false;
                }
            }
        }

        private void ApparitionSide_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (selectedEnemy != null)
            {

                int index = selectedWave.EnemyList.IndexOf(selectedEnemy);
                selectedEnemy.ApparitionDirection = properties["apparitionSide"].getControl().Text;
                if (selectedEnemy.EnemyType != "bomber")
                {
                    switch (properties["apparitionSide"].getControl().Text)
                    {
                        case "gauche":
                            enemyPicures[index].Angle = 180f;
                            break;
                        case "droite":
                            enemyPicures[index].Angle = 0;
                            break;
                        case "haut":
                            enemyPicures[index].Angle = 270;
                            break;
                        case "bas":
                            enemyPicures[index].Angle = 90;
                            break;
                    }
                }
                else
                {
                    enemyPicures[index].Angle = 0;
                }
                enemyPicures[index].Invalidate();
                enemyPicures[index].Update();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedEnemy != null)
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    CopyToClipboard<Enemy>(selectedEnemy);
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    Enemy? enemyToCopy = PasteFromClipboard<Enemy>();
                    if (enemyToCopy != null)
                    {
                        // ON ajoute l'ennemi et la pictureBox
                        Point point = enemyToCopy.getPos();
                        enemyToCopy.setPos(enemyToCopy.getPos());
                        selectedWave.addEnemy(enemyToCopy);
                        EnemyPictureBox enemybox = new EnemyPictureBox();
                        switch (enemyToCopy.EnemyType)
                        {
                            case "bomber":
                                enemybox.Image = enemies.Images["bomberImage"];
                                break;
                            case "shootingEnemy":
                                enemybox.Image = enemies.Images["shootingEnemyImage"];
                                break;
                            default:
                                enemybox.Image = enemies.Images["roamingEnemyImage"];
                                break;
                        }
                        enemybox.SizeMode = PictureBoxSizeMode.AutoSize;
                        enemybox.Location = new Point();
                        enemybox.Cursor = Cursors.Hand;
                        enemybox.BackColor = Color.Transparent;

                        switch (enemyToCopy.ApparitionDirection)
                        {
                            case "gauche":
                                enemybox.Angle = 180;
                                break;
                            case "haut":
                                enemybox.Angle = 270f;
                                break;
                            case "bas":
                                enemybox.Angle = 90f;
                                break;
                        }

                        // On ajoute les controleurs pour les bouger
                        enemybox.MouseDown += PictureBox1_MouseDown;
                        enemybox.MouseMove += PictureBox1_MouseMove;
                        enemybox.MouseUp += PictureBox1_MouseUp;
                        enemybox.MouseClick += enemy_MouseClick;

                        enemyPicures.Add(enemybox);
                        gamePanel.Controls.Add(enemybox);
                        gamePanel.Controls[gamePanel.Controls.Count - 1].BringToFront();
                    }
                }
            }
            else if (selectedWave != null)
            {
                if (e.Control && e.KeyCode == Keys.C)
                {
                    CopyToClipboard<Wave>(selectedWave);
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    Wave? waveToCopy = PasteFromClipboard<Wave>();
                    if (waveToCopy != null) selectedPattern.addWave(waveToCopy);
                    refreshPatternList();
                }
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (properties.Count == 4 && selectedPattern == null)
                {
                    //on a les propriétés d'un niveau
                    currentLevel.updateLevelWStrings(properties["levelName"].getControl().Text, properties["isInfinite"].getControl().Text, properties["isRandomLevel"].getControl().Text);

                    // On modifie le nom du niveau
                    string src = currentLevel.getFilePath();


                    string dest = src.Substring(0, src.LastIndexOf("\\") + 1);

                    currentLevel.setLevelName(properties["levelName"].getControl().Text);
                    dest += currentLevel.getLevelName() + ".piou";
                    currentLevel.setFilePath(dest);

                    File.Move(src, dest, false);

                    string[] names = currentLevel.getFilePath().Split(new char[] { '\\' });
                    string[] autre = names[names.Length - 1].Split(".piou");
                    levelName.Text = autre[autre.Length - 2];
                    currentLevel.setLevelName(levelName.Text);

                    refreshPatternList();
                    refreshProperties(true);
                }
                else if (properties.Count == 4 && selectedWave == null)
                {
                    //on a les propriétés d'un pattern
                    currentLevel.getPattern(patternList.SelectedNode.Index).updatePatternWString(properties["patternName"].getControl().Text, properties["order"].getControl().Text, properties["isRandomPattern"].getControl().Text);
                    refreshPatternList();
                }
                else if (properties.Count == 2 && selectedEnemy == null)
                {
                    //on a les propriétés d'une wave
                    currentLevel.getPattern(patternList.SelectedNode.Parent.Index).getWave(patternList.SelectedNode.Index).setDuration(properties["duration"].getControl().Text);
                    //refreshPatternList();
                }
                else if (properties.Count > 0 && selectedEnemy != null)
                {
                    if (properties.ContainsKey("spawnTime"))
                    {
                        selectedEnemy.SpawnTime = float.Parse(properties["spawnTime"].getControl().Text, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (properties.ContainsKey("damage"))
                    {
                        selectedEnemy.Damage = int.Parse(properties["damage"].getControl().Text);
                    }
                    if (properties.ContainsKey("damagePerBullet"))
                    {
                        selectedEnemy.DamagePerBullet = int.Parse(properties["damagePerBullet"].getControl().Text);
                    }
                    if (properties.ContainsKey("attackSpeed"))
                    {
                        selectedEnemy.AttackSpeed = float.Parse(properties["attackSpeed"].getControl().Text, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (properties.ContainsKey("bulletSpeed"))
                    {
                        selectedEnemy.BulletSpeed = float.Parse(properties["bulletSpeed"].getControl().Text, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (properties.ContainsKey("health"))
                    {
                        selectedEnemy.Health = int.Parse(properties["health"].getControl().Text);
                    }
                    if (properties.ContainsKey("scoreGived"))
                    {
                        selectedEnemy.ScoreGived = int.Parse(properties["scoreGived"].getControl().Text);
                    }
                    if (properties.ContainsKey("moveSpeed"))
                    {
                        selectedEnemy.MoveSpeed = float.Parse(properties["moveSpeed"].getControl().Text, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (properties.ContainsKey("xpGived"))
                    {
                        selectedEnemy.XpGived = int.Parse(properties["xpGived"].getControl().Text);
                    }
                }
                //refreshProperties();
            }
            catch (Exception ex)
            {
                //Pas cool
                string message = "Erreur dans la saisie des données : " + ex;
                string title = "Erreur";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
        }

        private void CopyToClipboard<T>(T obj)
        {
            if (obj != null)
            {
                string jsonString = JsonSerializer.Serialize(obj);

                // Copie de l'objet sérialisé dans le Presse-papiers
                Clipboard.SetText(jsonString);
            }
        }
        private T? PasteFromClipboard<T>()
        {
            // Récupération des données du Presse-papiers
            IDataObject clipboardData = Clipboard.GetDataObject();

            if (clipboardData != null && clipboardData.GetDataPresent(DataFormats.Text))
            {
                // Désérialisation de l'objet JSON
                string jsonString = (string)clipboardData.GetData(DataFormats.Text);
                return JsonSerializer.Deserialize<T>(jsonString);
            }

            return default(T);
        }

        private void comboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ComboBox? comboBoxModified = sender as ComboBox;
            if (comboBoxModified != null)
            {
                string indexKey = "";
                var val = properties.Keys.ToList();
                foreach (var key in val)
                {
                    if (comboBoxModified == properties[key].getControl())
                    {
                        indexKey = key;
                        break;
                    }
                }
                if (selectedPattern == null)
                {
                    // Level
                    if (indexKey == "isInfinite")
                    {
                        currentLevel.setIsInfinite(properties[indexKey].getControl().Text == "vrai");
                    }
                    else if (indexKey == "isRandomLevel")
                    {
                        currentLevel.setIsRandom(properties[indexKey].getControl().Text == "vrai");
                    }
                }
                else if (selectedWave == null)
                {
                    // Niveau
                    if (indexKey == "isRandomPattern")
                    {
                        selectedPattern.setIsRandom(properties[indexKey].getControl().Text == "vrai");
                    }
                }
                else if (selectedEnemy != null)
                {
                    if (indexKey == "autoAim")
                    {
                        selectedEnemy.AutoAim = properties[indexKey].getControl().Text == "Vrai";
                        if (selectedEnemy.AutoAim)
                        {
                            selectedEnemy.MustSetDirection = false;
                            crossPictureBox.Visible = false;
                        }
                        refreshProperties();
                    }
                }
            }
        }

        private void gameBackground_Click(object sender, EventArgs e)
        {

        }
    }
}
