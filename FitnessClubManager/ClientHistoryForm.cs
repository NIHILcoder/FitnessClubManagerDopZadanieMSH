using System;
using System.Data;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ClientHistoryForm : Form
    {
        private int clientId;
        private string clientName;
        private DataTable historyData;

        public ClientHistoryForm(int clientId, string clientName, DataTable historyData)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.clientName = clientName;
            this.historyData = historyData;
        }

        private void ClientHistoryForm_Load(object sender, EventArgs e)
        {
            this.Text = $"История изменений клиента: {clientName}";
            lblClientName.Text = clientName;

            // Настраиваем DataGridView
            dataGridHistory.DataSource = historyData;
            ConfigureDataGridView();

            // Настраиваем фильтрацию по дате
            dateTimeFrom.Value = DateTime.Today.AddMonths(-1);
            dateTimeTo.Value = DateTime.Today;
        }

        private void ConfigureDataGridView()
        {
            if (dataGridHistory.Columns.Count > 0)
            {
                dataGridHistory.Columns["ChangeDate"].HeaderText = "Дата изменения";
                dataGridHistory.Columns["ChangeDate"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridHistory.Columns["ChangeDescription"].HeaderText = "Описание";
                dataGridHistory.Columns["UserName"].HeaderText = "Пользователь";

                dataGridHistory.Columns["ChangeDate"].Width = 150;
                dataGridHistory.Columns["ChangeDescription"].Width = 300;
                dataGridHistory.Columns["UserName"].Width = 100;
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Фильтрация по дате
                string filter = $"ChangeDate >= '{dateTimeFrom.Value.ToString("yyyy-MM-dd")}' AND ChangeDate <= '{dateTimeTo.Value.ToString("yyyy-MM-dd 23:59:59")}'";

                if (historyData.DefaultView.RowFilter != filter)
                {
                    historyData.DefaultView.RowFilter = filter;
                    dataGridHistory.DataSource = historyData.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            historyData.DefaultView.RowFilter = "";
            dataGridHistory.DataSource = historyData.DefaultView;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}