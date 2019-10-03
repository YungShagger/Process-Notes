using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace ProcessNotes
{
    public partial class ProcessNotesMainWindow : Form
    {
        DataHandler currentData = DataHandler.Instance;
        public ProcessNotesMainWindow()
        {
            InitializeComponent();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are your sure you want to close the application?\r\nWarning! Your comment will not be saved!", "Exit", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;    
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] processlist = currentData.ProcessList;
            List<string> processNameList = new List<string>();

            foreach (Process x in processlist)
            {
                processNameList.Add(x.ProcessName);
            }
            ProcessList.DataSource = processNameList;
        }


        private void Label1_Click(object sender, EventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var form2 = new CommentChanger();
            form2.Show();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Process[] processList = currentData.ProcessList;
            int selectedIndex = ProcessList.SelectedIndex;
            currentData.SelectedIndex = selectedIndex;
            Process currentProcess = processList[selectedIndex];
            int ID = currentProcess.Id;
            string processName = currentProcess.ProcessName;

            var ram = new PerformanceCounter("Process", "Private Bytes", processName, true);
            var cpu = new PerformanceCounter("Process", "% Processor Time", processName, true);

            ram.NextValue();
            cpu.NextValue();
            Thread.Sleep(300);

            double RAM = Math.Round(ram.NextValue() / 1024 / 1024, 2);
            double CPU = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);
            string comment;

            if (currentData.ContainsID(ID))
            {
                comment = currentData.Get(ID);
            } else
            {
                comment = "no comment added yet";
            }

            try
            {
                DateTime starTime = currentProcess.StartTime;
                TimeSpan timeSpan = DateTime.Now - currentProcess.StartTime;
                textBox1.Text = $"Name: {processName}\r\nID: {ID}\r\nRunning since: {starTime}\r\nTotal timespan: {timeSpan}\r\n\r\nRAM usage: {RAM}MB\r\nCPU usage: {CPU}%\r\n\r\nUser comment: {comment}";
            }
            catch (Win32Exception)
            {
                string starTime = "Undefined";
                string timeSpan = "Undefined";
                textBox1.Text = $"Name: {processName}\r\nID: {ID}\r\nRunning since: {starTime}\r\ntotal timespan: {timeSpan}\r\n\r\nRAM usage: {RAM}MB\r\nCPU usage: {CPU}%\r\n\r\nUser comment: {comment}";
            }

        }
    }
}
