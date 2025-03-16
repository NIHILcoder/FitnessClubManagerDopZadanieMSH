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
            if (isEditMode)
            {
                LoadClientData();
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
                        txtEmail.Text
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
                        txtEmail.Text
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
    }
}