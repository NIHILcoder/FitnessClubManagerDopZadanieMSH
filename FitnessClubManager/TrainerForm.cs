using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class TrainerForm : Form
    {
        private int trainerId = 0;
        private bool isEditMode = false;

        public TrainerForm()
        {
            InitializeComponent();
            this.Text = "Добавление тренера";
        }

        public TrainerForm(int trainerId)
        {
            InitializeComponent();
            this.trainerId = trainerId;
            this.isEditMode = true;
            this.Text = "Редактирование данных тренера";
        }

        private void TrainerForm_Load(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                LoadTrainerData();
            }
        }

        private void LoadTrainerData()
        {
            try
            {
                var trainer = DatabaseManager.GetTrainer(trainerId);

                if (trainer != null)
                {
                    txtLastName.Text = trainer.LastName;
                    txtFirstName.Text = trainer.FirstName;
                    txtMiddleName.Text = trainer.MiddleName;
                    txtSpecialization.Text = trainer.Specialization;

                    if (trainer.Experience.HasValue)
                    {
                        numExperience.Value = trainer.Experience.Value;
                        numExperience.Enabled = true;
                    }

                    txtPhone.Text = trainer.Phone;
                    txtEmail.Text = trainer.Email;
                }
                else
                {
                    MessageBox.Show("Тренер не найден!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных тренера: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка заполнения обязательных полей
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию тренера!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя тренера!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Введите телефон тренера!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                bool result = true;

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить данные тренера!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных тренера: {ex.Message}",
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