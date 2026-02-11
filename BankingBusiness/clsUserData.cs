using Banking_DataAccess;
using BankingBusiness;
using System;
using System.Data;
using System.Data.SqlClient;


public static class clsUserData
{

    private static string ConnectionString = clsDataAccessSettings.ConnectionString;
    private static string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }



    public static clsUser AuthenticateUser(string username, string password)
    {
        clsUser user = null;

        // First try with hashed password (for new users)
        string hashedPassword = HashPassword(password);
        user = AuthenticateWithPassword(username, hashedPassword);

        // If hashed password fails, try with plain text (for existing users)
        if (user == null)
        {
            user = AuthenticateWithPassword(username, password);

            // If plain text works, automatically hash the password in database
            if (user != null)
            {
                Console.WriteLine("Converting plain text password to hashed...");
                HashUserPassword(user.UserID, password);
            }
        }

        return user;
    }

    private static clsUser AuthenticateWithPassword(string username, string passwordToTry)
    {
        clsUser user = null;

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = @"SELECT UserID, Username, Password, CreatedAt, IsActive,
                               CanAddClient, CanManageUser, CanShowClient,
                               CanUpdateClient, CanTransaction, CanFindDeleteClient
                        FROM Users
                        WHERE Username = @Username AND Password = @Password AND IsActive = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", passwordToTry);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new clsUser
                            {
                                UserID = (int)reader["UserID"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                IsActive = (bool)reader["IsActive"],
                                CanAddClient = reader["CanAddClient"] != DBNull.Value && (bool)reader["CanAddClient"],
                                CanManageUser = reader["CanManageUser"] != DBNull.Value && (bool)reader["CanManageUser"],
                                CanShowClient = reader["CanShowClient"] != DBNull.Value && (bool)reader["CanShowClient"],
                                CanUpdateClient = reader["CanUpdateClient"] != DBNull.Value && (bool)reader["CanUpdateClient"],
                                CanTransaction = reader["CanTransaction"] != DBNull.Value && (bool)reader["CanTransaction"],
                                CanFindDeleteClient = reader["CanFindDeleteClient"] != DBNull.Value && (bool)reader["CanFindDeleteClient"]
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Authentication error: {ex.Message}");
                }
            }
        }

        return user;
    }

    private static void HashUserPassword(int userID, string plainPassword)
    {
        string hashedPassword = HashPassword(plainPassword);

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = "UPDATE Users SET Password = @Password WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Password hashed for user ID: {userID}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error hashing password: {ex.Message}");
                }
            }
        }
    }




    // Optional: Add a method to get user by ID for permission checking
    public static clsUser GetUserByID(int userID)
    {
        clsUser user = null;

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = @"SELECT UserID, Username, Password, CreatedAt, IsActive,
                                   CanAddClient, CanManageUser, CanShowClient, 
                                   CanUpdateClient, CanTransaction, CanFindDeleteClient
                            FROM Users 
                            WHERE UserID = @UserID AND IsActive = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new clsUser
                            {
                                UserID = (int)reader["UserID"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                IsActive = (bool)reader["IsActive"],
                                CanAddClient = reader["CanAddClient"] != DBNull.Value && (bool)reader["CanAddClient"],
                                CanManageUser = reader["CanManageUser"] != DBNull.Value && (bool)reader["CanManageUser"],
                                CanShowClient = reader["CanShowClient"] != DBNull.Value && (bool)reader["CanShowClient"],
                                CanUpdateClient = reader["CanUpdateClient"] != DBNull.Value && (bool)reader["CanUpdateClient"],
                                CanTransaction = reader["CanTransaction"] != DBNull.Value && (bool)reader["CanTransaction"],
                                CanFindDeleteClient = reader["CanFindDeleteClient"] != DBNull.Value && (bool)reader["CanFindDeleteClient"]
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving user: {ex.Message}");
                }
            }
        }

        return user;
    }



    public static bool IsUsernameExists(string username, int excludeUserID = 0)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

            if (excludeUserID > 0)
            {
                query += " AND UserID != @ExcludeUserID";
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                if (excludeUserID > 0)
                {
                    command.Parameters.AddWithValue("@ExcludeUserID", excludeUserID);
                }

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return (count > 0);
                }
                catch (Exception ex)
                {
                    //    MessageBox.Show("Error checking username: " + ex.Message, "Error",
                    //                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true; // Assume exists to prevent duplicates
                }
            }
        }
    }



   public static bool AddUser(string username, string password,
                         bool canAddClient, bool canManageUser, bool canShowClient,
                         bool canUpdateClient, bool canTransaction, bool canFindDeleteClient)
{
    if (IsUsernameExists(username))
    {
        return false;
    }

    // Hash the password before storing
    string hashedPassword = HashPassword(password);
    
    int rowsAffected = 0;

    using (SqlConnection connection = new SqlConnection(ConnectionString))
    {
        string query = @"INSERT INTO Users 
                        (Username, Password, CreatedAt, IsActive,
                         CanAddClient, CanManageUser, CanShowClient, 
                         CanUpdateClient, CanTransaction, CanFindDeleteClient)
                        VALUES 
                        (@Username, @Password, @CreatedAt, @IsActive,
                         @CanAddClient, @CanManageUser, @CanShowClient,
                         @CanUpdateClient, @CanTransaction, @CanFindDeleteClient)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            // Add parameters - use hashedPassword instead of plain password
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", hashedPassword);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@CanAddClient", canAddClient);
            command.Parameters.AddWithValue("@CanManageUser", canManageUser);
            command.Parameters.AddWithValue("@CanShowClient", canShowClient);
            command.Parameters.AddWithValue("@CanUpdateClient", canUpdateClient);
            command.Parameters.AddWithValue("@CanTransaction", canTransaction);
            command.Parameters.AddWithValue("@CanFindDeleteClient", canFindDeleteClient);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    return (rowsAffected > 0);
}

public static bool UpdateUser(int userID, string username, string password,
                            bool canAddClient, bool canManageUser, bool canShowClient,
                            bool canUpdateClient, bool canTransaction, bool canFindDeleteClient)
{
    if (IsUsernameExists(username, userID))
    {
        return false;
    }

    // Hash the password before updating
    string hashedPassword = HashPassword(password);
    
    int rowsAffected = 0;

    using (SqlConnection connection = new SqlConnection(ConnectionString))
    {
        string query = @"UPDATE Users 
                        SET Username = @Username, 
                            Password = @Password,
                            CanAddClient = @CanAddClient,
                            CanManageUser = @CanManageUser,
                            CanShowClient = @CanShowClient,
                            CanUpdateClient = @CanUpdateClient,
                            CanTransaction = @CanTransaction,
                            CanFindDeleteClient = @CanFindDeleteClient
                        WHERE UserID = @UserID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", hashedPassword); // Use hashed password
            command.Parameters.AddWithValue("@CanAddClient", canAddClient);
            command.Parameters.AddWithValue("@CanManageUser", canManageUser);
            command.Parameters.AddWithValue("@CanShowClient", canShowClient);
            command.Parameters.AddWithValue("@CanUpdateClient", canUpdateClient);
            command.Parameters.AddWithValue("@CanTransaction", canTransaction);
            command.Parameters.AddWithValue("@CanFindDeleteClient", canFindDeleteClient);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    return (rowsAffected > 0);
}
    public static bool DeleteUser(int userID)
    {
        int rowsAffected = 0;

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error deleting user: " + ex.Message, "Error",
                    //              MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        return (rowsAffected > 0);
    }

    public static DataTable GetAllUsers()
    {
        DataTable usersTable = new DataTable();

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string query = @"SELECT UserID, Username, Password, CreatedAt, IsActive,
                            CanAddClient, CanManageUser, CanShowClient,
                            CanUpdateClient, CanTransaction, CanFindDeleteClient
                            FROM Users ORDER BY Username";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    usersTable.Load(reader);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error retrieving users: " + ex.Message, "Error",
                    //              MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        return usersTable;
    }

    
}