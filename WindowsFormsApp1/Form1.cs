using System;
using System.IO;
//using System.DateTime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        const string ext = "txt";
        string FileName;
        public Form1()
        {
            InitializeComponent();
        }
        private void FocusTB(TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectionStart = textBox1.Text.Length;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text != "")
            {
                string s = "listBox" + (TabControl.SelectedIndex + 1);
                ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
                //bool flag = false;
                //for (int i = 0; i < CurListBox.Items.Count; ++i)
                //{
                //    if (CurListBox.Items[i].ToString() == textBox1.Text)
                //        flag = true;
                //}
                //if (!flag)
                {
                    CurListBox.Items.Add(textBox1.Text);
                    textBox1.Text = "";
                }
                FocusTB(textBox1);
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurrentListBox = (ListBox)Controls.Find(s, true)[0];
            textBox1.Text = (string)CurrentListBox.SelectedItem;
            FocusTB(textBox1);
        }
        private void SaveToFile(string FileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        ListBox CurListBox = (ListBox)Controls.Find("listBox" + i, true)[0];
                        sw.WriteLine(CurListBox.Items.Count.ToString());
                        for (int j = 0; j < CurListBox.Items.Count; j++)
                            sw.WriteLine(CurListBox.Items[j]);
                        CurListBox.Items.Clear();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении!");
            }
        }
        private void LoadFromFile(string FileName)
        {
            try
            {
                using (StreamReader sw = new StreamReader(FileName))
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        ListBox CurListBox = (ListBox)Controls.Find("listBox" + i, true)[0];
                        int cnt = Convert.ToInt32(sw.ReadLine());
                        for (int j = 0; j < cnt; j++)
                            CurListBox.Items.Add(sw.ReadLine());
                    }
                }
            }
            catch
            {
                //+ очищение
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FileName = dateTimePicker1.Text + ext;
            LoadFromFile(FileName);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SaveToFile(FileName);
            FileName = dateTimePicker1.Text + ext;
            LoadFromFile(FileName);
            FocusTB(textBox1);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            SaveToFile(FileName);
            Application.Exit();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
            int j = CurListBox.SelectedIndex;

            //bool flag = false;
            //for (int i = 0; i < CurListBox.Items.Count; ++i)
            //{
            //    if (CurListBox.Items[i].ToString() == textBox1.Text)
            //        flag = true;
            //}
            if (/*!flag &&*/ j != -1)
                CurListBox.Items[j] = textBox1.Text;
            FocusTB(textBox1);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
            int j = CurListBox.SelectedIndex;
            if (j != -1)
            {
                if (j == CurListBox.Items.Count - 1)
                    CurListBox.SelectedIndex--;
                else
                    CurListBox.SelectedIndex++;
                CurListBox.Items.RemoveAt(j);
                textBox1.Text = (string)CurListBox.SelectedItem;
            }
            FocusTB(textBox1);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
            CurListBox.Items.Clear();
            FocusTB(textBox1);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
            int j = CurListBox.SelectedIndex;
            if (j == -1)
                textBox1.Text = "";
            else
                textBox1.Text = CurListBox.Items[j].ToString();
            FocusTB(textBox1);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddButton.PerformClick();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString() + ' ' + DateTime.Now.ToString("HH:mm:ss"); ;
        }

        private void EmptyButton_Click(object sender, EventArgs e)
        {
            string s = "listBox" + (TabControl.SelectedIndex + 1);
            ListBox CurListBox = (ListBox)Controls.Find(s, true)[0];
            for (int i = CurListBox.Items.Count - 1; i >= 0; --i)
            {
                if (CurListBox.Items[i] == "")
                    CurListBox.Items.RemoveAt(i);
            }
        }
    }
}
