using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ClientForm : Form
    {
        private int clientId = 0;
        private bool isEditMode = false;

        public ClientForm()
        {
            InitializeComponent();
            this.Text = "Добавление клиента";
        }

        public ClientForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.isEditMode = true;
            this.Text = "Редактирование клиента";
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            // Заполняем выпадающий список уровней активности
            comboActivityLevel.Items.Add("Низкий");
            comboActivityLevel.Items.Add("Средний");
            comboActivityLevel.Items.Add("Высокий");
            comboActivityLevel.Items.Add("Профессиональный");
            comboActivityLevel.SelectedIndex = 1; // По умолчанию "Средний"

            if (isEditMode)
            {
                LoadClientData();
                // Добавим кнопку для просмотра истории клиента
                btnViewHistory.Visible = true;
            }
            else
            {
                btnViewHistory.Visible = false;
            }
        }

        private void LoadClientData()
        {
            try
            {
                var client = DatabaseManager.GetClient(clientId);

                if (client != null)
                {
                    txtLastName.Text = client.LastName;
                    txtFirstName.Text = client.FirstName;
                    txtMiddleName.Text = client.MiddleName;

                    if (client.BirthDate.HasValue)
                    {
                        dateTimeBirthDate.Value = client.BirthDate.Value;
                        dateTimeBirthDate.Checked = true;
                    }
                    else
                    {
                        dateTimeBirthDate.Checked = false;
                    }

                    txtPhone.Text = client.Phone;
                    txtEmail.Text = client.Email;

                    // Заполняем новые поля
                    rtbNotes.Text = client.Notes;

                    // Устанавливаем уровень активности
                    if (!string.IsNullOrEmpty(client.ActivityLevel))
                    {
                        for (int i = 0; i < comboActivityLevel.Items.Count; i++)
                        {
                            if (comboActivityLevel.Items[i].ToString() == client.ActivityLevel)
                            {
                                comboActivityLevel.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных клиента: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка заполнения обязательных полей
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию клиента!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя клиента!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                bool result;

                // Получаем данные из формы
                DateTime? birthDate = dateTimeBirthDate.Checked ? dateTimeBirthDate.Value : (DateTime?)null;
                string activityLevel = comboActivityLevel.SelectedItem.ToString();

                if (isEditMode)
                {
                    // Обновление существующего клиента
                    result = DatabaseManager.UpdateClient(
                        clientId,
                        txtLastName.Text,
                        txtFirstName.Text,
                        txtMiddleName.Text,
                        birthDate,
                        txtPhone.Text,
                        txtEmail.Text,
                        rtbNotes.Text,
                        activityLevel
                    );
                }
                else
                {
                    // Добавление нового клиента
                    int newClientId = DatabaseManager.AddClient(
                        txtLastName.Text,
                        txtFirstName.Text,
                        txtMiddleName.Text,
                        birthDate,
                        txtPhone.Text,
                        txtEmail.Text,
                        rtbNotes.Text,
                        activityLevel
                    );

                    result = (newClientId > 0);
                }

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить данные клиента!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных клиента: {ex.Message}",
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

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем историю изменений клиента
                var historyData = DatabaseManager.GetClientHistory(clientId);

                // Открываем форму с историей
                using (var form = new ClientHistoryForm(clientId, $"{txtLastName.Text} {txtFirstName.Text}", historyData))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории клиента: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}