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
            this.StaticCheckBox = new System.Windows.Forms.CheckBox();
            this.OrbitButton = new System.Windows.Forms.Button();
            this.ShipCheckBox = new System.Windows.Forms.CheckBox();
            this.TextBoxWeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
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
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(13, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 20);
            this.textBox1.TabIndex = 8;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.AddButton.FlatAppearance.BorderSize = 0;
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Location = new System.Drawing.Point(175, 91);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(146, 32);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // StaticCheckBox
            // 
            this.StaticCheckBox.AutoSize = true;
            this.StaticCheckBox.Location = new System.Drawing.Point(66, 138);
            this.StaticCheckBox.Name = "StaticCheckBox";
            this.StaticCheckBox.Size = new System.Drawing.Size(53, 17);
            this.StaticCheckBox.TabIndex = 13;
            this.StaticCheckBox.Text = "Static";
            this.StaticCheckBox.UseVisualStyleBackColor = true;
            // 
            // OrbitButton
            // 
            this.OrbitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.OrbitButton.FlatAppearance.BorderSize = 0;
            this.OrbitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OrbitButton.Location = new System.Drawing.Point(175, 134);
            this.OrbitButton.Name = "OrbitButton";
            this.OrbitButton.Size = new System.Drawing.Size(146, 23);
            this.OrbitButton.TabIndex = 14;
            this.OrbitButton.Text = "Into Orbit";
            this.OrbitButton.UseVisualStyleBackColor = false;
            this.OrbitButton.Click += new System.EventHandler(this.OrbitButton_Click);
            // 
            // ShipCheckBox
            // 
            this.ShipCheckBox.AutoSize = true;
            this.ShipCheckBox.Location = new System.Drawing.Point(13, 138);
            this.ShipCheckBox.Name = "ShipCheckBox";
            this.ShipCheckBox.Size = new System.Drawing.Size(47, 17);
            this.ShipCheckBox.TabIndex = 15;
            this.ShipCheckBox.Text = "Ship";
            this.ShipCheckBox.UseVisualStyleBackColor = true;
            // 
            // TextBoxWeight
            // 
            this.TextBoxWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.TextBoxWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxWeight.ForeColor = System.Drawing.Color.White;
            this.TextBoxWeight.Location = new System.Drawing.Point(60, 108);
            this.TextBoxWeight.Name = "TextBoxWeight";
            this.TextBoxWeight.Size = new System.Drawing.Size(59, 20);
            this.TextBoxWeight.TabIndex = 16;
            this.TextBoxWeight.Text = "10";
            this.TextBoxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Weight:";
            // 
            // FormAddBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(333, 163);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxWeight);
            this.Controls.Add(this.ShipCheckBox);
            this.Controls.Add(this.OrbitButton);
            this.Controls.Add(this.StaticCheckBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximumSize = new System.Drawing.Size(349, 202);
            this.MinimumSize = new System.Drawing.Size(349, 202);
            this.Name = "FormAddBody";
            this.Text = "Add";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
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
        private System.Windows.Forms.CheckBox StaticCheckBox;
        private System.Windows.Forms.Button OrbitButton;
        private System.Windows.Forms.CheckBox ShipCheckBox;
        private System.Windows.Forms.TextBox TextBoxWeight;
        private System.Windows.Forms.Label label3;
    }
}