using System;
using System.Data;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ClientMembershipsForm : Form
    {
        private int clientId;
        private string clientName;

        public ClientMembershipsForm(int clientId, string clientName)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.clientName = clientName;
        }

        private void ClientMembershipsForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Абонементы клиента: {clientName}";
            lblClientName.Text = clientName;
            LoadMemberships();
        }

        private void LoadMemberships()
        {
            try
            {
                // Явно очищаем DataSource перед загрузкой новых данных
                dataGridMemberships.DataSource = null;

                // Загружаем абонементы клиента
                var memberships = DatabaseManager.GetClientMemberships(clientId);
                dataGridMemberships.DataSource = memberships;

                // Настраиваем отображение столбцов
                if (dataGridMemberships.Columns.Count > 0)
                {
                    dataGridMemberships.Columns["MembershipID"].HeaderText = "ID";
                    dataGridMemberships.Columns["MembershipID"].Width = 50;

                    dataGridMemberships.Columns["TypeID"].Visible = false;
                    dataGridMemberships.Columns["TypeName"].HeaderText = "Тип абонемента";
                    dataGridMemberships.Columns["StartDate"].HeaderText = "Дата начала";
                    dataGridMemberships.Columns["EndDate"].HeaderText = "Дата окончания";
                    dataGridMemberships.Columns["IssueDate"].HeaderText = "Дата выдачи";
                    dataGridMemberships.Columns["IsActive"].HeaderText = "Активен";
                    dataGridMemberships.Columns["Price"].HeaderText = "Стоимость";

                    // Добавляем форматирование для столбца с ценой
                    if (dataGridMemberships.Columns.Contains("Price"))
                    {
                        dataGridMemberships.Columns["Price"].DefaultCellStyle.Format = "N2";
                    }

                    // Добавляем форматирование для столбца IsActive (для лучшего отображения)
                    if (dataGridMemberships.Columns.Contains("IsActive"))
                    {
                        dataGridMemberships.Columns["IsActive"].DefaultCellStyle.NullValue = "Нет";
                    }
                }

                // Обновляем статистику
                UpdateStatistics(memberships);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке абонементов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics(DataTable memberships)
        {
            int activeCount = 0;
            int expiredCount = 0;

            foreach (DataRow row in memberships.Rows)
            {
                bool isActive = Convert.ToBoolean(row["IsActive"]);
                if (isActive)
                    activeCount++;
                else
                    expiredCount++;
            }
        }

        private void btnAddMembership_Click(object sender, EventArgs e)
        {
            // Передаем ID клиента в форму добавления абонемента
            using (var form = new MembershipForm(clientId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMemberships(); // Перезагружаем список абонементов
                    MessageBox.Show("Абонемент успешно добавлен!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}