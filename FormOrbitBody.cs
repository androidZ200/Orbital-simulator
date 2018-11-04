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
    public partial class FormOrbitBody : Form
    {
        FormAddBody form = null;
        FormChangeBody form2 = null;
        int NumBody;
        public FormOrbitBody(FormAddBody form)
        {
            InitializeComponent();
            this.form = form;
            for (int i = 0; i < form.form.spaceBody.Count; i++)
                checkedListBox1.Items.Add(form.form.spaceBody[i].Name);
        }
        public FormOrbitBody(FormChangeBody form2)
        {
            InitializeComponent();
            this.form2 = form2;
            for (int i = 0; i < form2.form.spaceBody.Count; i++)
                checkedListBox1.Items.Add(form2.form.spaceBody[i].Name);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (form != null)
            {
                if (NumBody == -1)
                    form.IntoOrbitOfThisBody = null;
                else
                    form.IntoOrbitOfThisBody = form.form.spaceBody[NumBody];
                form.clockwise = ClockwiseCheckBox.Checked;
            }
            else
            {
                if (NumBody == -1)
                    form2.IntoOrbitOfThisBody = null;
                else
                    form2.IntoOrbitOfThisBody = form2.form.spaceBody[NumBody];
                form2.clockwise = ClockwiseCheckBox.Checked;
            }
            Close();
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (form2 != null)
                        if (form2.body == form2.form.spaceBody[i])
                        {
                            checkedListBox1.SetItemChecked(i, false);
                            k--;
                        }
                    k++;
                    NumBody = i;
                }
            if (k == 1)
                AddButton.Enabled = true;
            else
                AddButton.Enabled = false;
        }
    }
}