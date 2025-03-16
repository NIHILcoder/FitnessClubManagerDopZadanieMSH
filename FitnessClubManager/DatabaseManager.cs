using System;
using System.Data;
using Npgsql;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;


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
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Добавление параметров к команде
        private static void AddParametersToCommand(NpgsqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
        }

        // Универсальный метод для выполнения запроса с возвратом объекта
        private static T ExecuteReaderSingle<T>(string sql, Dictionary<string, object> parameters, Func<NpgsqlDataReader, T> mapFunc)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    AddParametersToCommand(cmd, parameters);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return mapFunc(reader);
                    }
                }
            }
            return default(T);
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
            // Функция для чтения имени из любой таблицы
            string GetPersonName(string tableName)
            {
                return ExecuteReaderSingle<string>(
                    $"SELECT LastName, FirstName FROM {tableName} WHERE UserID = @userId",
                    new Dictionary<string, object> { { "@userId", userId } },
                    r => $"{r.GetString(0)} {r.GetString(1)}"
                );
            }

            // Сначала ищем в клиентах, затем в тренерах
            string name = GetPersonName("Clients");
            if (string.IsNullOrEmpty(name))
                name = GetPersonName("Trainers");

            return string.IsNullOrEmpty(name) ? "Пользователь" : name;
        }
        #endregion

        #region Клиенты
        // Получить список всех клиентов
        public static DataTable GetClients()
        {
            return ExecuteDataTable("SELECT * FROM Clients ORDER BY LastName");
        }

        // Получить список клиентов по уровню активности
        public static DataTable GetClientsByActivityLevel(string activityLevel)
        {
            return ExecuteDataTable("SELECT * FROM Clients WHERE ActivityLevel = @activityLevel ORDER BY LastName",
                new Dictionary<string, object> { { "@activityLevel", activityLevel } });
        }

        // Получить данные одного клиента
        public static Client GetClient(int clientId)
        {
            return ExecuteReaderSingle<Client>(
                "SELECT * FROM Clients WHERE ClientID = @clientId",
                new Dictionary<string, object> { { "@clientId", clientId } },
                r => new Client
                {
                    ClientID = r.GetInt32(0),
                    UserID = r.GetInt32(1),
                    LastName = r.GetString(2),
                    FirstName = r.GetString(3),
                    MiddleName = !r.IsDBNull(4) ? r.GetString(4) : null,
                    BirthDate = !r.IsDBNull(5) ? r.GetDateTime(5) : (DateTime?)null,
                    Phone = r.GetString(6),
                    Email = !r.IsDBNull(7) ? r.GetString(7) : null,
                    RegistrationDate = r.GetDateTime(8),
                    Notes = !r.IsDBNull(9) ? r.GetString(9) : null,
                    ActivityLevel = !r.IsDBNull(10) ? r.GetString(10) : "Средний"
                }
            );
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
            catch (Exception ex)
            {
                // Логируем ошибку
                System.Diagnostics.Debug.WriteLine($"Ошибка при логировании: {ex.Message}");
            }
        }

        // Получить историю изменений клиента
        public static DataTable GetClientHistory(int clientId)
        {
            return ExecuteDataTable(@"SELECT ch.ChangeDate, ch.ChangeDescription, 
                                     COALESCE(u.Login, 'Система') as UserName
                                     FROM ClientHistory ch
                                     LEFT JOIN Users u ON ch.UserID = u.UserID
                                     WHERE ch.ClientID = @clientId
                                     ORDER BY ch.ChangeDate DESC",
                                     new Dictionary<string, object> { { "@clientId", clientId } });
        }
        #endregion

        #region Абонементы
        // Получить список абонементов
        public static DataTable GetMemberships(bool activeOnly)
        {
            return ExecuteDataTable(@"SELECT m.MembershipID, m.ClientID, c.LastName || ' ' || c.FirstName AS ClientName, 
                                   m.TypeID, mt.TypeName, m.StartDate, m.EndDate, m.IssueDate, m.IsActive, m.AutoRenew 
                                   FROM Memberships m 
                                   JOIN Clients c ON m.ClientID = c.ClientID 
                                   JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                                   WHERE m.IsActive = @activeOnly 
                                   ORDER BY m.EndDate",
                                   new Dictionary<string, object> { { "@activeOnly", activeOnly } });
        }

        // Получить абонементы для конкретного клиента
        public static DataTable GetClientMemberships(int clientId)
        {
            return ExecuteDataTable(@"SELECT m.MembershipID, m.TypeID, mt.TypeName, m.StartDate, m.EndDate, 
                                   m.IssueDate, m.IsActive, mt.Price, m.AutoRenew 
                                   FROM Memberships m 
                                   JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                                   WHERE m.ClientID = @clientId 
                                   ORDER BY m.IssueDate DESC",
                                   new Dictionary<string, object> { { "@clientId", clientId } });
        }

        // Получить типы абонементов
        public static DataTable GetMembershipTypes()
        {
            return ExecuteDataTable("SELECT * FROM MembershipTypes ORDER BY Price");
        }

        // Добавить новый абонемент
        public static int AddMembership(int clientId, int typeId, DateTime startDate, bool autoRenew = false)
        {
            return Convert.ToInt32(ExecuteScalar(@"INSERT INTO Memberships (ClientID, TypeID, StartDate, EndDate, IssueDate, IsActive, AutoRenew) 
                                               VALUES (@clientId, @typeId, @startDate, 
                                               @startDate + ((SELECT DurationDays FROM MembershipTypes WHERE TypeID = @typeId) || ' days')::interval, 
                                               CURRENT_DATE, true, @autoRenew) 
                                               RETURNING MembershipID",
                                               new Dictionary<string, object> {
                                                   { "@clientId", clientId },
                                                   { "@typeId", typeId },
                                                   { "@startDate", startDate },
                                                   { "@autoRenew", autoRenew }
                                               }));
        }

        // Получить данные абонемента
        public static Membership GetMembership(int membershipId)
        {
            return ExecuteReaderSingle<Membership>(
                @"SELECT m.*, mt.DurationDays, mt.TypeName 
                 FROM Memberships m 
                 JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                 WHERE m.MembershipID = @membershipId",
                new Dictionary<string, object> { { "@membershipId", membershipId } },
                r => new Membership
                {
                    MembershipID = r.GetInt32(0),
                    ClientID = r.GetInt32(1),
                    TypeID = r.GetInt32(2),
                    StartDate = r.GetDateTime(3),
                    EndDate = r.GetDateTime(4),
                    IssueDate = r.GetDateTime(5),
                    IsActive = r.GetBoolean(6),
                    AutoRenew = !r.IsDBNull(7) && r.GetBoolean(7),
                    DurationDays = r.GetInt32(8),
                    TypeName = r.GetString(9)
                }
            );
        }

        // Продлить абонемент
        public static bool ExtendMembership(int membershipId, int extendDays)
        {
            return ExecuteNonQuery(@"UPDATE Memberships 
                                  SET EndDate = EndDate + (@extendDays || ' days')::interval, 
                                  IsActive = true 
                                  WHERE MembershipID = @membershipId",
                                  new Dictionary<string, object> {
                                      { "@membershipId", membershipId },
                                      { "@extendDays", extendDays }
                                  }) > 0;
        }

        // Обновить настройку автопродления
        public static bool UpdateMembershipAutoRenew(int membershipId, bool autoRenew)
        {
            return ExecuteNonQuery("UPDATE Memberships SET AutoRenew = @autoRenew WHERE MembershipID = @membershipId",
                                 new Dictionary<string, object> {
                                     { "@membershipId", membershipId },
                                     { "@autoRenew", autoRenew }
                                 }) > 0;
        }

        // Получить абонементы для автопродления
        public static List<Membership> GetMembershipsForAutoRenewal()
        {
            var memberships = new List<Membership>();
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
            return ExecuteDataTable(@"SELECT s.ScheduleID, s.ClassID, c.ClassName, s.StartTime, 
                                   c.Duration, s.TrainerID, t.LastName || ' ' || t.FirstName AS TrainerName, 
                                   s.MaxParticipants 
                                   FROM Schedule s 
                                   JOIN Classes c ON s.ClassID = c.ClassID 
                                   JOIN Trainers t ON s.TrainerID = t.TrainerID 
                                   WHERE s.ClassDate = @date 
                                   ORDER BY s.StartTime",
                                   new Dictionary<string, object> { { "@date", date.Date } });
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
            return ExecuteReaderSingle<ScheduleEntry>(
                "SELECT * FROM Schedule WHERE ScheduleID = @scheduleId",
                new Dictionary<string, object> { { "@scheduleId", scheduleId } },
                r => new ScheduleEntry
                {
                    ScheduleID = r.GetInt32(0),
                    ClassID = r.GetInt32(1),
                    TrainerID = r.GetInt32(2),
                    ClassDate = r.GetDateTime(3),
                    StartTime = r.GetTimeSpan(4),
                    MaxParticipants = r.GetInt32(5)
                }
            );
        }

        // Получить список занятий
        public static DataTable GetClasses()
        {
            return ExecuteDataTable(@"SELECT c.ClassID, c.ClassName, c.Description, c.Duration, 
                                   ct.ClassTypeID, ct.TypeName AS ClassTypeName 
                                   FROM Classes c 
                                   JOIN ClassTypes ct ON c.ClassTypeID = ct.ClassTypeID 
                                   ORDER BY c.ClassName");
        }
        #endregion

        #region Тренеры
        // Получить список тренеров
        public static DataTable GetTrainers()
        {
            return ExecuteDataTable(@"SELECT t.*, get_trainer_average_rating(t.TrainerID) as Rating
                                   FROM Trainers t ORDER BY t.LastName");
        }

        // Получить данные тренера
        public static Trainer GetTrainer(int trainerId)
        {
            Trainer trainer = ExecuteReaderSingle<Trainer>(
                "SELECT * FROM Trainers WHERE TrainerID = @trainerId",
                new Dictionary<string, object> { { "@trainerId", trainerId } },
                r => new Trainer
                {
                    TrainerID = r.GetInt32(0),
                    UserID = r.GetInt32(1),
                    LastName = r.GetString(2),
                    FirstName = r.GetString(3),
                    MiddleName = !r.IsDBNull(4) ? r.GetString(4) : null,
                    Specialization = !r.IsDBNull(5) ? r.GetString(5) : null,
                    Experience = !r.IsDBNull(6) ? r.GetInt32(6) : (int?)null,
                    Phone = r.GetString(7),
                    Email = !r.IsDBNull(8) ? r.GetString(8) : null
                }
            );

            // Получаем рейтинг тренера
            if (trainer != null)
                trainer.Rating = GetTrainerRating(trainerId);

            return trainer;
        }

        // Получить средний рейтинг тренера
        public static double GetTrainerRating(int trainerId)
        {
            object result = ExecuteScalar("SELECT get_trainer_average_rating(@trainerId) as Rating",
                                         new Dictionary<string, object> { { "@trainerId", trainerId } });
            return Convert.ToDouble(result);
        }

        // Добавить новую оценку тренера
        public static bool AddTrainerRating(int trainerId, int rating, string category, string comment)
        {
            // Получаем текущего пользователя (в идеале должен быть передан из формы)
            int userId = 1; // По умолчанию используем admin

            return ExecuteNonQuery(@"INSERT INTO TrainerRatings (TrainerID, Rating, Category, Comment, UserID, RatingDate)
                                   VALUES (@trainerId, @rating, @category, @comment, @userId, CURRENT_TIMESTAMP)",
                                   new Dictionary<string, object> {
                                       { "@trainerId", trainerId },
                                       { "@rating", rating },
                                       { "@category", category },
                                       { "@comment", string.IsNullOrEmpty(comment) ? DBNull.Value : (object)comment },
                                       { "@userId", userId }
                                   }) > 0;
        }

        // Получить все оценки тренера
        public static DataTable GetTrainerRatings(int trainerId)
        {
            return ExecuteDataTable(@"SELECT r.RatingID, r.Rating, r.Category, r.Comment, r.RatingDate,
                                   COALESCE(c.LastName || ' ' || c.FirstName, 'Аноним') as UserName
                                   FROM TrainerRatings r
                                   LEFT JOIN Users u ON r.UserID = u.UserID
                                   LEFT JOIN Clients c ON u.UserID = c.UserID
                                   WHERE r.TrainerID = @trainerId
                                   ORDER BY r.RatingDate DESC",
                                   new Dictionary<string, object> { { "@trainerId", trainerId } });
        }
        #endregion

        #region Отчеты
        // Универсальный метод для формирования отчета
        private static string GenerateReport(string title, string sqlQuery, Dictionary<string, object> parameters = null)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine(title);
            report.AppendLine();

            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                {
                    if (parameters != null)
                        foreach (var param in parameters)
                            cmd.Parameters.AddWithValue(param.Key, param.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int rowCount = 0;

                        while (reader.Read())
                        {
                            rowCount++;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string fieldName = reader.GetName(i);
                                string value = reader.IsDBNull(i) ? "Не указано" : reader[i].ToString();
                                report.AppendLine($"{fieldName}: {value}");
                            }
                            report.AppendLine();
                        }

                        if (rowCount == 0)
                            report.AppendLine("Данных по запросу не найдено.");
                    }
                }
            }

            return report.ToString();
        }

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
                    cmd.CommandText = @"SELECT to_char(s.ClassDate, 'Day') as День, COUNT(*) as Количество
                                      FROM Visits v 
                                      JOIN Schedule s ON v.ScheduleID = s.ScheduleID 
                                      WHERE s.ClassDate BETWEEN @startDate AND @endDate 
                                      GROUP BY to_char(s.ClassDate, 'Day') 
                                      ORDER BY to_char(s.ClassDate, 'Day')";

                    using (var reader = cmd.ExecuteReader())
                    {
                        report += "Посещения по дням недели:\r\n";
                        bool hasData = false;
                        while (reader.Read())
                        {
                            hasData = true;
                            report += $"{reader.GetString(0).Trim()}: {reader.GetInt32(1)}\r\n";
                        }

                        if (!hasData)
                            report += "Нет данных за указанный период\r\n";
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
                    cmd.CommandText = @"SELECT COALESCE(SUM(mt.Price), 0) FROM Memberships m 
                                      JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                                      WHERE m.IssueDate BETWEEN @startDate AND @endDate";
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    decimal totalSum = Convert.ToDecimal(cmd.ExecuteScalar());
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
                        bool hasData = false;
                        while (reader.Read())
                        {
                            hasData = true;
                            report += $"{reader.GetString(0)}: продано {reader.GetInt32(1)} шт. на сумму {reader.GetDecimal(2):C}\r\n";
                        }

                        if (!hasData)
                            report += "Нет данных за указанный период\r\n";
                    }
                }
            }

            return report;
        }

        // Отчет по истекшим абонементам
        public static string GetExpiredMembershipsReport(int monthsPeriod)
        {
            string sql = @"SELECT c.LastName, c.FirstName, c.Phone, c.Email, 
                         mt.TypeName, m.EndDate 
                         FROM Memberships m 
                         JOIN Clients c ON m.ClientID = c.ClientID 
                         JOIN MembershipTypes mt ON m.TypeID = mt.TypeID 
                         WHERE m.IsActive = false AND m.EndDate > CURRENT_DATE - INTERVAL '@monthsPeriod months' 
                         ORDER BY m.EndDate DESC";

            sql = sql.Replace("@monthsPeriod", monthsPeriod.ToString());

            string title = $"Отчет по клиентам с истекшими абонементами за последние {monthsPeriod} месяцев\r\n";
            var report = new System.Text.StringBuilder(title);
            report.AppendLine();

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
                            report.AppendLine($"{counter}. {reader.GetString(0)} {reader.GetString(1)}");
                            report.AppendLine($"   Телефон: {reader.GetString(2)}, Email: {(!reader.IsDBNull(3) ? reader.GetString(3) : "Не указан")}");
                            report.AppendLine($"   Абонемент: {reader.GetString(4)}");
                            report.AppendLine($"   Дата окончания: {reader.GetDateTime(5).ToShortDateString()}");
                            report.AppendLine();
                        }

                        if (counter == 0)
                            report.AppendLine("Клиентов с истекшими абонементами за указанный период не найдено.");
                    }
                }
            }

            return report.ToString();
        }

        // Экспорт отчета в файл
        public static bool ExportReportToFile(string reportText, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, reportText);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Метод-диспетчер для экспорта отчета
        public static bool ExportReport(string reportText, string filePath, string format)
        {
            try
            {
                switch (format.ToLower())
                {
                    case "txt":
                        return ExportReportToFile(reportText, filePath);
                    case "pdf":
                        return ExportReportToPDF(reportText, filePath);
                    case "excel":
                    case "xlsx":
                        return ExportReportToExcel(reportText, filePath);
                    default:
                        MessageBox.Show($"Неизвестный формат отчета: {format}",
                                       "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте отчета: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Экспорт отчета в PDF
        public static bool ExportReportToPDF(string reportText, string filePath)
        {
            try
            {
                // Создаем обычный текстовый файл как запасной вариант
                File.WriteAllText(filePath, reportText);

                try
                {
                    // Создаем новый PDF документ
                    using (var document = new iTextSharp.text.Document())
                    {
                        // Создаем writer для записи в PDF
                        var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(
                            document, new FileStream(filePath, FileMode.Create));

                        document.Open();

                        // Разбиваем текст отчета на строки
                        string[] lines = reportText.Split(
                            new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                        // Извлекаем заголовок (первую строку)
                        string title = lines.Length > 0 ? lines[0] : "Отчет";

                        // Добавляем заголовок с форматированием
                        var titleFont = iTextSharp.text.FontFactory.GetFont(
                            iTextSharp.text.FontFactory.HELVETICA_BOLD, 16);
                        document.Add(new iTextSharp.text.Paragraph(title, titleFont));
                        document.Add(new iTextSharp.text.Paragraph(" ")); // интервал

                        // Добавляем основной текст
                        var contentFont = iTextSharp.text.FontFactory.GetFont(
                            iTextSharp.text.FontFactory.HELVETICA, 12);
                        for (int i = 1; i < lines.Length; i++)
                        {
                            document.Add(new iTextSharp.text.Paragraph(lines[i], contentFont));
                        }

                        document.Close();
                        writer.Close();

                        return true;
                    }
                }
                catch (Exception)
                {
                    // Если iTextSharp не установлен или произошла ошибка, 
                    // продолжаем работу с текстовым файлом
                    MessageBox.Show("PDF экспорт не доступен, отчет сохранен как текстовый файл.\n" +
                                   "Для экспорта в PDF необходимо установить библиотеку iTextSharp:\n" +
                                   "Install-Package iTextSharp -Version 5.5.13.3",
                                   "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте отчета: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Экспорт отчета в Excel
        public static bool ExportReportToExcel(string reportText, string filePath)
        {
            try
            {
                // Создаем обычный текстовый файл как запасной вариант
                File.WriteAllText(filePath, reportText);

                try
                {
                    // Включаем LicenseContext.NonCommercial для EPPlus
                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                    // Создаем новый Excel пакет
                    using (var package = new OfficeOpenXml.ExcelPackage())
                    {
                        // Добавляем новый лист
                        var worksheet = package.Workbook.Worksheets.Add("Отчет");

                        // Разбиваем текст отчета на строки
                        string[] lines = reportText.Split(
                            new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                        // Извлекаем заголовок (первую строку)
                        string title = lines.Length > 0 ? lines[0] : "Отчет";

                        // Добавляем заголовок с форматированием
                        worksheet.Cells[1, 1].Value = title;
                        worksheet.Cells[1, 1].Style.Font.Bold = true;
                        worksheet.Cells[1, 1].Style.Font.Size = 14;

                        // Добавляем основной текст
                        for (int i = 1; i < lines.Length; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = lines[i];
                        }

                        // Авторазмер столбцов
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // Сохраняем в файл
                        package.SaveAs(new FileInfo(filePath));

                        return true;
                    }
                }
                catch (Exception)
                {
                    // Если EPPlus не установлен или произошла ошибка, 
                    // продолжаем работу с текстовым файлом
                    MessageBox.Show("Excel экспорт не доступен, отчет сохранен как текстовый файл.\n" +
                                   "Для экспорта в Excel необходимо установить библиотеку EPPlus:\n" +
                                   "Install-Package EPPlus -Version 4.5.3.3",
                                   "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте отчета: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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

        // Вычисляемые свойства
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {(string.IsNullOrEmpty(MiddleName) ? "" : MiddleName)}".Trim();
            }
        }

        public int Age
        {
            get
            {
                if (!BirthDate.HasValue) return 0;

                int age = DateTime.Now.Year - BirthDate.Value.Year;
                if (DateTime.Now.DayOfYear < BirthDate.Value.DayOfYear)
                    age--;

                return age;
            }
        }
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

        // Вычисляемые свойства
        public bool IsExpired
        {
            get { return DateTime.Now > EndDate; }
        }

        public int DaysLeft
        {
            get
            {
                if (IsExpired) return 0;
                return (EndDate - DateTime.Now).Days;
            }
        }
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

        // Вычисляемые свойства
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {(string.IsNullOrEmpty(MiddleName) ? "" : MiddleName)}".Trim();
            }
        }
    }

    public class ScheduleEntry
    {
        public int ScheduleID { get; set; }
        public int ClassID { get; set; }
        public int TrainerID { get; set; }
        public DateTime ClassDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int MaxParticipants { get; set; }

        // Вычисляемые свойства
        public DateTime FullStartTime
        {
            get { return ClassDate.Add(StartTime); }
        }

        public bool IsInPast
        {
            get { return DateTime.Now > FullStartTime; }
        }
    }
    #endregion
}