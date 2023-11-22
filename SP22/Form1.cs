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
using System.IO;


namespace SP22
{
    public partial class Form1 : Form
    {
        private Timer updateTimer = new Timer();
        private string filePath = "process_info.txt";
        public Form1()
        {
            InitializeComponent();
            InitializeListView();
            UpdateProcessList();
            updateTimer.Interval = 5000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Process Name", 150);
            listView1.Columns.Add("ID", 70);
            listView1.Columns.Add("Flows", 120);
            listView1.Columns.Add("Descript", 120);
        }
        private void UpdateProcessList()
        {
            listView1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                ListViewItem item = new ListViewItem(process.ProcessName);
                item.SubItems.Add(process.Id.ToString());
                item.SubItems.Add(process.Threads.Count.ToString());
                item.SubItems.Add(process.HandleCount.ToString());
                listView1.Items.Add(item);
            }
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateProcessList();
        }
        private void SaveProcessInfoToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    writer.WriteLine($"Process Name: {item.SubItems[0].Text}, ID: {item.SubItems[1].Text}, Flows: {item.SubItems[2].Text}, Descript: {item.SubItems[3].Text}");
                }
            }
            MessageBox.Show("Info saved.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveProcessInfoToFile();
        }
    }
}
