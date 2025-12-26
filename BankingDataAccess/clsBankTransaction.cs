using System;
using System.Data;
using System.Data.SqlClient;

namespace Banking_DataAccess
{
    public class clsBankTransaction
    {
        public static bool GetTransactionInfoByID(int TransactionID,
    ref int TransactionTypeID, ref decimal Amount, ref decimal BalanceAfter,
    ref DateTime TransactionDate, ref string Description, ref string FromAccount,
    ref string ToAccount, ref int UserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM BankTransaction WHERE TransactionID = @TransactionID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TransactionID", TransactionID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                TransactionTypeID = (int)reader["TransactionTypeID"];
                                Amount = (decimal)reader["Amount"];
                                BalanceAfter = (decimal)reader["BalanceAfter"];
                                TransactionDate = (DateTime)reader["TransactionDate"];
                                Description = reader["Description"].ToString();
                                FromAccount = reader["FromAccount"].ToString();
                                ToAccount = reader["ToAccount"].ToString();
                                UserID = (int)reader["UserID"]; // FIXED: Changed from UseID to UserID
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }


        public static DataTable GetAllBankTransaction()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                // First, test without the Users table join
                string query = @"SELECT bt.TransactionID, tt.TypeName, bt.Amount, 
                                bt.BalanceAfter, bt.TransactionDate, bt.Description,
                                bt.FromAccount, bt.ToAccount, bt.UserID
                         FROM BankTransaction bt
                         INNER JOIN TransactionTypes tt ON bt.TransactionTypeID = tt.TransactionTypeID
                         ORDER BY bt.TransactionDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }

                        // If this works, the issue is with the Users table join
                        //MessageBox.Show("Success without Users join. The issue is in the Users table relationship.");
                    }
                    catch (SqlException sqlEx)
                    {
                        //MessageBox.Show($"Error without Users join: {sqlEx.Message}");
                        throw;
                    }
                }
            }
            return dt;
        }

        public static int AddNewTransaction(int TransactionTypeID, decimal Amount,
     decimal BalanceAfter, DateTime TransactionDate, string Description,
     string FromAccount, string ToAccount, int UserID)
        {
            int TransactionID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                // FIXED: Changed UseID to UserID in INSERT statement
                string query = @"INSERT INTO BankTransaction 
                     (TransactionTypeID, Amount, BalanceAfter, TransactionDate, 
                      Description, FromAccount, ToAccount, UserID)
                     VALUES (@TransactionTypeID, @Amount, @BalanceAfter, @TransactionDate, 
                             @Description, @FromAccount, @ToAccount, @UserID);
                     SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TransactionTypeID", TransactionTypeID);
                    command.Parameters.AddWithValue("@Amount", Amount);
                    command.Parameters.AddWithValue("@BalanceAfter", BalanceAfter);
                    command.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@FromAccount", FromAccount);
                    command.Parameters.AddWithValue("@ToAccount", ToAccount);
                    command.Parameters.AddWithValue("@UserID", UserID); // FIXED: Parameter name

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TransactionID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Consider logging the error
                    }
                }
            }

            return TransactionID;
        }
        public static DataTable GetTransactionsByUserID(int UserID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM BankTransaction WHERE UserID = @UserID ORDER BY TransactionDate DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetTransactionsByAccount(string AccountNumber)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM BankTransaction 
                            WHERE FromAccount = @AccountNumber OR ToAccount = @AccountNumber 
                            ORDER BY TransactionDate DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}