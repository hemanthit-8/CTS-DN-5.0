-- =====================================================================
-- SQL Exercise - Stored Procedures (Employee Management System)
-- =====================================================================

-- Database Schema
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    Salary DECIMAL(10,2),
    JoinDate DATE
);

-- Sample Data
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Marketing');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
(1, 'John', 'Doe', 1, 5000.00, '2020-01-15'),
(2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'),
(3, 'Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
(4, 'Emily', 'Davis', 4, 5500.00, '2021-11-05');
GO

-- =====================================================================
-- Exercise 1: Create a Stored Procedure
-- Goal: Retrieve employee details by department, plus an insert procedure.
-- =====================================================================

-- 1a. Retrieve employee details by DepartmentID
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, DepartmentID, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- 1b. Insert a new employee
CREATE PROCEDURE sp_InsertEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DepartmentID INT,
    @Salary DECIMAL(10,2),
    @JoinDate DATE
AS
BEGIN
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
END;
GO

-- =====================================================================
-- Exercise 2: Modify a Stored Procedure
-- Goal: Include employee salary in the result.
-- =====================================================================

ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- =====================================================================
-- Exercise 3: Delete a Stored Procedure
-- Goal: Delete the stored procedure created in Exercise 1.
-- =====================================================================
-- (Demonstration only - uncomment to actually drop)
-- DROP PROCEDURE sp_InsertEmployee;

-- =====================================================================
-- Exercise 4: Execute a Stored Procedure
-- Goal: Execute sp_GetEmployeesByDepartment for a specific department.
-- =====================================================================

EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;
GO

-- =====================================================================
-- Exercise 5: Return Data from a Stored Procedure
-- Goal: Return the total number of employees in a department.
-- =====================================================================

CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT COUNT(*) AS EmployeeCount
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 1;
GO

-- =====================================================================
-- Exercise 6: Use Output Parameters in a Stored Procedure
-- Goal: Return total salary of employees in a department via OUTPUT.
-- =====================================================================

CREATE PROCEDURE sp_GetTotalSalaryByDepartment
    @DepartmentID INT,
    @TotalSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @TotalSalary = SUM(Salary)
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- Test
DECLARE @Total DECIMAL(10,2);
EXEC sp_GetTotalSalaryByDepartment @DepartmentID = 1, @TotalSalary = @Total OUTPUT;
SELECT @Total AS TotalSalaryForDept1;
GO

-- =====================================================================
-- Exercise 7: Create a Stored Procedure with Multiple Parameters
-- Goal: Update employee salary.
-- =====================================================================

CREATE PROCEDURE sp_UpdateEmployeeSalary
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees
    SET Salary = @NewSalary
    WHERE EmployeeID = @EmployeeID;
END;
GO

EXEC sp_UpdateEmployeeSalary 1, 5500.00;
GO

-- =====================================================================
-- Exercise 8: Create a Stored Procedure with Conditional Logic
-- Goal: Give a bonus to employees based on their department.
-- =====================================================================

CREATE PROCEDURE sp_GiveBonus
    @DepartmentID INT,
    @BonusAmount DECIMAL(10,2)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @DepartmentID)
    BEGIN
        UPDATE Employees
        SET Salary = Salary + @BonusAmount
        WHERE DepartmentID = @DepartmentID;
    END
    ELSE
    BEGIN
        PRINT 'Invalid DepartmentID. No bonus applied.';
    END
END;
GO

EXEC sp_GiveBonus 1, 500.00;
GO

-- =====================================================================
-- Exercise 9: Use Transactions in a Stored Procedure
-- Goal: Update employee salary with transaction-based data integrity.
-- =====================================================================

CREATE PROCEDURE sp_UpdateSalaryWithTransaction
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

EXEC sp_UpdateSalaryWithTransaction @EmployeeID = 2, @NewSalary = 6200.00;
GO

-- =====================================================================
-- Exercise 10: Use Dynamic SQL in a Stored Procedure
-- Goal: Retrieve employee details based on a flexible filter column/value.
-- =====================================================================

CREATE PROCEDURE sp_GetEmployeesDynamic
    @FilterColumn NVARCHAR(50),
    @FilterValue NVARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);

    -- Whitelist allowed columns to prevent SQL injection
    IF @FilterColumn NOT IN ('FirstName', 'LastName', 'DepartmentID', 'Salary')
    BEGIN
        RAISERROR('Invalid filter column.', 16, 1);
        RETURN;
    END

    SET @SQL = N'SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
                  FROM Employees
                  WHERE ' + QUOTENAME(@FilterColumn) + N' = @Value';

    EXEC sp_executesql @SQL, N'@Value NVARCHAR(100)', @Value = @FilterValue;
END;
GO

EXEC sp_GetEmployeesDynamic @FilterColumn = 'DepartmentID', @FilterValue = '3';
GO

-- =====================================================================
-- Exercise 11: Handle Errors in a Stored Procedure
-- Goal: Update employee salary with TRY...CATCH and a custom error message.
-- =====================================================================

CREATE PROCEDURE sp_UpdateSalarySafe
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID)
        BEGIN
            RAISERROR('Employee with the specified EmployeeID does not exist.', 16, 1);
            RETURN;
        END

        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;

        PRINT 'Salary updated successfully.';
    END TRY
    BEGIN CATCH
        PRINT 'An error occurred: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

EXEC sp_UpdateSalarySafe @EmployeeID = 99, @NewSalary = 4000.00; -- triggers custom error
EXEC sp_UpdateSalarySafe @EmployeeID = 1,  @NewSalary = 5800.00; -- succeeds
GO

-- =====================================================================
-- Cleanup (optional, for re-running the script)
-- =====================================================================
-- DROP PROCEDURE IF EXISTS sp_GetEmployeesByDepartment;
-- DROP PROCEDURE IF EXISTS sp_InsertEmployee;
-- DROP PROCEDURE IF EXISTS sp_GetEmployeeCountByDepartment;
-- DROP PROCEDURE IF EXISTS sp_GetTotalSalaryByDepartment;
-- DROP PROCEDURE IF EXISTS sp_UpdateEmployeeSalary;
-- DROP PROCEDURE IF EXISTS sp_GiveBonus;
-- DROP PROCEDURE IF EXISTS sp_UpdateSalaryWithTransaction;
-- DROP PROCEDURE IF EXISTS sp_GetEmployeesDynamic;
-- DROP PROCEDURE IF EXISTS sp_UpdateSalarySafe;