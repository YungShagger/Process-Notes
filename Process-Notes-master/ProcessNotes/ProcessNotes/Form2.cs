using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessNotes
{
    public partial class CommentChanger : Form
    {
        DataHandler currentData = DataHandler.Instance;
        public CommentChanger()
        {
            InitializeComponent();
            int processIndex = currentData.ProcessList[currentData.SelectedIndex].Id;
            textBoxComment.Text = currentData.Get(processIndex);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string comment = textBoxComment.Text;
            int processIndex = currentData.ProcessList[currentData.SelectedIndex].Id;
            currentData.Set(processIndex, comment);
            CommentChanger.ActiveForm.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBoxComment.Text = String.Empty;
            CommentChanger.ActiveForm.Hide();
        }
    }
}
