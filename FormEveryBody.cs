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
        SpaceBody Body = null;
        public FormEveryBody(FormMain form)
        {
            InitializeComponent();
            this.form = form;
            var bodies = form.space.AllBodies();
            for (int i = 0; i < bodies.Length; i++)
                if (bodies[i] is SpaceBody)
                    checkedListBox1.Items.Add(bodies[i].Name);
        }
        private void ShowButton_Click(object sender, EventArgs e)
        {
            var s = form.space.GetSpace();
            var bodies = form.space.AllBodies();
            s.follow = null;
            s.Beginning = new PointF(Body.point.X - form.pictureBox1.Width / 2,
                Body.point.Y - form.pictureBox1.Height / 2);
        }
        private void FollowButton_Click(object sender, EventArgs e)
        {
            ShowButton_Click(sender, e);
            form.space.GetSpace().follow = Body;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            List<SpaceBody> list = new List<SpaceBody>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                    list.Add((SpaceBody)form.space.FindBody(checkedListBox1.Items[i].ToString()));
            var bodies = form.space.AllBodies();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (form.space.GetSpace().follow == list[i])
                    form.space.GetSpace().follow = null;
                if (form.drawingShip == list[i])
                    form.drawingShip = null;
                form.space.DeleteBody(list[i]);
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
            FormChangeBody form = new FormChangeBody(this.form, Body);
            form.ShowDialog();
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                {
                    k++;
                    Body = (SpaceBody)form.space.FindBody(checkedListBox1.Items[i].ToString());
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