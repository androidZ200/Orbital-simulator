namespace орбитальная_механика
{
    partial class FormEveryBody
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ShowButton = new System.Windows.Forms.Button();
            this.FollowButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(247, 244);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // ShowButton
            // 
            this.ShowButton.Enabled = false;
            this.ShowButton.Location = new System.Drawing.Point(12, 266);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(119, 23);
            this.ShowButton.TabIndex = 1;
            this.ShowButton.Text = "ShowBody";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // FollowButton
            // 
            this.FollowButton.Enabled = false;
            this.FollowButton.Location = new System.Drawing.Point(138, 266);
            this.FollowButton.Name = "FollowButton";
            this.FollowButton.Size = new System.Drawing.Size(121, 23);
            this.FollowButton.TabIndex = 2;
            this.FollowButton.Text = "FollowBody";
            this.FollowButton.UseVisualStyleBackColor = true;
            this.FollowButton.Click += new System.EventHandler(this.FollowButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(12, 295);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(119, 23);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "DeleteBody";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ChangeButton
            // 
            this.ChangeButton.Enabled = false;
            this.ChangeButton.Location = new System.Drawing.Point(138, 295);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(121, 23);
            this.ChangeButton.TabIndex = 4;
            this.ChangeButton.Text = "ChangeBody";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // FormEveryBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 330);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.FollowButton);
            this.Controls.Add(this.ShowButton);
            this.Controls.Add(this.checkedListBox1);
            this.MaximumSize = new System.Drawing.Size(287, 369);
            this.MinimumSize = new System.Drawing.Size(287, 369);
            this.Name = "FormEveryBody";
            this.Text = "EveryBody";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Button FollowButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ChangeButton;
    }
}