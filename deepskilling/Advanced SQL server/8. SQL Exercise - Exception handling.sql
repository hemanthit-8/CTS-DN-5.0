-- =====================================================================
-- SQL Exercise - Exception Handling (Employee Management System)
-- =====================================================================

-- Database Schema
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100) NOT NULL
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE,
    Salary DECIMAL(10, 2),
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

CREATE TABLE AuditLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    Action VARCHAR(100),
    ErrorMessage VARCHAR(4000),
    ActionDate DATETIME DEFAULT GETDATE()
);

-- Sample Data
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT');

INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID) VALUES
(1, 'John', 'Doe', 'john.doe@company.com', 5000.00, 1),
(2, 'Jane', 'Smith', 'jane.smith@company.com', 6000.00, 2);
GO

-- =====================================================================
-- Question 1: Basic TRY...CATCH for Error Logging
-- Scenario: Inserting an employee whose email might already exist.
-- =====================================================================

CREATE PROCEDURE AddEmployee
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);

        PRINT 'Employee added successfully.';
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', ERROR_MESSAGE());
    END CATCH
END;
GO

-- Test: duplicate email should fail and get logged
EXEC AddEmployee 3, 'Johnny', 'Doeson', 'john.doe@company.com', 5200.00, 1;
SELECT * FROM AuditLog;
GO

-- =====================================================================
-- Question 2: Using THROW to Re-raise Errors
-- Scenario: Log the error, but also propagate it to the caller/application.
-- =====================================================================

ALTER PROCEDURE AddEmployee
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);

        PRINT 'Employee added successfully.';
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', ERROR_MESSAGE());

        THROW;  -- re-raise the original error to the caller
    END CATCH
END;
GO

-- Test (will raise the original error to the client after logging)
-- EXEC AddEmployee 3, 'Johnny', 'Doeson', 'john.doe@company.com', 5200.00, 1;

-- =====================================================================
-- Question 3: Custom Error with RAISERROR
-- Scenario: Salary must be greater than 0.
-- =====================================================================

ALTER PROCEDURE AddEmployee
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        IF @Salary <= 0
        BEGIN
            RAISERROR('Salary must be greater than zero.', 16, 1);
            RETURN;
        END

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);

        PRINT 'Employee added successfully.';
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', ERROR_MESSAGE());

        THROW;
    END CATCH
END;
GO

-- Test
-- EXEC AddEmployee 4, 'Test', 'User', 'test.user@company.com', -100.00, 1; -- triggers custom error

-- =====================================================================
-- Question 4: Nested TRY...CATCH with RAISERROR
-- Scenario: Transfer an employee to a department; raise a custom
--           error if the target department doesn't exist.
-- =====================================================================

CREATE PROCEDURE TransferEmployee
    @EmployeeID INT,
    @NewDepartmentID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRY
            IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @NewDepartmentID)
            BEGIN
                RAISERROR('Target department does not exist.', 16, 1);
            END

            UPDATE Employees
            SET DepartmentID = @NewDepartmentID
            WHERE EmployeeID = @EmployeeID;

            PRINT 'Employee transferred successfully.';
        END TRY
        BEGIN CATCH
            -- Inner catch: log the error, then re-raise to outer block
            INSERT INTO AuditLog (Action, ErrorMessage)
            VALUES ('TransferEmployee - Inner', ERROR_MESSAGE());

            THROW;
        END CATCH
    END TRY
    BEGIN CATCH
        -- Outer catch: final handling / additional logging if needed
        PRINT 'Transfer failed: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

-- Test
EXEC TransferEmployee @EmployeeID = 1, @NewDepartmentID = 99; -- invalid department
SELECT * FROM AuditLog;
GO

-- =====================================================================
-- Question 5: Logging All Errors in a Transaction
-- Scenario: Insert multiple employee records; if any insert fails,
--           roll back everything and log the error.
-- =====================================================================

CREATE PROCEDURE BatchInsertEmployees
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (10, 'Alice', 'Walker', 'alice.walker@company.com', 5400.00, 1);

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (11, 'Brian', 'Lee', 'brian.lee@company.com', 5600.00, 2);

        -- Simulate a failure: duplicate email or duplicate PK below
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (12, 'Carla', 'Diaz', 'alice.walker@company.com', 5800.00, 3); -- duplicate email

        COMMIT TRANSACTION;
        PRINT 'Batch insert completed successfully.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('BatchInsertEmployees', ERROR_MESSAGE());

        PRINT 'Batch insert failed and was rolled back.';
    END CATCH
END;
GO

-- Test: none of the 3 inserts should persist due to rollback
EXEC BatchInsertEmployees;
SELECT * FROM Employees WHERE EmployeeID IN (10, 11, 12); -- should return no rows
SELECT * FROM AuditLog;
GO

-- =====================================================================
-- Question 6: Dynamic RAISERROR with Severity and State
-- Scenario: Raise different errors based on salary conditions.
-- =====================================================================

ALTER PROCEDURE AddEmployee
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        IF @Salary < 0
        BEGIN
            -- Severity 16: a serious, user-correctable error; halts execution
            RAISERROR('Salary cannot be negative.', 16, 1);
            RETURN;
        END
        ELSE IF @Salary < 1000
        BEGIN
            -- Severity 10: informational warning; does not stop execution
            RAISERROR('Warning: Salary is unusually low.', 10, 1) WITH NOWAIT;
        END

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);

        PRINT 'Employee added successfully.';
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', ERROR_MESSAGE());

        THROW;
    END CATCH
END;
GO

-- Test cases
EXEC AddEmployee 20, 'Low', 'Salary', 'low.salary@company.com', 500.00, 1;   -- warning, severity 10, still inserts
-- EXEC AddEmployee 21, 'Neg', 'Salary', 'neg.salary@company.com', -50.00, 1; -- error, severity 16, blocks insert
GO

-- =====================================================================
-- Cleanup (optional, for re-running the script)
-- =====================================================================
-- DROP PROCEDURE IF EXISTS AddEmployee;
-- DROP PROCEDURE IF EXISTS TransferEmployee;
-- DROP PROCEDURE IF EXISTS BatchInsertEmployees;