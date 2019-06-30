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
    public partial class FormEveryBody : Form
    {
        FormMain form;
        int NumBody = 0;
        public FormEveryBody(FormMain form)
        {
            InitializeComponent();
            this.form = form;
            var bodies = form.space.AllBodies();
            for (int i = 0; i < bodies.Length; i++)
                checkedListBox1.Items.Add(bodies[i].Name);
        }
        private void ShowButton_Click(object sender, EventArgs e)
        {
            var s = form.space.GetSpace();
            var bodies = form.space.AllBodies();
            s.follow = null;
            s.Beginning = new PointF(bodies[NumBody].point.X - form.pictureBox1.Width / 2,
                bodies[NumBody].point.Y - form.pictureBox1.Height / 2);
        }
        private void FollowButton_Click(object sender, EventArgs e)
        {
            ShowButton_Click(sender, e);
            form.space.GetSpace().follow = form.space.AllBodies()[NumBody];
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                    list.Add(i);
            var bodies = form.space.AllBodies();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (form.space.GetSpace().follow == bodies[list[i]])
                    form.space.GetSpace().follow = null;
                if (form.drawingShip == bodies[list[i]])
                    form.drawingShip = null;
                form.space.DeleteBody(bodies[list[i]]);
            }
            checkedListBox1.Items.Clear();
            bodies = form.space.AllBodies();
            for (int i = 0; i < bodies.Length; i++)
                checkedListBox1.Items.Add(bodies[i].Name);
            ShowButton.Enabled = false;
            FollowButton.Enabled = false;
            ChangeButton.Enabled = false;
        }
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            FormChangeBody form = new FormChangeBody(this.form, this.form.space.AllBodies()[NumBody]);
            form.ShowDialog();
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                {
                    k++;
                    NumBody = i;
                }
            if (k == 1)
            {
                ShowButton.Enabled = true;
                FollowButton.Enabled = true;
                ChangeButton.Enabled = true;
            }
            else
            {
                ShowButton.Enabled = false;
                FollowButton.Enabled = false;
                ChangeButton.Enabled = false;
            }
        }
    }
}