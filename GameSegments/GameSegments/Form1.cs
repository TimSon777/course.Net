using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XClient;
using XProtocol;
using XProtocol.Serializator;

namespace GameSegments
{
    public partial class Form1 : Form
    {
        private GameSettings settings;
        private readonly Client client;
        private static int size; 
        public Form1()
        {
            settings = new GameSettings(8);
            InitializeComponent();
            button2.Hide();
            client = new Client();
            client.OnPacketRecieve += OnPacketRecieve;
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
            var packet = new XPacketCreateLobby {HostUser = client };
            var handshake = XPacketConverter.Serialize(XPacketType.CreateLobby,packet).ToPacket();
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
        
        private static void OnPacketRecieve(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private static void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.LobbyCreated:
                    ProcessCreatedLobby(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessCreatedLobby(XPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<XPacketLobbyCreated>(packet);
        }
    }
}