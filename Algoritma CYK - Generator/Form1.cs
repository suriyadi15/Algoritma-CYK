using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algoritma_CYK___Generator
{
    public partial class Form1 : Form
    {
        ListView dataListview;
        TextBox dataJalan;
        string value;
        public Form1()
        {
            InitializeComponent();
            value = "";
            textBoxAturan.Text = @"S->AB|BC
A->BA|a
B->CC|b
C->AB|a";
            dataListview = new ListView();
            dataJalan = new TextBox();


            textBoxString.Text = "aabab";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBoxAturan.Enabled = textBoxString.Enabled = button1.Enabled = button2.Enabled = false;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = textBoxString.Text.Length - 1;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataListview.Items.Clear();
            dataListview.Columns.Clear();
            dataJalan.Text = "";

            progressBar1.Value = 0;

            labelJalan.Text = "";
            listViewTabel.Items.Clear();
            listViewTabel.Columns.Clear();

            textBoxAturan.Enabled = textBoxString.Enabled = button1.Enabled = true;
        }

        public void proses()
        {
            foreach (char i in textBoxString.Text)
                dataListview.Columns.Add(i.ToString());

            char[] inputString = textBoxString.Text.ToCharArray();

            string[] tmpAturan = textBoxAturan.Text.Split('\n');
            for (int i = 0; i < tmpAturan.Length; i++)
            {
                tmpAturan[i] = tmpAturan[i].Trim();
            }
            ArrayList aturan = new ArrayList();
            for (int i = 0; i < tmpAturan.Length; i++)
            {
                List<string> tmp = new List<string>();
                tmp.Add(tmpAturan[i][0].ToString());
                string[] tmpStr = tmpAturan[i].Substring(3).Split('|');
                foreach (string j in tmpStr)
                    tmp.Add(j);
                aturan.Add(tmp);
            }

            /////////////Listview baris pertama
            for (int i = 0; i < textBoxString.Text.Length; i++)
            {
                string nonTerminal = "";
                foreach (List<string> j in aturan)
                {

                    for (int k = 1; k < j.Count; k++)
                    {
                        if (j[k] == inputString[i].ToString())
                        {
                            nonTerminal += j[0];
                            break;
                        }
                    }
                }
                if (i == 0)
                {
                    if (nonTerminal == "")
                        dataListview.Items.Add("ε");
                    else
                        dataListview.Items.Add(nonTerminal);
                }
                else
                {
                    if (nonTerminal == "")
                        dataListview.Items[dataListview.Items.Count - 1].SubItems.Add("ε");
                    else
                        dataListview.Items[dataListview.Items.Count - 1].SubItems.Add(nonTerminal);
                }
            }

            for (int i = 1; i < inputString.Length; i++) //Isi ecek" nilai di listview
            {
                dataListview.Items.Add("");
                for (int j = 1; j < inputString.Length - i; j++)
                    dataListview.Items[i].SubItems.Add("");
            }

            ////Algoritma CYK
            for (int j = 1; j < inputString.Length; j++)
            {
                backgroundWorker1.ReportProgress(j);

                dataJalan.Text += string.Format("Untuk j = {0}\r\n", j + 1); //jalan
                for (int i = 0; i < inputString.Length - 1 - j + 1; i++)
                {
                    dataJalan.Text += string.Format("\tUntuk i = {0}\r\n", i + 1); //jalan
                    dataListview.Items[j].SubItems[i].Text = "ε";
                    string hasilNonTerminal = "";
                    for (int k = 0; k < j; k++)
                    {
                        dataJalan.Text += string.Format("\t\tk = {0}, i = {1}, j = {2}\r\n", k + 1,i+1,j+1); //jalan
                        string Vik = dataListview.Items[k].SubItems[i].Text;
                        string VikJK = dataListview.Items[j - k - 1].SubItems[i + k + 1].Text;

                        //dataJalan.Text += string.Format("\t\t\tV{0}{1} = V{2}{3} ⋃ V{4}{5} ", i + 1, j + 1, i + 1, k + 1, i + 1 + k + 1, j + 1 - k - 1); //jalan
                        char[] Ci1 = (i + 1).ToString().ToArray();
                        string Si1 = "";
                        foreach(char c in Ci1)
                            Si1+=(char)int.Parse(string.Format("208{0}",int.Parse(c.ToString())), System.Globalization.NumberStyles.HexNumber);
                        //int i1 = int.Parse(string.Format("208{0}", i + 1), System.Globalization.NumberStyles.HexNumber);

                        char[] Cj1 = (j + 1).ToString().ToArray();
                        string Sj1 = "";
                        foreach (char c in Cj1)
                            Sj1 += (char)int.Parse(string.Format("208{0}", int.Parse(c.ToString())), System.Globalization.NumberStyles.HexNumber);

                        char[] Ck1 = (k + 1).ToString().ToArray();
                        string Sk1 = "";
                        foreach (char c in Ck1)
                            Sk1 += (char)int.Parse(string.Format("208{0}", int.Parse(c.ToString())), System.Globalization.NumberStyles.HexNumber);

                        char[] Ci1k1 = (i + 1 + k + 1).ToString().ToArray();
                        string Si1k1 = "";
                        foreach (char c in Ci1k1)
                            Si1k1 += (char)int.Parse(string.Format("208{0}", int.Parse(c.ToString())), System.Globalization.NumberStyles.HexNumber);

                        char[] Cj1k1 = (j + 1 - k - 1).ToString().ToArray();
                        string Sj1k1 = "";
                        foreach (char c in Cj1k1)
                            Sj1k1 += (char)int.Parse(string.Format("208{0}", int.Parse(c.ToString())), System.Globalization.NumberStyles.HexNumber);
                        //int j1 = int.Parse(string.Format("208{0}", j + 1), System.Globalization.NumberStyles.HexNumber);
                        //int k1 = int.Parse(string.Format("208{0}", k + 1), System.Globalization.NumberStyles.HexNumber);
                        //int i1k1 = int.Parse(string.Format("208{0}", i + 1 + k + 1), System.Globalization.NumberStyles.HexNumber);
                        //int j1k1=int.Parse(string.Format("208{0}", j + 1 - k - 1), System.Globalization.NumberStyles.HexNumber);

                        //dataJalan.Text += string.Format("\t\t\tV{0}{1} = Vi,k ⋃ Vi+k,j-k = V{2}{3} ⋃ V{4}{5} ", (char)i1, (char)j1, (char)i1, (char)k1, (char)i1k1, (char)j1k1); //jalan
                        dataJalan.Text += string.Format("\t\t\tV{0},{1}\t = Vi,k ⋃ Vi+k,j-k\r\n\t\t\t\t = V{2},{3} ⋃ V{4},{5}\r\n\t\t\t\t ", Si1, Sj1, Si1, Sk1, Si1k1, Sj1k1); //jalan
                        dataJalan.Text += string.Format("= {0} ⋃ {1}\r\n\t\t\t\t = ", Vik, VikJK); //jalan

                        List<string> Union = new List<string>();

                        if (Vik == "ε" && VikJK != "ε")
                        {
                            foreach (char v in VikJK)
                            {
                                Union.Add(v.ToString());
                                dataJalan.Text += string.Format(" {0} ", v.ToString());
                            }
                        }
                        else if (VikJK == "ε" && Vik != "ε")
                        {
                            foreach(char v in Vik)
                            {
                                Union.Add(v.ToString());
                                dataJalan.Text += string.Format(" {0} ", v.ToString());
                            }
                        }
                        else if (Vik != "ε" && VikJK != "ε")
                        {
                            foreach (char ik in Vik)
                                foreach (char ikJK in VikJK)
                                {
                                    Union.Add(String.Concat(ik, ikJK));
                                    dataJalan.Text += string.Format(" {0} ", String.Concat(ik, ikJK));
                                }
                        }
                        dataJalan.Text += "\r\n";
                        if (Vik != "ε" || VikJK != "ε")
                        {
                            foreach (List<string> a in aturan) //Cari Hasil dari Unnion
                            {
                                for (int non = 1; non < a.Count; non++)
                                {
                                    foreach (string u in Union)
                                    {
                                        if (u == a[non])
                                        {
                                            if (hasilNonTerminal.IndexOf(char.Parse(a[0])) == -1)
                                                hasilNonTerminal += a[0];
                                            break;
                                        }
                                    }
                                }
                            }////////////////////End
                        }
                    }
                    if (hasilNonTerminal == "")
                        dataListview.Items[j].SubItems[i].Text = "ε";
                    else
                        dataListview.Items[j].SubItems[i].Text = hasilNonTerminal;
                }
                dataJalan.Text += "\r\n\r\n";
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            listViewTabel.Width = this.Width - 323;
            labelJalan.Width = this.Width - 323;
            labelJalan.Height = this.Height - 239;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            proses();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            foreach (ColumnHeader i in dataListview.Columns)
                listViewTabel.Columns.Add(i.Text);

            for (int i=0;i<dataListview.Items.Count;i++)
            {
                listViewTabel.Items.Add(dataListview.Items[i].Text);
                for (int j = 1; j < dataListview.Items[i].SubItems.Count; j++)
                    listViewTabel.Items[i].SubItems.Add(dataListview.Items[i].SubItems[j].Text);
            }

            labelJalan.Text = dataJalan.Text;

            if (dataListview.Items[dataListview.Items.Count - 1].Text.IndexOf(textBoxAturan.Text[0]) != -1)
                MessageBox.Show(this, "String DITERIMA...!!!", "Status", MessageBoxButtons.OK);
            else
                MessageBox.Show(this, "String Tidak DITERIMA...!!!", "Status", MessageBoxButtons.OK);
        }
    }
}
