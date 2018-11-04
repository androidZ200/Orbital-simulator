namespace орбитальная_механика
{
    partial class FormAddBody
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.StaticCheckBox = new System.Windows.Forms.CheckBox();
            this.OrbitButton = new System.Windows.Forms.Button();
            this.ShipCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(13, 13);
            this.trackBar1.Minimum = -10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TabStop = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "pX: 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "pY: 0";
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.Location = new System.Drawing.Point(175, 13);
            this.trackBar2.Minimum = -10;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.TabStop = false;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 20);
            this.textBox1.TabIndex = 8;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(210, 91);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(111, 32);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(13, 91);
            this.trackBar5.Maximum = 110;
            this.trackBar5.Minimum = 10;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(191, 45);
            this.trackBar5.TabIndex = 11;
            this.trackBar5.Value = 10;
            this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Weight: 10";
            // 
            // StaticCheckBox
            // 
            this.StaticCheckBox.AutoSize = true;
            this.StaticCheckBox.Location = new System.Drawing.Point(103, 138);
            this.StaticCheckBox.Name = "StaticCheckBox";
            this.StaticCheckBox.Size = new System.Drawing.Size(53, 17);
            this.StaticCheckBox.TabIndex = 13;
            this.StaticCheckBox.Text = "Static";
            this.StaticCheckBox.UseVisualStyleBackColor = true;
            // 
            // OrbitButton
            // 
            this.OrbitButton.Location = new System.Drawing.Point(175, 134);
            this.OrbitButton.Name = "OrbitButton";
            this.OrbitButton.Size = new System.Drawing.Size(146, 23);
            this.OrbitButton.TabIndex = 14;
            this.OrbitButton.Text = "Into Orbit";
            this.OrbitButton.UseVisualStyleBackColor = true;
            this.OrbitButton.Click += new System.EventHandler(this.OrbitButton_Click);
            // 
            // ShipCheckBox
            // 
            this.ShipCheckBox.AutoSize = true;
            this.ShipCheckBox.Location = new System.Drawing.Point(103, 119);
            this.ShipCheckBox.Name = "ShipCheckBox";
            this.ShipCheckBox.Size = new System.Drawing.Size(47, 17);
            this.ShipCheckBox.TabIndex = 15;
            this.ShipCheckBox.Text = "Ship";
            this.ShipCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormAddBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 163);
            this.Controls.Add(this.ShipCheckBox);
            this.Controls.Add(this.OrbitButton);
            this.Controls.Add(this.StaticCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.MaximumSize = new System.Drawing.Size(349, 202);
            this.MinimumSize = new System.Drawing.Size(349, 202);
            this.Name = "FormAddBody";
            this.Text = "Add";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox StaticCheckBox;
        private System.Windows.Forms.Button OrbitButton;
        private System.Windows.Forms.CheckBox ShipCheckBox;
    }
}