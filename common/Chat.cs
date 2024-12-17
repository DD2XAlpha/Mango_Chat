using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Mango_Chat.common
{

    public class Chat
    {
        public string? Chat_ID { get; set; }
        public string? Name { get; set; }
        public bool Check_If_DB_Exists()
        {
            return false;
        }

        public void Get_List()
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE Chat (
                        Chat_ID TEXT NOT NULL,
                        Name TEXT NOT NULL
                    );
                    
                    CREATE TABLE Message (
                        Chat_ID TEXT NOT NULL
                        Messages_ID TEXT NOT NULL
                        Role TEXT NOT NULL
                        Content TEXT NOT NULL
                        DateOfInteraction TEXT NOT NULL
                    )
                    
                     CREATE TABLE Tool (
                        Messages_ID TEXT NOT NULL
                        Role TEXT NOT NULL
                        Arguments TEXT NOT NULL
                        Data TEXT NOT NULL
                    );
                    ";

                command.ExecuteNonQuery();
            }
        }

        public static List<Chat> Get_Chats()
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                string query =
                @"
                    SELECT *
                    FROM Chat
                ";

                return connection.Query<Chat>(query).AsList();
            }
        }

        public void Insert()
        {
            Guid uuid = Guid.NewGuid();

            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                connection.Open();
                connection.Execute("INSERT INTO Chat (Chat_ID, Name) VALUES (@Chat_ID,@Name)", new { Name = Name, Chat_ID = uuid.ToString() });
            }
        }

        public static void Delete(string Chat_ID)
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                connection.Open();
                connection.Execute("DELETE FROM Chat WHERE Chat_ID = @Chat_ID", new { Chat_ID = Chat_ID });
                connection.Execute("DELETE FROM Messages WHERE Chat_ID = @Chat_ID", new { Chat_ID = Chat_ID });
                connection.Execute("DELETE FROM Tool WHERE Chat_ID = @Chat_ID", new { Chat_ID = Chat_ID });
            }
        }
    }

    public class Message
    {
        public string? Chat_ID { get; set; }
        public string? Message_ID { get; set; }
        public string? Role { get; set; }
        public string? Content { get; set; }
        public DateTime DateOfInteraction { get; set; }
        public List<Tool>? Tool_List { get; set; }

        public static List<Message> Get_Messages(string? Chat_ID)
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                string query = $" SELECT * FROM Message WHERE Chat_ID = {Chat_ID}";
                return connection.Query<Message>(query).AsList();
            }
        }

        public void Insert()
        {
            Guid uuid = Guid.NewGuid();

            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                string query =
                    @"INSERT INTO Message (Chat_ID, Message_ID, Role, Content, DateOfInteraction) 
                        VALUES (@Chat_ID, @Message_ID, @Role, @Content, @DateOfInteraction)";

                connection.Open();

                connection.Execute(query,
                    new
                    {
                        Message_ID = uuid.ToString(),
                        Chat_ID = Chat_ID,
                        Role = Role,
                        Content = Content,
                        DateOfInteraction = DateTime.Now.ToString()
                    }
                );
            }
        }

        public static void Delete(string Message_ID)
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                connection.Open();
                connection.Execute("DELETE FROM Message WHERE Message_ID = @Message_ID", new { Message_ID = Message_ID });
                connection.Execute("DELETE FROM Tool WHERE Message_ID = @Message_ID", new { Message_ID = Message_ID });
            }
        }


    }

    public class Tool
    {
        public string? Chat_ID { get; set; }
        public string? Message_ID { get; set; }
        public string? Name { get; set; }
        public string? Arguments { get; set; }
        public string? Data { get; set; }
        public string? ID { get; set; }

        public static List<Tool> Get_Messages(string? Message_ID)
        {
            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                string query = $" SELECT * FROM Tool WHERE Message_ID = {Message_ID}";
                return connection.Query<Tool>(query).AsList();
            }
        }

        public void Insert()
        {
            Guid uuid = Guid.NewGuid();

            using (var connection = new SqliteConnection("Data Source=Chat.db"))
            {
                string query =
                    @"INSERT INTO Tool (Message_ID, Name, Arguments, Data, ID, Chat_ID) 
                            VALUES (@Message_ID, @Name, @Arguments, @Data, @ID, @Chat_ID)";

                connection.Open();

                connection.Execute(query,
                    new
                    {
                        Message_ID = Message_ID,
                        Name = Name,
                        Arguments = Arguments,
                        Data = Data,
                        ID = ID,
                        Chat_ID = Chat_ID
                    }
                );
            }
        }


    }


}