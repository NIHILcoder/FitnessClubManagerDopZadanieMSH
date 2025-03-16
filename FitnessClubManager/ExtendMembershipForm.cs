using System;
using System.Data;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class ExtendMembershipForm : Form
    {
        private int membershipId;
        private Membership membership;

        public ExtendMembershipForm(int membershipId)
        {
            InitializeComponent();
            this.membershipId = membershipId;
        }

        private void ExtendMembershipForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Загружаем данные о выбранном абонементе
                membership = DatabaseManager.GetMembership(membershipId);

                if (membership != null)
                {
                    // Отображаем информацию о текущем абонементе
                    lblClient.Text = $"Клиент: {DatabaseManager.GetUserName(membership.ClientID)}";
                    lblType.Text = $"Тип абонемента: {membership.TypeName}";
                    lblDuration.Text = $"Срок действия: {membership.DurationDays} дней";
                    lblStartDate.Text = $"Дата начала: {membership.StartDate.ToShortDateString()}";
                    lblEndDate.Text = $"Дата окончания: {membership.EndDate.ToShortDateString()}";

                    // По умолчанию продлеваем на такой же срок
                    numDays.Value = membership.DurationDays;

                    UpdateNewEndDate();
                }
                else
                {
                    MessageBox.Show("Абонемент не найден!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных абонемента: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void numDays_ValueChanged(object sender, EventArgs e)
        {
            UpdateNewEndDate();
        }

        private void UpdateNewEndDate()
        {
            if (membership != null)
            {
                // Вычисляем новую дату окончания
                DateTime newEndDate = membership.EndDate.AddDays((int)numDays.Value);
                lblNewEndDate.Text = $"Новая дата окончания: {newEndDate.ToShortDateString()}";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Продлеваем абонемент
                bool result = DatabaseManager.ExtendMembership(membershipId, (int)numDays.Value);

                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось продлить абонемент!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при продлении абонемента: {ex.Message}",
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