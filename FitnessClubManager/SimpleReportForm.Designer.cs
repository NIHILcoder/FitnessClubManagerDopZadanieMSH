namespace FitnessClubManager
{
    partial class SimpleReportForm
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
            this.panelControls = new System.Windows.Forms.Panel();
            this.groupExportOptions = new System.Windows.Forms.GroupBox();
            this.lblExportInfo = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.comboExportFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextReport = new System.Windows.Forms.RichTextBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.groupReportSettings = new System.Windows.Forms.GroupBox();
            this.comboReportParam = new System.Windows.Forms.ComboBox();
            this.dateTimeReportParam2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeReportParam1 = new System.Windows.Forms.DateTimePicker();
            this.lblReportParam2 = new System.Windows.Forms.Label();
            this.lblReportParam1 = new System.Windows.Forms.Label();
            this.comboReportType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.panelControls.SuspendLayout();
            this.groupExportOptions.SuspendLayout();
            this.groupReportSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.groupExportOptions);
            this.panelControls.Controls.Add(this.richTextReport);
            this.panelControls.Controls.Add(this.btnGenerateReport);
            this.panelControls.Controls.Add(this.groupReportSettings);
            this.panelControls.Controls.Add(this.comboReportType);
            this.panelControls.Controls.Add(this.label2);
            this.panelControls.Controls.Add(this.btnClose);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(984, 561);
            this.panelControls.TabIndex = 0;
            // 
            // groupExportOptions
            // 
            this.groupExportOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupExportOptions.Controls.Add(this.lblExportInfo);
            this.groupExportOptions.Controls.Add(this.btnPrint);
            this.groupExportOptions.Controls.Add(this.btnExport);
            this.groupExportOptions.Controls.Add(this.comboExportFormat);
            this.groupExportOptions.Controls.Add(this.label3);
            this.groupExportOptions.Enabled = false;
            this.groupExportOptions.Location = new System.Drawing.Point(19, 467);
            this.groupExportOptions.Name = "groupExportOptions";
            this.groupExportOptions.Size = new System.Drawing.Size(754, 82);
            this.groupExportOptions.TabIndex = 7;
            this.groupExportOptions.TabStop = false;
            this.groupExportOptions.Text = "Экспорт отчета";
            // 
            // lblExportInfo
            // 
            this.lblExportInfo.AutoSize = true;
            this.lblExportInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblExportInfo.Location = new System.Drawing.Point(344, 25);
            this.lblExportInfo.Name = "lblExportInfo";
            this.lblExportInfo.Size = new System.Drawing.Size(329, 13);
            this.lblExportInfo.TabIndex = 4;
            this.lblExportInfo.Text = "Простой текстовый формат. Подходит для базового сохранения";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(231, 46);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(114, 46);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Экспорт";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // comboExportFormat
            // 
            this.comboExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboExportFormat.FormattingEnabled = true;
            this.comboExportFormat.Location = new System.Drawing.Point(114, 22);
            this.comboExportFormat.Name = "comboExportFormat";
            this.comboExportFormat.Size = new System.Drawing.Size(217, 21);
            this.comboExportFormat.TabIndex = 1;
            this.comboExportFormat.SelectedIndexChanged += new System.EventHandler(this.comboExportFormat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Формат экспорта:";
            // 
            // richTextReport
            // 
            this.richTextReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextReport.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextReport.Location = new System.Drawing.Point(19, 190);
            this.richTextReport.Name = "richTextReport";
            this.richTextReport.ReadOnly = true;
            this.richTextReport.Size = new System.Drawing.Size(940, 271);
            this.richTextReport.TabIndex = 5;
            this.richTextReport.Text = "";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(19, 143);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(139, 40);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Сформировать";
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
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(859, 498);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // SimpleReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panelControls);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "SimpleReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Расширенные отчеты";
            this.Load += new System.EventHandler(this.SimpleReportForm_Load);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.groupExportOptions.ResumeLayout(false);
            this.groupExportOptions.PerformLayout();
            this.groupReportSettings.ResumeLayout(false);
            this.groupReportSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.RichTextBox richTextReport;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.GroupBox groupReportSettings;
        private System.Windows.Forms.ComboBox comboReportParam;
        private System.Windows.Forms.DateTimePicker dateTimeReportParam2;
        private System.Windows.Forms.DateTimePicker dateTimeReportParam1;
        private System.Windows.Forms.Label lblReportParam2;
        private System.Windows.Forms.Label lblReportParam1;
        private System.Windows.Forms.ComboBox comboReportType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupExportOptions;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox comboExportFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExportInfo;
        private System.Drawing.Printing.PrintDocument printDocument;
    }
}