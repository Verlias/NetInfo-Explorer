using System;
using System.Net;
using System.Net.Sockets;

namespace NetInfo_Explorer
    //this is a comment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text;
            MessageBox.Show("Submit Button: " + text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Local_IP = GetLocalIPAddress();
            MessageBox.Show("Your local network IP address: \n" + Local_IP);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 3 Clicked");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 4 Clicked");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button 5 Clicked");
        }


        //Network Methods

        public static string GetLocalIPAddress()
        {
            //DNS -- Directive method; AddressFamily&List -- Directive Method 
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
