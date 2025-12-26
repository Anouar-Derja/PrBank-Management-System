using Banking_DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;


public static class clsCustomerData
{
    public static bool GetCustomerInfoByID(int CustomerID,
        ref string AccountNumber, ref string PinCode, ref string FirstName,
        ref string LastName, ref string Phone, ref DateTime DateOfBirth,
        ref decimal AccountBalance, ref string AccountStatus, ref DateTime CreatedAt,
        ref int UserID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CustomerID", CustomerID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                isFound = true;
                AccountNumber = reader["AccountNumber"].ToString();
                PinCode = reader["PinCode"].ToString();
                FirstName = reader["FirstName"].ToString();
                LastName = reader["LastName"].ToString();
                Phone = reader["Phone"].ToString();
                DateOfBirth = (DateTime)reader["DateOfBirth"];
                AccountBalance = (decimal)reader["AccountBalance"];
                AccountStatus = reader["AccountStatus"].ToString();
                CreatedAt = (DateTime)reader["CreatedAt"];
                UserID = (int)reader["UserID"];
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            isFound = false;
        }
        finally
        {
            connection.Close();
        }

        return isFound;
    }

    public static bool GetCustomerInfoByAccountNumber(string AccountNumber,
        ref int CustomerID, ref string PinCode, ref string FirstName,
        ref string LastName, ref string Phone, ref DateTime DateOfBirth,
        ref decimal AccountBalance, ref string AccountStatus, ref DateTime CreatedAt,
        ref int UserID)
    {
        bool isFound = false;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "SELECT * FROM Customer WHERE AccountNumber = @AccountNumber";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                isFound = true;
                CustomerID = (int)reader["CustomerID"];
                PinCode = reader["PinCode"].ToString();
                FirstName = reader["FirstName"].ToString();
                LastName = reader["LastName"].ToString();
                Phone = reader["Phone"].ToString();
                DateOfBirth = (DateTime)reader["DateOfBirth"];
                AccountBalance = (decimal)reader["AccountBalance"];
                AccountStatus = reader["AccountStatus"].ToString();
                CreatedAt = (DateTime)reader["CreatedAt"];
                UserID = (int)reader["UserID"];
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            isFound = false;
        }
        finally
        {
            connection.Close();
        }

        return isFound;
    }

    public static DataTable GetAllCustomers()
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query =
            @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                     c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                     c.CreatedAt, c.UserID, u.Username
              FROM Customer c
              INNER JOIN Users u ON c.UserID = u.UserID
              ORDER BY c.FirstName, c.LastName";

        SqlCommand command = new SqlCommand(query, connection);

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
            // Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return dt;
    }

    public static int AddNewCustomer(string AccountNumber, string PinCode,
    string FirstName, string LastName, string Phone, DateTime DateOfBirth,
    decimal AccountBalance, string AccountStatus, DateTime CreatedAt, int UserID)
    {
        int CustomerID = -1;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = @"INSERT INTO Customer 
                        (AccountNumber, PinCode, FirstName, LastName, Phone, 
                         DateOfBirth, AccountBalance, AccountStatus, CreatedAt, UserID)
                        VALUES (@AccountNumber, @PinCode, @FirstName, @LastName, @Phone, 
                                @DateOfBirth, @AccountBalance, @AccountStatus, @CreatedAt, @UserID);
                        SELECT SCOPE_IDENTITY();";

        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@AccountNumber", AccountNumber);
        command.Parameters.AddWithValue("@PinCode", PinCode);
        command.Parameters.AddWithValue("@FirstName", FirstName);
        command.Parameters.AddWithValue("@LastName", LastName);
        command.Parameters.AddWithValue("@Phone", Phone);
        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
        command.Parameters.AddWithValue("@AccountBalance", AccountBalance);
        command.Parameters.AddWithValue("@AccountStatus", AccountStatus);
        command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
        command.Parameters.AddWithValue("@UserID", UserID);

        try
        {
            connection.Open();
            object result = command.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out int insertedID))
            {
                CustomerID = insertedID;
            }
        }
        catch (Exception ex)
        {
            // Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return CustomerID;
    }

   

    // Additional method: Get Customer by Account Number (returns DataTable)
    public static DataTable GetCustomerByAccountNumber(string accountNumber)
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query =
            @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                     c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                     c.CreatedAt, c.UserID, u.Username
              FROM Customer c
              INNER JOIN Users u ON c.UserID = u.UserID
              WHERE c.AccountNumber = @AccountNumber";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@AccountNumber", accountNumber);

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
            // Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return dt;
    }

    // Additional method: Get Customer by UserID
    public static DataTable GetCustomerByUserID(int userID)
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query =
            @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                     c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                     c.CreatedAt, c.UserID, u.Username
              FROM Customer c
              INNER JOIN Users u ON c.UserID = u.UserID
              WHERE c.UserID = @UserID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserID", userID);

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
            // Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return dt;
    }

    // Additional method: Update Customer Information
    public static bool UpdateCustomer(int CustomerID, string FirstName, string LastName,
      string Phone, DateTime DateOfBirth, string AccountStatus)
    {
        int rowsAffected = 0;
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = @"UPDATE Customer 
                        SET FirstName = @FirstName, LastName = @LastName, 
                            Phone = @Phone, DateOfBirth = @DateOfBirth, 
                            AccountStatus = @AccountStatus
                        WHERE CustomerID = @CustomerID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@AccountStatus", AccountStatus);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error updating customer: " + ex.Message);
                    return false;
                }
            }
        }
        return (rowsAffected > 0);
    }

    public static bool UpdateAccountBalance(int CustomerID, decimal NewBalance)
    {
        int rowsAffected = 0;
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "UPDATE Customer SET AccountBalance = @NewBalance WHERE CustomerID = @CustomerID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NewBalance", NewBalance);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);

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
    // Add this method to your clsCustomerData class
    public static DataTable FindCustomer(string searchValue, string searchBy = "FirstName")
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "";

            switch (searchBy.ToLower())
            {
                case "accountnumber":
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.AccountNumber LIKE @SearchValue";
                    break;
                case "firstname":
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.FirstName LIKE @SearchValue";
                    break;
                case "lastname":
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.LastName LIKE @SearchValue";
                    break;
                case "phone":
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.Phone LIKE @SearchValue";
                    break;
                case "customerid":
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.CustomerID = @SearchValue";
                    break;
                default:
                    query = @"SELECT c.CustomerID, c.AccountNumber, c.PinCode, c.FirstName, c.LastName, 
                                 c.Phone, c.DateOfBirth, c.AccountBalance, c.AccountStatus, 
                                 c.CreatedAt, c.UserID, u.Username
                          FROM Customer c
                          INNER JOIN Users u ON c.UserID = u.UserID
                          WHERE c.FirstName LIKE @SearchValue OR c.LastName LIKE @SearchValue";
                    break;
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (searchBy.ToLower() == "customerid")
                {
                    command.Parameters.AddWithValue("@SearchValue", Convert.ToInt32(searchValue));
                }
                else
                {
                    command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");
                }

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
                    //MessageBox.Show("Error finding customer: " + ex.Message);
                }
            }
        }
        return dt;
    }
   

    // Additional method: Delete Customer
    public static bool DeleteCustomer(int CustomerID)
    {
        int rowsAffected = 0;

        SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CustomerID", CustomerID);

        try
        {
            connection.Open();
            rowsAffected = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }

        return (rowsAffected > 0);
    }
    public static bool DoesCustomerExist(int customerID)
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        using (SqlCommand command = new SqlCommand(
            "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID",
            connection))
        {
            command.Parameters.AddWithValue("@CustomerID", customerID);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            return (count > 0);
        }
    }
}