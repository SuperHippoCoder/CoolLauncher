namespace Launcher
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Title = new Label();
            Nickname = new TextBox();
            PlayButton = new Button();
            VersionsBox = new ComboBox();
            DownloadBar = new ProgressBar();
            DownloadText = new Label();
            VersionCheck = new CheckedListBox();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Location = new Point(303, 26);
            Title.Name = "Title";
            Title.Size = new Size(91, 17);
            Title.TabIndex = 0;
            Title.Text = "Cool Launcher";
            // 
            // Nickname
            // 
            Nickname.Location = new Point(71, 55);
            Nickname.Name = "Nickname";
            Nickname.PlaceholderText = "Write nicnkame here...";
            Nickname.Size = new Size(344, 25);
            Nickname.TabIndex = 1;
            // 
            // PlayButton
            // 
            PlayButton.BackColor = Color.FromArgb(255, 128, 128);
            PlayButton.FlatAppearance.BorderColor = Color.Maroon;
            PlayButton.FlatAppearance.BorderSize = 2;
            PlayButton.FlatStyle = FlatStyle.Flat;
            PlayButton.Location = new Point(71, 97);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(81, 31);
            PlayButton.TabIndex = 2;
            PlayButton.Text = "Play";
            PlayButton.UseVisualStyleBackColor = false;
            PlayButton.Click += PlayButton_Click;
            // 
            // VersionsBox
            // 
            VersionsBox.FormattingEnabled = true;
            VersionsBox.Location = new Point(173, 103);
            VersionsBox.Name = "VersionsBox";
            VersionsBox.Size = new Size(131, 25);
            VersionsBox.TabIndex = 3;
            VersionsBox.SelectedIndexChanged += VersionsBox_SelectedIndexChanged;
            // 
            // DownloadBar
            // 
            DownloadBar.BackColor = SystemColors.ButtonShadow;
            DownloadBar.Location = new Point(71, 413);
            DownloadBar.Name = "DownloadBar";
            DownloadBar.Size = new Size(375, 25);
            DownloadBar.Style = ProgressBarStyle.Continuous;
            DownloadBar.TabIndex = 4;
            DownloadBar.Value = 10;
            // 
            // DownloadText
            // 
            DownloadText.AutoSize = true;
            DownloadText.Location = new Point(25, 421);
            DownloadText.Name = "DownloadText";
            DownloadText.Size = new Size(40, 17);
            DownloadText.TabIndex = 5;
            DownloadText.Text = "100%";
            // 
            // VersionCheck
            // 
            VersionCheck.FormattingEnabled = true;
            VersionCheck.Items.AddRange(new object[] { "Release", "Snapshot", "Beta", "Alpha" });
            VersionCheck.Location = new Point(322, 108);
            VersionCheck.Name = "VersionCheck";
            VersionCheck.Size = new Size(130, 104);
            VersionCheck.TabIndex = 6;
            VersionCheck.ItemCheck += VersionCheck_ItemCheck;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(VersionCheck);
            Controls.Add(DownloadText);
            Controls.Add(DownloadBar);
            Controls.Add(VersionsBox);
            Controls.Add(PlayButton);
            Controls.Add(Nickname);
            Controls.Add(Title);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Title;
        private TextBox Nickname;
        private Button PlayButton;
        private ComboBox VersionsBox;
        private ProgressBar DownloadBar;
        private Label DownloadText;
        private CheckedListBox VersionCheck;
    }
}
