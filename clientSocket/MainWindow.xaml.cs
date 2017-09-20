using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace clientSocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static TcpClient client = new TcpClient();
        byte[] input = new byte[65536];
        NetworkStream stream;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender , RoutedEventArgs e)
        {
            box.Focus();
            BeginConnection();
        }

        public void BeginConnection()
        {
            try
            {
                //client.Connect("192.168.0.44" , 8888);
                client.Connect("82.7.141.89" , 8888);
                stream = client.GetStream();
                block.Text += ">>Connected to server!" + Environment.NewLine;
            }catch(Exception ex)
            {
                MessageBox.Show("Could not connect to server!");
                Close();
            }
        }

        private void Btn_Click(object sender , RoutedEventArgs e)
        {
            byte[] output = Encoding.ASCII.GetBytes(box.Text + "$");
            stream.Write(output , 0 , box.Text.Length + 1);
            stream.Flush();

            stream.Read(input,0,(int)client.ReceiveBufferSize);
            string response = Encoding.Default.GetString(input);
            block.Text += "You: " + box.Text + "\t" + response.Substring(0,response.LastIndexOf("$")) + Environment.NewLine;
            box.Text = "";
            box.Focus();
        }
    }
}
