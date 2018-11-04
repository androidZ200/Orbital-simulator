using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace орбитальная_механика
{
    public partial class FormAddBody : Form
    {
        public FormMain form;
        public SpaceBody IntoOrbitOfThisBody = null;
        public bool clockwise;
        public FormAddBody(FormMain form)
        {
            InitializeComponent();
            this.form = form;
            textBox1.Text = "Body " + Convert.ToString(form.spaceBody.Count + 1);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            bool TheOnlyName = true;
            for (int i = 0; i < form.spaceBody.Count; i++)
                if (form.spaceBody[i].Name == textBox1.Text)
                    TheOnlyName = false;
            if (TheOnlyName)
            {
                if (!ShipCheckBox.Checked)
                {
                    form.AddSpaseBody((trackBar1.Value + 10) * form.pictureBox1.Width / 20, (trackBar2.Value + 10) * form.pictureBox1.Height / 20,
                        trackBar5.Value, StaticCheckBox.Checked, textBox1.Text);
                    if (IntoOrbitOfThisBody != null)
                    {
                        form.Orbit(IntoOrbitOfThisBody, form.spaceBody[form.spaceBody.Count - 1], clockwise);
                    }
                    Close();
                }
                else
                {
                    form.AddSpaseShip((trackBar1.Value + 10) * form.pictureBox1.Width / 20, (trackBar2.Value + 10) * form.pictureBox1.Height / 20,
                        textBox1.Text);
                    if (IntoOrbitOfThisBody != null)
                    {
                        form.Orbit(IntoOrbitOfThisBody, form.spaceBody[form.spaceBody.Count - 1], clockwise);
                    }
                    Close();
                }
            }
            else
                MessageBox.Show("Такое имя уже существует");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "pX: " + Convert.ToString(trackBar1.Value);
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = "pY: " + Convert.ToString(trackBar2.Value);
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label5.Text = "Weight: " + Convert.ToString(trackBar5.Value);
        }
        private void OrbitButton_Click(object sender, EventArgs e)
        {
            FormOrbitBody form = new FormOrbitBody(this);
            form.ShowDialog();
        }
    }
}