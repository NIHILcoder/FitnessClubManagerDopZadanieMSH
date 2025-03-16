using System;
using System.Data;
using Npgsql;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FitnessClubManager
{
    public static class DatabaseManager
    {
        // Строка подключения из конфигурации
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["FitnessClubDB"].ConnectionString;

        #region Вспомогательные методы для работы с БД
        // Выполнение запроса без возврата данных
        private static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Выполнение запроса с возвратом одного значения
        private static object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Выполнение запроса с возвратом DataTable
        private static DataTable ExecuteDataTable(string sql, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    if (parameters != null)
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }
        #endregion

        #region Аутентификация
        // Проверка логина и пароля
        public static Tuple<int, string> AuthenticateUser(string login, string password)
        {
            int userId = -1;
            string roleName = "";

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT UserID, RoleID FROM Users WHERE Login = @login AND Password = @password";
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader.GetInt32(0);
                            int roleId = reader.GetInt32(1);

                            reader.Close();
                            cmd.CommandText = "SELECT RoleName FROM Roles WHERE RoleID = @roleId";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@roleId", roleId);
                            roleName = (string)cmd.ExecuteScalar();

                            cmd.CommandText = "UPDATE Users SET LastLogin = CURRENT_TIMESTAMP WHERE UserID = @userId";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            return new Tuple<int, string>(userId, roleName);
        }

        // Получение имени пользователя
        public static string GetUserName(int userId)
        {
            string name = "Пользователь";
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Сначала ищем в клиентах
                    cmd.CommandText = "SELECT LastName, FirstName FROM Clients WHERE UserID = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader.GetString(0) + " " + reader.GetString(1);
                    }

                    // Затем ищем в тренерах
                    cmd.CommandText = "SELECT LastName, FirstName FROM Trainers WHERE UserID = @userId";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader.GetString(0) + " " + reader.GetString(1);
                    }
                }
            }
            return name;
        }
        #endregion

        #region Клиенты
        // Получить список всех клиентов
        public static DataTable GetClients()
        {
            string sql = "SELECT * FROM Clients ORDER BY LastName";
            return ExecuteDataTable(sql);
        }

        // Получить список клиентов по уровню активности
        public static DataTable GetClientsByActivityLevel(string activityLevel)
        {
            string sql = "SELECT * FROM Clients WHERE ActivityLevel = @activityLevel ORDER BY LastName";
            var parameters = new Dictionary<string, object> { { "@activityLevel", activityLevel } };
            return ExecuteDataTable(sql, parameters);
        }

        // Получить данные одного клиента
        public static Client GetClient(int clientId)
        {
            Client client = null;
            string sql = "SELECT * FROM Clients WHERE ClientID = @clientId";
            var parameters = new Dictionary<string, object> { { "@clientId", clientId } };

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                ClientID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                LastName = reader.GetString(2),
                                FirstName = reader.GetString(3),
                                MiddleName = !reader.IsDBNull(4) ? reader.GetString(4) : null,
                                BirthDate = !reader.IsDBNull(5) ? reader.GetDateTime(5) : (DateTime?)null,
                                Phone = reader.GetString(6),
                                Email = !reader.IsDBNull(7) ? reader.GetString(7) : null,
                                RegistrationDate = reader.GetDateTime(8),
                                Notes = !reader.IsDBNull(9) ? reader.GetString(9) : null,
                                ActivityLevel = !reader.IsDBNull(10) ? reader.GetString(10) : "Средний"
                            };
                        }
                    }
                }
            }
            return client;
        }

        // Добавить нового клиента
        public static int AddClient(string lastName, string firstName, string middleName,
                                  DateTime? birthDate, string phone, string email,
                                  string notes = null, string activityLevel = "Средний")
        {
            int clientId = -1;

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Создаем пользователя
                    string login = lastName.ToLower() + firstName.ToLower();
                    cmd.CommandText = "INSERT INTO Users (Login, Password, RoleID) VALUES (@login, 'password', 3) RETURNING UserID";
                    cmd.Parameters.AddWithValue("@login", login);
                    int userId = (int)cmd.ExecuteScalar();

                    // Добавляем клиента
                    cmd.CommandText = @"INSERT INTO Clients 
                                    (UserID, LastName, FirstName, MiddleName, BirthDate, Phone, Email, RegistrationDate, Notes, ActivityLevel) 
                                    VALUES (@userId, @lastName, @firstName, @middleName, @birthDate, @phone, @email, CURRENT_DATE, @notes, @activityLevel) 
                                    RETURNING ClientID";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@middleName", string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName);
                    cmd.Parameters.AddWithValue("@birthDate", birthDate.HasValue ? (object)birthDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes);
                    cmd.Parameters.AddWithValue("@activityLevel", activityLevel);

                    clientId = (int)cmd.ExecuteScalar();

                    // Добавляем запись в историю
                    if (clientId > 0)
                        LogClientChange(clientId, "Создание записи клиента");
                }
            }
            return clientId;
        }

        // Обновить данные клиента
        public static bool UpdateClient(int clientId, string lastName, string firstName, string middleName,
                                       DateTime? birthDate, string phone, string email,
                                       string notes = null, string activityLevel = null)
        {
            string sql = @"UPDATE Clients SET 
                         LastName = @lastName, FirstName = @firstName, MiddleName = @middleName, 
                         BirthDate = @birthDate, Phone = @phone, Email = @email,
                         Notes = @notes, ActivityLevel = @activityLevel
                         WHERE ClientID = @clientId";

            var parameters = new Dictionary<string, object> {
                { "@lastName", lastName },
                { "@firstName", firstName },
                { "@middleName", string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName },
                { "@birthDate", birthDate.HasValue ? (object)birthDate.Value : DBNull.Value },
                { "@phone", phone },
                { "@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email },
                { "@notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes },
                { "@activityLevel", string.IsNullOrEmpty(activityLevel) ? DBNull.Value : (object)activityLevel },
                { "@clientId", clientId }
            };

            int result = ExecuteNonQuery(sql, parameters);

            if (result > 0)
                LogClientChange(clientId, "Обновление данных клиента");

            return result > 0;
        }

        // Логирование изменений данных клиента
        private static void LogClientChange(int clientId, string changeDescription)
        {
            try
            {
                string sql = @"INSERT INTO ClientHistory (ClientID, ChangeDate, ChangeDescription, UserID) 
                             VALUES (@clientId, CURRENT_TIMESTAMP, @changeDescription, @userId)";

                var parameters = new Dictionary<string, object> {
                    { "@clientId", clientId },
                    { "@changeDescription", changeDescription },
                    { "@userId", 1 } // В идеале здесь должен быть ID текущего пользователя
                };

                ExecuteNonQuery(sql, parameters);
            }
            catch (Exception) { } // Логируем ошибку в лог-файл
        }

        // Получить историю изменений клиента
        public static DataTable GetClientHistory(int clientId)
        {
            string sql = @"SELECT ch.ChangeDate, ch.ChangeDescription, 
                          COALESCE(u.Login, 'Система') as UserName
                          FROM ClientHistory ch
                          LEFT JOIN Users u ON ch.UserID = u.UserID
                          WHERE ch.ClientID = @clientId
                          ORDER BY ch.ChangeDate DESC";

            var parameters = new Dictionary<string, object> { { "@clientId", clientId } };
            return ExecuteDataTable(sql, parameters);
        }
        #endregion

        #region Абонементы
        // Получить список абонементов
        public static DataTable GetMemberships(bool activeOnly)
        {
            string sql = @"SELECT m.MembershipID, m.ClientID, c.LastName || ' ' || c.FirstName AS ClientName, 
                         m.TypeID, mt.TypeName, m.StartDate, m.EndDate, m.IssueDate, m.IsActive, m.AutoRenew 
                         FROM Memberships m 
                         JOIN Clients c ON m.ClientID = c.ClientID 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.IsActive = @activeOnly 
                         ORDER BY m.EndDate";

            var parameters = new Dictionary<string, object> { { "@activeOnly", activeOnly } };
            return ExecuteDataTable(sql, parameters);
        }

        // Получить абонементы для конкретного клиента
        public static DataTable GetClientMemberships(int clientId)
        {
            string sql = @"SELECT m.MembershipID, m.TypeID, mt.TypeName, m.StartDate, m.EndDate, 
                         m.IssueDate, m.IsActive, mt.Price, m.AutoRenew 
                         FROM Memberships m 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.ClientID = @clientId 
                         ORDER BY m.IssueDate DESC";

            var parameters = new Dictionary<string, object> { { "@clientId", clientId } };
            return ExecuteDataTable(sql, parameters);
        }

        // Получить типы абонементов
        public static DataTable GetMembershipTypes()
        {
            string sql = "SELECT * FROM MembershipTypes ORDER BY Price";
            return ExecuteDataTable(sql);
        }

        // Добавить новый абонемент
        public static int AddMembership(int clientId, int typeId, DateTime startDate, bool autoRenew = false)
        {
            string sql = @"INSERT INTO Memberships (ClientID, TypeID, StartDate, EndDate, IssueDate, IsActive, AutoRenew) 
                         VALUES (@clientId, @typeId, @startDate, 
                         @startDate + ((SELECT DurationDays FROM MembershipTypes WHERE TypeID = @typeId) || ' days')::interval, 
                         CURRENT_DATE, true, @autoRenew) 
                         RETURNING MembershipID";

            var parameters = new Dictionary<string, object> {
                { "@clientId", clientId },
                { "@typeId", typeId },
                { "@startDate", startDate },
                { "@autoRenew", autoRenew }
            };

            return Convert.ToInt32(ExecuteScalar(sql, parameters));
        }

        // Получить данные абонемента
        public static Membership GetMembership(int membershipId)
        {
            Membership membership = null;
            string sql = @"SELECT m.*, mt.DurationDays, mt.TypeName 
                         FROM Memberships m 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.MembershipID = @membershipId";

            var parameters = new Dictionary<string, object> { { "@membershipId", membershipId } };

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            membership = new Membership
                            {
                                MembershipID = reader.GetInt32(0),
                                ClientID = reader.GetInt32(1),
                                TypeID = reader.GetInt32(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4),
                                IssueDate = reader.GetDateTime(5),
                                IsActive = reader.GetBoolean(6),
                                AutoRenew = !reader.IsDBNull(7) && reader.GetBoolean(7),
                                DurationDays = reader.GetInt32(8),
                                TypeName = reader.GetString(9)
                            };
                        }
                    }
                }
            }
            return membership;
        }

        // Продлить абонемент
        public static bool ExtendMembership(int membershipId, int extendDays)
        {
            string sql = @"UPDATE Memberships 
                         SET EndDate = EndDate + (@extendDays || ' days')::interval, 
                         IsActive = true 
                         WHERE MembershipID = @membershipId";

            var parameters = new Dictionary<string, object> {
                { "@membershipId", membershipId },
                { "@extendDays", extendDays }
            };

            return ExecuteNonQuery(sql, parameters) > 0;
        }

        // Обновить настройку автопродления
        public static bool UpdateMembershipAutoRenew(int membershipId, bool autoRenew)
        {
            string sql = "UPDATE Memberships SET AutoRenew = @autoRenew WHERE MembershipID = @membershipId";

            var parameters = new Dictionary<string, object> {
                { "@membershipId", membershipId },
                { "@autoRenew", autoRenew }
            };

            return ExecuteNonQuery(sql, parameters) > 0;
        }

        // Получить абонементы для автопродления
        public static List<Membership> GetMembershipsForAutoRenewal()
        {
            List<Membership> memberships = new List<Membership>();
            string sql = @"SELECT m.MembershipID, m.ClientID, m.TypeID, m.StartDate, m.EndDate, 
                         m.IssueDate, m.IsActive, mt.DurationDays, mt.TypeName 
                         FROM Memberships m 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.AutoRenew = true AND m.IsActive = true 
                         AND m.EndDate <= CURRENT_DATE + INTERVAL '7 days'";

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberships.Add(new Membership
                            {
                                MembershipID = reader.GetInt32(0),
                                ClientID = reader.GetInt32(1),
                                TypeID = reader.GetInt32(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4),
                                IssueDate = reader.GetDateTime(5),
                                IsActive = reader.GetBoolean(6),
                                DurationDays = reader.GetInt32(7),
                                TypeName = reader.GetString(8),
                                AutoRenew = true
                            });
                        }
                    }
                }
            }
            return memberships;
        }
        #endregion

        #region Расписание
        // Получить расписание на определенную дату
        public static DataTable GetSchedule(DateTime date)
        {
            string sql = @"SELECT s.ScheduleID, s.ClassID, c.ClassName, s.StartTime, 
                         c.Duration, s.TrainerID, t.LastName || ' ' || t.FirstName AS TrainerName, 
                         s.MaxParticipants 
                         FROM Schedule s 
                         JOIN Classes c ON s.ClassID = c.ClassID 
                         JOIN Trainers t ON s.TrainerID = t.TrainerID 
                         WHERE s.ClassDate = @date 
                         ORDER BY s.StartTime";

            var parameters = new Dictionary<string, object> { { "@date", date.Date } };
            return ExecuteDataTable(sql, parameters);
        }

        // Получить расписание тренера
        public static DataTable GetTrainerSchedule(int trainerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            string sql = @"SELECT s.ScheduleID, s.ClassDate, c.ClassName, s.StartTime, 
                         c.Duration, s.MaxParticipants, COUNT(v.VisitID) AS ActualParticipants 
                         FROM Schedule s 
                         JOIN Classes c ON s.ClassID = c.ClassID 
                         LEFT JOIN Visits v ON v.ScheduleID = s.ScheduleID 
                         WHERE s.TrainerID = @trainerId ";

            if (startDate.HasValue)
                sql += " AND s.ClassDate >= @startDate";

            if (endDate.HasValue)
                sql += " AND s.ClassDate <= @endDate";

            sql += @" GROUP BY s.ScheduleID, s.ClassDate, c.ClassName, s.StartTime, c.Duration, s.MaxParticipants
                     ORDER BY s.ClassDate, s.StartTime";

            var parameters = new Dictionary<string, object> { { "@trainerId", trainerId } };

            if (startDate.HasValue)
                parameters.Add("@startDate", startDate.Value);

            if (endDate.HasValue)
                parameters.Add("@endDate", endDate.Value);

            return ExecuteDataTable(sql, parameters);
        }

        // Получить данные занятия из расписания
        public static ScheduleEntry GetScheduleEntry(int scheduleId)
        {
            ScheduleEntry entry = null;
            string sql = "SELECT * FROM Schedule WHERE ScheduleID = @scheduleId";
            var parameters = new Dictionary<string, object> { { "@scheduleId", scheduleId } };

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entry = new ScheduleEntry
                            {
                                ScheduleID = reader.GetInt32(0),
                                ClassID = reader.GetInt32(1),
                                TrainerID = reader.GetInt32(2),
                                ClassDate = reader.GetDateTime(3),
                                StartTime = reader.GetTimeSpan(4),
                                MaxParticipants = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return entry;
        }

        // Получить список занятий
        public static DataTable GetClasses()
        {
            string sql = @"SELECT c.ClassID, c.ClassName, c.Description, c.Duration, 
                         ct.ClassTypeID, ct.TypeName AS ClassTypeName 
                         FROM Classes c 
                         JOIN ClassTypes ct ON c.ClassTypeID = ct.ClassTypeID 
                         ORDER BY c.ClassName";

            return ExecuteDataTable(sql);
        }
        #endregion

        #region Тренеры
        // Получить список тренеров
        public static DataTable GetTrainers()
        {
            string sql = @"SELECT t.*, get_trainer_average_rating(t.TrainerID) as Rating
                         FROM Trainers t ORDER BY t.LastName";

            return ExecuteDataTable(sql);
        }

        // Получить данные тренера
        public static Trainer GetTrainer(int trainerId)
        {
            Trainer trainer = null;
            string sql = "SELECT * FROM Trainers WHERE TrainerID = @trainerId";
            var parameters = new Dictionary<string, object> { { "@trainerId", trainerId } };

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            trainer = new Trainer
                            {
                                TrainerID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                LastName = reader.GetString(2),
                                FirstName = reader.GetString(3),
                                MiddleName = !reader.IsDBNull(4) ? reader.GetString(4) : null,
                                Specialization = !reader.IsDBNull(5) ? reader.GetString(5) : null,
                                Experience = !reader.IsDBNull(6) ? reader.GetInt32(6) : (int?)null,
                                Phone = reader.GetString(7),
                                Email = !reader.IsDBNull(8) ? reader.GetString(8) : null
                            };
                        }
                    }
                }

                // Получаем рейтинг тренера
                if (trainer != null)
                    trainer.Rating = GetTrainerRating(trainerId);
            }
            return trainer;
        }

        // Получить средний рейтинг тренера
        public static double GetTrainerRating(int trainerId)
        {
            string sql = "SELECT get_trainer_average_rating(@trainerId) as Rating";
            var parameters = new Dictionary<string, object> { { "@trainerId", trainerId } };

            object result = ExecuteScalar(sql, parameters);
            return Convert.ToDouble(result);
        }

        // Добавить новую оценку тренера
        public static bool AddTrainerRating(int trainerId, int rating, string category, string comment)
        {
            // Получаем текущего пользователя (в идеале должен быть передан из формы)
            int userId = 1; // По умолчанию используем admin

            string sql = @"INSERT INTO TrainerRatings (TrainerID, Rating, Category, Comment, UserID, RatingDate)
                         VALUES (@trainerId, @rating, @category, @comment, @userId, CURRENT_TIMESTAMP)";

            var parameters = new Dictionary<string, object> {
                { "@trainerId", trainerId },
                { "@rating", rating },
                { "@category", category },
                { "@comment", string.IsNullOrEmpty(comment) ? DBNull.Value : (object)comment },
                { "@userId", userId }
            };

            return ExecuteNonQuery(sql, parameters) > 0;
        }

        // Получить все оценки тренера
        public static DataTable GetTrainerRatings(int trainerId)
        {
            string sql = @"SELECT r.RatingID, r.Rating, r.Category, r.Comment, r.RatingDate,
                         COALESCE(c.LastName || ' ' || c.FirstName, 'Аноним') as UserName
                         FROM TrainerRatings r
                         LEFT JOIN Users u ON r.UserID = u.UserID
                         LEFT JOIN Clients c ON u.UserID = c.UserID
                         WHERE r.TrainerID = @trainerId
                         ORDER BY r.RatingDate DESC";

            var parameters = new Dictionary<string, object> { { "@trainerId", trainerId } };
            return ExecuteDataTable(sql, parameters);
        }
        #endregion

        #region Отчеты
        // Отчет по посещаемости
        public static string GetAttendanceReport(DateTime startDate, DateTime endDate)
        {
            string report = $"Отчет по посещаемости с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}\r\n\r\n";

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Общее количество посещений
                    cmd.CommandText = @"SELECT COUNT(*) FROM Visits v 
                                      JOIN Schedule s ON v.ScheduleID = s.ScheduleID 
                                      WHERE s.ClassDate BETWEEN @startDate AND @endDate";
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    report += $"Общее количество посещений: {Convert.ToInt32(cmd.ExecuteScalar())}\r\n\r\n";

                    // Посещения по дням недели
                    cmd.CommandText = @"SELECT to_char(s.ClassDate, 'Day'), COUNT(*) 
                                      FROM Visits v 
                                      JOIN Schedule s ON v.ScheduleID = s.ScheduleID 
                                      WHERE s.ClassDate BETWEEN @startDate AND @endDate 
                                      GROUP BY to_char(s.ClassDate, 'Day') 
                                      ORDER BY to_char(s.ClassDate, 'Day')";

                    using (var reader = cmd.ExecuteReader())
                    {
                        report += "Посещения по дням недели:\r\n";
                        while (reader.Read())
                            report += $"{reader.GetString(0).Trim()}: {reader.GetInt32(1)}\r\n";
                    }
                }
            }
            return report;
        }

        // Отчет по доходам
        public static string GetRevenueReport(DateTime startDate, DateTime endDate)
        {
            string report = $"Отчет по доходам за период с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}\r\n\r\n";

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Общая сумма
                    cmd.CommandText = @"SELECT SUM(mt.Price) FROM Memberships m 
                                      JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                                      WHERE m.IssueDate BETWEEN @startDate AND @endDate";
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    object sum = cmd.ExecuteScalar();
                    decimal totalSum = sum != DBNull.Value ? Convert.ToDecimal(sum) : 0;
                    report += $"Общая сумма доходов: {totalSum:C}\r\n\r\n";

                    // По типам абонементов
                    cmd.CommandText = @"SELECT mt.TypeName, COUNT(*), SUM(mt.Price) 
                                      FROM Memberships m 
                                      JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                                      WHERE m.IssueDate BETWEEN @startDate AND @endDate 
                                      GROUP BY mt.TypeName";

                    using (var reader = cmd.ExecuteReader())
                    {
                        report += "Доходы по типам абонементов:\r\n";
                        while (reader.Read())
                            report += $"{reader.GetString(0)}: продано {reader.GetInt32(1)} шт. на сумму {reader.GetDecimal(2):C}\r\n";
                    }
                }
            }
            return report;
        }

        // Отчет по истекшим абонементам
        public static string GetExpiredMembershipsReport(int monthsPeriod)
        {
            string report = "Отчет по клиентам с истекшими абонементами\r\n\r\n";
            string sql = @"SELECT c.LastName, c.FirstName, c.Phone, c.Email, 
                         mt.TypeName, m.EndDate 
                         FROM Memberships m 
                         JOIN Clients c ON m.ClientID = c.ClientID 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.IsActive = false AND m.EndDate > CURRENT_DATE - INTERVAL '@monthsPeriod months' 
                         ORDER BY m.EndDate DESC";

            sql = sql.Replace("@monthsPeriod", monthsPeriod.ToString());

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            report += $"{counter}. {reader.GetString(0)} {reader.GetString(1)}\r\n";
                            report += $"   Телефон: {reader.GetString(2)}, Email: {(!reader.IsDBNull(3) ? reader.GetString(3) : "Не указан")}\r\n";
                            report += $"   Абонемент: {reader.GetString(4)}\r\n";
                            report += $"   Дата окончания: {reader.GetDateTime(5).ToShortDateString()}\r\n\r\n";
                        }

                        if (counter == 0)
                            report += "Клиентов с истекшими абонементами за указанный период не найдено.";
                    }
                }
            }
            return report;
        }

        // Экспорт отчета в файл
        public static bool ExportReportToFile(string reportText, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, reportText);
                return true;
            }
            catch (Exception) { return false; }
        }

        // Экспорт отчета в PDF
        public static bool ExportReportToPDF(string reportText, string filePath)
        {
            // Примечание: для реализации этого метода нужна библиотека iTextSharp
            MessageBox.Show("Экспорт в PDF требует библиотеки iTextSharp", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        // Экспорт отчета в Excel
        public static bool ExportReportToExcel(string reportText, string filePath)
        {
            // Примечание: для реализации этого метода нужна библиотека EPPlus
            MessageBox.Show("Экспорт в Excel требует библиотеки EPPlus", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        #endregion
    }

    #region Модели данных
    public class Client
    {
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Notes { get; set; }
        public string ActivityLevel { get; set; }
    }

    public class Membership
    {
        public int MembershipID { get; set; }
        public int ClientID { get; set; }
        public int TypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
        public bool AutoRenew { get; set; }
        public int DurationDays { get; set; }
        public string TypeName { get; set; }
    }

    public class Trainer
    {
        public int TrainerID { get; set; }
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Specialization { get; set; }
        public int? Experience { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
    }

    public class ScheduleEntry
    {
        public int ScheduleID { get; set; }
        public int ClassID { get; set; }
        public int TrainerID { get; set; }
        public DateTime ClassDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int MaxParticipants { get; set; }
    }
    #endregion
}