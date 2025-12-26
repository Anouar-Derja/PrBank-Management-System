-- Create the database
CREATE DATABASE Bank;
GO

USE Bank;
GO

-- Create Users table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);
GO

-- Create Customer table
CREATE TABLE Customer (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    AccountNumber NVARCHAR(20) NOT NULL UNIQUE,
    PinCode NVARCHAR(4) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    DateOfBirth DATE NOT NULL,
    AccountBalance DECIMAL(18,2) DEFAULT 0.00,
    AccountStatus NVARCHAR(20) DEFAULT 'Active',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Create TransactionType table
CREATE TABLE TransactionType (
    TransactionTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(20) NOT NULL UNIQUE,
    Description NVARCHAR(100)
);
GO

-- Create Transaction table (named BankTransaction to avoid keyword conflict)
CREATE TABLE BankTransaction (
    TransactionID INT IDENTITY(1,1) PRIMARY KEY,
    TransactionTypeID INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    BalanceAfter DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    Description NVARCHAR(255),
    FromAccount NVARCHAR(20),
    ToAccount NVARCHAR(20),
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (TransactionTypeID) REFERENCES TransactionType(TransactionTypeID)
);
GO

-- Insert default transaction types
INSERT INTO TransactionType (TypeName, Description) VALUES
('Deposit', 'Money deposited into account'),
('Withdraw', 'Money withdrawn from account'),
('Transfer', 'Money transferred between accounts');
GO

-- Create indexes for better performance
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Customer_AccountNumber ON Customer(AccountNumber);
CREATE INDEX IX_Customer_UserID ON Customer(UserID);
CREATE INDEX IX_BankTransaction_UserID ON BankTransaction(UserID);
CREATE INDEX IX_BankTransaction_TransactionDate ON BankTransaction(TransactionDate);
GO

-- Insert sample data
INSERT INTO Users (Username, Password) VALUES
('john_doe', 'hashed_password_1'),
('jane_smith', 'hashed_password_2'),
('bob_wilson', 'hashed_password_3');
GO

INSERT INTO Customer (AccountNumber, PinCode, FirstName, LastName, Phone, DateOfBirth, AccountBalance, UserID) VALUES
('CHK00123456', '1234', 'John', 'Doe', '+1-555-0101', '1985-03-15', 2500.75, 1),
('SAV00987654', '5678', 'Jane', 'Smith', '+1-555-0102', '1990-07-22', 18500.00, 2),
('BUS00543210', '9012', 'Bob', 'Wilson', '+1-555-0103', '1978-11-30', 50000.25, 3);
GO

-- Sample transactions
INSERT INTO BankTransaction (TransactionTypeID, Amount, BalanceAfter, UserID, Description, FromAccount, ToAccount) VALUES
(1, 500.00, 500.00, 1, 'Initial deposit', NULL, 'CHK00123456'),
(1, 2000.75, 2500.75, 1, 'Second deposit', NULL, 'CHK00123456'),
(2, 200.00, 2300.75, 1, 'ATM withdrawal', 'CHK00123456', NULL);
GO

-- Create useful views
CREATE VIEW CustomerDetails AS
SELECT 
    c.CustomerID,
    c.AccountNumber,
    c.FirstName,
    c.LastName,
    c.Phone,
    c.AccountBalance,
    c.AccountStatus,
    u.Username
FROM Customer c
INNER JOIN Users u ON c.UserID = u.UserID;
GO

CREATE VIEW TransactionHistory AS
SELECT 
    t.TransactionID,
    tt.TypeName AS TransactionType,
    t.Amount,
    t.BalanceAfter,
    t.TransactionDate,
    t.Description,
    t.FromAccount,
    t.ToAccount,
    u.Username
FROM BankTransaction t
INNER JOIN TransactionType tt ON t.TransactionTypeID = tt.TransactionTypeID
INNER JOIN Users u ON t.UserID = u.UserID;
GO

-- Create stored procedures
CREATE PROCEDURE sp_Deposit
    @AccountNumber NVARCHAR(20),
    @Amount DECIMAL(18,2),
    @UserID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Update customer balance
        UPDATE Customer 
        SET AccountBalance = AccountBalance + @Amount
        WHERE AccountNumber = @AccountNumber;
        
        -- Get new balance
        DECLARE @NewBalance DECIMAL(18,2);
        SELECT @NewBalance = AccountBalance FROM Customer WHERE AccountNumber = @AccountNumber;
        
        -- Record transaction
        INSERT INTO BankTransaction (TransactionTypeID, Amount, BalanceAfter, UserID, Description, ToAccount)
        VALUES (1, @Amount, @NewBalance, @UserID, 'Deposit transaction', @AccountNumber);
        
        COMMIT TRANSACTION;
        SELECT 'Success' AS Status, @NewBalance AS NewBalance;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'Error' AS Status, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO

CREATE PROCEDURE sp_Withdraw
    @AccountNumber NVARCHAR(20),
    @PinCode NVARCHAR(4),
    @Amount DECIMAL(18,2),
    @UserID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verify PIN and sufficient funds
        IF EXISTS (SELECT 1 FROM Customer WHERE AccountNumber = @AccountNumber AND PinCode = @PinCode AND AccountBalance >= @Amount)
        BEGIN
            -- Update customer balance
            UPDATE Customer 
            SET AccountBalance = AccountBalance - @Amount
            WHERE AccountNumber = @AccountNumber;
            
            -- Get new balance
            DECLARE @NewBalance DECIMAL(18,2);
            SELECT @NewBalance = AccountBalance FROM Customer WHERE AccountNumber = @AccountNumber;
            
            -- Record transaction
            INSERT INTO BankTransaction (TransactionTypeID, Amount, BalanceAfter, UserID, Description, FromAccount)
            VALUES (2, @Amount, @NewBalance, @UserID, 'Withdrawal transaction', @AccountNumber);
            
            COMMIT TRANSACTION;
            SELECT 'Success' AS Status, @NewBalance AS NewBalance;
        END
        ELSE
        BEGIN
            SELECT 'Error' AS Status, 'Invalid PIN or insufficient funds' AS Message;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'Error' AS Status, ERROR_MESSAGE() AS Message;
    END CATCH
END;
GO

-- Test the database
SELECT * FROM CustomerDetails;
SELECT * FROM TransactionHistory;