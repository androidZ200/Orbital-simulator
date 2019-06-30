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
    public partial class FormChangeBody : Form
    {
        public FormMain form;
        public SpaceBody IntoOrbitOfThisBody = null;
        public bool clockwise;
        public SpaceBody body;
        Color colorBody;
        public FormChangeBody(FormMain form, SpaceBody body)
        {
            InitializeComponent();
            this.form = form;
            this.body = body;
            Text = body.Name;
            textBox1.Text = Convert.ToString(body.point.X);
            textBox2.Text = Convert.ToString(body.point.Y);
            textBox4.Text = Convert.ToString(body.speed.X);
            textBox3.Text = Convert.ToString(body.speed.Y);
            textBox5.Text = Convert.ToString(body.Weight);
            textBox6.Text = body.Name;
            StaticCheckBox.Checked = body is SpaceStaticBody;
            FollowCheckBox.Checked = body == form.space.GetSpace().follow;
            colorBody = body.color;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(colorBody);
            pictureBox1.Image = bmp;
            if (body is SpaceShip)
            {
                textBox5.Enabled = false;
                StaticCheckBox.Text = "Driving";
                StaticCheckBox.Checked = body == form.drawingShip;
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            var bodies = form.space.AllBodies();
            bool TheOnlyName = true;
            for (int i = 0; i < bodies.Length; i++)
                if (bodies[i].Name == textBox6.Text && bodies[i] != body)
                    TheOnlyName = false;

            if (form.space.GetSpace().follow == body)
                form.space.GetSpace().follow = null;
            if (form.drawingShip == body)
                form.drawingShip = null;

            if (TheOnlyName)
                try
                {
                    body.color = colorBody;

                    body.point.X = (float)Convert.ToDouble(textBox1.Text);
                    body.point.Y = (float)Convert.ToDouble(textBox2.Text);
                    body.speed.X = (float)Convert.ToDouble(textBox4.Text);
                    body.speed.Y = (float)Convert.ToDouble(textBox3.Text);
                    body.Weight  = (float)Convert.ToDouble(textBox5.Text);
                    body.Name = textBox6.Text;
                    if (body is SpaceShip)
                    {
                        if (StaticCheckBox.Checked)
                            form.drawingShip = (SpaceShip)body;
                        ((SpaceShip)body).DrawShip();
                    }
                    else
                    {
                        SpaceBody t;
                        if (StaticCheckBox.Checked)
                            t = new SpaceStaticBody(body);
                        else t = new SpaceBody(body);
                        form.space.DeleteBody(body);
                        form.space.AddBody(t);
                        body = t;
                    }

                    if (FollowCheckBox.Checked)
                        form.space.GetSpace().follow = body;

                    if (IntoOrbitOfThisBody != null)
                        form.space.Orbit(IntoOrbitOfThisBody, body, clockwise);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Недопустимый параметр");
                }
            else
                MessageBox.Show("Такое имя уже существует");
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (form.space.GetSpace().follow == body)
                form.space.GetSpace().follow = null;
            if (form.drawingShip == body)
                form.drawingShip = null;
            form.space.DeleteBody(body);
            Close();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK) colorBody = colorDialog1.Color;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(colorBody);
            pictureBox1.Image = bmp;
        }
        private void OrbitButton_Click(object sender, EventArgs e)
        {
            FormOrbitBody form = new FormOrbitBody(this);
            form.ShowDialog();
        }
    }
}