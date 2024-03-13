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
            modifyPropertyButton = new Button();
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
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gameBackground).BeginInit();
            contextMenuPattern.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 383);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(748, 125);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listView1);
            tabPage1.ImageKey = "(aucun)";
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(740, 92);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Ennemis";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            listView1.LargeImageList = enemies;
            listView1.Location = new Point(6, 6);
            listView1.Name = "listView1";
            listView1.Size = new Size(728, 80);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.ItemDrag += ListView1_ItemDrag;
            // 
            // enemies
            // 
            enemies.ColorDepth = ColorDepth.Depth32Bit;
            enemies.ImageStream = (ImageListStreamer)resources.GetObject("enemies.ImageStream");
            enemies.TransparentColor = Color.Transparent;
            enemies.Images.SetKeyName(0, "enemy.png");
            enemies.Images.SetKeyName(1, "lance-bombe.png");
            enemies.Images.SetKeyName(2, "shootingEnemySolo.png");
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(740, 92);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Presets perso";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // gamePanel
            // 
            gamePanel.AllowDrop = true;
            gamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamePanel.BackColor = Color.Transparent;
            gamePanel.BackgroundImageLayout = ImageLayout.Stretch;
            gamePanel.Controls.Add(gameBackground);
            gamePanel.Location = new Point(189, 14);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(571, 363);
            gamePanel.TabIndex = 2;
            gamePanel.Visible = false;
            gamePanel.DragDrop += gamePanel_DragDrop;
            gamePanel.DragEnter += gamePanel_DragEnter;
            // 
            // gameBackground
            // 
            gameBackground.BackgroundImage = Properties.Resources.parallax_mountain_bg;
            gameBackground.BackgroundImageLayout = ImageLayout.Stretch;
            gameBackground.Dock = DockStyle.Fill;
            gameBackground.Location = new Point(0, 0);
            gameBackground.Name = "gameBackground";
            gameBackground.Size = new Size(571, 363);
            gameBackground.TabIndex = 0;
            gameBackground.TabStop = false;
            // 
            // patternList
            // 
            patternList.Location = new Point(12, 74);
            patternList.Name = "patternList";
            patternList.Size = new Size(158, 303);
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
            propertiesPanel.Location = new Point(791, 41);
            propertiesPanel.Name = "propertiesPanel";
            propertiesPanel.Size = new Size(206, 311);
            propertiesPanel.TabIndex = 3;
            // 
            // modifyPropertyButton
            // 
            modifyPropertyButton.Location = new Point(837, 358);
            modifyPropertyButton.Name = "modifyPropertyButton";
            modifyPropertyButton.Size = new Size(114, 29);
            modifyPropertyButton.TabIndex = 11;
            modifyPropertyButton.Text = "Modifier";
            modifyPropertyButton.UseVisualStyleBackColor = true;
            modifyPropertyButton.Visible = false;
            modifyPropertyButton.Click += modifyPropertyButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            label1.Location = new Point(849, 14);
            label1.Name = "label1";
            label1.Size = new Size(81, 20);
            label1.TabIndex = 0;
            label1.Text = "Propriétés";
            // 
            // levelName
            // 
            levelName.BorderStyle = BorderStyle.FixedSingle;
            levelName.Cursor = Cursors.Hand;
            levelName.Location = new Point(12, 14);
            levelName.Name = "levelName";
            levelName.Size = new Size(158, 46);
            levelName.TabIndex = 5;
            levelName.Text = "Pas de niveau sélectionné";
            levelName.Click += label2_Click;
            levelName.MouseClick += label2_MouseClick;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(791, 412);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(206, 92);
            saveButton.TabIndex = 6;
            saveButton.Text = "Valider le niveau";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // openLevelButton
            // 
            openLevelButton.Location = new Point(12, 127);
            openLevelButton.Name = "openLevelButton";
            openLevelButton.Size = new Size(158, 29);
            openLevelButton.TabIndex = 7;
            openLevelButton.Text = "Ouvrir un niveau";
            openLevelButton.UseVisualStyleBackColor = true;
            openLevelButton.Click += openLevelButton_Click;
            // 
            // createLevelButton
            // 
            createLevelButton.Location = new Point(12, 189);
            createLevelButton.Name = "createLevelButton";
            createLevelButton.Size = new Size(158, 29);
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
            contextMenuPattern.Size = new Size(216, 124);
            contextMenuPattern.Closing += contextMenuPattern_Closing;
            contextMenuPattern.ItemClicked += contexMenu_ItemClicked;
            // 
            // ajouterUnPatternToolStripMenuItem
            // 
            ajouterUnPatternToolStripMenuItem.Name = "ajouterUnPatternToolStripMenuItem";
            ajouterUnPatternToolStripMenuItem.Size = new Size(215, 24);
            ajouterUnPatternToolStripMenuItem.Text = "Ajouter un pattern";
            ajouterUnPatternToolStripMenuItem.Visible = false;
            // 
            // ajouterUneVagueToolStripMenuItem
            // 
            ajouterUneVagueToolStripMenuItem.Name = "ajouterUneVagueToolStripMenuItem";
            ajouterUneVagueToolStripMenuItem.Size = new Size(215, 24);
            ajouterUneVagueToolStripMenuItem.Text = "Ajouter une vague";
            ajouterUneVagueToolStripMenuItem.Visible = false;
            // 
            // supprimerLePatternToolStripMenuItem
            // 
            supprimerLePatternToolStripMenuItem.Name = "supprimerLePatternToolStripMenuItem";
            supprimerLePatternToolStripMenuItem.Size = new Size(215, 24);
            supprimerLePatternToolStripMenuItem.Text = "Supprimer le pattern";
            supprimerLePatternToolStripMenuItem.Visible = false;
            // 
            // supprimerLaVagueToolStripMenuItem
            // 
            supprimerLaVagueToolStripMenuItem.Name = "supprimerLaVagueToolStripMenuItem";
            supprimerLaVagueToolStripMenuItem.Size = new Size(215, 24);
            supprimerLaVagueToolStripMenuItem.Text = "Supprimer la vague";
            supprimerLaVagueToolStripMenuItem.Visible = false;
            // 
            // supprimerLennemiToolStripMenuItem
            // 
            supprimerLennemiToolStripMenuItem.Name = "supprimerLennemiToolStripMenuItem";
            supprimerLennemiToolStripMenuItem.Size = new Size(215, 24);
            supprimerLennemiToolStripMenuItem.Text = "Supprimer l'ennemi";
            supprimerLennemiToolStripMenuItem.Visible = false;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1019, 520);
            Controls.Add(modifyPropertyButton);
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
            Name = "Form1";
            Text = "PiouMaker";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            gamePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gameBackground).EndInit();
            contextMenuPattern.ResumeLayout(false);
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
        private Button modifyPropertyButton;
        private TreeView patternList;
        private ContextMenuStrip contextMenuPattern;
        private ToolStripMenuItem ajouterUnPatternToolStripMenuItem;
        private ToolStripMenuItem ajouterUneVagueToolStripMenuItem;
        private ToolStripMenuItem supprimerLePatternToolStripMenuItem;
        private ToolStripMenuItem supprimerLaVagueToolStripMenuItem;
        private ToolStripMenuItem supprimerLennemiToolStripMenuItem;
        private PictureBox gameBackground;
    }
}