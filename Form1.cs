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
        private DataTable statisticsTable;

        public Form1()
        {
            InitializeComponent();
            timeLeft = workTime;
            UpdateTimeLabel();
            UpdateCyclesLabel();
            InitializeStatistics();
            LoadSettings();
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
                    SaveStatistics();
                }
                UpdateTimeLabel();
            }
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
                    SaveSettings();
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

        private void LoadSettings()
        {
            if (File.Exists("settings.txt"))
            {
                string[] lines = File.ReadAllLines("settings.txt");
                workTime = int.Parse(lines[0]);
                breakTime = int.Parse(lines[1]);
                timeLeft = isWorkTime ? workTime : breakTime;
                UpdateTimeLabel();
            }
        }

        private void SaveSettings()
        {
            File.WriteAllText("settings.txt", $"{workTime}\n{breakTime}");
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            if (this.BackColor == SystemColors.Control)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                btnTheme.Text = "Light Theme";
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                btnTheme.Text = "Dark Theme";
            }
        }
    }
}