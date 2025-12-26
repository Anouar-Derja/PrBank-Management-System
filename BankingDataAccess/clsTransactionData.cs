using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Banking_DataAccess
{
    public class clsTransactionData
    {

            public static bool TransferFunds(string fromAccount, string toAccount, decimal amount, string description, int userID)
            {
                // Validation: Prevent transferring to same account
                if (fromAccount.Equals(toAccount, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                // Validation: Amount must be positive
                if (amount <= 0)
                {
                    return false;
                }

                bool success = false;

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Validate both accounts exist using the Customer table
                        if (!CustomerExists(connection, transaction, fromAccount) ||
                            !CustomerExists(connection, transaction, toAccount))
                        {
                            transaction.Rollback();
                            return false;
                        }

                        // 2. Get current balance of sender BEFORE the transfer
                        decimal senderBalanceBefore = GetCustomerBalance(connection, transaction, fromAccount);

                        // 3. Check if sender has sufficient funds
                        if (senderBalanceBefore < amount)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        // 4. Get current balance of recipient
                        decimal recipientBalanceBefore = GetCustomerBalance(connection, transaction, toAccount);

                        // 5. Debit from sender's account (UPDATE Customer table)
                        string debitQuery = @"UPDATE Customer 
                       SET AccountBalance = AccountBalance - @Amount 
                       WHERE AccountNumber = @FromAccount";

                        using (SqlCommand debitCommand = new SqlCommand(debitQuery, connection, transaction))
                        {
                            debitCommand.Parameters.AddWithValue("@Amount", amount);
                            debitCommand.Parameters.AddWithValue("@FromAccount", fromAccount);

                            int rowsAffected = debitCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // 6. Credit to receiver's account (UPDATE Customer table)
                        string creditQuery = @"UPDATE Customer 
                            SET AccountBalance = AccountBalance + @Amount 
                            WHERE AccountNumber = @ToAccount";

                        using (SqlCommand creditCommand = new SqlCommand(creditQuery, connection, transaction))
                        {
                            creditCommand.Parameters.AddWithValue("@Amount", amount);
                            creditCommand.Parameters.AddWithValue("@ToAccount", toAccount);

                            int rowsAffected = creditCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // 7. Calculate the balances AFTER the transfer
                        decimal senderBalanceAfter = senderBalanceBefore - amount;
                        decimal recipientBalanceAfter = recipientBalanceBefore + amount;

                        // 8. Record the transaction for SENDER (Transfer Out - Type 3)
                        RecordTransaction(connection, transaction, fromAccount, toAccount,
                                         -amount, senderBalanceAfter, description, userID, 3);

                        // 9. Record the transaction for RECIPIENT (Deposit - Type 1)
                        RecordTransaction(connection, transaction, toAccount, fromAccount,
                                         amount, recipientBalanceAfter, "Transfer from " + fromAccount, userID, 1);

                        transaction.Commit();
                        success = true;
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                }

                return success;
            }

            // Helper method to check if customer exists
            private static bool CustomerExists(SqlConnection connection, SqlTransaction transaction, string accountNumber)
            {
                string query = "SELECT COUNT(1) FROM Customer WHERE AccountNumber = @AccountNumber";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }

            // Helper method to get customer balance
            private static decimal GetCustomerBalance(SqlConnection connection, SqlTransaction transaction, string accountNumber)
            {
                string query = "SELECT AccountBalance FROM Customer WHERE AccountNumber = @AccountNumber";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    object result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            // Helper method to record transaction
            private static void RecordTransaction(SqlConnection connection, SqlTransaction transaction,
                                                string fromAccount, string toAccount, decimal amount,
                                                decimal balanceAfter, string description, int userID, int transactionTypeID)
            {
                string query = @"INSERT INTO BankTransaction 
            (TransactionTypeID, Amount, BalanceAfter, Description, FromAccount, ToAccount, TransactionDate, UserID) 
            VALUES (@TransactionTypeID, @Amount, @BalanceAfter, @Description, @FromAccount, @ToAccount, GETDATE(), @UserID)";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@TransactionTypeID", transactionTypeID);
                    command.Parameters.AddWithValue("@Amount", Math.Abs(amount));
                    command.Parameters.AddWithValue("@BalanceAfter", balanceAfter);
                    command.Parameters.AddWithValue("@Description", description);

                    // Handle FromAccount/ToAccount
                    command.Parameters.AddWithValue("@FromAccount", string.IsNullOrEmpty(fromAccount) ? (object)DBNull.Value : fromAccount);
                    command.Parameters.AddWithValue("@ToAccount", string.IsNullOrEmpty(toAccount) ? (object)DBNull.Value : toAccount);
                    command.Parameters.AddWithValue("@UserID", userID);

                    command.ExecuteNonQuery();
                }
            }

            public static bool TestConnection()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                    {
                        connection.Open();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public static DataRow GetTransactionByID(int transactionID)
            {
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT bt.*, tt.TypeName 
                 FROM BankTransaction bt
                 INNER JOIN TransactionTypes tt ON bt.TransactionTypeID = tt.TransactionTypeID
                 WHERE bt.TransactionID = @TransactionID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionID", transactionID);

                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                dt.Load(reader);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                return dt.Rows[0];
                            }
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                        }
                    }
                }
                return null;
            }

            public static bool GetTransactionTypeInfoByID(int transactionTypeID, ref string typeName, ref string description)
            {
                bool isFound = false;

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TransactionTypes WHERE TransactionTypeID = @TransactionTypeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionTypeID", transactionTypeID);

                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    isFound = true;
                                    typeName = reader["TypeName"].ToString();
                                    description = reader["Description"].ToString();
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

            public static DataTable GetAllTransactionTypes()
            {
                DataTable dt = new DataTable();

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM TransactionTypes ORDER BY TypeName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    dt.Load(reader);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                        }
                    }
                }

                return dt;
            }

            public static int AddNewTransactionType(string typeName, string description)
            {
                int transactionTypeID = -1;

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO TransactionTypes (TypeName, Description)
                 VALUES (@TypeName, @Description);
                 SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TypeName", typeName);
                        command.Parameters.AddWithValue("@Description", description);

                        try
                        {
                            connection.Open();
                            object result = command.ExecuteScalar();

                            if (result != null && int.TryParse(result.ToString(), out int insertedID))
                            {
                                transactionTypeID = insertedID;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                        }
                    }
                }

                return transactionTypeID;
            }

            // FIXED: Changed bt.UseID to bt.UserID
            public static DataTable GetAllBankTransactions()
            {
                DataTable dt = new DataTable();

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT bt.TransactionID, bt.TransactionTypeID, tt.TypeName, 
                 bt.Amount, bt.BalanceAfter, bt.TransactionDate, 
                 bt.Description, bt.FromAccount, bt.ToAccount, bt.UserID
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
                        }
                        catch (Exception ex)
                        {
                            // Error handling
                        }
                    }
                }

                return dt;
            }
        public static bool ProcessWithdrawal(string accountNumber, decimal amount, string description, int userID)
        {
            if (amount <= 0)
            {
                //MessageBox.Show("Withdrawal amount must be greater than zero!", "Validation Error");
                return false;
            }

            bool success = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Database connection failed: {ex.Message}", "Connection Error");
                    return false;
                }

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Validate account exists
                    if (!CustomerExists(connection, transaction, accountNumber))
                    {
                        transaction.Rollback();
                        //MessageBox.Show("Account does not exist!", "Validation Error");
                        return false;
                    }

                    // 2. Get current balance
                    decimal currentBalance = GetCustomerBalance(connection, transaction, accountNumber);

                    // 3. Check sufficient funds
                    if (currentBalance < amount)
                    {
                        transaction.Rollback();
                        //MessageBox.Show("Insufficient funds for withdrawal!", "Validation Error");
                        return false;
                    }

                    // 4. Update customer balance
                    string updateQuery = @"UPDATE Customer 
                           SET AccountBalance = AccountBalance - @Amount 
                           WHERE AccountNumber = @AccountNumber";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@Amount", amount);
                        updateCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            transaction.Rollback();
                            //MessageBox.Show("Failed to update account balance!", "Withdrawal Error");
                            return false;
                        }
                    }

                    // 5. Calculate new balance
                    decimal newBalance = currentBalance - amount;

                    // 6. Record the transaction (Withdrawal - Type 2)
                    RecordTransaction(connection, transaction,
                                    fromAccount: accountNumber,
                                    toAccount: null,
                                    amount: -amount,
                                    balanceAfter: newBalance,
                                    description: description,
                                    userID: userID,
                                    transactionTypeID: 2); // 2 = Withdrawal

                    transaction.Commit();
                    success = true;
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    //MessageBox.Show($"Database error: {sqlEx.Message}", "Withdrawal Error");
                    success = false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //MessageBox.Show($"Error during withdrawal: {ex.Message}", "Withdrawal Error");
                    success = false;
                }
            }

            return success;
        }
        public static bool ProcessDeposit(string accountNumber, decimal amount, string description, int userID)
            {
                if (amount <= 0)
                {
                    return false;
                }

                bool success = false;

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Validate account exists
                        if (!CustomerExists(connection, transaction, accountNumber))
                        {
                            transaction.Rollback();
                            return false;
                        }

                        // 2. Get current balance
                        decimal currentBalance = GetCustomerBalance(connection, transaction, accountNumber);

                        // 3. Update customer balance
                        string updateQuery = @"UPDATE Customer 
                       SET AccountBalance = AccountBalance + @Amount 
                       WHERE AccountNumber = @AccountNumber";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@Amount", amount);
                            updateCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // 4. Calculate new balance
                        decimal newBalance = currentBalance + amount;

                        // 5. Record the transaction (Deposit - Type 1)
                        RecordTransaction(connection, transaction,
                                        fromAccount: null,
                                        toAccount: accountNumber,
                                        amount: amount,
                                        balanceAfter: newBalance,
                                        description: description,
                                        userID: userID,
                                        transactionTypeID: 1); // 1 = Deposit

                        transaction.Commit();
                        success = true;
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                }

                return success;
            }
        
    }
}
