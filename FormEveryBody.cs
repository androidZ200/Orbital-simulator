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
            for (int i = 0; i < form.spaceBody.Count; i++)
                checkedListBox1.Items.Add(form.spaceBody[i].Name);
        }
        private void ShowButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < form.spaceBody.Count; i++)
                form.spaceBody[i].Follow = false;
            form.Beginning.X = (int)(form.spaceBody[NumBody].pointX - form.pictureBox1.Width / 2);
            form.Beginning.Y = (int)(form.spaceBody[NumBody].pointY - form.pictureBox1.Height / 2);
            form.DrawSpace();
        }
        private void FollowButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < form.spaceBody.Count; i++)
                form.spaceBody[i].Follow = false;
            form.Beginning.X = (int)(form.spaceBody[NumBody].pointX - form.pictureBox1.Width / 2);
            form.Beginning.Y = (int)(form.spaceBody[NumBody].pointY - form.pictureBox1.Height / 2);
            for (int i = 0; i < form.spaceBody.Count; i++)
                form.spaceBody[i].Follow = false;
            form.spaceBody[NumBody].Follow = true;
            form.spaceBody[NumBody].OnPicture.X = (int)form.spaceBody[NumBody].pointX - form.Beginning.X;
            form.spaceBody[NumBody].OnPicture.Y = (int)form.spaceBody[NumBody].pointY - form.Beginning.Y;
            form.DrawSpace();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                    list.Add(i);
            for (int i = list.Count - 1; i >= 0; i--)
                form.spaceBody.RemoveAt(list[i]);
            checkedListBox1.Items.Clear();
            for (int i = 0; i < form.spaceBody.Count; i++)
                checkedListBox1.Items.Add(form.spaceBody[i].Name);
            ShowButton.Enabled = false;
            FollowButton.Enabled = false;
            ChangeButton.Enabled = false;
            form.DrawSpace();
        }
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            FormChangeBody form = new FormChangeBody(this.form, this.form.spaceBody[NumBody]);
            form.ShowDialog();
            this.form.DrawSpace();
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