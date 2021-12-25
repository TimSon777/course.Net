using System;
using System.Windows.Forms;

namespace GameSegments
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            textBox1.Text = GameSettings.MaxPointsCount.ToString();
            textBox2.Text = GameSettings.MinPointsCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out var maxPoints) && int.TryParse(textBox2.Text, out var minPoints))
            {
                if(minPoints < 8)
                    MessageBox.Show("точек должно быть минимум 8");
                else if(minPoints > maxPoints)
                    MessageBox.Show("минимальное количество точек не должно быть больше максимального");
                else
                {
                    GameSettings.MaxPointsCount = maxPoints;
                    GameSettings.MinPointsCount = minPoints;   
                }
            }
            else
                MessageBox.Show("параметры должны быть числом");
        }
    }
}