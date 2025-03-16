using System;
using System.Data;
using Npgsql;
using System.Configuration;

namespace FitnessClubManager
{
    // Простой класс для работы с базой данных
    public static class DatabaseManager
    {
        // Получаем строку подключения из конфига
        private static string ConnString = ConfigurationManager.ConnectionStrings["FitnessClubDB"].ConnectionString;

        #region Аутентификация
        // Проверка логина и пароля
        public static Tuple<int, string> AuthenticateUser(string login, string password)
        {
            int userId = -1;
            string roleName = "";

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            // Ищем пользователя с таким логином и паролем
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT UserID, RoleID FROM Users WHERE Login = '" + login + "' AND Password = '" + password + "'";

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                userId = reader.GetInt32(0);
                int roleId = reader.GetInt32(1);

                // Определяем название роли
                reader.Close();
                cmd.CommandText = "SELECT RoleName FROM Roles WHERE RoleID = " + roleId;
                roleName = (string)cmd.ExecuteScalar();

                // Обновляем время входа
                cmd.CommandText = "UPDATE Users SET LastLogin = CURRENT_TIMESTAMP WHERE UserID = " + userId;
                cmd.ExecuteNonQuery();
            }

            conn.Close();
            return new Tuple<int, string>(userId, roleName);
        }

        // Получение имени пользователя
        public static string GetUserName(int userId)
        {
            string name = "Пользователь";

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            // Пробуем найти среди клиентов
            cmd.CommandText = "SELECT LastName, FirstName FROM Clients WHERE UserID = " + userId;
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                name = reader.GetString(0) + " " + reader.GetString(1);
                reader.Close();
                conn.Close();
                return name;
            }

            reader.Close();

            // Пробуем найти среди тренеров
            cmd.CommandText = "SELECT LastName, FirstName FROM Trainers WHERE UserID = " + userId;
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                name = reader.GetString(0) + " " + reader.GetString(1);
                reader.Close();
                conn.Close();
                return name;
            }

            reader.Close();
            conn.Close();

            return name;
        }
        #endregion

        #region Клиенты
        // Получить список всех клиентов
        public static DataTable GetClients()
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var adapter = new NpgsqlDataAdapter("SELECT * FROM Clients ORDER BY LastName", conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить данные одного клиента
        public static Client GetClient(int clientId)
        {
            Client client = null;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Clients WHERE ClientID = " + clientId;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                client = new Client();
                client.ClientID = reader.GetInt32(0);
                client.UserID = reader.GetInt32(1);
                client.LastName = reader.GetString(2);
                client.FirstName = reader.GetString(3);

                if (!reader.IsDBNull(4))
                    client.MiddleName = reader.GetString(4);

                if (!reader.IsDBNull(5))
                    client.BirthDate = reader.GetDateTime(5);

                client.Phone = reader.GetString(6);

                if (!reader.IsDBNull(7))
                    client.Email = reader.GetString(7);

                client.RegistrationDate = reader.GetDateTime(8);
            }

            reader.Close();
            conn.Close();

            return client;
        }

        // Добавить нового клиента
        public static int AddClient(string lastName, string firstName, string middleName,
                                   DateTime? birthDate, string phone, string email)
        {
            int clientId = -1;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            // Создаем пользователя
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            string login = lastName.ToLower() + firstName.ToLower();
            cmd.CommandText = "INSERT INTO Users (Login, Password, RoleID) VALUES ('" + login + "', 'password', 3) RETURNING UserID";

            int userId = (int)cmd.ExecuteScalar();

            // Добавляем клиента
            cmd.CommandText = "INSERT INTO Clients (UserID, LastName, FirstName, MiddleName, BirthDate, Phone, Email, RegistrationDate) " +
                              "VALUES (" + userId + ", '" + lastName + "', '" + firstName + "', ";

            // Отчество
            if (string.IsNullOrEmpty(middleName))
                cmd.CommandText += "NULL, ";
            else
                cmd.CommandText += "'" + middleName + "', ";

            // Дата рождения
            if (!birthDate.HasValue)
                cmd.CommandText += "NULL, ";
            else
                cmd.CommandText += "'" + birthDate.Value.ToString("yyyy-MM-dd") + "', ";

            // Телефон и Email
            cmd.CommandText += "'" + phone + "', ";

            if (string.IsNullOrEmpty(email))
                cmd.CommandText += "NULL, ";
            else
                cmd.CommandText += "'" + email + "', ";

            cmd.CommandText += "CURRENT_DATE) RETURNING ClientID";

            clientId = (int)cmd.ExecuteScalar();

            conn.Close();
            return clientId;
        }

        // Обновить данные клиента
        public static bool UpdateClient(int clientId, string lastName, string firstName, string middleName,
                                       DateTime? birthDate, string phone, string email)
        {
            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "UPDATE Clients SET LastName = '" + lastName + "', " +
                              "FirstName = '" + firstName + "', ";

            // Отчество
            if (string.IsNullOrEmpty(middleName))
                cmd.CommandText += "MiddleName = NULL, ";
            else
                cmd.CommandText += "MiddleName = '" + middleName + "', ";

            // Дата рождения
            if (!birthDate.HasValue)
                cmd.CommandText += "BirthDate = NULL, ";
            else
                cmd.CommandText += "BirthDate = '" + birthDate.Value.ToString("yyyy-MM-dd") + "', ";

            // Телефон и Email
            cmd.CommandText += "Phone = '" + phone + "', ";

            if (string.IsNullOrEmpty(email))
                cmd.CommandText += "Email = NULL ";
            else
                cmd.CommandText += "Email = '" + email + "' ";

            cmd.CommandText += "WHERE ClientID = " + clientId;

            int result = cmd.ExecuteNonQuery();

            conn.Close();
            return result > 0;
        }
        #endregion

        #region Абонементы
        // Получить список абонементов
        public static DataTable GetMemberships(bool activeOnly)
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            string sql = "SELECT m.MembershipID, m.ClientID, c.LastName || ' ' || c.FirstName AS ClientName, " +
                         "m.TypeID, mt.TypeName, m.StartDate, m.EndDate, m.IssueDate, m.IsActive " +
                         "FROM Memberships m " +
                         "JOIN Clients c ON m.ClientID = c.ClientID " +
                         "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                         "WHERE m.IsActive = " + (activeOnly ? "true" : "false") + " " +
                         "ORDER BY m.EndDate";

            var adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить абонементы для конкретного клиента
        public static DataTable GetClientMemberships(int clientId)
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            string sql = "SELECT m.MembershipID, m.TypeID, mt.TypeName, m.StartDate, m.EndDate, " +
                         "m.IssueDate, m.IsActive, mt.Price " +
                         "FROM Memberships m " +
                         "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                         "WHERE m.ClientID = " + clientId + " " +
                         "ORDER BY m.IssueDate DESC";

            var adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить типы абонементов
        public static DataTable GetMembershipTypes()
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var adapter = new NpgsqlDataAdapter("SELECT * FROM MembershipTypes ORDER BY Price", conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Добавить новый абонемент
        public static int AddMembership(int clientId, int typeId, DateTime startDate)
        {
            int membershipId = -1;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            // Получаем длительность абонемента
            cmd.CommandText = "SELECT DurationDays FROM MembershipTypes WHERE TypeID = " + typeId;
            int days = Convert.ToInt32(cmd.ExecuteScalar());

            // Вычисляем дату окончания
            DateTime endDate = startDate.AddDays(days);

            // Добавляем абонемент
            cmd.CommandText = "INSERT INTO Memberships (ClientID, TypeID, StartDate, EndDate, IssueDate, IsActive) " +
                              "VALUES (" + clientId + ", " + typeId + ", '" + startDate.ToString("yyyy-MM-dd") + "', " +
                              "'" + endDate.ToString("yyyy-MM-dd") + "', CURRENT_DATE, true) " +
                              "RETURNING MembershipID";

            membershipId = (int)cmd.ExecuteScalar();

            conn.Close();
            return membershipId;
        }

        // Получить данные абонемента
        public static Membership GetMembership(int membershipId)
        {
            Membership membership = null;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT m.*, mt.DurationDays, mt.TypeName " +
                             "FROM Memberships m " +
                             "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                             "WHERE m.MembershipID = " + membershipId;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                membership = new Membership();
                membership.MembershipID = reader.GetInt32(0);
                membership.ClientID = reader.GetInt32(1);
                membership.TypeID = reader.GetInt32(2);
                membership.StartDate = reader.GetDateTime(3);
                membership.EndDate = reader.GetDateTime(4);
                membership.IssueDate = reader.GetDateTime(5);
                membership.IsActive = reader.GetBoolean(6);
                membership.DurationDays = reader.GetInt32(7);
                membership.TypeName = reader.GetString(8);
            }

            reader.Close();
            conn.Close();

            return membership;
        }

        // Продлить абонемент
        public static bool ExtendMembership(int membershipId, int extendDays)
        {
            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Memberships SET EndDate = EndDate + '" + extendDays + " days'::interval, " +
                             "IsActive = true WHERE MembershipID = " + membershipId;

            int result = cmd.ExecuteNonQuery();

            conn.Close();
            return result > 0;
        }
        #endregion

        #region Расписание
        // Получить расписание на определенную дату
        public static DataTable GetSchedule(DateTime date)
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            string sql = "SELECT s.ScheduleID, s.ClassID, c.ClassName, s.StartTime, " +
                         "c.Duration, s.TrainerID, t.LastName || ' ' || t.FirstName AS TrainerName, " +
                         "s.MaxParticipants " +
                         "FROM Schedule s " +
                         "JOIN Classes c ON s.ClassID = c.ClassID " +
                         "JOIN Trainers t ON s.TrainerID = t.TrainerID " +
                         "WHERE s.ClassDate = '" + date.ToString("yyyy-MM-dd") + "' " +
                         "ORDER BY s.StartTime";

            var adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить расписание тренера
        public static DataTable GetTrainerSchedule(int trainerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            string sql = "SELECT s.ScheduleID, s.ClassDate, c.ClassName, s.StartTime, " +
                         "c.Duration, s.MaxParticipants, COUNT(v.VisitID) AS ActualParticipants " +
                         "FROM Schedule s " +
                         "JOIN Classes c ON s.ClassID = c.ClassID " +
                         "LEFT JOIN Visits v ON v.ScheduleID = s.ScheduleID " +
                         "WHERE s.TrainerID = " + trainerId;

            if (startDate.HasValue)
            {
                sql += " AND s.ClassDate >= '" + startDate.Value.ToString("yyyy-MM-dd") + "'";
            }

            if (endDate.HasValue)
            {
                sql += " AND s.ClassDate <= '" + endDate.Value.ToString("yyyy-MM-dd") + "'";
            }

            sql += " GROUP BY s.ScheduleID, s.ClassDate, c.ClassName, s.StartTime, c.Duration, s.MaxParticipants";
            sql += " ORDER BY s.ClassDate, s.StartTime";

            var adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить список занятий
        public static DataTable GetClasses()
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            string sql = "SELECT c.ClassID, c.ClassName, c.Description, c.Duration, " +
                         "ct.ClassTypeID, ct.TypeName AS ClassTypeName " +
                         "FROM Classes c " +
                         "JOIN ClassTypes ct ON c.ClassTypeID = ct.ClassTypeID " +
                         "ORDER BY c.ClassName";

            var adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить данные занятия из расписания
        public static ScheduleEntry GetScheduleEntry(int scheduleId)
        {
            ScheduleEntry entry = null;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Schedule WHERE ScheduleID = " + scheduleId;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                entry = new ScheduleEntry();
                entry.ScheduleID = reader.GetInt32(0);
                entry.ClassID = reader.GetInt32(1);
                entry.TrainerID = reader.GetInt32(2);
                entry.ClassDate = reader.GetDateTime(3);
                entry.StartTime = reader.GetTimeSpan(4);
                entry.MaxParticipants = reader.GetInt32(5);
            }

            reader.Close();
            conn.Close();

            return entry;
        }
        #endregion

        #region Тренеры
        // Получить список тренеров
        public static DataTable GetTrainers()
        {
            var dt = new DataTable();

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var adapter = new NpgsqlDataAdapter("SELECT * FROM Trainers ORDER BY LastName", conn);
            adapter.Fill(dt);

            conn.Close();
            return dt;
        }

        // Получить данные тренера
        public static Trainer GetTrainer(int trainerId)
        {
            Trainer trainer = null;

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Trainers WHERE TrainerID = " + trainerId;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                trainer = new Trainer();
                trainer.TrainerID = reader.GetInt32(0);
                trainer.UserID = reader.GetInt32(1);
                trainer.LastName = reader.GetString(2);
                trainer.FirstName = reader.GetString(3);

                if (!reader.IsDBNull(4))
                    trainer.MiddleName = reader.GetString(4);

                if (!reader.IsDBNull(5))
                    trainer.Specialization = reader.GetString(5);

                if (!reader.IsDBNull(6))
                    trainer.Experience = reader.GetInt32(6);

                trainer.Phone = reader.GetString(7);

                if (!reader.IsDBNull(8))
                    trainer.Email = reader.GetString(8);
            }

            reader.Close();
            conn.Close();

            return trainer;
        }
        #endregion

        #region Отчеты
        // Отчет по посещаемости
        public static string GetAttendanceReport(DateTime startDate, DateTime endDate)
        {
            string report = "Отчет по посещаемости с " + startDate.ToShortDateString() +
                           " по " + endDate.ToShortDateString() + "\r\n\r\n";

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            // Общее количество посещений
            cmd.CommandText = "SELECT COUNT(*) FROM Visits v " +
                             "JOIN Schedule s ON v.ScheduleID = s.ScheduleID " +
                             "WHERE s.ClassDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" +
                             endDate.ToString("yyyy-MM-dd") + "'";

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            report += "Общее количество посещений: " + count + "\r\n\r\n";

            // Посещения по дням недели
            cmd.CommandText = "SELECT to_char(s.ClassDate, 'Day'), COUNT(*) " +
                             "FROM Visits v " +
                             "JOIN Schedule s ON v.ScheduleID = s.ScheduleID " +
                             "WHERE s.ClassDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" +
                             endDate.ToString("yyyy-MM-dd") + "' " +
                             "GROUP BY to_char(s.ClassDate, 'Day') " +
                             "ORDER BY to_char(s.ClassDate, 'Day')";

            var reader = cmd.ExecuteReader();
            report += "Посещения по дням недели:\r\n";

            while (reader.Read())
            {
                string day = reader.GetString(0).Trim();
                int visits = reader.GetInt32(1);
                report += day + ": " + visits + "\r\n";
            }

            conn.Close();
            return report;
        }

        // Отчет по доходам
        public static string GetRevenueReport(DateTime startDate, DateTime endDate)
        {
            string report = "Отчет по доходам за период с " + startDate.ToShortDateString() +
                           " по " + endDate.ToShortDateString() + "\r\n\r\n";

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            // Общая сумма
            cmd.CommandText = "SELECT SUM(mt.Price) FROM Memberships m " +
                             "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                             "WHERE m.IssueDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" +
                             endDate.ToString("yyyy-MM-dd") + "'";

            object sum = cmd.ExecuteScalar();
            decimal totalSum = sum != DBNull.Value ? Convert.ToDecimal(sum) : 0;

            report += "Общая сумма доходов: " + totalSum.ToString("C") + "\r\n\r\n";

            // По типам абонементов
            cmd.CommandText = "SELECT mt.TypeName, COUNT(*), SUM(mt.Price) " +
                             "FROM Memberships m " +
                             "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                             "WHERE m.IssueDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" +
                             endDate.ToString("yyyy-MM-dd") + "' " +
                             "GROUP BY mt.TypeName";

            var reader = cmd.ExecuteReader();
            report += "Доходы по типам абонементов:\r\n";

            while (reader.Read())
            {
                string typeName = reader.GetString(0);
                int count = reader.GetInt32(1);
                decimal sum2 = reader.GetDecimal(2);

                report += typeName + ": продано " + count + " шт. на сумму " + sum2.ToString("C") + "\r\n";
            }

            conn.Close();
            return report;
        }

        // Отчет по истекшим абонементам
        public static string GetExpiredMembershipsReport(int monthsPeriod)
        {
            string report = "Отчет по клиентам с истекшими абонементами\r\n\r\n";

            var conn = new NpgsqlConnection(ConnString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT c.LastName, c.FirstName, c.Phone, c.Email, " +
                             "mt.TypeName, m.EndDate " +
                             "FROM Memberships m " +
                             "JOIN Clients c ON m.ClientID = c.ClientID " +
                             "JOIN MembershipTypes mt ON m.TypeID = mt.TypeID " +
                             "WHERE m.IsActive = false AND m.EndDate > CURRENT_DATE - INTERVAL '" +
                             monthsPeriod + " months' " +
                             "ORDER BY m.EndDate DESC";

            var reader = cmd.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                counter++;
                string lastName = reader.GetString(0);
                string firstName = reader.GetString(1);
                string phone = reader.GetString(2);
                string email = !reader.IsDBNull(3) ? reader.GetString(3) : "Не указан";
                string typeName = reader.GetString(4);
                DateTime endDate = reader.GetDateTime(5);

                report += counter + ". " + lastName + " " + firstName + "\r\n";
                report += "   Телефон: " + phone + ", Email: " + email + "\r\n";
                report += "   Абонемент: " + typeName + "\r\n";
                report += "   Дата окончания: " + endDate.ToShortDateString() + "\r\n\r\n";
            }

            if (counter == 0)
            {
                report += "Клиентов с истекшими абонементами за указанный период не найдено.";
            }

            conn.Close();
            return report;
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