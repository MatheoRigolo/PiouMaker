namespace PiouMaker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            listView1 = new ListView();
            enemies = new ImageList(components);
            tabPage2 = new TabPage();
            gamePanel = new Panel();
            gameBackground = new PictureBox();
            patternList = new TreeView();
            propertiesPanel = new Panel();
            label1 = new Label();
            levelName = new Label();
            saveButton = new Button();
            openLevelButton = new Button();
            createLevelButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            contextMenuPattern = new ContextMenuStrip(components);
            ajouterUnPatternToolStripMenuItem = new ToolStripMenuItem();
            ajouterUneVagueToolStripMenuItem = new ToolStripMenuItem();
            supprimerLePatternToolStripMenuItem = new ToolStripMenuItem();
            supprimerLaVagueToolStripMenuItem = new ToolStripMenuItem();
            supprimerLennemiToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            fichierToolStripMenuItem = new ToolStripMenuItem();
            ouvrirUnNiveauToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            créerUnNiveauToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            enregistrerLeNiveauToolStripMenuItem = new ToolStripMenuItem();
            crossPictureBox = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gameBackground).BeginInit();
            contextMenuPattern.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)crossPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(10, 287);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(654, 94);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listView1);
            tabPage1.ImageKey = "(aucun)";
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(646, 66);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Ennemis";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.LargeImageList = enemies;
            listView1.Location = new Point(5, 4);
            listView1.Margin = new Padding(3, 2, 3, 2);
            listView1.Name = "listView1";
            listView1.Size = new Size(638, 61);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.ItemDrag += ListView1_ItemDrag;
            // 
            // enemies
            // 
            enemies.ColorDepth = ColorDepth.Depth32Bit;
            enemies.ImageStream = (ImageListStreamer)resources.GetObject("enemies.ImageStream");
            enemies.TransparentColor = Color.Transparent;
            enemies.Images.SetKeyName(0, "roamingEnemyImage");
            enemies.Images.SetKeyName(1, "bomberImage");
            enemies.Images.SetKeyName(2, "shootingEnemyImage");
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(646, 66);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Presets perso";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // gamePanel
            // 
            gamePanel.AllowDrop = true;
            gamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamePanel.BackColor = Color.Transparent;
            gamePanel.BackgroundImage = Properties.Resources.parallax_mountain_bg;
            gamePanel.BackgroundImageLayout = ImageLayout.Zoom;
            gamePanel.Controls.Add(gameBackground);
            gamePanel.Location = new Point(165, 10);
            gamePanel.Margin = new Padding(3, 2, 3, 2);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(500, 272);
            gamePanel.TabIndex = 2;
            gamePanel.Visible = false;
            gamePanel.DragDrop += gamePanel_DragDrop;
            gamePanel.DragEnter += gamePanel_DragEnter;
            // 
            // gameBackground
            // 
            gameBackground.BackgroundImageLayout = ImageLayout.Stretch;
            gameBackground.Dock = DockStyle.Fill;
            gameBackground.Location = new Point(0, 0);
            gameBackground.Margin = new Padding(3, 2, 3, 2);
            gameBackground.Name = "gameBackground";
            gameBackground.Size = new Size(500, 272);
            gameBackground.SizeMode = PictureBoxSizeMode.Zoom;
            gameBackground.TabIndex = 0;
            gameBackground.TabStop = false;
            gameBackground.Click += gameBackground_Click;
            // 
            // patternList
            // 
            patternList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            patternList.Location = new Point(10, 63);
            patternList.Margin = new Padding(3, 2, 3, 2);
            patternList.Name = "patternList";
            patternList.Size = new Size(139, 221);
            patternList.TabIndex = 9;
            patternList.Visible = false;
            patternList.AfterSelect += patternList_AfterSelect;
            patternList.NodeMouseClick += patternList_NodeMouseClick;
            patternList.MouseUp += patternList_MouseClick;
            // 
            // propertiesPanel
            // 
            propertiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            propertiesPanel.AutoScroll = true;
            propertiesPanel.BackColor = Color.White;
            propertiesPanel.Location = new Point(692, 40);
            propertiesPanel.Margin = new Padding(3, 2, 3, 2);
            propertiesPanel.Name = "propertiesPanel";
            propertiesPanel.Size = new Size(180, 242);
            propertiesPanel.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            label1.Location = new Point(744, 21);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "Propriétés";
            // 
            // levelName
            // 
            levelName.BorderStyle = BorderStyle.FixedSingle;
            levelName.Cursor = Cursors.Hand;
            levelName.Location = new Point(10, 21);
            levelName.Name = "levelName";
            levelName.Size = new Size(138, 35);
            levelName.TabIndex = 5;
            levelName.Text = "Pas de niveau sélectionné";
            levelName.Click += label2_Click;
            levelName.MouseClick += label2_MouseClick;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(692, 309);
            saveButton.Margin = new Padding(3, 2, 3, 2);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(180, 69);
            saveButton.TabIndex = 6;
            saveButton.Text = "Valider le niveau";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // openLevelButton
            // 
            openLevelButton.Anchor = AnchorStyles.Left;
            openLevelButton.Location = new Point(10, 95);
            openLevelButton.Margin = new Padding(3, 2, 3, 2);
            openLevelButton.Name = "openLevelButton";
            openLevelButton.Size = new Size(138, 22);
            openLevelButton.TabIndex = 7;
            openLevelButton.Text = "Ouvrir un niveau";
            openLevelButton.UseVisualStyleBackColor = true;
            openLevelButton.Click += openLevelButton_Click;
            // 
            // createLevelButton
            // 
            createLevelButton.Anchor = AnchorStyles.Left;
            createLevelButton.Location = new Point(10, 142);
            createLevelButton.Margin = new Padding(3, 2, 3, 2);
            createLevelButton.Name = "createLevelButton";
            createLevelButton.Size = new Size(138, 22);
            createLevelButton.TabIndex = 8;
            createLevelButton.Text = "Créer un niveau";
            createLevelButton.UseVisualStyleBackColor = true;
            createLevelButton.Click += createLevelButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuPattern
            // 
            contextMenuPattern.ImageScalingSize = new Size(20, 20);
            contextMenuPattern.Items.AddRange(new ToolStripItem[] { ajouterUnPatternToolStripMenuItem, ajouterUneVagueToolStripMenuItem, supprimerLePatternToolStripMenuItem, supprimerLaVagueToolStripMenuItem, supprimerLennemiToolStripMenuItem });
            contextMenuPattern.Name = "contextMenuPattern";
            contextMenuPattern.Size = new Size(183, 114);
            contextMenuPattern.Closing += contextMenuPattern_Closing;
            contextMenuPattern.ItemClicked += contexMenu_ItemClicked;
            // 
            // ajouterUnPatternToolStripMenuItem
            // 
            ajouterUnPatternToolStripMenuItem.Name = "ajouterUnPatternToolStripMenuItem";
            ajouterUnPatternToolStripMenuItem.Size = new Size(182, 22);
            ajouterUnPatternToolStripMenuItem.Text = "Ajouter un pattern";
            ajouterUnPatternToolStripMenuItem.Visible = false;
            // 
            // ajouterUneVagueToolStripMenuItem
            // 
            ajouterUneVagueToolStripMenuItem.Name = "ajouterUneVagueToolStripMenuItem";
            ajouterUneVagueToolStripMenuItem.Size = new Size(182, 22);
            ajouterUneVagueToolStripMenuItem.Text = "Ajouter une vague";
            ajouterUneVagueToolStripMenuItem.Visible = false;
            // 
            // supprimerLePatternToolStripMenuItem
            // 
            supprimerLePatternToolStripMenuItem.Name = "supprimerLePatternToolStripMenuItem";
            supprimerLePatternToolStripMenuItem.Size = new Size(182, 22);
            supprimerLePatternToolStripMenuItem.Text = "Supprimer le pattern";
            supprimerLePatternToolStripMenuItem.Visible = false;
            // 
            // supprimerLaVagueToolStripMenuItem
            // 
            supprimerLaVagueToolStripMenuItem.Name = "supprimerLaVagueToolStripMenuItem";
            supprimerLaVagueToolStripMenuItem.Size = new Size(182, 22);
            supprimerLaVagueToolStripMenuItem.Text = "Supprimer la vague";
            supprimerLaVagueToolStripMenuItem.Visible = false;
            // 
            // supprimerLennemiToolStripMenuItem
            // 
            supprimerLennemiToolStripMenuItem.Name = "supprimerLennemiToolStripMenuItem";
            supprimerLennemiToolStripMenuItem.Size = new Size(182, 22);
            supprimerLennemiToolStripMenuItem.Text = "Supprimer l'ennemi";
            supprimerLennemiToolStripMenuItem.Visible = false;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fichierToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(892, 24);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            fichierToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ouvrirUnNiveauToolStripMenuItem, toolStripSeparator1, créerUnNiveauToolStripMenuItem, toolStripSeparator2, enregistrerLeNiveauToolStripMenuItem });
            fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            fichierToolStripMenuItem.Size = new Size(54, 20);
            fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirUnNiveauToolStripMenuItem
            // 
            ouvrirUnNiveauToolStripMenuItem.Name = "ouvrirUnNiveauToolStripMenuItem";
            ouvrirUnNiveauToolStripMenuItem.Size = new Size(180, 22);
            ouvrirUnNiveauToolStripMenuItem.Text = "Ouvrir un niveau";
            ouvrirUnNiveauToolStripMenuItem.Click += ouvrirUnNiveauToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // créerUnNiveauToolStripMenuItem
            // 
            créerUnNiveauToolStripMenuItem.Name = "créerUnNiveauToolStripMenuItem";
            créerUnNiveauToolStripMenuItem.Size = new Size(180, 22);
            créerUnNiveauToolStripMenuItem.Text = "Créer un niveau";
            créerUnNiveauToolStripMenuItem.Click += créerUnNiveauToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // enregistrerLeNiveauToolStripMenuItem
            // 
            enregistrerLeNiveauToolStripMenuItem.Name = "enregistrerLeNiveauToolStripMenuItem";
            enregistrerLeNiveauToolStripMenuItem.Size = new Size(180, 22);
            enregistrerLeNiveauToolStripMenuItem.Text = "Enregistrer le niveau";
            enregistrerLeNiveauToolStripMenuItem.Click += enregistrerLeNiveauToolStripMenuItem_Click;
            // 
            // crossPictureBox
            // 
            crossPictureBox.BackColor = Color.Transparent;
            crossPictureBox.Image = Properties.Resources.Cross;
            crossPictureBox.Location = new Point(145, 168);
            crossPictureBox.Margin = new Padding(3, 2, 3, 2);
            crossPictureBox.Name = "crossPictureBox";
            crossPictureBox.Size = new Size(39, 34);
            crossPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            crossPictureBox.TabIndex = 13;
            crossPictureBox.TabStop = false;
            crossPictureBox.Visible = false;
            crossPictureBox.MouseDown += PictureBox1_MouseDown;
            crossPictureBox.MouseMove += PictureBox1_MouseMove;
            crossPictureBox.MouseUp += cross_MouseUp;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(892, 390);
            Controls.Add(crossPictureBox);
            Controls.Add(menuStrip1);
            Controls.Add(createLevelButton);
            Controls.Add(openLevelButton);
            Controls.Add(saveButton);
            Controls.Add(levelName);
            Controls.Add(patternList);
            Controls.Add(propertiesPanel);
            Controls.Add(gamePanel);
            Controls.Add(tabControl1);
            Controls.Add(label1);
            DoubleBuffered = true;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "PiouMaker";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            gamePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gameBackground).EndInit();
            contextMenuPattern.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)crossPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ImageList enemies;
        private ListView listView1;
        private TreeView levelList;
        private Panel gamePanel;
        private Panel propertiesPanel;
        private Label label1;
        private Label levelName;
        private Button saveButton;
        private Button openLevelButton;
        private Button createLevelButton;
        private OpenFileDialog openFileDialog1;
        private TreeView patternList;
        private ContextMenuStrip contextMenuPattern;
        private ToolStripMenuItem ajouterUnPatternToolStripMenuItem;
        private ToolStripMenuItem ajouterUneVagueToolStripMenuItem;
        private ToolStripMenuItem supprimerLePatternToolStripMenuItem;
        private ToolStripMenuItem supprimerLaVagueToolStripMenuItem;
        private ToolStripMenuItem supprimerLennemiToolStripMenuItem;
        private PictureBox gameBackground;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fichierToolStripMenuItem;
        private ToolStripMenuItem ouvrirUnNiveauToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem créerUnNiveauToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem enregistrerLeNiveauToolStripMenuItem;
        private PictureBox crossPictureBox;
    }
}