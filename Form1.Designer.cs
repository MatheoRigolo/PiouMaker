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
            patternList = new TreeView();
            propertiesPanel = new Panel();
            modifyPropertyButton = new Button();
            fourthPropertiesName = new Label();
            fourthPropertiesContent = new Label();
            thirdPropertiesName = new Label();
            thirdPropertiesContent = new Label();
            secondPropertiesName = new Label();
            secondPropertiesContent = new Label();
            firstPropertiesName = new Label();
            firstPropertiesContent = new Label();
            label1 = new Label();
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
            // patternList
            // 
            patternList.Location = new Point(12, 74);
            patternList.Name = "patternList";
            patternList.Size = new Size(158, 303);
            patternList.TabIndex = 9;
            patternList.Visible = false;
            patternList.AfterSelect += patternList_AfterSelect;
            // 
            // propertiesPanel
            // 
            propertiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            propertiesPanel.BackColor = Color.White;
            propertiesPanel.Controls.Add(modifyPropertyButton);
            propertiesPanel.Controls.Add(fourthPropertiesName);
            propertiesPanel.Controls.Add(fourthPropertiesContent);
            propertiesPanel.Controls.Add(thirdPropertiesName);
            propertiesPanel.Controls.Add(thirdPropertiesContent);
            propertiesPanel.Controls.Add(secondPropertiesName);
            propertiesPanel.Controls.Add(secondPropertiesContent);
            propertiesPanel.Controls.Add(firstPropertiesName);
            propertiesPanel.Controls.Add(firstPropertiesContent);
            propertiesPanel.Controls.Add(label1);
            propertiesPanel.Location = new Point(791, 14);
            propertiesPanel.Name = "propertiesPanel";
            propertiesPanel.Size = new Size(206, 363);
            propertiesPanel.TabIndex = 3;
            // 
            // modifyPropertyButton
            // 
            modifyPropertyButton.Location = new Point(48, 322);
            modifyPropertyButton.Name = "modifyPropertyButton";
            modifyPropertyButton.Size = new Size(114, 29);
            modifyPropertyButton.TabIndex = 11;
            modifyPropertyButton.Text = "Modifier";
            modifyPropertyButton.UseVisualStyleBackColor = true;
            // 
            // fourthPropertiesName
            // 
            fourthPropertiesName.AutoSize = true;
            fourthPropertiesName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fourthPropertiesName.Location = new Point(17, 243);
            fourthPropertiesName.Name = "fourthPropertiesName";
            fourthPropertiesName.Size = new Size(168, 20);
            fourthPropertiesName.TabIndex = 8;
            fourthPropertiesName.Text = "Quatrième propriétée :";
            fourthPropertiesName.Visible = false;
            // 
            // fourthPropertiesContent
            // 
            fourthPropertiesContent.AutoSize = true;
            fourthPropertiesContent.Location = new Point(17, 263);
            fourthPropertiesContent.Name = "fourthPropertiesContent";
            fourthPropertiesContent.Size = new Size(62, 20);
            fourthPropertiesContent.TabIndex = 7;
            fourthPropertiesContent.Text = "Valeur 4";
            fourthPropertiesContent.Visible = false;
            // 
            // thirdPropertiesName
            // 
            thirdPropertiesName.AutoSize = true;
            thirdPropertiesName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            thirdPropertiesName.Location = new Point(17, 179);
            thirdPropertiesName.Name = "thirdPropertiesName";
            thirdPropertiesName.Size = new Size(162, 20);
            thirdPropertiesName.TabIndex = 6;
            thirdPropertiesName.Text = "Troisième propriétée :";
            thirdPropertiesName.Visible = false;
            // 
            // thirdPropertiesContent
            // 
            thirdPropertiesContent.AutoSize = true;
            thirdPropertiesContent.Location = new Point(17, 199);
            thirdPropertiesContent.Name = "thirdPropertiesContent";
            thirdPropertiesContent.Size = new Size(62, 20);
            thirdPropertiesContent.TabIndex = 5;
            thirdPropertiesContent.Text = "Valeur 3";
            thirdPropertiesContent.Visible = false;
            // 
            // secondPropertiesName
            // 
            secondPropertiesName.AutoSize = true;
            secondPropertiesName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            secondPropertiesName.Location = new Point(15, 117);
            secondPropertiesName.Name = "secondPropertiesName";
            secondPropertiesName.Size = new Size(164, 20);
            secondPropertiesName.TabIndex = 4;
            secondPropertiesName.Text = "Deuxième propriétée :";
            secondPropertiesName.Visible = false;
            // 
            // secondPropertiesContent
            // 
            secondPropertiesContent.AutoSize = true;
            secondPropertiesContent.Location = new Point(15, 137);
            secondPropertiesContent.Name = "secondPropertiesContent";
            secondPropertiesContent.Size = new Size(62, 20);
            secondPropertiesContent.TabIndex = 3;
            secondPropertiesContent.Text = "Valeur 2";
            secondPropertiesContent.Visible = false;
            // 
            // firstPropertiesName
            // 
            firstPropertiesName.AutoSize = true;
            firstPropertiesName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            firstPropertiesName.Location = new Point(17, 60);
            firstPropertiesName.Name = "firstPropertiesName";
            firstPropertiesName.Size = new Size(157, 20);
            firstPropertiesName.TabIndex = 2;
            firstPropertiesName.Text = "Première propriétée :";
            firstPropertiesName.Visible = false;
            // 
            // firstPropertiesContent
            // 
            firstPropertiesContent.AutoSize = true;
            firstPropertiesContent.Location = new Point(17, 80);
            firstPropertiesContent.Name = "firstPropertiesContent";
            firstPropertiesContent.Size = new Size(62, 20);
            firstPropertiesContent.TabIndex = 1;
            firstPropertiesContent.Text = "Valeur 1";
            firstPropertiesContent.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            label1.Location = new Point(17, 14);
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
        private Label levelName;
        private Button saveButton;
        private Button openLevelButton;
        private Button createLevelButton;
        private OpenFileDialog openFileDialog1;
        private Label firstPropertiesContent;
        private Label firstPropertiesName;
        private Button modifyPropertyButton;
        private Label fourthPropertiesName;
        private Label fourthPropertiesContent;
        private Label thirdPropertiesName;
        private Label thirdPropertiesContent;
        private Label secondPropertiesName;
        private Label secondPropertiesContent;
        private TreeView patternList;
    }
}