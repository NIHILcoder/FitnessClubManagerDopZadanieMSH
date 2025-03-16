namespace FitnessClubManager
{
    partial class ModernMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModernMainForm));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnTrainers = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnMemberships = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelAdminSettings = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelReports = new System.Windows.Forms.Panel();
            this.btnViewEnhancedReports = new System.Windows.Forms.Button();
            this.panelTrainers = new System.Windows.Forms.Panel();
            this.btnRateTrainer = new System.Windows.Forms.Button();
            this.btnViewTrainerClasses = new System.Windows.Forms.Button();
            this.btnEditTrainer = new System.Windows.Forms.Button();
            this.btnAddTrainer = new System.Windows.Forms.Button();
            this.dataGridTrainers = new System.Windows.Forms.DataGridView();
            this.panelSchedule = new System.Windows.Forms.Panel();
            this.dateTimePickerSchedule = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEditClass = new System.Windows.Forms.Button();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.dataGridSchedule = new System.Windows.Forms.DataGridView();
            this.panelMemberships = new System.Windows.Forms.Panel();
            this.txtTotalMemberships = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRunAutoRenewal = new System.Windows.Forms.Button();
            this.btnToggleAutoRenew = new System.Windows.Forms.Button();
            this.radioExpiredMemb = new System.Windows.Forms.RadioButton();
            this.radioActiveMemb = new System.Windows.Forms.RadioButton();
            this.btnExtendMembership = new System.Windows.Forms.Button();
            this.btnAddMembership = new System.Windows.Forms.Button();
            this.dataGridMemberships = new System.Windows.Forms.DataGridView();
            this.panelClients = new System.Windows.Forms.Panel();
            this.txtTotalClients = new System.Windows.Forms.TextBox();
            this.comboActivityFilter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFilterClients = new System.Windows.Forms.Button();
            this.txtSearchClient = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnViewMemberships = new System.Windows.Forms.Button();
            this.btnEditClient = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.dataGridClients = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.panelAdminSettings.SuspendLayout();
            this.panelReports.SuspendLayout();
            this.panelTrainers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrainers)).BeginInit();
            this.panelSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSchedule)).BeginInit();
            this.panelMemberships.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).BeginInit();
            this.panelClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClients)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.btnAdminPanel);
            this.panelMenu.Controls.Add(this.btnReports);
            this.panelMenu.Controls.Add(this.btnTrainers);
            this.panelMenu.Controls.Add(this.btnSchedule);
            this.panelMenu.Controls.Add(this.btnMemberships);
            this.panelMenu.Controls.Add(this.btnClients);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 561);
            this.panelMenu.TabIndex = 0;
            // 
            // btnAdminPanel
            // 
            this.btnAdminPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAdminPanel.FlatAppearance.BorderSize = 0;
            this.btnAdminPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdminPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAdminPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnAdminPanel.Image")));
            this.btnAdminPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminPanel.Location = new System.Drawing.Point(0, 380);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAdminPanel.Size = new System.Drawing.Size(220, 60);
            this.btnAdminPanel.TabIndex = 6;
            this.btnAdminPanel.Text = "  Администрирование";
            this.btnAdminPanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdminPanel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdminPanel.UseVisualStyleBackColor = true;
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);
            // 
            // btnReports
            // 
            this.btnReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnReports.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReports.Image = ((System.Drawing.Image)(resources.GetObject("btnReports.Image")));
            this.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.Location = new System.Drawing.Point(0, 320);
            this.btnReports.Name = "btnReports";
            this.btnReports.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnReports.Size = new System.Drawing.Size(220, 60);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "  Отчеты";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnTrainers
            // 
            this.btnTrainers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrainers.FlatAppearance.BorderSize = 0;
            this.btnTrainers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrainers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTrainers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnTrainers.Image = ((System.Drawing.Image)(resources.GetObject("btnTrainers.Image")));
            this.btnTrainers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrainers.Location = new System.Drawing.Point(0, 260);
            this.btnTrainers.Name = "btnTrainers";
            this.btnTrainers.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTrainers.Size = new System.Drawing.Size(220, 60);
            this.btnTrainers.TabIndex = 4;
            this.btnTrainers.Text = "  Тренеры";
            this.btnTrainers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrainers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTrainers.UseVisualStyleBackColor = true;
            this.btnTrainers.Click += new System.EventHandler(this.btnTrainers_Click);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSchedule.FlatAppearance.BorderSize = 0;
            this.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSchedule.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSchedule.Image = ((System.Drawing.Image)(resources.GetObject("btnSchedule.Image")));
            this.btnSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSchedule.Location = new System.Drawing.Point(0, 200);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSchedule.Size = new System.Drawing.Size(220, 60);
            this.btnSchedule.TabIndex = 3;
            this.btnSchedule.Text = "  Расписание";
            this.btnSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // btnMemberships
            // 
            this.btnMemberships.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMemberships.FlatAppearance.BorderSize = 0;
            this.btnMemberships.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemberships.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMemberships.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMemberships.Image = ((System.Drawing.Image)(resources.GetObject("btnMemberships.Image")));
            this.btnMemberships.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemberships.Location = new System.Drawing.Point(0, 140);
            this.btnMemberships.Name = "btnMemberships";
            this.btnMemberships.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnMemberships.Size = new System.Drawing.Size(220, 60);
            this.btnMemberships.TabIndex = 2;
            this.btnMemberships.Text = "  Абонементы";
            this.btnMemberships.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemberships.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMemberships.UseVisualStyleBackColor = true;
            this.btnMemberships.Click += new System.EventHandler(this.btnMemberships_Click);
            // 
            // btnClients
            // 
            this.btnClients.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClients.FlatAppearance.BorderSize = 0;
            this.btnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClients.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnClients.Image = ((System.Drawing.Image)(resources.GetObject("btnClients.Image")));
            this.btnClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClients.Location = new System.Drawing.Point(0, 80);
            this.btnClients.Name = "btnClients";
            this.btnClients.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnClients.Size = new System.Drawing.Size(220, 60);
            this.btnClients.TabIndex = 1;
            this.btnClients.Text = "  Клиенты";
            this.btnClients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClients.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClients.UseVisualStyleBackColor = true;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelLogo.Controls.Add(this.lblLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 80);
            this.panelLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(220, 80);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "ActiveLife";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.btnLogout);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(764, 80);
            this.panelTitleBar.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(672, 25);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(80, 30);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(101, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Клиенты";
            // 
            // panelDesktop
            // 
            this.panelDesktop.Controls.Add(this.panelAdminSettings);
            this.panelDesktop.Controls.Add(this.panelReports);
            this.panelDesktop.Controls.Add(this.panelTrainers);
            this.panelDesktop.Controls.Add(this.panelSchedule);
            this.panelDesktop.Controls.Add(this.panelMemberships);
            this.panelDesktop.Controls.Add(this.panelClients);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 80);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(764, 459);
            this.panelDesktop.TabIndex = 2;
            // 
            // panelAdminSettings
            // 
            this.panelAdminSettings.Controls.Add(this.label1);
            this.panelAdminSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdminSettings.Location = new System.Drawing.Point(0, 0);
            this.panelAdminSettings.Name = "panelAdminSettings";
            this.panelAdminSettings.Size = new System.Drawing.Size(764, 459);
            this.panelAdminSettings.TabIndex = 5;
            this.panelAdminSettings.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(248, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Модуль в режиме разработки";
            // 
            // panelReports
            // 
            this.panelReports.Controls.Add(this.btnViewEnhancedReports);
            this.panelReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReports.Location = new System.Drawing.Point(0, 0);
            this.panelReports.Name = "panelReports";
            this.panelReports.Size = new System.Drawing.Size(764, 459);
            this.panelReports.TabIndex = 4;
            this.panelReports.Visible = false;
            // 
            // btnViewEnhancedReports
            // 
            this.btnViewEnhancedReports.Location = new System.Drawing.Point(27, 23);
            this.btnViewEnhancedReports.Name = "btnViewEnhancedReports";
            this.btnViewEnhancedReports.Size = new System.Drawing.Size(177, 45);
            this.btnViewEnhancedReports.TabIndex = 0;
            this.btnViewEnhancedReports.Text = "Открыть расширенные отчеты";
            this.btnViewEnhancedReports.UseVisualStyleBackColor = true;
            this.btnViewEnhancedReports.Click += new System.EventHandler(this.btnViewEnhancedReports_Click);
            // 
            // panelTrainers
            // 
            this.panelTrainers.Controls.Add(this.btnRateTrainer);
            this.panelTrainers.Controls.Add(this.btnViewTrainerClasses);
            this.panelTrainers.Controls.Add(this.btnEditTrainer);
            this.panelTrainers.Controls.Add(this.btnAddTrainer);
            this.panelTrainers.Controls.Add(this.dataGridTrainers);
            this.panelTrainers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrainers.Location = new System.Drawing.Point(0, 0);
            this.panelTrainers.Name = "panelTrainers";
            this.panelTrainers.Size = new System.Drawing.Size(764, 459);
            this.panelTrainers.TabIndex = 3;
            this.panelTrainers.Visible = false;
            // 
            // btnRateTrainer
            // 
            this.btnRateTrainer.Location = new System.Drawing.Point(391, 21);
            this.btnRateTrainer.Name = "btnRateTrainer";
            this.btnRateTrainer.Size = new System.Drawing.Size(116, 40);
            this.btnRateTrainer.TabIndex = 4;
            this.btnRateTrainer.Text = "Оценить тренера";
            this.btnRateTrainer.UseVisualStyleBackColor = true;
            this.btnRateTrainer.Click += new System.EventHandler(this.btnRateTrainer_Click);
            // 
            // btnViewTrainerClasses
            // 
            this.btnViewTrainerClasses.Location = new System.Drawing.Point(269, 21);
            this.btnViewTrainerClasses.Name = "btnViewTrainerClasses";
            this.btnViewTrainerClasses.Size = new System.Drawing.Size(116, 40);
            this.btnViewTrainerClasses.TabIndex = 3;
            this.btnViewTrainerClasses.Text = "Просмотр занятий";
            this.btnViewTrainerClasses.UseVisualStyleBackColor = true;
            this.btnViewTrainerClasses.Click += new System.EventHandler(this.btnViewTrainerClasses_Click);
            // 
            // btnEditTrainer
            // 
            this.btnEditTrainer.Location = new System.Drawing.Point(147, 21);
            this.btnEditTrainer.Name = "btnEditTrainer";
            this.btnEditTrainer.Size = new System.Drawing.Size(116, 40);
            this.btnEditTrainer.TabIndex = 2;
            this.btnEditTrainer.Text = "Редактировать";
            this.btnEditTrainer.UseVisualStyleBackColor = true;
            this.btnEditTrainer.Click += new System.EventHandler(this.btnEditTrainer_Click);
            // 
            // btnAddTrainer
            // 
            this.btnAddTrainer.Location = new System.Drawing.Point(25, 21);
            this.btnAddTrainer.Name = "btnAddTrainer";
            this.btnAddTrainer.Size = new System.Drawing.Size(116, 40);
            this.btnAddTrainer.TabIndex = 1;
            this.btnAddTrainer.Text = "Добавить";
            this.btnAddTrainer.UseVisualStyleBackColor = true;
            this.btnAddTrainer.Click += new System.EventHandler(this.btnAddTrainer_Click);
            // 
            // dataGridTrainers
            // 
            this.dataGridTrainers.AllowUserToAddRows = false;
            this.dataGridTrainers.AllowUserToDeleteRows = false;
            this.dataGridTrainers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridTrainers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridTrainers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridTrainers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTrainers.Location = new System.Drawing.Point(25, 76);
            this.dataGridTrainers.MultiSelect = false;
            this.dataGridTrainers.Name = "dataGridTrainers";
            this.dataGridTrainers.ReadOnly = true;
            this.dataGridTrainers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridTrainers.Size = new System.Drawing.Size(714, 359);
            this.dataGridTrainers.TabIndex = 0;
            // 
            // panelSchedule
            // 
            this.panelSchedule.Controls.Add(this.dateTimePickerSchedule);
            this.panelSchedule.Controls.Add(this.label6);
            this.panelSchedule.Controls.Add(this.btnEditClass);
            this.panelSchedule.Controls.Add(this.btnAddClass);
            this.panelSchedule.Controls.Add(this.dataGridSchedule);
            this.panelSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSchedule.Location = new System.Drawing.Point(0, 0);
            this.panelSchedule.Name = "panelSchedule";
            this.panelSchedule.Size = new System.Drawing.Size(764, 459);
            this.panelSchedule.TabIndex = 2;
            this.panelSchedule.Visible = false;
            // 
            // dateTimePickerSchedule
            // 
            this.dateTimePickerSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSchedule.Location = new System.Drawing.Point(599, 31);
            this.dateTimePickerSchedule.Name = "dateTimePickerSchedule";
            this.dateTimePickerSchedule.Size = new System.Drawing.Size(140, 20);
            this.dateTimePickerSchedule.TabIndex = 4;
            this.dateTimePickerSchedule.ValueChanged += new System.EventHandler(this.dateTimePickerSchedule_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(557, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Дата:";
            // 
            // btnEditClass
            // 
            this.btnEditClass.Location = new System.Drawing.Point(147, 21);
            this.btnEditClass.Name = "btnEditClass";
            this.btnEditClass.Size = new System.Drawing.Size(116, 40);
            this.btnEditClass.TabIndex = 2;
            this.btnEditClass.Text = "Редактировать";
            this.btnEditClass.UseVisualStyleBackColor = true;
            this.btnEditClass.Click += new System.EventHandler(this.btnEditClass_Click);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new System.Drawing.Point(25, 21);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(116, 40);
            this.btnAddClass.TabIndex = 1;
            this.btnAddClass.Text = "Добавить";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new System.EventHandler(this.btnAddClass_Click);
            // 
            // dataGridSchedule
            // 
            this.dataGridSchedule.AllowUserToAddRows = false;
            this.dataGridSchedule.AllowUserToDeleteRows = false;
            this.dataGridSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridSchedule.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSchedule.Location = new System.Drawing.Point(25, 76);
            this.dataGridSchedule.MultiSelect = false;
            this.dataGridSchedule.Name = "dataGridSchedule";
            this.dataGridSchedule.ReadOnly = true;
            this.dataGridSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSchedule.Size = new System.Drawing.Size(714, 359);
            this.dataGridSchedule.TabIndex = 0;
            // 
            // panelMemberships
            // 
            this.panelMemberships.Controls.Add(this.txtTotalMemberships);
            this.panelMemberships.Controls.Add(this.panel1);
            this.panelMemberships.Controls.Add(this.dataGridMemberships);
            this.panelMemberships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMemberships.Location = new System.Drawing.Point(0, 0);
            this.panelMemberships.Name = "panelMemberships";
            this.panelMemberships.Size = new System.Drawing.Size(764, 459);
            this.panelMemberships.TabIndex = 1;
            this.panelMemberships.Visible = false;
            // 
            // txtTotalMemberships
            // 
            this.txtTotalMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalMemberships.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalMemberships.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtTotalMemberships.Location = new System.Drawing.Point(25, 441);
            this.txtTotalMemberships.Name = "txtTotalMemberships";
            this.txtTotalMemberships.ReadOnly = true;
            this.txtTotalMemberships.Size = new System.Drawing.Size(282, 16);
            this.txtTotalMemberships.TabIndex = 2;
            this.txtTotalMemberships.Text = "Всего абонементов: 0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnRunAutoRenewal);
            this.panel1.Controls.Add(this.btnToggleAutoRenew);
            this.panel1.Controls.Add(this.radioExpiredMemb);
            this.panel1.Controls.Add(this.radioActiveMemb);
            this.panel1.Controls.Add(this.btnExtendMembership);
            this.panel1.Controls.Add(this.btnAddMembership);
            this.panel1.Location = new System.Drawing.Point(24, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 65);
            this.panel1.TabIndex = 1;
            // 
            // btnRunAutoRenewal
            // 
            this.btnRunAutoRenewal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunAutoRenewal.Location = new System.Drawing.Point(501, 16);
            this.btnRunAutoRenewal.Name = "btnRunAutoRenewal";
            this.btnRunAutoRenewal.Size = new System.Drawing.Size(116, 40);
            this.btnRunAutoRenewal.TabIndex = 5;
            this.btnRunAutoRenewal.Text = "Запустить автопродление";
            this.btnRunAutoRenewal.UseVisualStyleBackColor = true;
            this.btnRunAutoRenewal.Click += new System.EventHandler(this.btnRunAutoRenewal_Click);
            // 
            // btnToggleAutoRenew
            // 
            this.btnToggleAutoRenew.Location = new System.Drawing.Point(269, 16);
            this.btnToggleAutoRenew.Name = "btnToggleAutoRenew";
            this.btnToggleAutoRenew.Size = new System.Drawing.Size(116, 40);
            this.btnToggleAutoRenew.TabIndex = 4;
            this.btnToggleAutoRenew.Text = "Вкл/выкл автопродление";
            this.btnToggleAutoRenew.UseVisualStyleBackColor = true;
            this.btnToggleAutoRenew.Click += new System.EventHandler(this.btnToggleAutoRenew_Click);
            // 
            // radioExpiredMemb
            // 
            this.radioExpiredMemb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioExpiredMemb.AutoSize = true;
            this.radioExpiredMemb.Location = new System.Drawing.Point(641, 35);
            this.radioExpiredMemb.Name = "radioExpiredMemb";
            this.radioExpiredMemb.Size = new System.Drawing.Size(74, 17);
            this.radioExpiredMemb.TabIndex = 3;
            this.radioExpiredMemb.Text = "Истекшие";
            this.radioExpiredMemb.UseVisualStyleBackColor = true;
            this.radioExpiredMemb.CheckedChanged += new System.EventHandler(this.radioExpiredMemb_CheckedChanged);
            // 
            // radioActiveMemb
            // 
            this.radioActiveMemb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioActiveMemb.AutoSize = true;
            this.radioActiveMemb.Checked = true;
            this.radioActiveMemb.Location = new System.Drawing.Point(641, 16);
            this.radioActiveMemb.Name = "radioActiveMemb";
            this.radioActiveMemb.Size = new System.Drawing.Size(74, 17);
            this.radioActiveMemb.TabIndex = 2;
            this.radioActiveMemb.TabStop = true;
            this.radioActiveMemb.Text = "Активные";
            this.radioActiveMemb.UseVisualStyleBackColor = true;
            this.radioActiveMemb.CheckedChanged += new System.EventHandler(this.radioActiveMemb_CheckedChanged);
            // 
            // btnExtendMembership
            // 
            this.btnExtendMembership.Location = new System.Drawing.Point(147, 16);
            this.btnExtendMembership.Name = "btnExtendMembership";
            this.btnExtendMembership.Size = new System.Drawing.Size(116, 40);
            this.btnExtendMembership.TabIndex = 1;
            this.btnExtendMembership.Text = "Продлить";
            this.btnExtendMembership.UseVisualStyleBackColor = true;
            this.btnExtendMembership.Click += new System.EventHandler(this.btnExtendMembership_Click);
            // 
            // btnAddMembership
            // 
            this.btnAddMembership.Location = new System.Drawing.Point(25, 16);
            this.btnAddMembership.Name = "btnAddMembership";
            this.btnAddMembership.Size = new System.Drawing.Size(116, 40);
            this.btnAddMembership.TabIndex = 0;
            this.btnAddMembership.Text = "Добавить";
            this.btnAddMembership.UseVisualStyleBackColor = true;
            this.btnAddMembership.Click += new System.EventHandler(this.btnAddMembership_Click);
            // 
            // dataGridMemberships
            // 
            this.dataGridMemberships.AllowUserToAddRows = false;
            this.dataGridMemberships.AllowUserToDeleteRows = false;
            this.dataGridMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMemberships.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMemberships.Location = new System.Drawing.Point(25, 76);
            this.dataGridMemberships.MultiSelect = false;
            this.dataGridMemberships.Name = "dataGridMemberships";
            this.dataGridMemberships.ReadOnly = true;
            this.dataGridMemberships.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMemberships.Size = new System.Drawing.Size(714, 359);
            this.dataGridMemberships.TabIndex = 0;
            // 
            // panelClients
            // 
            this.panelClients.Controls.Add(this.txtTotalClients);
            this.panelClients.Controls.Add(this.comboActivityFilter);
            this.panelClients.Controls.Add(this.label5);
            this.panelClients.Controls.Add(this.btnFilterClients);
            this.panelClients.Controls.Add(this.txtSearchClient);
            this.panelClients.Controls.Add(this.label4);
            this.panelClients.Controls.Add(this.btnViewMemberships);
            this.panelClients.Controls.Add(this.btnEditClient);
            this.panelClients.Controls.Add(this.btnAddClient);
            this.panelClients.Controls.Add(this.dataGridClients);
            this.panelClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClients.Location = new System.Drawing.Point(0, 0);
            this.panelClients.Name = "panelClients";
            this.panelClients.Size = new System.Drawing.Size(764, 459);
            this.panelClients.TabIndex = 0;
            // 
            // txtTotalClients
            // 
            this.txtTotalClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalClients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtTotalClients.Location = new System.Drawing.Point(25, 441);
            this.txtTotalClients.Name = "txtTotalClients";
            this.txtTotalClients.ReadOnly = true;
            this.txtTotalClients.Size = new System.Drawing.Size(282, 16);
            this.txtTotalClients.TabIndex = 9;
            this.txtTotalClients.Text = "Всего клиентов: 0";
            // 
            // comboActivityFilter
            // 
            this.comboActivityFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboActivityFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActivityFilter.FormattingEnabled = true;
            this.comboActivityFilter.Items.AddRange(new object[] {
            "Все",
            "Низкий",
            "Средний",
            "Высокий",
            "Профессиональный"});
            this.comboActivityFilter.Location = new System.Drawing.Point(566, 47);
            this.comboActivityFilter.Name = "comboActivityFilter";
            this.comboActivityFilter.Size = new System.Drawing.Size(121, 21);
            this.comboActivityFilter.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "По активности";
            // 
            // btnFilterClients
            // 
            this.btnFilterClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterClients.Location = new System.Drawing.Point(693, 47);
            this.btnFilterClients.Name = "btnFilterClients";
            this.btnFilterClients.Size = new System.Drawing.Size(46, 21);
            this.btnFilterClients.TabIndex = 6;
            this.btnFilterClients.Text = "Ок";
            this.btnFilterClients.UseVisualStyleBackColor = true;
            this.btnFilterClients.Click += new System.EventHandler(this.btnFilterClients_Click);
            // 
            // txtSearchClient
            // 
            this.txtSearchClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchClient.Location = new System.Drawing.Point(566, 20);
            this.txtSearchClient.Name = "txtSearchClient";
            this.txtSearchClient.Size = new System.Drawing.Size(173, 20);
            this.txtSearchClient.TabIndex = 5;
            this.txtSearchClient.TextChanged += new System.EventHandler(this.txtSearchClient_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(478, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Поиск";
            // 
            // btnViewMemberships
            // 
            this.btnViewMemberships.Location = new System.Drawing.Point(269, 21);
            this.btnViewMemberships.Name = "btnViewMemberships";
            this.btnViewMemberships.Size = new System.Drawing.Size(116, 40);
            this.btnViewMemberships.TabIndex = 3;
            this.btnViewMemberships.Text = "Просмотр абонементов";
            this.btnViewMemberships.UseVisualStyleBackColor = true;
            this.btnViewMemberships.Click += new System.EventHandler(this.btnViewMemberships_Click);
            // 
            // btnEditClient
            // 
            this.btnEditClient.Location = new System.Drawing.Point(147, 21);
            this.btnEditClient.Name = "btnEditClient";
            this.btnEditClient.Size = new System.Drawing.Size(116, 40);
            this.btnEditClient.TabIndex = 2;
            this.btnEditClient.Text = "Редактировать";
            this.btnEditClient.UseVisualStyleBackColor = true;
            this.btnEditClient.Click += new System.EventHandler(this.btnEditClient_Click);
            // 
            // btnAddClient
            // 
            this.btnAddClient.Location = new System.Drawing.Point(25, 21);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(116, 40);
            this.btnAddClient.TabIndex = 1;
            this.btnAddClient.Text = "Добавить";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
            // 
            // dataGridClients
            // 
            this.dataGridClients.AllowUserToAddRows = false;
            this.dataGridClients.AllowUserToDeleteRows = false;
            this.dataGridClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridClients.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridClients.Location = new System.Drawing.Point(25, 76);
            this.dataGridClients.MultiSelect = false;
            this.dataGridClients.Name = "dataGridClients";
            this.dataGridClients.ReadOnly = true;
            this.dataGridClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridClients.Size = new System.Drawing.Size(714, 359);
            this.dataGridClients.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCurrentUser,
            this.statusLabelDateTime,
            this.statusLabelTime});
            this.statusStrip.Location = new System.Drawing.Point(220, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(764, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabelCurrentUser
            // 
            this.statusLabelCurrentUser.Name = "statusLabelCurrentUser";
            this.statusLabelCurrentUser.Size = new System.Drawing.Size(85, 17);
            this.statusLabelCurrentUser.Text = "Пользователь:";
            // 
            // statusLabelDateTime
            // 
            this.statusLabelDateTime.Name = "statusLabelDateTime";
            this.statusLabelDateTime.Size = new System.Drawing.Size(35, 17);
            this.statusLabelDateTime.Text = "Дата:";
            // 
            // statusLabelTime
            // 
            this.statusLabelTime.Name = "statusLabelTime";
            this.statusLabelTime.Size = new System.Drawing.Size(44, 17);
            this.statusLabelTime.Text = "Время:";
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // ModernMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "ModernMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фитнес-клуб \"ActiveLife\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModernMainForm_FormClosing);
            this.Load += new System.EventHandler(this.ModernMainForm_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelDesktop.ResumeLayout(false);
            this.panelAdminSettings.ResumeLayout(false);
            this.panelAdminSettings.PerformLayout();
            this.panelReports.ResumeLayout(false);
            this.panelTrainers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrainers)).EndInit();
            this.panelSchedule.ResumeLayout(false);
            this.panelSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSchedule)).EndInit();
            this.panelMemberships.ResumeLayout(false);
            this.panelMemberships.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).EndInit();
            this.panelClients.ResumeLayout(false);
            this.panelClients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClients)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnTrainers;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnMemberships;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Panel panelClients;
        private System.Windows.Forms.Button btnViewMemberships;
        private System.Windows.Forms.Button btnEditClient;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.DataGridView dataGridClients;
        private System.Windows.Forms.Panel panelMemberships;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioExpiredMemb;
        private System.Windows.Forms.RadioButton radioActiveMemb;
        private System.Windows.Forms.Button btnExtendMembership;
        private System.Windows.Forms.Button btnAddMembership;
        private System.Windows.Forms.DataGridView dataGridMemberships;
        private System.Windows.Forms.Panel panelSchedule;
        private System.Windows.Forms.DateTimePicker dateTimePickerSchedule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEditClass;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.DataGridView dataGridSchedule;
        private System.Windows.Forms.Panel panelTrainers;
        private System.Windows.Forms.Button btnViewTrainerClasses;
        private System.Windows.Forms.Button btnEditTrainer;
        private System.Windows.Forms.Button btnAddTrainer;
        private System.Windows.Forms.DataGridView dataGridTrainers;
        private System.Windows.Forms.Panel panelReports;
        private System.Windows.Forms.Button btnViewEnhancedReports;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDateTime;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelTime;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAdminPanel;
        private System.Windows.Forms.Button btnFilterClients;
        private System.Windows.Forms.TextBox txtSearchClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelAdminSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboActivityFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalClients;
        private System.Windows.Forms.Button btnRateTrainer;
        private System.Windows.Forms.Button btnToggleAutoRenew;
        private System.Windows.Forms.Button btnRunAutoRenewal;
        private System.Windows.Forms.TextBox txtTotalMemberships;
    }
}