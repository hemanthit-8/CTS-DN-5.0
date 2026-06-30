-- =====================================================================
-- SQL Exercise - Cursors (Employee Management System)
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
(2, 'IT'),
(3, 'Finance');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
(1, 'John', 'Doe', 1, 5000.00, '2020-01-15'),
(2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'),
(3, 'Bob', 'Johnson', 3, 5500.00, '2021-07-30');
GO

-- =====================================================================
-- Exercise 1: Create a Cursor
-- Goal: Iterate over all employees and print their details.
-- =====================================================================

DECLARE @EmployeeID INT;
DECLARE @FirstName VARCHAR(50);
DECLARE @LastName VARCHAR(50);
DECLARE @Salary DECIMAL(10,2);

-- Step 1: Declare the cursor
DECLARE employee_cursor CURSOR FOR
    SELECT EmployeeID, FirstName, LastName, Salary
    FROM Employees;

-- Step 2: Open the cursor
OPEN employee_cursor;

-- Step 3: Fetch the first row
FETCH NEXT FROM employee_cursor INTO @EmployeeID, @FirstName, @LastName, @Salary;

-- Step 4: Loop through and print each row
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'EmployeeID: ' + CAST(@EmployeeID AS VARCHAR(10))
        + ', Name: ' + @FirstName + ' ' + @LastName
        + ', Salary: ' + CAST(@Salary AS VARCHAR(20));

    FETCH NEXT FROM employee_cursor INTO @EmployeeID, @FirstName, @LastName, @Salary;
END;

-- Step 5: Close and deallocate the cursor
CLOSE employee_cursor;
DEALLOCATE employee_cursor;
GO

-- =====================================================================
-- Exercise 2: Types of Cursors
-- Goal: Understand the different types of cursors in SQL Server.
-- =====================================================================

-- 2a. STATIC cursor: snapshot of data taken at OPEN time; insensitive
-- to subsequent changes made by other sessions or the same session.
DECLARE static_cursor CURSOR STATIC FOR
    SELECT EmployeeID, FirstName, LastName FROM Employees;

OPEN static_cursor;
FETCH NEXT FROM static_cursor;
WHILE @@FETCH_STATUS = 0
BEGIN
    FETCH NEXT FROM static_cursor;
END;
CLOSE static_cursor;
DEALLOCATE static_cursor;
GO

-- 2b. DYNAMIC cursor: reflects all data changes made while the
-- cursor is open (inserts/updates/deletes visible during iteration).
DECLARE dynamic_cursor CURSOR DYNAMIC FOR
    SELECT EmployeeID, FirstName, LastName FROM Employees;

OPEN dynamic_cursor;
FETCH NEXT FROM dynamic_cursor;
WHILE @@FETCH_STATUS = 0
BEGIN
    FETCH NEXT FROM dynamic_cursor;
END;
CLOSE dynamic_cursor;
DEALLOCATE dynamic_cursor;
GO

-- 2c. FORWARD_ONLY cursor: can only move forward (FETCH NEXT) from
-- the first to the last row; cannot scroll backward.
DECLARE forward_only_cursor CURSOR FORWARD_ONLY FOR
    SELECT EmployeeID, FirstName, LastName FROM Employees;

OPEN forward_only_cursor;
FETCH NEXT FROM forward_only_cursor;
WHILE @@FETCH_STATUS = 0
BEGIN
    FETCH NEXT FROM forward_only_cursor;
END;
CLOSE forward_only_cursor;
DEALLOCATE forward_only_cursor;
GO

-- 2d. KEYSET-driven cursor: membership and row order are fixed at
-- OPEN time, but updates to non-key column values made by other
-- users ARE visible as you fetch (unlike STATIC).
DECLARE keyset_cursor CURSOR KEYSET FOR
    SELECT EmployeeID, FirstName, LastName FROM Employees;

OPEN keyset_cursor;
FETCH NEXT FROM keyset_cursor;
WHILE @@FETCH_STATUS = 0
BEGIN
    FETCH NEXT FROM keyset_cursor;
END;
CLOSE keyset_cursor;
DEALLOCATE keyset_cursor;
GO

-- =====================================================================
-- Comparison Summary
-- =====================================================================
-- | Cursor Type   | Sees data changes made after OPEN? | Scrollable? | Membership fixed? |
-- |---------------|-------------------------------------|-------------|--------------------|
-- | STATIC        | No (frozen snapshot)                | Yes         | Yes                |
-- | DYNAMIC       | Yes (fully live)                    | Yes         | No                 |
-- | FORWARD_ONLY  | Depends on underlying type (default dynamic-like) | No (forward only) | N/A |
-- | KEYSET        | Values yes, membership no            | Yes         | Yes (rows), No (values) |