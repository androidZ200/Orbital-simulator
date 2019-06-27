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
        FormAddBody form1 = null;
        FormChangeBody form2 = null;
        FormMain form;
        int NumBody;

        public FormOrbitBody(FormAddBody form)
        {
            InitializeComponent();
            form1 = form;
            this.form = form.form;
            var bodies = form.form.space.AllBodies();
            for (int i = 0; i < bodies.Length; i++)
                checkedListBox1.Items.Add(bodies[i].Name);
        }
        public FormOrbitBody(FormChangeBody form)
        {
            InitializeComponent();
            form2 = form;
            this.form = form.form;
            var bodies = form2.form.space.AllBodies();
            for (int i = 0; i < bodies.Length; i++)
                checkedListBox1.Items.Add(bodies[i].Name);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (form1 != null)
            {
                if (NumBody == -1)
                    form1.IntoOrbitOfThisBody = null;
                else
                {
                    var bodies = form.space.AllBodies();
                    form1.IntoOrbitOfThisBody = bodies[NumBody];
                }
                form1.clockwise = ClockwiseCheckBox.Checked;
            }
            else
            {
                if (NumBody == -1)
                    form2.IntoOrbitOfThisBody = null;
                else
                {
                    var bodies = form.space.AllBodies();
                    form2.IntoOrbitOfThisBody = bodies[NumBody];
                }
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
                        if (form2.body == form.space.AllBodies()[i])
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