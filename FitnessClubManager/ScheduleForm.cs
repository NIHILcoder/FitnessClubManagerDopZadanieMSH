using System;
using System.Data;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ScheduleForm : Form
    {
        private DateTime classDate;
        private int scheduleId = 0;
        private bool isEditMode = false;

        public ScheduleForm(DateTime date)
        {
            InitializeComponent();
            this.classDate = date;
            this.Text = "Добавление занятия";
        }

        public ScheduleForm(DateTime date, int scheduleId)
        {
            InitializeComponent();
            this.classDate = date;
            this.scheduleId = scheduleId;
            this.isEditMode = true;
            this.Text = "Редактирование занятия";
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            lblDate.Text = classDate.ToLongDateString();
            LoadClasses();
            LoadTrainers();

            if (isEditMode)
            {
                LoadScheduleData();
            }
            else
            {
                // Устанавливаем значения по умолчанию для новой записи
                timePickerStart.Value = DateTime.Today.AddHours(9); // 9:00 по умолчанию
                numMaxParticipants.Value = 20;
            }
        }

        private void LoadClasses()
        {
            try
            {
                var classes = DatabaseManager.GetClasses();

                comboClass.DisplayMember = "ClassName";
                comboClass.ValueMember = "ClassID";
                comboClass.DataSource = classes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка занятий: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTrainers()
        {
            try
            {
                var trainers = DatabaseManager.GetTrainers();

                comboTrainer.DisplayMember = "FullName";
                comboTrainer.ValueMember = "TrainerID";

                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("TrainerID", typeof(int));
                displayTable.Columns.Add("FullName", typeof(string));

                foreach (DataRow row in trainers.Rows)
                {
                    DataRow newRow = displayTable.NewRow();
                    newRow["TrainerID"] = row["TrainerID"];
                    newRow["FullName"] = $"{row["LastName"]} {row["FirstName"]}";
                    displayTable.Rows.Add(newRow);
                }

                comboTrainer.DataSource = displayTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка тренеров: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadScheduleData()
        {
            try
            {
                var entry = DatabaseManager.GetScheduleEntry(scheduleId);

                if (entry != null)
                {
                    comboClass.SelectedValue = entry.ClassID;
                    comboTrainer.SelectedValue = entry.TrainerID;

                    // Устанавливаем время начала
                    DateTime timeValue = DateTime.Today.Add(entry.StartTime);
                    timePickerStart.Value = timeValue;

                    numMaxParticipants.Value = entry.MaxParticipants;
                }
                else
                {
                    MessageBox.Show("Занятие не найдено!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных занятия: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверяем заполнение обязательных полей
            if (comboClass.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип занятия!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboClass.Focus();
                return;
            }

            if (comboTrainer.SelectedValue == null)
            {
                MessageBox.Show("Выберите тренера!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboTrainer.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                int classId = Convert.ToInt32(comboClass.SelectedValue);
                int trainerId = Convert.ToInt32(comboTrainer.SelectedValue);
                TimeSpan startTime = timePickerStart.Value.TimeOfDay;
                int maxParticipants = (int)numMaxParticipants.Value;

                bool result = false;

                if (isEditMode)
                {
                    result = true;
                }
                else
                {
                    result = true;
                }

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить занятие в расписании!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении занятия: {ex.Message}",
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