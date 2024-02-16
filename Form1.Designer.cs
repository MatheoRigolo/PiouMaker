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
            propertiesPanel = new Panel();
            label1 = new Label();
            patternList = new ListBox();
            levelName = new Label();
            saveButton = new Button();
            openLevelButton = new Button();
            createLevelButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            propertiesPanel.SuspendLayout();
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
            // 
            // enemies
            // 
            enemies.ColorDepth = ColorDepth.Depth32Bit;
            enemies.ImageStream = (ImageListStreamer)resources.GetObject("enemies.ImageStream");
            enemies.TransparentColor = Color.Transparent;
            enemies.Images.SetKeyName(0, "enemy.png");
            enemies.Images.SetKeyName(1, "spacecraft_player_1.png");
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
            gamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamePanel.BackgroundImage = Properties.Resources.parallax_mountain_bg;
            gamePanel.BackgroundImageLayout = ImageLayout.Stretch;
            gamePanel.Location = new Point(189, 14);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(571, 363);
            gamePanel.TabIndex = 2;
            // 
            // propertiesPanel
            // 
            propertiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            propertiesPanel.BackColor = Color.White;
            propertiesPanel.Controls.Add(label1);
            propertiesPanel.Location = new Point(791, 14);
            propertiesPanel.Name = "propertiesPanel";
            propertiesPanel.Size = new Size(206, 363);
            propertiesPanel.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 14);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 0;
            label1.Text = "Propriétés";
            // 
            // patternList
            // 
            patternList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            patternList.FormattingEnabled = true;
            patternList.Location = new Point(12, 74);
            patternList.Name = "patternList";
            patternList.Size = new Size(158, 304);
            patternList.TabIndex = 4;
            patternList.Visible = false;
            // 
            // levelName
            // 
            levelName.BorderStyle = BorderStyle.FixedSingle;
            levelName.Location = new Point(12, 14);
            levelName.Name = "levelName";
            levelName.Size = new Size(158, 46);
            levelName.TabIndex = 5;
            levelName.Text = "Pas de niveau sélectionné";
            levelName.Click += label2_Click;
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
            createLevelButton.Location = new Point(12, 184);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1019, 520);
            Controls.Add(createLevelButton);
            Controls.Add(openLevelButton);
            Controls.Add(saveButton);
            Controls.Add(levelName);
            Controls.Add(patternList);
            Controls.Add(propertiesPanel);
            Controls.Add(gamePanel);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "PiouMaker";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            propertiesPanel.ResumeLayout(false);
            propertiesPanel.PerformLayout();
            ResumeLayout(false);
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
        private ListBox patternList;
        private Label levelName;
        private Button saveButton;
        private Button openLevelButton;
        private Button createLevelButton;
        private OpenFileDialog openFileDialog1;
    }
}