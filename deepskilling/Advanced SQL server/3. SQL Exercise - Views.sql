-- =====================================================================
-- SQL Exercise - Views (Employee Management System)
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
    Salary DECIMAL(10, 2),
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
-- Exercise 1: Create a Simple View
-- Goal: Show basic employee details.
-- =====================================================================

CREATE VIEW vw_EmployeeBasicInfo AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName
FROM Employees e
JOIN Departments d ON d.DepartmentID = e.DepartmentID;
GO

-- Test
SELECT * FROM vw_EmployeeBasicInfo;
GO

-- =====================================================================
-- Exercise 2: Add Computed Column - Full Name
-- Goal: Use a computed column in a view.
-- =====================================================================

CREATE VIEW vw_EmployeeFullName AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName
FROM Employees e
JOIN Departments d ON d.DepartmentID = e.DepartmentID;
GO

-- Test
SELECT * FROM vw_EmployeeFullName;
GO

-- =====================================================================
-- Exercise 3: Add Computed Column - Annual Salary
-- Goal: Add a financial computed column.
-- =====================================================================

CREATE VIEW vw_EmployeeAnnualSalary AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    e.Salary * 12 AS AnnualSalary
FROM Employees e;
GO

-- Test
SELECT * FROM vw_EmployeeAnnualSalary;
GO

-- =====================================================================
-- Exercise 4: Add Multiple Computed Columns
-- Goal: Combine multiple computed columns in a single view.
--   - EmployeeID
--   - FullName
--   - DepartmentName
--   - AnnualSalary
--   - Bonus (10% of AnnualSalary)
-- =====================================================================

CREATE VIEW vw_EmployeeReport AS
SELECT
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName,
    e.Salary * 12 AS AnnualSalary,
    (e.Salary * 12) * 0.10 AS Bonus
FROM Employees e
JOIN Departments d ON d.DepartmentID = e.DepartmentID;
GO

-- Test
SELECT * FROM vw_EmployeeReport;
GO

-- =====================================================================
-- Cleanup (optional, for re-running the script)
-- =====================================================================
-- DROP VIEW IF EXISTS vw_EmployeeBasicInfo;
-- DROP VIEW IF EXISTS vw_EmployeeFullName;
-- DROP VIEW IF EXISTS vw_EmployeeAnnualSalary;
-- DROP VIEW IF EXISTS vw_EmployeeReport;