using System;
using System.Media;
using System.Windows.Forms;

namespace Приложение_Для_Концентрации
{
    public partial class Form1 : Form
    {
        private int workTime = 25 * 60; // 25 минут в секундах
        private int breakTime = 5 * 60; // 5 минут в секундах
        private int timeLeft;
        private bool isWorkTime = true;
        private int cyclesCompleted = 0;
        private bool isPaused = false;

        public Form1()
        {
            InitializeComponent();
            timeLeft = workTime;
            UpdateTimeLabel();
            UpdateCyclesLabel();
        }

        private void UpdateTimeLabel()
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;
            lblTime.Text = $"{minutes:00}:{seconds:00}";
        }

        private void UpdateCyclesLabel()
        {
            lblCycles.Text = $"Cycles: {cyclesCompleted}";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                timer1.Start();
                btnStart.Text = "Pause";
                isPaused = false;
            }
            else
            {
                timer1.Start();
                btnStart.Text = "Pause";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            isWorkTime = true;
            timeLeft = workTime;
            cyclesCompleted = 0;
            UpdateTimeLabel();
            UpdateCyclesLabel();
            btnStart.Text = "Start";
            isPaused = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                btnStart.Text = "Resume";
                isPaused = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                UpdateTimeLabel();
            }
            else
            {
                timer1.Stop();
                if (isWorkTime)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Время работы закончилось! Начните перерыв.");
                    isWorkTime = false;
                    timeLeft = breakTime;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Перерыв закончился! Начните работу.");
                    isWorkTime = true;
                    timeLeft = workTime;
                    cyclesCompleted++;
                    UpdateCyclesLabel();
                }
                UpdateTimeLabel();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(workTime, breakTime))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    workTime = settingsForm.WorkTime;
                    breakTime = settingsForm.BreakTime;
                    timeLeft = isWorkTime ? workTime : breakTime;
                    UpdateTimeLabel();
                }
            }
        }
    }
}