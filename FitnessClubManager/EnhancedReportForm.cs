using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace FitnessClubManager
{
    public partial class EnhancedReportForm : Form
    {
        private string reportContent = "";
        private string reportTitle = "";
        private int reportType = 0;

        public EnhancedReportForm()
        {
            InitializeComponent();
        }

        private void EnhancedReportForm_Load(object sender, EventArgs e)
        {
            // Инициализация выпадающих списков
            comboReportType.Items.Add("Статистика по посещаемости");
            comboReportType.Items.Add("Отчеты по доходам от абонементов");
            comboReportType.Items.Add("Список клиентов с истекшими абонементами");
            comboReportType.SelectedIndex = 0;

            // Настройка дат
            dateTimeReportParam1.Value = DateTime.Today.AddMonths(-1);
            dateTimeReportParam2.Value = DateTime.Today;

            // Инициализация списка форматов экспорта
            comboExportFormat.Items.Add("TXT - Текстовый формат");
            comboExportFormat.Items.Add("PDF - Adobe Portable Document Format");
            comboExportFormat.Items.Add("XLS - Microsoft Excel");
            comboExportFormat.SelectedIndex = 0;
        }

        private void comboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Настройка интерфейса отчета в зависимости от выбранного типа
            reportType = comboReportType.SelectedIndex;

            switch (reportType)
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
            reportContent = "";

            try
            {
                Cursor = Cursors.WaitCursor;

                switch (reportType)
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

                // Активируем кнопки экспорта
                groupExportOptions.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reportContent))
            {
                MessageBox.Show("Сначала сформируйте отчет!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string fileExtension = ".txt";
                string filter = "Текстовые файлы (*.txt)|*.txt";

                // Определяем формат экспорта
                switch (comboExportFormat.SelectedIndex)
                {
                    case 0: // TXT
                        fileExtension = ".txt";
                        filter = "Текстовые файлы (*.txt)|*.txt";
                        break;
                    case 1: // PDF
                        fileExtension = ".pdf";
                        filter = "PDF файлы (*.pdf)|*.pdf";
                        break;
                    case 2: // XLS
                        fileExtension = ".xlsx";
                        filter = "Excel файлы (*.xlsx)|*.xlsx";
                        break;
                }

                // Диалог сохранения файла
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = filter;
                saveDialog.Title = "Экспорт отчета";
                saveDialog.FileName = $"{reportTitle.Replace(" ", "_")}_{DateTime.Now:yyyy-MM-dd}";
                saveDialog.DefaultExt = fileExtension;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    bool success = false;

                    // Экспортируем в выбранный формат
                    switch (comboExportFormat.SelectedIndex)
                    {
                        case 0: // TXT
                            success = DatabaseManager.ExportReportToFile(richTextReport.Text, saveDialog.FileName);
                            break;
                        case 1: // PDF
                            success = DatabaseManager.ExportReportToPDF(richTextReport.Text, saveDialog.FileName);
                            break;
                        case 2: // XLS
                            success = DatabaseManager.ExportReportToExcel(richTextReport.Text, saveDialog.FileName);
                            break;
                    }

                    if (success)
                    {
                        MessageBox.Show("Отчет успешно экспортирован!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Предлагаем открыть файл
                        if (MessageBox.Show("Открыть файл?", "Вопрос",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось экспортировать отчет!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reportContent))
            {
                MessageBox.Show("Сначала сформируйте отчет!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    PrintPreviewDialog preview = new PrintPreviewDialog();
                    preview.Document = printDocument;
                    preview.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при печати отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Получаем текст отчета
            string text = richTextReport.Text;

            // Настройки шрифта и отступов
            System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
            float lineHeight = font.GetHeight(e.Graphics);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float width = e.MarginBounds.Width;

            // Печатаем заголовок жирным шрифтом
            System.Drawing.Font boldFont = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            e.Graphics.DrawString(reportTitle, boldFont, System.Drawing.Brushes.Black, x, y);
            y += lineHeight * 2;

            // Разбиваем текст на строки
            string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Пропускаем заголовок (первую строку)
            for (int i = 1; i < lines.Length; i++)
            {
                e.Graphics.DrawString(lines[i], font, System.Drawing.Brushes.Black, x, y);
                y += lineHeight;

                // Если достигли конца страницы
                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    break;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обновление подсказки о формате
            switch (comboExportFormat.SelectedIndex)
            {
                case 0: // TXT
                    lblExportInfo.Text = "Простой текстовый формат. Подходит для базового сохранения.";
                    break;
                case 1: // PDF
                    lblExportInfo.Text = "PDF формат для профессиональных отчетов (требуется iTextSharp).";
                    break;
                case 2: // XLS
                    lblExportInfo.Text = "Excel формат для дальнейшего анализа данных (требуется EPPlus).";
                    break;
            }
        }
    }
}