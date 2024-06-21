//Imports
using System.Net.NetworkInformation;

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
            MessageBox.Show("Button 2 Clicked");
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
    }
}
