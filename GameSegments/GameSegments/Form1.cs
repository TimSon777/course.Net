using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using TCPClient;
using XProtocol;
using XProtocol.Serializator;

namespace GameSegments
{
    public partial class Form1 : Form
    {
        private GameSettings settings;
        private readonly XClient client;
        private static int size; 
        public Form1()
        {
            settings = new GameSettings(8);
            InitializeComponent();
            button2.Hide();
            client = new XClient();
           // client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("[::1]", 4910);
        }

        private static IEnumerable<Point> GenerateRandomPoints(int count, float maxXValue, float maxYValue)
        {
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                yield return new Point(rnd.NextDouble() * maxXValue, rnd.NextDouble() * maxYValue);
            }
        }
        
        private static void DrawPoints(IEnumerable<Point> points,Graphics graphics,Color color,float radius)
        {
            foreach (var point in points)
            {
                graphics.FillEllipse(new SolidBrush(color),
                    new RectangleF((float)point.X - radius / 2, (float)point.Y - radius / 2, radius, radius));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var handshake = XPacketConverter.Serialize(XPacketType.CreateLobby,client).ToPacket();
            client.QueuePacketSend(handshake);
            var graphics = pictureBox1.CreateGraphics();
            pictureBox1.Refresh();
            var generatedPoints = GenerateRandomPoints(settings.PointsCount, pictureBox1.Width,
                pictureBox1.Height);
            DrawPoints(generatedPoints,graphics,Color.Green, 5f);
            button1.Hide();
            button2.Show();
        }
        
        
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings(settings).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            button1.Show();
            button2.Hide();
        }
    }
}