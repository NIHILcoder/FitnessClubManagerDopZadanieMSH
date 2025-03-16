using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Проверка заполнения полей
            if (string.IsNullOrEmpty(txtLogin.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Аутентификация пользователя
            try
            {
                Cursor = Cursors.WaitCursor;

                var userInfo = DatabaseManager.AuthenticateUser(txtLogin.Text, txtPassword.Text);

                if (userInfo.Item1 > 0)
                {
                    // Успешная аутентификация
                    this.Hide();

                    using (var ModernMainForm = new ModernMainForm(userInfo.Item1, userInfo.Item2))
                    {
                        // Если пользователь нажал кнопку "Выход", то показываем снова форму логина
                        if (ModernMainForm.ShowDialog() == DialogResult.Abort)
                        {
                            txtLogin.Clear();
                            txtPassword.Clear();
                            txtLogin.Focus();
                            this.Show();
                        }
                        else
                        {
                            // Если форма закрыта обычным способом, то завершаем приложение
                            this.Close();
                        }
                    }
                }
                else
                {
                    // Неудачная аутентификация
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка аутентификации",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при входе в систему: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // В режиме разработки можно предустановить значения для быстрого входа
#if DEBUG
            txtLogin.Text = "admin";
            txtPassword.Text = "admin";
#endif
        }
    }
}