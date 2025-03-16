using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class TrainerClassesForm : Form
    {
        private int trainerId;
        private string trainerName;

        public TrainerClassesForm(int trainerId, string trainerName)
        {
            InitializeComponent();
            this.trainerId = trainerId;
            this.trainerName = trainerName;
        }

        private void TrainerClassesForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Занятия тренера: {trainerName}";
            lblTrainerName.Text = trainerName;

            // Установка начальных дат для фильтрации
            dateTimeFrom.Value = DateTime.Today.AddDays(-30); // за последние 30 дней
            dateTimeTo.Value = DateTime.Today.AddDays(30); // на 30 дней вперед

            LoadTrainerClasses();
        }

        private void LoadTrainerClasses()
        {
            try
            {
                // Явно очищаем DataSource перед загрузкой новых данных
                dataGridClasses.DataSource = null;

                // Загружаем занятия тренера
                var schedule = DatabaseManager.GetTrainerSchedule(trainerId, dateTimeFrom.Value, dateTimeTo.Value);
                dataGridClasses.DataSource = schedule;

                // Настраиваем отображение столбцов
                if (dataGridClasses.Columns.Count > 0)
                {
                    dataGridClasses.Columns["ScheduleID"].HeaderText = "ID";
                    dataGridClasses.Columns["ScheduleID"].Width = 50;

                    dataGridClasses.Columns["ClassDate"].HeaderText = "Дата";
                    dataGridClasses.Columns["ClassDate"].DefaultCellStyle.Format = "dd.MM.yyyy";

                    dataGridClasses.Columns["ClassName"].HeaderText = "Занятие";
                    dataGridClasses.Columns["StartTime"].HeaderText = "Начало";
                    dataGridClasses.Columns["StartTime"].DefaultCellStyle.Format = "HH:mm";

                    dataGridClasses.Columns["Duration"].HeaderText = "Длительность (мин)";
                    dataGridClasses.Columns["MaxParticipants"].HeaderText = "Макс. участников";
                    dataGridClasses.Columns["ActualParticipants"].HeaderText = "Записано";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке занятий: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadTrainerClasses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}