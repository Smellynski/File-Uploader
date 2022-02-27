using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int an_wen = 0;
        public string Server = "";
        public int port = 22;
        public string filepath = "";
        public string remoteFilePath = "";
        public string an_wen_file = "";
        public string ordnungs_file = @"";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = openFileDialog.FileName;
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            using (ScpClient client = new ScpClient(Server, "root", "YcouEhcylr"))
            {
                client.Connect();
                if (!File.Exists(ordnungs_file))
                {
                    string text_an_wen = Convert.ToString(an_wen);
                    using (StreamWriter sw = File.CreateText(ordnungs_file))
                    {
                        sw.WriteLine(text_an_wen);
                    }
                }
                else
                {
                    string text_an_wen = Convert.ToString(an_wen);
                    string fileName = "MyTest.txt";
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes(text_an_wen);
                    }
                }
                using (Stream localFile = File.OpenRead(filepath))
                {
                    client.Upload(localFile, remoteFilePath);
                }
                using(Stream mitarbeiterfile = File.OpenRead(ordnungs_file))
                {
                    client.Upload(mitarbeiterfile, an_wen_file);
                }
                Environment.Exit(0);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                CheckedBox_Funktion(2, 3, 1, 1);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                CheckedBox_Funktion(1, 3, 2, 2);
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                CheckedBox_Funktion(1, 2, 3, 3);
            }
        }


        public void CheckedBox_Funktion(int wert1, int wert2, int wert3, int wert4)
        {
            // Vermeiden von Fehlern
            if (an_wen == wert1 || an_wen == wert2)
            {
                MessageBox.Show("Sie haben schon ein anderes Feld angekreuzt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                an_wen = 0;
            }
            else
            {
                an_wen = wert3;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Connect1(Server, port);
        }
        public static void Connect1(string host, int port)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(host);

            Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            Console.WriteLine("Establishing Connection to {0}",
                host);
            s.Connect(IPs[0], port);
            MessageBox.Show("Connection established");
        }


    }

}
