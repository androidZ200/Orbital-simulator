﻿namespace орбитальная_механика
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.LenghtTrackBar = new System.Windows.Forms.TrackBar();
            this.LenghtLabel = new System.Windows.Forms.Label();
            this.SpaceBodyCountLabel = new System.Windows.Forms.Label();
            this.LabelPointX = new System.Windows.Forms.Label();
            this.LabelPointY = new System.Windows.Forms.Label();
            this.AdditionallyButton = new System.Windows.Forms.Button();
            this.SpeedVectorCheckBox = new System.Windows.Forms.CheckBox();
            this.SpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.RadarCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LenghtTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(760, 460);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // StartStopButton
            // 
            this.StartStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartStopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.StartStopButton.FlatAppearance.BorderSize = 0;
            this.StartStopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartStopButton.Location = new System.Drawing.Point(779, 13);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(76, 23);
            this.StartStopButton.TabIndex = 1;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = false;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.AddButton.FlatAppearance.BorderSize = 0;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Location = new System.Drawing.Point(779, 43);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(76, 23);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.ResetButton.FlatAppearance.BorderSize = 0;
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(779, 73);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(76, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // LenghtTrackBar
            // 
            this.LenghtTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LenghtTrackBar.Location = new System.Drawing.Point(780, 103);
            this.LenghtTrackBar.Maximum = 5000;
            this.LenghtTrackBar.Name = "LenghtTrackBar";
            this.LenghtTrackBar.Size = new System.Drawing.Size(75, 45);
            this.LenghtTrackBar.SmallChange = 10;
            this.LenghtTrackBar.TabIndex = 4;
            this.LenghtTrackBar.Value = 150;
            this.LenghtTrackBar.Scroll += new System.EventHandler(this.LenghtTrackBar_Scroll);
            // 
            // LenghtLabel
            // 
            this.LenghtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LenghtLabel.AutoSize = true;
            this.LenghtLabel.Location = new System.Drawing.Point(797, 135);
            this.LenghtLabel.Name = "LenghtLabel";
            this.LenghtLabel.Size = new System.Drawing.Size(19, 13);
            this.LenghtLabel.TabIndex = 5;
            this.LenghtLabel.Text = "50";
            // 
            // SpaceBodyCountLabel
            // 
            this.SpaceBodyCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SpaceBodyCountLabel.AutoSize = true;
            this.SpaceBodyCountLabel.Location = new System.Drawing.Point(781, 460);
            this.SpaceBodyCountLabel.Name = "SpaceBodyCountLabel";
            this.SpaceBodyCountLabel.Size = new System.Drawing.Size(13, 13);
            this.SpaceBodyCountLabel.TabIndex = 6;
            this.SpaceBodyCountLabel.Text = "0";
            // 
            // LabelPointX
            // 
            this.LabelPointX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPointX.AutoSize = true;
            this.LabelPointX.Location = new System.Drawing.Point(780, 444);
            this.LabelPointX.Name = "LabelPointX";
            this.LabelPointX.Size = new System.Drawing.Size(14, 13);
            this.LabelPointX.TabIndex = 7;
            this.LabelPointX.Text = "X";
            // 
            // LabelPointY
            // 
            this.LabelPointY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPointY.AutoSize = true;
            this.LabelPointY.Location = new System.Drawing.Point(821, 444);
            this.LabelPointY.Name = "LabelPointY";
            this.LabelPointY.Size = new System.Drawing.Size(14, 13);
            this.LabelPointY.TabIndex = 8;
            this.LabelPointY.Text = "Y";
            // 
            // AdditionallyButton
            // 
            this.AdditionallyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AdditionallyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.AdditionallyButton.FlatAppearance.BorderSize = 0;
            this.AdditionallyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdditionallyButton.Location = new System.Drawing.Point(780, 155);
            this.AdditionallyButton.Name = "AdditionallyButton";
            this.AdditionallyButton.Size = new System.Drawing.Size(75, 23);
            this.AdditionallyButton.TabIndex = 9;
            this.AdditionallyButton.Text = "AllBody";
            this.AdditionallyButton.UseVisualStyleBackColor = false;
            this.AdditionallyButton.Click += new System.EventHandler(this.AdditionallyButton_Click);
            // 
            // SpeedVectorCheckBox
            // 
            this.SpeedVectorCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedVectorCheckBox.AutoSize = true;
            this.SpeedVectorCheckBox.Checked = true;
            this.SpeedVectorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SpeedVectorCheckBox.Location = new System.Drawing.Point(779, 184);
            this.SpeedVectorCheckBox.Name = "SpeedVectorCheckBox";
            this.SpeedVectorCheckBox.Size = new System.Drawing.Size(91, 17);
            this.SpeedVectorCheckBox.TabIndex = 10;
            this.SpeedVectorCheckBox.Text = "Speed Vector";
            this.SpeedVectorCheckBox.UseVisualStyleBackColor = true;
            this.SpeedVectorCheckBox.CheckedChanged += new System.EventHandler(this.SpeedVectorCheckBox_CheckedChanged);
            // 
            // SpeedTrackBar
            // 
            this.SpeedTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedTrackBar.Location = new System.Drawing.Point(779, 230);
            this.SpeedTrackBar.Maximum = 8;
            this.SpeedTrackBar.Minimum = -8;
            this.SpeedTrackBar.Name = "SpeedTrackBar";
            this.SpeedTrackBar.Size = new System.Drawing.Size(75, 45);
            this.SpeedTrackBar.TabIndex = 4;
            this.SpeedTrackBar.Value = 3;
            this.SpeedTrackBar.Scroll += new System.EventHandler(this.SpeedTrackBar_Scroll);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Black",
            "Stars",
            "Retro",
            "Static Stars",
            "Static Retro"});
            this.comboBox1.Location = new System.Drawing.Point(782, 269);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(71, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "Black";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // RadarCheckBox
            // 
            this.RadarCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RadarCheckBox.AutoSize = true;
            this.RadarCheckBox.Checked = true;
            this.RadarCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RadarCheckBox.Location = new System.Drawing.Point(779, 207);
            this.RadarCheckBox.Name = "RadarCheckBox";
            this.RadarCheckBox.Size = new System.Drawing.Size(55, 17);
            this.RadarCheckBox.TabIndex = 13;
            this.RadarCheckBox.Text = "Radar";
            this.RadarCheckBox.UseVisualStyleBackColor = true;
            this.RadarCheckBox.CheckedChanged += new System.EventHandler(this.RadarCheckBox_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(866, 485);
            this.Controls.Add(this.RadarCheckBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SpeedTrackBar);
            this.Controls.Add(this.SpeedVectorCheckBox);
            this.Controls.Add(this.AdditionallyButton);
            this.Controls.Add(this.LabelPointY);
            this.Controls.Add(this.LabelPointX);
            this.Controls.Add(this.SpaceBodyCountLabel);
            this.Controls.Add(this.LenghtLabel);
            this.Controls.Add(this.LenghtTrackBar);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.StartStopButton);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(450, 363);
            this.Name = "FormMain";
            this.Text = "орбитальная механика";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LenghtTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button ResetButton;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar LenghtTrackBar;
        private System.Windows.Forms.Label LenghtLabel;
        private System.Windows.Forms.Label SpaceBodyCountLabel;
        private System.Windows.Forms.Label LabelPointX;
        private System.Windows.Forms.Label LabelPointY;
        private System.Windows.Forms.Button AdditionallyButton;
        private System.Windows.Forms.CheckBox SpeedVectorCheckBox;
        private System.Windows.Forms.TrackBar SpeedTrackBar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox RadarCheckBox;
    }
}

