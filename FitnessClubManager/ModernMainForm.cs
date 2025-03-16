using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ModernMainForm : Form
    {
        private int currentUserId;
        private string currentUserRole;
        private Button currentButton;
        private AutoRenewalService autoRenewalService;

        public ModernMainForm(int userId, string userRole)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUserRole = userRole;

            // Инициализируем сервис автопродления абонементов
            autoRenewalService = new AutoRenewalService();
        }

        private void ModernMainForm_Load(object sender, EventArgs e)
        {
            // Настройка видимости кнопок в зависимости от роли пользователя
            ConfigureAccessByRole();

            // Устанавливаем заголовок формы с информацией о пользователе
            this.Text = $"Фитнес-клуб \"ActiveLife\" - {GetCurrentUserName()} ({currentUserRole})";

            // Обновляем статус
            statusLabelCurrentUser.Text = $"Пользователь: {GetCurrentUserName()}";
            statusLabelDateTime.Text = $"Дата: {DateTime.Now.ToShortDateString()}";

            // Активируем первую доступную кнопку меню
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button btn && btn.Visible)
                {
                    ActivateButton(btn);
                    break;
                }
            }

            // Запускаем сервис автопродления
            autoRenewalService.Start();
        }

        private void ConfigureAccessByRole()
        {
            // Настройка доступа к функциям в зависимости от роли
            switch (currentUserRole)
            {
                case "Администратор":
                    // Администратор имеет доступ ко всем функциям
                    break;
                case "Тренер":
                    // Тренер имеет ограниченный доступ
                    btnReports.Visible = false;
                    btnAdminPanel.Visible = false;
                    break;
                case "Клиент":
                    // Клиент имеет очень ограниченный доступ
                    btnClients.Visible = false;
                    btnTrainers.Visible = false;
                    btnReports.Visible = false;
                    btnAdminPanel.Visible = false;
                    break;
            }
        }

        private string GetCurrentUserName()
        {
            return DatabaseManager.GetUserName(currentUserId);
        }

        private void ActivateButton(Button button)
        {
            if (button != null && button != currentButton)
            {
                // Деактивируем текущую кнопку
                if (currentButton != null)
                {
                    currentButton.BackColor = Color.FromArgb(51, 51, 76);
                    currentButton.ForeColor = Color.Gainsboro;
                    currentButton.Font = new Font("Microsoft Sans Serif", 9F);
                }

                // Активируем новую кнопку
                button.BackColor = Color.FromArgb(90, 90, 130);
                button.ForeColor = Color.White;
                button.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
                currentButton = button;

                // Обновляем заголовок
                lblTitle.Text = button.Text;

                // Отображаем соответствующую панель
                HideAllPanels();
                ShowPanel(button.Name);
            }
        }

        private void HideAllPanels()
        {
            panelClients.Visible = false;
            panelMemberships.Visible = false;
            panelSchedule.Visible = false;
            panelTrainers.Visible = false;
            panelReports.Visible = false;
            panelAdminSettings.Visible = false;
        }

        private void ShowPanel(string buttonName)
        {
            switch (buttonName)
            {
                case "btnClients":
                    panelClients.Visible = true;
                    LoadClients();
                    break;
                case "btnMemberships":
                    panelMemberships.Visible = true;
                    LoadMemberships();
                    break;
                case "btnSchedule":
                    panelSchedule.Visible = true;
                    LoadSchedule();
                    break;
                case "btnTrainers":
                    panelTrainers.Visible = true;
                    LoadTrainers();
                    break;
                case "btnReports":
                    panelReports.Visible = true;
                    // Не загружаем данные здесь, т.к. отчеты генерируются по запросу
                    break;
                case "btnAdminPanel":
                    panelAdminSettings.Visible = true;
                    // Загружаем настройки
                    break;
            }
        }

        #region Обработчики меню
        private void btnClients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnMemberships_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnTrainers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Останавливаем сервис автопродления
            autoRenewalService.Stop();

            this.DialogResult = DialogResult.Abort; // Специальный код для перезапуска приложения
            this.Close();
        }
        #endregion

        #region Клиенты
        private void LoadClients()
        {
            try
            {
                // Очищаем текущий источник данных
                dataGridClients.DataSource = null;

                // Загрузка списка клиентов из базы данных
                var clients = DatabaseManager.GetClients();
                dataGridClients.DataSource = clients;

                // Настройка отображения столбцов
                ConfigureClientsGridView();

                // Обновляем счетчик
                txtTotalClients.Text = $"Всего клиентов: {clients.Rows.Count}";
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

                // Новые поля
                if (dataGridClients.Columns.Contains("ActivityLevel"))
                {
                    dataGridClients.Columns["ActivityLevel"].HeaderText = "Активность";
                }

                if (dataGridClients.Columns.Contains("Notes"))
                {
                    dataGridClients.Columns["Notes"].HeaderText = "Заметки";
                    dataGridClients.Columns["Notes"].Visible = false; // Скрываем, т.к. может быть длинным
                }

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

        private void btnFilterClients_Click(object sender, EventArgs e)
        {
            try
            {
                // Если выбран определенный уровень активности
                if (comboActivityFilter.SelectedIndex > 0)
                {
                    string activityLevel = comboActivityFilter.SelectedItem.ToString();
                    var filteredClients = DatabaseManager.GetClientsByActivityLevel(activityLevel);
                    dataGridClients.DataSource = filteredClients;
                    ConfigureClientsGridView();

                    // Обновляем счетчик
                    txtTotalClients.Text = $"Клиентов с активностью '{activityLevel}': {filteredClients.Rows.Count}";
                }
                else
                {
                    // Загружаем всех клиентов
                    LoadClients();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации клиентов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchClient_TextChanged(object sender, EventArgs e)
        {
            if (dataGridClients.DataSource != null)
            {
                string searchText = txtSearchClient.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(searchText))
                {
                    // Создаем фильтр для поиска по ФИО и телефону
                    DataTable dt = (DataTable)dataGridClients.DataSource;
                    dt.DefaultView.RowFilter = $"LOWER(LastName) LIKE '%{searchText}%' OR " +
                                              $"LOWER(FirstName) LIKE '%{searchText}%' OR " +
                                              $"LOWER(Phone) LIKE '%{searchText}%'";
                }
                else
                {
                    // Сбрасываем фильтр
                    ((DataTable)dataGridClients.DataSource).DefaultView.RowFilter = "";
                }
            }
        }
        #endregion

        #region Абонементы
        private void LoadMemberships()
        {
            try
            {
                // Загрузка списка всех активных абонементов
                var memberships = DatabaseManager.GetMemberships(radioActiveMemb.Checked);
                dataGridMemberships.DataSource = memberships;

                // Настройка отображения столбцов
                ConfigureMembershipsGridView();

                // Обновляем счетчик
                txtTotalMemberships.Text = $"Всего абонементов: {memberships.Rows.Count}";
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

                // Новое поле автопродления
                if (dataGridMemberships.Columns.Contains("AutoRenew"))
                {
                    dataGridMemberships.Columns["AutoRenew"].HeaderText = "Автопродление";
                }

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
                LoadMemberships();
            }
        }

        private void radioExpiredMemb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioExpiredMemb.Checked)
            {
                LoadMemberships();
            }
        }

        private void btnToggleAutoRenew_Click(object sender, EventArgs e)
        {
            if (dataGridMemberships.SelectedRows.Count > 0)
            {
                try
                {
                    int membershipId = Convert.ToInt32(dataGridMemberships.SelectedRows[0].Cells["MembershipID"].Value);
                    bool currentAutoRenew = Convert.ToBoolean(dataGridMemberships.SelectedRows[0].Cells["AutoRenew"].Value);

                    // Меняем значение на противоположное
                    bool newAutoRenew = !currentAutoRenew;
                    bool result = DatabaseManager.UpdateMembershipAutoRenew(membershipId, newAutoRenew);

                    if (result)
                    {
                        LoadMemberships(); // Перезагружаем список абонементов
                        MessageBox.Show($"Автопродление {(newAutoRenew ? "включено" : "отключено")}!",
                            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось изменить настройку автопродления!",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении настройки автопродления: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите абонемент для изменения настройки автопродления!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRunAutoRenewal_Click(object sender, EventArgs e)
        {
            // Запускаем проверку автопродления вручную
            autoRenewalService.RunManualCheck();
            MessageBox.Show("Запущена проверка абонементов для автопродления.",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Новое поле рейтинга
                if (dataGridTrainers.Columns.Contains("Rating"))
                {
                    dataGridTrainers.Columns["Rating"].HeaderText = "Рейтинг";
                    dataGridTrainers.Columns["Rating"].DefaultCellStyle.Format = "F1";
                }

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

        private void btnRateTrainer_Click(object sender, EventArgs e)
        {
            if (dataGridTrainers.SelectedRows.Count > 0)
            {
                int trainerId = Convert.ToInt32(dataGridTrainers.SelectedRows[0].Cells["TrainerID"].Value);
                string trainerName = $"{dataGridTrainers.SelectedRows[0].Cells["LastName"].Value} " +
                                    $"{dataGridTrainers.SelectedRows[0].Cells["FirstName"].Value}";

                using (var form = new TrainerRatingForm(trainerId, trainerName))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadTrainers(); // Перезагружаем список тренеров для обновления рейтинга
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите тренера для оценки!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Отчеты
        private void btnViewEnhancedReports_Click(object sender, EventArgs e)
        {
            using (var form = new SimpleReportForm())
            {
                form.ShowDialog();
            }
        }
        #endregion

        private void timerClock_Tick(object sender, EventArgs e)
        {
            // Обновляем время в статусной строке
            statusLabelTime.Text = $"Время: {DateTime.Now.ToLongTimeString()}";
        }

        private void ModernMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Останавливаем сервис автопродления при закрытии формы
            if (autoRenewalService != null)
            {
                autoRenewalService.Stop();
            }
        }
    }
}