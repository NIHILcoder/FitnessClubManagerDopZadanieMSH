namespace FitnessClubManager
{
    partial class MainForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewMemberships = new System.Windows.Forms.Button();
            this.btnEditClient = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.dataGridClients = new System.Windows.Forms.DataGridView();
            this.tabMemberships = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioExpiredMemb = new System.Windows.Forms.RadioButton();
            this.radioActiveMemb = new System.Windows.Forms.RadioButton();
            this.btnExtendMembership = new System.Windows.Forms.Button();
            this.btnAddMembership = new System.Windows.Forms.Button();
            this.dataGridMemberships = new System.Windows.Forms.DataGridView();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dateTimePickerSchedule = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditClass = new System.Windows.Forms.Button();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.dataGridSchedule = new System.Windows.Forms.DataGridView();
            this.tabTrainers = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnViewTrainerClasses = new System.Windows.Forms.Button();
            this.btnEditTrainer = new System.Windows.Forms.Button();
            this.btnAddTrainer = new System.Windows.Forms.Button();
            this.dataGridTrainers = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.richTextReport = new System.Windows.Forms.RichTextBox();
            this.btnSaveReport = new System.Windows.Forms.Button();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.groupReportSettings = new System.Windows.Forms.GroupBox();
            this.comboReportParam = new System.Windows.Forms.ComboBox();
            this.dateTimeReportParam2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeReportParam1 = new System.Windows.Forms.DateTimePicker();
            this.lblReportParam2 = new System.Windows.Forms.Label();
            this.lblReportParam1 = new System.Windows.Forms.Label();
            this.comboReportType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnLogout = new System.Windows.Forms.ToolStripButton();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.tabControl.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClients)).BeginInit();
            this.tabMemberships.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).BeginInit();
            this.tabSchedule.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSchedule)).BeginInit();
            this.tabTrainers.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrainers)).BeginInit();
            this.tabReports.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupReportSettings.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabClients);
            this.tabControl.Controls.Add(this.tabMemberships);
            this.tabControl.Controls.Add(this.tabSchedule);
            this.tabControl.Controls.Add(this.tabTrainers);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(984, 514);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.panel1);
            this.tabClients.Controls.Add(this.dataGridClients);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(976, 488);
            this.tabClients.TabIndex = 0;
            this.tabClients.Text = "Клиенты";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewMemberships);
            this.panel1.Controls.Add(this.btnEditClient);
            this.panel1.Controls.Add(this.btnAddClient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 60);
            this.panel1.TabIndex = 1;
            // 
            // btnViewMemberships
            // 
            this.btnViewMemberships.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewMemberships.Location = new System.Drawing.Point(254, 10);
            this.btnViewMemberships.Name = "btnViewMemberships";
            this.btnViewMemberships.Size = new System.Drawing.Size(160, 40);
            this.btnViewMemberships.TabIndex = 2;
            this.btnViewMemberships.Text = "Просмотр абонементов";
            this.btnViewMemberships.UseVisualStyleBackColor = true;
            this.btnViewMemberships.Click += new System.EventHandler(this.btnViewMemberships_Click);
            // 
            // btnEditClient
            // 
            this.btnEditClient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditClient.Location = new System.Drawing.Point(132, 10);
            this.btnEditClient.Name = "btnEditClient";
            this.btnEditClient.Size = new System.Drawing.Size(116, 40);
            this.btnEditClient.TabIndex = 1;
            this.btnEditClient.Text = "Редактировать";
            this.btnEditClient.UseVisualStyleBackColor = true;
            this.btnEditClient.Click += new System.EventHandler(this.btnEditClient_Click);
            // 
            // btnAddClient
            // 
            this.btnAddClient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddClient.Location = new System.Drawing.Point(10, 10);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(116, 40);
            this.btnAddClient.TabIndex = 0;
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
            this.dataGridClients.Location = new System.Drawing.Point(3, 69);
            this.dataGridClients.MultiSelect = false;
            this.dataGridClients.Name = "dataGridClients";
            this.dataGridClients.ReadOnly = true;
            this.dataGridClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridClients.Size = new System.Drawing.Size(970, 416);
            this.dataGridClients.TabIndex = 0;
            // 
            // tabMemberships
            // 
            this.tabMemberships.Controls.Add(this.panel2);
            this.tabMemberships.Controls.Add(this.dataGridMemberships);
            this.tabMemberships.Location = new System.Drawing.Point(4, 22);
            this.tabMemberships.Name = "tabMemberships";
            this.tabMemberships.Padding = new System.Windows.Forms.Padding(3);
            this.tabMemberships.Size = new System.Drawing.Size(976, 488);
            this.tabMemberships.TabIndex = 1;
            this.tabMemberships.Text = "Абонементы";
            this.tabMemberships.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioExpiredMemb);
            this.panel2.Controls.Add(this.radioActiveMemb);
            this.panel2.Controls.Add(this.btnExtendMembership);
            this.panel2.Controls.Add(this.btnAddMembership);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(970, 60);
            this.panel2.TabIndex = 3;
            // 
            // radioExpiredMemb
            // 
            this.radioExpiredMemb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioExpiredMemb.AutoSize = true;
            this.radioExpiredMemb.Location = new System.Drawing.Point(805, 21);
            this.radioExpiredMemb.Name = "radioExpiredMemb";
            this.radioExpiredMemb.Size = new System.Drawing.Size(142, 17);
            this.radioExpiredMemb.TabIndex = 3;
            this.radioExpiredMemb.Text = "Истекшие абонементы";
            this.radioExpiredMemb.UseVisualStyleBackColor = true;
            this.radioExpiredMemb.CheckedChanged += new System.EventHandler(this.radioExpiredMemb_CheckedChanged);
            // 
            // radioActiveMemb
            // 
            this.radioActiveMemb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioActiveMemb.AutoSize = true;
            this.radioActiveMemb.Checked = true;
            this.radioActiveMemb.Location = new System.Drawing.Point(670, 21);
            this.radioActiveMemb.Name = "radioActiveMemb";
            this.radioActiveMemb.Size = new System.Drawing.Size(141, 17);
            this.radioActiveMemb.TabIndex = 2;
            this.radioActiveMemb.TabStop = true;
            this.radioActiveMemb.Text = "Активные абонементы";
            this.radioActiveMemb.UseVisualStyleBackColor = true;
            this.radioActiveMemb.CheckedChanged += new System.EventHandler(this.radioActiveMemb_CheckedChanged);
            // 
            // btnExtendMembership
            // 
            this.btnExtendMembership.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExtendMembership.Location = new System.Drawing.Point(132, 10);
            this.btnExtendMembership.Name = "btnExtendMembership";
            this.btnExtendMembership.Size = new System.Drawing.Size(116, 40);
            this.btnExtendMembership.TabIndex = 1;
            this.btnExtendMembership.Text = "Продлить";
            this.btnExtendMembership.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExtendMembership.UseVisualStyleBackColor = true;
            this.btnExtendMembership.Click += new System.EventHandler(this.btnExtendMembership_Click);
            // 
            // btnAddMembership
            // 
            this.btnAddMembership.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddMembership.Location = new System.Drawing.Point(10, 10);
            this.btnAddMembership.Name = "btnAddMembership";
            this.btnAddMembership.Size = new System.Drawing.Size(116, 40);
            this.btnAddMembership.TabIndex = 0;
            this.btnAddMembership.Text = "Добавить";
            this.btnAddMembership.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dataGridMemberships.Location = new System.Drawing.Point(3, 69);
            this.dataGridMemberships.MultiSelect = false;
            this.dataGridMemberships.Name = "dataGridMemberships";
            this.dataGridMemberships.ReadOnly = true;
            this.dataGridMemberships.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMemberships.Size = new System.Drawing.Size(970, 416);
            this.dataGridMemberships.TabIndex = 2;
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.panel3);
            this.tabSchedule.Controls.Add(this.dataGridSchedule);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Size = new System.Drawing.Size(976, 488);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Расписание";
            this.tabSchedule.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dateTimePickerSchedule);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnEditClass);
            this.panel3.Controls.Add(this.btnAddClass);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(976, 60);
            this.panel3.TabIndex = 3;
            // 
            // dateTimePickerSchedule
            // 
            this.dateTimePickerSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSchedule.Location = new System.Drawing.Point(790, 20);
            this.dateTimePickerSchedule.Name = "dateTimePickerSchedule";
            this.dateTimePickerSchedule.Size = new System.Drawing.Size(144, 20);
            this.dateTimePickerSchedule.TabIndex = 3;
            this.dateTimePickerSchedule.ValueChanged += new System.EventHandler(this.dateTimePickerSchedule_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(753, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата:";
            // 
            // btnEditClass
            // 
            this.btnEditClass.Location = new System.Drawing.Point(132, 10);
            this.btnEditClass.Name = "btnEditClass";
            this.btnEditClass.Size = new System.Drawing.Size(116, 40);
            this.btnEditClass.TabIndex = 1;
            this.btnEditClass.Text = "Редактировать";
            this.btnEditClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditClass.UseVisualStyleBackColor = true;
            this.btnEditClass.Click += new System.EventHandler(this.btnEditClass_Click);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new System.Drawing.Point(10, 10);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(116, 40);
            this.btnAddClass.TabIndex = 0;
            this.btnAddClass.Text = "Добавить";
            this.btnAddClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dataGridSchedule.Location = new System.Drawing.Point(3, 69);
            this.dataGridSchedule.MultiSelect = false;
            this.dataGridSchedule.Name = "dataGridSchedule";
            this.dataGridSchedule.ReadOnly = true;
            this.dataGridSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSchedule.Size = new System.Drawing.Size(970, 416);
            this.dataGridSchedule.TabIndex = 2;
            // 
            // tabTrainers
            // 
            this.tabTrainers.Controls.Add(this.panel4);
            this.tabTrainers.Controls.Add(this.dataGridTrainers);
            this.tabTrainers.Location = new System.Drawing.Point(4, 22);
            this.tabTrainers.Name = "tabTrainers";
            this.tabTrainers.Size = new System.Drawing.Size(976, 488);
            this.tabTrainers.TabIndex = 3;
            this.tabTrainers.Text = "Тренеры";
            this.tabTrainers.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnViewTrainerClasses);
            this.panel4.Controls.Add(this.btnEditTrainer);
            this.panel4.Controls.Add(this.btnAddTrainer);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(976, 60);
            this.panel4.TabIndex = 3;
            // 
            // btnViewTrainerClasses
            // 
            this.btnViewTrainerClasses.Location = new System.Drawing.Point(254, 10);
            this.btnViewTrainerClasses.Name = "btnViewTrainerClasses";
            this.btnViewTrainerClasses.Size = new System.Drawing.Size(160, 40);
            this.btnViewTrainerClasses.TabIndex = 2;
            this.btnViewTrainerClasses.Text = "Просмотр занятий";
            this.btnViewTrainerClasses.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewTrainerClasses.UseVisualStyleBackColor = true;
            this.btnViewTrainerClasses.Click += new System.EventHandler(this.btnViewTrainerClasses_Click);
            // 
            // btnEditTrainer
            // 
            this.btnEditTrainer.Location = new System.Drawing.Point(132, 10);
            this.btnEditTrainer.Name = "btnEditTrainer";
            this.btnEditTrainer.Size = new System.Drawing.Size(116, 40);
            this.btnEditTrainer.TabIndex = 1;
            this.btnEditTrainer.Text = "Редактировать";
            this.btnEditTrainer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditTrainer.UseVisualStyleBackColor = true;
            this.btnEditTrainer.Click += new System.EventHandler(this.btnEditTrainer_Click);
            // 
            // btnAddTrainer
            // 
            this.btnAddTrainer.Location = new System.Drawing.Point(10, 10);
            this.btnAddTrainer.Name = "btnAddTrainer";
            this.btnAddTrainer.Size = new System.Drawing.Size(116, 40);
            this.btnAddTrainer.TabIndex = 0;
            this.btnAddTrainer.Text = "Добавить";
            this.btnAddTrainer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dataGridTrainers.Location = new System.Drawing.Point(3, 69);
            this.dataGridTrainers.MultiSelect = false;
            this.dataGridTrainers.Name = "dataGridTrainers";
            this.dataGridTrainers.ReadOnly = true;
            this.dataGridTrainers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridTrainers.Size = new System.Drawing.Size(970, 416);
            this.dataGridTrainers.TabIndex = 2;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.panel5);
            this.tabReports.Location = new System.Drawing.Point(4, 22);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(976, 488);
            this.tabReports.TabIndex = 4;
            this.tabReports.Text = "Отчеты";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.richTextReport);
            this.panel5.Controls.Add(this.btnSaveReport);
            this.panel5.Controls.Add(this.btnGenerateReport);
            this.panel5.Controls.Add(this.groupReportSettings);
            this.panel5.Controls.Add(this.comboReportType);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(976, 488);
            this.panel5.TabIndex = 0;
            // 
            // richTextReport
            // 
            this.richTextReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextReport.Location = new System.Drawing.Point(19, 190);
            this.richTextReport.Name = "richTextReport";
            this.richTextReport.ReadOnly = true;
            this.richTextReport.Size = new System.Drawing.Size(940, 285);
            this.richTextReport.TabIndex = 5;
            this.richTextReport.Text = "";
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.Location = new System.Drawing.Point(164, 143);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Size = new System.Drawing.Size(139, 40);
            this.btnSaveReport.TabIndex = 4;
            this.btnSaveReport.Text = "Сохранить отчет";
            this.btnSaveReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveReport.UseVisualStyleBackColor = true;
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(19, 143);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(139, 40);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Сформировать";
            this.btnGenerateReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // groupReportSettings
            // 
            this.groupReportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupReportSettings.Controls.Add(this.comboReportParam);
            this.groupReportSettings.Controls.Add(this.dateTimeReportParam2);
            this.groupReportSettings.Controls.Add(this.dateTimeReportParam1);
            this.groupReportSettings.Controls.Add(this.lblReportParam2);
            this.groupReportSettings.Controls.Add(this.lblReportParam1);
            this.groupReportSettings.Location = new System.Drawing.Point(19, 58);
            this.groupReportSettings.Name = "groupReportSettings";
            this.groupReportSettings.Size = new System.Drawing.Size(940, 79);
            this.groupReportSettings.TabIndex = 2;
            this.groupReportSettings.TabStop = false;
            this.groupReportSettings.Text = "Настройки отчета";
            // 
            // comboReportParam
            // 
            this.comboReportParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReportParam.FormattingEnabled = true;
            this.comboReportParam.Location = new System.Drawing.Point(145, 33);
            this.comboReportParam.Name = "comboReportParam";
            this.comboReportParam.Size = new System.Drawing.Size(200, 21);
            this.comboReportParam.TabIndex = 4;
            this.comboReportParam.Visible = false;
            // 
            // dateTimeReportParam2
            // 
            this.dateTimeReportParam2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeReportParam2.Location = new System.Drawing.Point(425, 33);
            this.dateTimeReportParam2.Name = "dateTimeReportParam2";
            this.dateTimeReportParam2.Size = new System.Drawing.Size(144, 20);
            this.dateTimeReportParam2.TabIndex = 3;
            // 
            // dateTimeReportParam1
            // 
            this.dateTimeReportParam1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeReportParam1.Location = new System.Drawing.Point(145, 33);
            this.dateTimeReportParam1.Name = "dateTimeReportParam1";
            this.dateTimeReportParam1.Size = new System.Drawing.Size(144, 20);
            this.dateTimeReportParam1.TabIndex = 2;
            // 
            // lblReportParam2
            // 
            this.lblReportParam2.AutoSize = true;
            this.lblReportParam2.Location = new System.Drawing.Point(403, 36);
            this.lblReportParam2.Name = "lblReportParam2";
            this.lblReportParam2.Size = new System.Drawing.Size(22, 13);
            this.lblReportParam2.TabIndex = 1;
            this.lblReportParam2.Text = "по:";
            // 
            // lblReportParam1
            // 
            this.lblReportParam1.AutoSize = true;
            this.lblReportParam1.Location = new System.Drawing.Point(85, 36);
            this.lblReportParam1.Name = "lblReportParam1";
            this.lblReportParam1.Size = new System.Drawing.Size(57, 13);
            this.lblReportParam1.TabIndex = 0;
            this.lblReportParam1.Text = "Период с:";
            // 
            // comboReportType
            // 
            this.comboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReportType.FormattingEnabled = true;
            this.comboReportType.Items.AddRange(new object[] {
            "Статистика по посещаемости",
            "Отчеты по доходам от абонементов",
            "Список клиентов с истекшими абонементами"});
            this.comboReportType.Location = new System.Drawing.Point(112, 20);
            this.comboReportType.Name = "comboReportType";
            this.comboReportType.Size = new System.Drawing.Size(300, 21);
            this.comboReportType.TabIndex = 1;
            this.comboReportType.SelectedIndexChanged += new System.EventHandler(this.comboReportType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Выберите отчет:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCurrentUser,
            this.statusLabelDateTime,
            this.statusLabelTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 1;
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
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogout});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(984, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnLogout
            // 
            this.btnLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(47, 22);
            this.btnLogout.Text = "Выход";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фитнес-клуб \"ActiveLife\"";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridClients)).EndInit();
            this.tabMemberships.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).EndInit();
            this.tabSchedule.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSchedule)).EndInit();
            this.tabTrainers.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrainers)).EndInit();
            this.tabReports.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupReportSettings.ResumeLayout(false);
            this.groupReportSettings.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.TabPage tabMemberships;
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.TabPage tabTrainers;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditClient;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.DataGridView dataGridClients;
        private System.Windows.Forms.DataGridView dataGridMemberships;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExtendMembership;
        private System.Windows.Forms.Button btnAddMembership;
        private System.Windows.Forms.DataGridView dataGridSchedule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnEditClass;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.DataGridView dataGridTrainers;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnEditTrainer;
        private System.Windows.Forms.Button btnAddTrainer;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDateTime;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelTime;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.DateTimePicker dateTimePickerSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnLogout;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox comboReportType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupReportSettings;
        private System.Windows.Forms.DateTimePicker dateTimeReportParam2;
        private System.Windows.Forms.DateTimePicker dateTimeReportParam1;
        private System.Windows.Forms.Label lblReportParam2;
        private System.Windows.Forms.Label lblReportParam1;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.RichTextBox richTextReport;
        private System.Windows.Forms.Button btnSaveReport;
        private System.Windows.Forms.ComboBox comboReportParam;
        private System.Windows.Forms.Button btnViewMemberships;
        private System.Windows.Forms.Button btnViewTrainerClasses;
        private System.Windows.Forms.RadioButton radioExpiredMemb;
        private System.Windows.Forms.RadioButton radioActiveMemb;
    }
}