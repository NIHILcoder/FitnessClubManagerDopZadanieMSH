using System;
using System.Data;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class MembershipForm : Form
    {
        private int selectedClientId = 0;

        public MembershipForm()
        {
            InitializeComponent();
        }

        // Конструктор с параметром ID клиента
        public MembershipForm(int clientId)
        {
            InitializeComponent();
            selectedClientId = clientId;
        }

        private void MembershipForm_Load(object sender, EventArgs e)
        {
            LoadMembershipTypes();
            dateTimeStartDate.Value = DateTime.Today;

            // Если ID клиента задан, предвыбираем клиента и делаем поле недоступным
            if (selectedClientId > 0)
            {
                LoadClientWithSelection();
            }
            else
            {
                LoadClients();
            }
        }

        // Загрузка всех клиентов для выбора
        private void LoadClients()
        {
            try
            {
                var clients = DatabaseManager.GetClients();

                comboClient.DisplayMember = "FullName";
                comboClient.ValueMember = "ClientID";

                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("ClientID", typeof(int));
                displayTable.Columns.Add("FullName", typeof(string));

                foreach (DataRow row in clients.Rows)
                {
                    DataRow newRow = displayTable.NewRow();
                    newRow["ClientID"] = row["ClientID"];
                    newRow["FullName"] = $"{row["LastName"]} {row["FirstName"]} {row["MiddleName"]}".Trim();
                    displayTable.Rows.Add(newRow);
                }

                comboClient.DataSource = displayTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка клиентов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для загрузки конкретного клиента
        private void LoadClientWithSelection()
        {
            try
            {
                // Получаем данные о клиенте
                var client = DatabaseManager.GetClient(selectedClientId);
                if (client == null)
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Создаем таблицу с одним клиентом
                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("ClientID", typeof(int));
                displayTable.Columns.Add("FullName", typeof(string));

                DataRow newRow = displayTable.NewRow();
                newRow["ClientID"] = selectedClientId;
                newRow["FullName"] = $"{client.LastName} {client.FirstName} {(client.MiddleName != null ? client.MiddleName : "")}".Trim();
                displayTable.Rows.Add(newRow);

                comboClient.DisplayMember = "FullName";
                comboClient.ValueMember = "ClientID";
                comboClient.DataSource = displayTable;
                comboClient.Enabled = false; // Делаем поле недоступным
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных клиента: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadMembershipTypes()
        {
            try
            {
                var types = DatabaseManager.GetMembershipTypes();

                comboType.DisplayMember = "DisplayText";
                comboType.ValueMember = "TypeID";

                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("TypeID", typeof(int));
                displayTable.Columns.Add("DisplayText", typeof(string));
                displayTable.Columns.Add("DurationDays", typeof(int));
                displayTable.Columns.Add("Price", typeof(decimal));

                foreach (DataRow row in types.Rows)
                {
                    DataRow newRow = displayTable.NewRow();
                    newRow["TypeID"] = row["TypeID"];
                    newRow["DisplayText"] = $"{row["TypeName"]} ({row["DurationDays"]} дней, {row["Price"]} руб.)";
                    newRow["DurationDays"] = row["DurationDays"];
                    newRow["Price"] = row["Price"];
                    displayTable.Rows.Add(newRow);
                }

                comboType.DataSource = displayTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов абонементов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboType.SelectedItem != null)
            {
                // Получаем выбранный тип абонемента
                DataRowView selectedRow = (DataRowView)comboType.SelectedItem;
                int durationDays = Convert.ToInt32(selectedRow["DurationDays"]);
                decimal price = Convert.ToDecimal(selectedRow["Price"]);

                // Вычисляем и отображаем дату окончания абонемента
                DateTime startDate = dateTimeStartDate.Value;
                DateTime endDate = startDate.AddDays(durationDays);

                labelEndDate.Text = $"Дата окончания: {endDate.ToShortDateString()}";
                labelPrice.Text = $"Стоимость: {price:C}";
            }
        }

        private void dateTimeStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Обновляем отображение даты окончания при изменении даты начала
            if (comboType.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboType.SelectedItem;
                int durationDays = Convert.ToInt32(selectedRow["DurationDays"]);

                DateTime startDate = dateTimeStartDate.Value;
                DateTime endDate = startDate.AddDays(durationDays);

                labelEndDate.Text = $"Дата окончания: {endDate.ToShortDateString()}";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка на выбор клиента
            if (comboClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboClient.Focus();
                return;
            }

            // Проверка на выбор типа абонемента
            if (comboType.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип абонемента!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboType.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                int clientId = Convert.ToInt32(comboClient.SelectedValue);
                int typeId = Convert.ToInt32(comboType.SelectedValue);
                DateTime startDate = dateTimeStartDate.Value;

                int membershipId = DatabaseManager.AddMembership(clientId, typeId, startDate);

                if (membershipId > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить абонемент!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении абонемента: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}