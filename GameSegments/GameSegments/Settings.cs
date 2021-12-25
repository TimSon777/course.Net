using System;
using System.Windows.Forms;

namespace GameSegments
{
    public partial class Settings : Form
    {
        private readonly GameSettings settings;
        public Settings(GameSettings settings)
        {
            this.settings = settings;
            InitializeComponent();
            textBox1.Text = settings.PointsCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out var points))
            {
                if(points < 8)
                    MessageBox.Show("точек должно быть минимум 8");
                else
                {
                    settings.PointsCount = points;
                }
            }
            else
                MessageBox.Show("параметр должен быть числом");
        }
    }
}