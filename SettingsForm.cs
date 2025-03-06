using System;
using System.Windows.Forms;

namespace Приложение_Для_Концентрации
{
    public partial class SettingsForm : Form
    {
        public int WorkTime { get; private set; }
        public int BreakTime { get; private set; }

        public SettingsForm(int workTime, int breakTime)
        {
            InitializeComponent();
            WorkTime = workTime;
            BreakTime = breakTime;
            numWorkTime.Value = WorkTime / 60;
            numBreakTime.Value = BreakTime / 60;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WorkTime = (int)numWorkTime.Value * 60;
            BreakTime = (int)numBreakTime.Value * 60;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}