using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NetInfo_Explorer
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
            string text = textBox1.Text;
            MessageBox.Show("Submit Button: " + text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string localIP = GetLocalIPAddress();
            MessageBox.Show("Your local network IP address: \n" + localIP);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList devices = DiscoverDevices();
            DisplayDiscoveredDevices(devices);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Example IP address to ping (you can change this to any IP address)
            string ipAddress = GetLocalIPAddress();

            // Get current ping to the IP address
            long pingTime = GetPingTime(ipAddress);

            // Display the ping time in milliseconds
            MessageBox.Show($"Ping to {ipAddress}: {pingTime} ms");
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string networkInfo = GetNetworkInformation();
            MessageBox.Show(networkInfo, "Network Interfaces Information");
        }


        // Method to discover devices using ARP scan
        private ArrayList DiscoverDevices()
        {
            ArrayList devices = new ArrayList();

            // Get the local IP address
            string localIpAddress = GetLocalIPAddress();

            // Get the local network prefix (e.g., "192.168.1.")
            string[] ipParts = localIpAddress.Split('.');
            string prefix = ipParts[0] + "." + ipParts[1] + "." + ipParts[2] + ".";

            // Perform ARP scan
            for (int i = 1; i < 255; i++)
            {
                string target = prefix + i.ToString();
                Process p = new Process();
                p.StartInfo.FileName = "arp";
                p.StartInfo.Arguments = "-a " + target;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                string output = p.StandardOutput.ReadToEnd();
                if (output.Contains(target))
                {
                    // Parse MAC address from ARP command output
                    int index = output.IndexOf(target) + target.Length + 5;
                    string macAddress = output.Substring(index, 17);

                    // Get hostname from IP address
                    string hostname = GetHostName(target);

                    // Add device info to the list
                    devices.Add($"IP Address: {target} | MAC Address: {macAddress} | Hostname: {hostname}");
                }
            }

            return devices;
        }

        // Method to get local IP address
        private string GetLocalIPAddress()
        {
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

        // Method to get hostname from IP address
        private string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(ipAddress);
                return host.HostName;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        // Method to display discovered devices
        private void DisplayDiscoveredDevices(ArrayList devices)
        {
            // Show devices in a message box or any other UI component
            if (devices.Count > 0)
            {
                string message = "Discovered Devices:\n";
                foreach (string device in devices)
                {
                    message += device + "\n";
                }
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("No devices discovered.");
            }
        }

        private long GetPingTime(string ipAddress)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = ping.Send(ipAddress);
                    if (reply != null)
                    {
                        return reply.RoundtripTime;
                    }
                    else
                    {
                        return -1; // Indicate failure
                    }
                }
                catch (PingException)
                {
                    return -1; // Indicate failure
                }
            }
        }

        private string GetNetworkInformation()
        {
            string info = "";

            // Get network interfaces
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface ni in interfaces)
            {
                // Display information about each interface
                info += $"Name: {ni.Name}\n";
                info += $"Description: {ni.Description}\n";
                info += $"Type: {ni.NetworkInterfaceType}\n";
                info += $"Operational Status: {ni.OperationalStatus}\n";

                // Get IP properties for the interface
                IPInterfaceProperties ipProps = ni.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
                {
                    info += $"  IP Address: {ip.Address}\n";
                    info += $"  Subnet Mask: {ip.IPv4Mask}\n";
                }

                info += "\n";
            }

            return info;
        }


    }
}
