using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Приложение_Для_Концентрации;

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
        private NotifyIcon notifyIcon;
        private DataTable statisticsTable;

        public Form1()
        {
            InitializeComponent();
            timeLeft = workTime;
            UpdateTimeLabel();
            UpdateCyclesLabel();
            InitializeNotifyIcon();
            InitializeStatistics();
            btnPause.Visible = false; // Скрываем кнопку Pause изначально
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = false
            };
        }

        private void InitializeStatistics()
        {
            statisticsTable = new DataTable();
            statisticsTable.Columns.Add("Date", typeof(string));
            statisticsTable.Columns.Add("Cycles", typeof(int));
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
            timer1.Start();
            btnStart.Visible = false; // Скрываем кнопку Start
            btnPause.Visible = true; // Показываем кнопку Pause
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnPause.Visible = false; // Скрываем кнопку Pause
            btnStart.Visible = true; // Показываем кнопку Start
            isPaused = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            isWorkTime = true;
            timeLeft = workTime;
            cyclesCompleted = 0;
            UpdateTimeLabel();
            UpdateCyclesLabel();
            btnStart.Visible = true; // Показываем кнопку Start
            btnPause.Visible = false; // Скрываем кнопку Pause
            isPaused = false;
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
                    ShowNotification("Work Time Ended", "Time for a break!");
                    SystemSounds.Beep.Play();
                    isWorkTime = false;
                    timeLeft = breakTime;
                }
                else
                {
                    ShowNotification("Break Time Ended", "Time to work!");
                    SystemSounds.Beep.Play();
                    isWorkTime = true;
                    timeLeft = workTime;
                    cyclesCompleted++;
                    UpdateCyclesLabel();
                    SaveStatistics();
                }
                UpdateTimeLabel();
                btnStart.Visible = true; // Показываем кнопку Start
                btnPause.Visible = false; // Скрываем кнопку Pause
            }
        }

        private void ShowNotification(string title, string message)
        {
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(3000, title, message, ToolTipIcon.Info);
        }

        private void SaveStatistics()
        {
            string today = DateTime.Now.ToShortDateString();
            DataRow[] rows = statisticsTable.Select($"Date = '{today}'");
            if (rows.Length > 0)
            {
                rows[0]["Cycles"] = (int)rows[0]["Cycles"] + 1;
            }
            else
            {
                statisticsTable.Rows.Add(today, 1);
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

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            using (var statisticsForm = new StatisticsForm(statisticsTable))
            {
                statisticsForm.ShowDialog();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportStatistics(saveFileDialog.FileName);
            }
        }

        private void ExportStatistics(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Date,Cycles");
                foreach (DataRow row in statisticsTable.Rows)
                {
                    writer.WriteLine($"{row["Date"]},{row["Cycles"]}");
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Dispose(); // Освобождаем ресурсы NotifyIcon
        }
    }
}