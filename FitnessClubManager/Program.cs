using System;
using System.Windows.Forms;

namespace FitnessClubManager
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем форму авторизации
            Application.Run(new LoginForm());
        }
    }
}