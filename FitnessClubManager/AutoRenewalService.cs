using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FitnessClubManager
{
    public class AutoRenewalService
    {
        private Timer checkTimer;
        private BackgroundWorker worker;
        private bool isRunning = false;

        // Конструктор сервиса
        public AutoRenewalService()
        {
            // Инициализируем таймер
            checkTimer = new Timer();
            checkTimer.Interval = 1000 * 60 * 60; // Проверка каждый час
            checkTimer.Tick += CheckTimer_Tick;

            // Инициализируем фоновый обработчик
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        // Старт сервиса
        public void Start()
        {
            if (!isRunning)
            {
                checkTimer.Start();
                isRunning = true;
                Console.WriteLine("Сервис автопродления запущен");
            }
        }

        // Остановка сервиса
        public void Stop()
        {
            if (isRunning)
            {
                checkTimer.Stop();
                isRunning = false;
                Console.WriteLine("Сервис автопродления остановлен");
            }
        }

        // Обработчик тика таймера
        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            // Запускаем фоновый процесс, если он еще не запущен
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        // Метод для выполнения в фоновом потоке
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.WriteLine("Проверка абонементов для автоматического продления...");

                // Получаем список абонементов, подлежащих автопродлению
                List<Membership> memberships = DatabaseManager.GetMembershipsForAutoRenewal();
                Console.WriteLine($"Найдено {memberships.Count} абонементов для продления");

                // Обрабатываем каждый абонемент
                foreach (var membership in memberships)
                {
                    // Продлеваем абонемент на изначальную длительность
                    bool result = DatabaseManager.ExtendMembership(membership.MembershipID, membership.DurationDays);

                    if (result)
                    {
                        Console.WriteLine($"Абонемент #{membership.MembershipID} успешно продлен на {membership.DurationDays} дней");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при продлении абонемента #{membership.MembershipID}");
                    }
                }

                // Возвращаем результат
                e.Result = memberships.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в сервисе автопродления: {ex.Message}");
                e.Result = -1;
            }
        }

        // Метод, выполняемый после завершения фонового процесса
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine($"Ошибка в сервисе автопродления: {e.Error.Message}");
            }
            else if (e.Result != null)
            {
                int count = (int)e.Result;
                if (count > 0)
                {
                    Console.WriteLine($"Автопродление успешно выполнено для {count} абонементов");
                }
                else if (count == 0)
                {
                    Console.WriteLine("Нет абонементов для автопродления");
                }
            }
        }

        // Метод для ручного запуска проверки автопродления
        public void RunManualCheck()
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Проверка абонементов уже выполняется",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}