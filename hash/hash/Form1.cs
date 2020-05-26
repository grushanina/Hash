using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hash
{
    public partial class Form1 : Form
    {
        public int n;
        public string path;
        public List<string>[] rows_public;

        public Form1()
        {
            InitializeComponent();
        }

        public int f(int number, int size)
        {
            return (number%size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            path = openFileDialog1.FileName;
            StreamReader sr1 = new StreamReader(path, Encoding.GetEncoding(1251));

            n = 0;
            while ((sr1.ReadLine()) != null)
                n++;


        


            textBox1.Text = n.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            textBox3.Clear();
            int x = 0;
            if (textBox2.Text == null || !Int32.TryParse(textBox2.Text, out x))
            {
                return;
            }



            List<string>[] rows = new List<string>[Int32.Parse(textBox2.Text)];
            for (int i = 0; i < rows.Length; i++)
                rows[i] = new List<string>();

            StreamReader sr2 = new StreamReader(path, Encoding.GetEncoding(1251));
            string line;
            while ((line = sr2.ReadLine()) != null)
            {
                string word = "";

                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] != ' ')
                    {
                        word += line[j];
                    }
                    else
                    {
                        break;
                    }

                }
                rows[f(Int32.Parse(word), rows.Length)].Add(line);
            }

            rows_public = rows;

            int[] counts = new int[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                
                chart1.Series["Series1"].Points.AddXY(i, rows[i].Count);
                counts[i] = rows[i].Count;
            }

            textBox3.Text += "Max = " + counts.Max() + "\r\n" + "Min = " + counts.Min() + "\r\n" + "Avg = " + counts.Average() + "\r\n";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            int x = 0;
            if (textBox4.Text == null || !Int32.TryParse(textBox4.Text, out x))
            {
                return;
            }

            string line;

            for (int i = 0; i < rows_public[f(Int32.Parse(textBox4.Text), rows_public.Length)].Count; i++)
            {
                if (rows_public[f(Int32.Parse(textBox4.Text), rows_public.Length)][i].Substring(0, 5) == textBox4.Text)
                {
                    textBox5.Text += rows_public[f(Int32.Parse(textBox4.Text), rows_public.Length)][i] + "\r\n";
                }
            }

            textBox5.Text += "В ячейке:" + "\r\n";

            for (int i = 0; i < rows_public[f(Int32.Parse(textBox4.Text), rows_public.Length)].Count; i++)
            {

                textBox5.Text += rows_public[f(Int32.Parse(textBox4.Text), rows_public.Length)][i] + "\r\n";
            }

            //textBox5.Text += rows_public.[f(Int32.Parse(textBox4.Text), rows_public.Length)];
        }
    }
}
