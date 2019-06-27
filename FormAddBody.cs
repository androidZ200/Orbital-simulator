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
            textBox1.Text = "Body " + Convert.ToString(form.space.AllBodies().Length + 1);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            var bodies = form.space.AllBodies();
            bool TheOnlyName = true;
            for (int i = 0; i < bodies.Length; i++)
                if (bodies[i].Name == textBox1.Text)
                    TheOnlyName = false;
            if (TheOnlyName)
            {
                if (!ShipCheckBox.Checked)
                {
                    if (StaticCheckBox.Checked)
                        form.space.AddStaticBody(new Point((trackBar1.Value + 10) * form.pictureBox1.Width / 20,
                            (trackBar2.Value + 10) * form.pictureBox1.Height / 20), trackBar5.Value, textBox1.Text);
                    else
                        form.space.AddBody(new Point((trackBar1.Value + 10) * form.pictureBox1.Width / 20,
                            (trackBar2.Value + 10) * form.pictureBox1.Height / 20), trackBar5.Value, textBox1.Text);
                }
                else
                {
                    form.space.AddShip(new Point((trackBar1.Value + 10) * form.pictureBox1.Width / 20,
                            (trackBar2.Value + 10) * form.pictureBox1.Height / 20), textBox1.Text);
                    form.drawingShip = (SpaceShip)form.space.GetSpace().bodies.Last();
                }
                bodies = form.space.AllBodies();
                if (IntoOrbitOfThisBody != null)
                    form.space.Orbit(IntoOrbitOfThisBody, bodies[bodies.Length - 1], clockwise);
                Close();
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