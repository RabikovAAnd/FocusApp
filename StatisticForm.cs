using System;
using System.Data;
using System.Windows.Forms;

namespace Приложение_Для_Концентрации
{
    public partial class StatisticsForm : Form
    {
        private DataTable statisticsTable;

        public StatisticsForm(DataTable table)
        {
            InitializeComponent();
            statisticsTable = table;
            dataGridView1.DataSource = statisticsTable;
        }
    }
}