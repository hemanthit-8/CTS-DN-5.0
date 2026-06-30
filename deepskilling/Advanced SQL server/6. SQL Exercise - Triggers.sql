-- =====================================================================
-- SQL Exercise - Triggers (Employee Management System)
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
    DepartmentID INT,
    Salary DECIMAL(10,2),
    JoinDate DATE,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- Sample Data
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Marketing');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
(1, 'John', 'Doe', 1, 5000.00, '2022-01-15'),
(2, 'Jane', 'Smith', 2, 6000.00, '2021-03-22'),
(3, 'Michael', 'Johnson', 3, 7000.00, '2020-07-30'),
(4, 'Emily', 'Davis', 4, 5500.00, '2019-11-05');
GO

-- =====================================================================
-- Exercise 1: Create an AFTER Trigger
-- Goal: Log changes in the Employees table when salary is updated.
-- =====================================================================

-- Step 1: Table to store change logs
CREATE TABLE EmployeeChanges (
    ChangeID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT,
    OldSalary DECIMAL(10,2),
    NewSalary DECIMAL(10,2),
    ChangeDate DATETIME DEFAULT GETDATE()
);
GO

-- Step 2: AFTER UPDATE trigger to log salary changes
CREATE TRIGGER trg_LogSalaryChange
ON Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Salary)
    BEGIN
        INSERT INTO EmployeeChanges (EmployeeID, OldSalary, NewSalary)
        SELECT i.EmployeeID, d.Salary, i.Salary
        FROM inserted i
        JOIN deleted d ON d.EmployeeID = i.EmployeeID
        WHERE i.Salary <> d.Salary;
    END
END;
GO

-- Test
UPDATE Employees SET Salary = 5200.00 WHERE EmployeeID = 1;
SELECT * FROM EmployeeChanges;
GO

-- =====================================================================
-- Exercise 2: Create an INSTEAD OF Trigger
-- Goal: Prevent deletions from the Employees table.
-- =====================================================================

CREATE TRIGGER trg_PreventEmployeeDelete
ON Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    RAISERROR('Deletion of employee records is not allowed.', 16, 1);
END;
GO

-- Test (should fail and raise the custom error)
-- DELETE FROM Employees WHERE EmployeeID = 1;

-- =====================================================================
-- Exercise 3: Create a LOGON Trigger
-- Goal: Restrict access to the database during maintenance hours
--       (e.g., 2 AM to 3 AM).
-- =====================================================================
-- NOTE: LOGON triggers are server-scoped (created in the master
-- database context) and affect the whole SQL Server instance, not a
-- single user database. Run this separately with appropriate privileges.

CREATE TRIGGER trg_RestrictMaintenanceLogon
ON ALL SERVER
FOR LOGON
AS
BEGIN
    DECLARE @CurrentHour INT = DATEPART(HOUR, GETDATE());

    IF @CurrentHour = 2  -- 2 AM to 3 AM maintenance window
    BEGIN
        ROLLBACK;
        -- Connection will be denied; SQL Server returns a generic
        -- logon failure message to the client for security reasons.
    END
END;
GO

-- To remove this trigger later:
-- DROP TRIGGER trg_RestrictMaintenanceLogon ON ALL SERVER;

-- =====================================================================
-- Exercise 4: Modify a Trigger using SSMS
-- Goal: Modify an existing trigger's logic.
-- (Below is the T-SQL equivalent of using SSMS's "Modify" option,
--  which generates an ALTER TRIGGER script.)
-- =====================================================================

ALTER TRIGGER trg_LogSalaryChange
ON Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Salary)
    BEGIN
        INSERT INTO EmployeeChanges (EmployeeID, OldSalary, NewSalary, ChangeDate)
        SELECT i.EmployeeID, d.Salary, i.Salary, GETDATE()
        FROM inserted i
        JOIN deleted d ON d.EmployeeID = i.EmployeeID
        WHERE i.Salary <> d.Salary;
    END
END;
GO

-- =====================================================================
-- Exercise 5: Delete a Trigger
-- Goal: Delete an existing trigger from the Employees table.
-- =====================================================================
-- (Demonstration only - uncomment to actually drop)
-- DROP TRIGGER trg_PreventEmployeeDelete;

-- =====================================================================
-- Exercise 6: Create a Trigger to Update a Computed Column
-- Goal: Maintain an AnnualSalary column = Salary * 12 whenever
--       Salary is updated.
-- =====================================================================

-- Step 1: Add the AnnualSalary column
ALTER TABLE Employees ADD AnnualSalary DECIMAL(10,2) NULL;
GO

-- Backfill existing rows
UPDATE Employees SET AnnualSalary = Salary * 12;
GO

-- Step 2: Trigger to keep AnnualSalary in sync with Salary
CREATE TRIGGER trg_UpdateAnnualSalary
ON Employees
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Salary)
    BEGIN
        UPDATE e
        SET e.AnnualSalary = i.Salary * 12
        FROM Employees e
        JOIN inserted i ON i.EmployeeID = e.EmployeeID;
    END
END;
GO

-- Test
UPDATE Employees SET Salary = 5300.00 WHERE EmployeeID = 1;
SELECT EmployeeID, Salary, AnnualSalary FROM Employees WHERE EmployeeID = 1;
GO

-- =====================================================================
-- Cleanup (optional, for re-running the script)
-- =====================================================================
-- DROP TRIGGER IF EXISTS trg_LogSalaryChange;
-- DROP TRIGGER IF EXISTS trg_PreventEmployeeDelete;
-- DROP TRIGGER IF EXISTS trg_UpdateAnnualSalary;
-- DROP TRIGGER trg_RestrictMaintenanceLogon ON ALL SERVER;