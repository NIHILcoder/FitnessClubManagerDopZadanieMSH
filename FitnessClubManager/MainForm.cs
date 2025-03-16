using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class MainForm : Form
    {
        private int currentUserId;
        private string currentUserRole;

        public MainForm(int userId, string userRole)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUserRole = userRole;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Настройка видимости вкладок в зависимости от роли пользователя
            ConfigureTabAccessByRole();

            // Загружаем данные для первой вкладки
            LoadClients();

            // Устанавливаем заголовок формы с информацией о пользователе
            this.Text = $"Фитнес-клуб \"ActiveLife\" - {GetCurrentUserName()} ({currentUserRole})";

            // Обновляем статус
            statusLabelCurrentUser.Text = $"Пользователь: {GetCurrentUserName()}";
            statusLabelDateTime.Text = $"Дата: {DateTime.Now.ToShortDateString()}";
        }

        private void ConfigureTabAccessByRole()
        {
            // Настройка доступа к вкладкам в зависимости от роли
            switch (currentUserRole)
            {
                case "Администратор":
                    // Администратор имеет доступ ко всем вкладкам
                    break;
                case "Тренер":
                    // Тренер имеет доступ только к определенным вкладкам
                    tabReports.Parent = null;  // Скрываем вкладку отчетов
                    break;
                case "Клиент":
                    // Клиент имеет самый ограниченный доступ
                    tabClients.Parent = null;
                    tabTrainers.Parent = null;
                    tabReports.Parent = null;
                    break;
            }
        }

        private string GetCurrentUserName()
        {
            return DatabaseManager.GetUserName(currentUserId);
        }

        #region Клиенты
        private void LoadClients()
        {
            try
            {
                // Загрузка списка клиентов из базы данных
                var clients = DatabaseManager.GetClients();
                dataGridClients.DataSource = clients;

                // Настройка отображения столбцов
                ConfigureClientsGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureClientsGridView()
        {
            // Настройка отображения столбцов таблицы клиентов
            if (dataGridClients.Columns.Count > 0)
            {
                dataGridClients.Columns["ClientID"].HeaderText = "ID";
                dataGridClients.Columns["ClientID"].Width = 50;

                dataGridClients.Columns["LastName"].HeaderText = "Фамилия";
                dataGridClients.Columns["FirstName"].HeaderText = "Имя";
                dataGridClients.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridClients.Columns["BirthDate"].HeaderText = "Дата рождения";
                dataGridClients.Columns["Phone"].HeaderText = "Телефон";
                dataGridClients.Columns["Email"].HeaderText = "Email";
                dataGridClients.Columns["RegistrationDate"].HeaderText = "Дата регистрации";

                // Скрываем служебные поля
                dataGridClients.Columns["UserID"].Visible = false;
            }
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            using (var form = new ClientForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadClients(); // Перезагружаем список клиентов
                    MessageBox.Show("Клиент успешно добавлен!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (dataGridClients.SelectedRows.Count > 0)
            {
                int clientId = Convert.ToInt32(dataGridClients.SelectedRows[0].Cells["ClientID"].Value);

                using (var form = new ClientForm(clientId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadClients(); // Перезагружаем список клиентов
                        MessageBox.Show("Данные клиента обновлены!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewMemberships_Click(object sender, EventArgs e)
        {
            if (dataGridClients.SelectedRows.Count > 0)
            {
                int clientId = Convert.ToInt32(dataGridClients.SelectedRows[0].Cells["ClientID"].Value);
                string clientName = $"{dataGridClients.SelectedRows[0].Cells["LastName"].Value} " +
                                   $"{dataGridClients.SelectedRows[0].Cells["FirstName"].Value}";

                using (var form = new ClientMembershipsForm(clientId, clientName))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для просмотра абонементов!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Абонементы
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Загрузка данных в зависимости от выбранной вкладки
            switch (tabControl.SelectedTab.Name)
            {
                case "tabClients":
                    LoadClients();
                    break;
                case "tabMemberships":
                    LoadMemberships();
                    break;
                case "tabSchedule":
                    LoadSchedule();
                    break;
                case "tabTrainers":
                    LoadTrainers();
                    break;
                case "tabReports":
                    // Подготовка интерфейса вкладки отчетов
                    break;
            }
        }

        private void LoadMemberships()
        {
            try
            {
                // Загрузка списка всех активных абонементов
                var memberships = DatabaseManager.GetMemberships(true);
                dataGridMemberships.DataSource = memberships;

                // Настройка отображения столбцов
                ConfigureMembershipsGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке абонементов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureMembershipsGridView()
        {
            // Настройка отображения столбцов таблицы абонементов
            if (dataGridMemberships.Columns.Count > 0)
            {
                dataGridMemberships.Columns["MembershipID"].HeaderText = "ID";
                dataGridMemberships.Columns["MembershipID"].Width = 50;

                dataGridMemberships.Columns["ClientName"].HeaderText = "Клиент";
                dataGridMemberships.Columns["TypeName"].HeaderText = "Тип абонемента";
                dataGridMemberships.Columns["StartDate"].HeaderText = "Дата начала";
                dataGridMemberships.Columns["EndDate"].HeaderText = "Дата окончания";
                dataGridMemberships.Columns["IssueDate"].HeaderText = "Дата выдачи";
                dataGridMemberships.Columns["IsActive"].HeaderText = "Активен";

                // Скрываем служебные поля
                dataGridMemberships.Columns["ClientID"].Visible = false;
                dataGridMemberships.Columns["TypeID"].Visible = false;
            }
        }

        private void btnAddMembership_Click(object sender, EventArgs e)
        {
            using (var form = new MembershipForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMemberships(); // Перезагружаем список абонементов
                    MessageBox.Show("Абонемент успешно добавлен!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExtendMembership_Click(object sender, EventArgs e)
        {
            if (dataGridMemberships.SelectedRows.Count > 0)
            {
                int membershipId = Convert.ToInt32(dataGridMemberships.SelectedRows[0].Cells["MembershipID"].Value);

                using (var form = new ExtendMembershipForm(membershipId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadMemberships(); // Перезагружаем список абонементов
                        MessageBox.Show("Абонемент успешно продлен!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите абонемент для продления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioActiveMemb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioActiveMemb.Checked)
            {
                // Показываем только активные абонементы
                var memberships = DatabaseManager.GetMemberships(true);
                dataGridMemberships.DataSource = memberships;
            }
        }

        private void radioExpiredMemb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioExpiredMemb.Checked)
            {
                // Показываем только истекшие абонементы
                var memberships = DatabaseManager.GetMemberships(false);
                dataGridMemberships.DataSource = memberships;
            }
        }
        #endregion

        #region Расписание
        private void LoadSchedule()
        {
            try
            {
                // Загрузка расписания на выбранную дату
                DateTime selectedDate = dateTimePickerSchedule.Value.Date;
                var schedule = DatabaseManager.GetSchedule(selectedDate);
                dataGridSchedule.DataSource = schedule;

                // Настройка отображения столбцов
                ConfigureScheduleGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке расписания: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureScheduleGridView()
        {
            // Настройка отображения столбцов таблицы расписания
            if (dataGridSchedule.Columns.Count > 0)
            {
                dataGridSchedule.Columns["ScheduleID"].HeaderText = "ID";
                dataGridSchedule.Columns["ScheduleID"].Width = 50;

                dataGridSchedule.Columns["ClassName"].HeaderText = "Название";
                dataGridSchedule.Columns["StartTime"].HeaderText = "Время начала";
                dataGridSchedule.Columns["Duration"].HeaderText = "Длительность (мин)";
                dataGridSchedule.Columns["TrainerName"].HeaderText = "Тренер";
                dataGridSchedule.Columns["MaxParticipants"].HeaderText = "Макс. участников";

                // Скрываем служебные поля
                dataGridSchedule.Columns["ClassID"].Visible = false;
                dataGridSchedule.Columns["TrainerID"].Visible = false;
            }
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            using (var form = new ScheduleForm(dateTimePickerSchedule.Value.Date))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadSchedule(); // Перезагружаем расписание
                    MessageBox.Show("Занятие успешно добавлено в расписание!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditClass_Click(object sender, EventArgs e)
        {
            if (dataGridSchedule.SelectedRows.Count > 0)
            {
                int scheduleId = Convert.ToInt32(dataGridSchedule.SelectedRows[0].Cells["ScheduleID"].Value);

                using (var form = new ScheduleForm(dateTimePickerSchedule.Value.Date, scheduleId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadSchedule(); // Перезагружаем расписание
                        MessageBox.Show("Занятие успешно обновлено!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите занятие для редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePickerSchedule_ValueChanged(object sender, EventArgs e)
        {
            // Перезагружаем расписание при изменении даты
            LoadSchedule();
        }
        #endregion

        #region Тренеры
        private void LoadTrainers()
        {
            try
            {
                // Загрузка списка тренеров из базы данных
                var trainers = DatabaseManager.GetTrainers();
                dataGridTrainers.DataSource = trainers;

                // Настройка отображения столбцов
                ConfigureTrainersGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке тренеров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureTrainersGridView()
        {
            // Настройка отображения столбцов таблицы тренеров
            if (dataGridTrainers.Columns.Count > 0)
            {
                dataGridTrainers.Columns["TrainerID"].HeaderText = "ID";
                dataGridTrainers.Columns["TrainerID"].Width = 50;

                dataGridTrainers.Columns["LastName"].HeaderText = "Фамилия";
                dataGridTrainers.Columns["FirstName"].HeaderText = "Имя";
                dataGridTrainers.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridTrainers.Columns["Specialization"].HeaderText = "Специализация";
                dataGridTrainers.Columns["Experience"].HeaderText = "Опыт (лет)";
                dataGridTrainers.Columns["Phone"].HeaderText = "Телефон";
                dataGridTrainers.Columns["Email"].HeaderText = "Email";

                // Скрываем служебные поля
                dataGridTrainers.Columns["UserID"].Visible = false;
            }
        }

        private void btnAddTrainer_Click(object sender, EventArgs e)
        {
            using (var form = new TrainerForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTrainers(); // Перезагружаем список тренеров
                    MessageBox.Show("Тренер успешно добавлен!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditTrainer_Click(object sender, EventArgs e)
        {
            if (dataGridTrainers.SelectedRows.Count > 0)
            {
                int trainerId = Convert.ToInt32(dataGridTrainers.SelectedRows[0].Cells["TrainerID"].Value);

                using (var form = new TrainerForm(trainerId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadTrainers(); // Перезагружаем список тренеров
                        MessageBox.Show("Данные тренера обновлены!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите тренера для редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewTrainerClasses_Click(object sender, EventArgs e)
        {
            if (dataGridTrainers.SelectedRows.Count > 0)
            {
                int trainerId = Convert.ToInt32(dataGridTrainers.SelectedRows[0].Cells["TrainerID"].Value);
                string trainerName = $"{dataGridTrainers.SelectedRows[0].Cells["LastName"].Value} " +
                                    $"{dataGridTrainers.SelectedRows[0].Cells["FirstName"].Value}";

                using (var form = new TrainerClassesForm(trainerId, trainerName))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Выберите тренера для просмотра занятий!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Отчеты
        private void comboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Настройка интерфейса отчета в зависимости от выбранного типа
            switch (comboReportType.SelectedIndex)
            {
                case 0: // Статистика по посещаемости
                    groupReportSettings.Text = "Настройки отчета по посещаемости";
                    lblReportParam1.Text = "Период с:";
                    lblReportParam2.Text = "по:";
                    dateTimeReportParam1.Visible = true;
                    dateTimeReportParam2.Visible = true;
                    comboReportParam.Visible = false;
                    break;

                case 1: // Отчеты по доходам от абонементов
                    groupReportSettings.Text = "Настройки отчета по доходам";
                    lblReportParam1.Text = "Период с:";
                    lblReportParam2.Text = "по:";
                    dateTimeReportParam1.Visible = true;
                    dateTimeReportParam2.Visible = true;
                    comboReportParam.Visible = false;
                    break;

                case 2: // Список клиентов с истекшими абонементами
                    groupReportSettings.Text = "Настройки отчета по истекшим абонементам";
                    lblReportParam1.Text = "Период:";
                    lblReportParam2.Text = "";
                    dateTimeReportParam1.Visible = false;
                    dateTimeReportParam2.Visible = false;
                    comboReportParam.Visible = true;
                    comboReportParam.Items.Clear();
                    comboReportParam.Items.Add("За последний месяц");
                    comboReportParam.Items.Add("За последние 3 месяца");
                    comboReportParam.Items.Add("За текущий год");
                    comboReportParam.SelectedIndex = 0;
                    break;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportTitle = "";
            string reportContent = "";

            try
            {
                switch (comboReportType.SelectedIndex)
                {
                    case 0: // Статистика по посещаемости
                        reportTitle = "Отчет по посещаемости";
                        reportContent = DatabaseManager.GetAttendanceReport(
                            dateTimeReportParam1.Value.Date,
                            dateTimeReportParam2.Value.Date);
                        break;

                    case 1: // Отчеты по доходам от абонементов
                        reportTitle = "Отчет по доходам от абонементов";
                        reportContent = DatabaseManager.GetRevenueReport(
                            dateTimeReportParam1.Value.Date,
                            dateTimeReportParam2.Value.Date);
                        break;

                    case 2: // Список клиентов с истекшими абонементами
                        reportTitle = "Отчет по клиентам с истекшими абонементами";
                        int monthsPeriod = 1;
                        switch (comboReportParam.SelectedIndex)
                        {
                            case 0: monthsPeriod = 1; break;
                            case 1: monthsPeriod = 3; break;
                            case 2: monthsPeriod = 12; break;
                        }
                        reportContent = DatabaseManager.GetExpiredMembershipsReport(monthsPeriod);
                        break;
                }

                // Отображаем отчет
                richTextReport.Text = reportTitle + "\r\n\r\n" + reportContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextReport.Text))
            {
                MessageBox.Show("Сначала сформируйте отчет!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveDialog.Title = "Сохранить отчет";
            saveDialog.DefaultExt = "txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(saveDialog.FileName, richTextReport.Text);
                    MessageBox.Show("Отчет успешно сохранен!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort; // Специальный код для перезапуска приложения
            this.Close();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            // Обновляем время в статусной строке
            statusLabelTime.Text = $"Время: {DateTime.Now.ToLongTimeString()}";
        }
    }
}