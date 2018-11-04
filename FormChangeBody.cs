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
            textBox1.Text = Convert.ToString(body.pointX);
            textBox2.Text = Convert.ToString(body.pointY);
            textBox4.Text = Convert.ToString(body.speedX);
            textBox3.Text = Convert.ToString(body.speedY);
            textBox5.Text = Convert.ToString(body.Weight);
            textBox6.Text = body.Name;
            StaticCheckBox.Checked = body.Static;
            FollowCheckBox.Checked = body.Follow;
            colorBody = body.color;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(colorBody);
            pictureBox1.Image = bmp;
            if (body is SpaceShip)
            {
                textBox5.Enabled = false;
                StaticCheckBox.Text = "Driving";
                var s = (SpaceShip)body;
                StaticCheckBox.Checked = s.Driving;
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool TheOnlyName = true;
            for (int i = 0; i < form.spaceBody.Count; i++)
                if (form.spaceBody[i].Name == textBox6.Text && form.spaceBody[i] != body)
                    TheOnlyName = false;
            if (TheOnlyName)
                try
                {
                    body.pointX = Convert.ToDouble(textBox1.Text);
                    body.pointY = Convert.ToDouble(textBox2.Text);
                    body.speedX = Convert.ToDouble(textBox4.Text);
                    body.speedY = Convert.ToDouble(textBox3.Text);
                    body.Weight = Convert.ToDouble(textBox5.Text);
                    body.Name = textBox6.Text;
                    if (body is SpaceShip)
                    {
                        SpaceShip s = (SpaceShip)body;
                        if (StaticCheckBox.Checked)
                            for (int i = 0; i < form.spaceBody.Count; i++)
                                if(form.spaceBody[i] is SpaceShip)
                                {
                                    SpaceShip ss = (SpaceShip)form.spaceBody[i];
                                    ss.Driving = false;
                                }
                        s.Driving = StaticCheckBox.Checked;
                    }
                    else
                        body.Static = StaticCheckBox.Checked;
                    if (FollowCheckBox.Checked)
                        for (int i = 0; i < form.spaceBody.Count; i++)
                            form.spaceBody[i].Follow = false;
                    body.Follow = FollowCheckBox.Checked;
                    body.OnPicture.X = (int)body.pointX - form.Beginning.X;
                    body.OnPicture.Y = (int)body.pointY - form.Beginning.Y;
                    body.color = colorBody;
                    if (IntoOrbitOfThisBody != null)
                        form.Orbit(IntoOrbitOfThisBody, body, clockwise);
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
            for (int i = 0; i < form.spaceBody.Count; i++)
                if (form.spaceBody[i] == body)
                {
                    form.spaceBody.RemoveAt(i);
                    break;
                }
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