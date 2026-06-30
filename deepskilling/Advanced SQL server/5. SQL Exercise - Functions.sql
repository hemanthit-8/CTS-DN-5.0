-- =====================================================================
-- SQL Exercise - Functions (Employee Management System)
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
(3, 'Bob', 'Johnson', 3, 5500.00, '2021-07-01');
GO

-- =====================================================================
-- Exercise 1: Create a Scalar Function
-- Goal: Calculate the annual salary of an employee.
-- =====================================================================

CREATE FUNCTION fn_CalculateAnnualSalary (@Salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Salary * 12;
END;
GO

-- Test
SELECT EmployeeID, FirstName, LastName, dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
GO

-- =====================================================================
-- Exercise 2: Create a Table-Valued Function
-- Goal: Return employees in a specific department.
-- =====================================================================

CREATE FUNCTION fn_GetEmployeesByDepartment (@DepartmentID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID
);
GO

-- Test: employees from the IT department
SELECT * FROM dbo.fn_GetEmployeesByDepartment(2);
GO

-- =====================================================================
-- Exercise 3: Create a User-Defined Function
-- Goal: Calculate the bonus for an employee (10% of salary).
-- =====================================================================

CREATE FUNCTION fn_CalculateBonus (@Salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Salary * 0.10;
END;
GO

-- Test
SELECT EmployeeID, FirstName, LastName, dbo.fn_CalculateBonus(Salary) AS Bonus
FROM Employees;
GO

-- =====================================================================
-- Exercise 4: Modify a User-Defined Function
-- Goal: Modify fn_CalculateBonus to return Salary * 0.15.
-- =====================================================================

ALTER FUNCTION fn_CalculateBonus (@Salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN @Salary * 0.15;
END;
GO

-- Test
SELECT EmployeeID, FirstName, LastName, dbo.fn_CalculateBonus(Salary) AS Bonus
FROM Employees;
GO

-- =====================================================================
-- Exercise 5: Delete a User-Defined Function
-- Goal: Delete fn_CalculateBonus.
-- =====================================================================
-- NOTE: Exercise 9/10 below depend on fn_CalculateBonus, so this DROP
-- is shown for completeness but commented out so the rest of the
-- script keeps working end-to-end. Uncomment when running in isolation.

-- DROP FUNCTION fn_CalculateBonus;
-- GO
-- Verify deletion:
-- SELECT * FROM sys.objects WHERE name = 'fn_CalculateBonus'; -- should return no rows

-- =====================================================================
-- Exercise 6: Execute a User-Defined Function
-- Goal: Execute fn_CalculateAnnualSalary for each employee.
-- =====================================================================

SELECT EmployeeID, dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
GO

-- =====================================================================
-- Exercise 7: Return Data from a Scalar Function
-- Goal: Return the annual salary for EmployeeID = 1.
-- =====================================================================

SELECT dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees
WHERE EmployeeID = 1;
GO

-- =====================================================================
-- Exercise 8: Return Data from a Table-Valued Function
-- Goal: Return employees from the Finance department (DepartmentID = 3).
-- =====================================================================

SELECT * FROM dbo.fn_GetEmployeesByDepartment(3);
GO

-- =====================================================================
-- Exercise 9: Create a Nested User-Defined Function
-- Goal: Calculate total compensation using fn_CalculateAnnualSalary
--       and fn_CalculateBonus.
-- =====================================================================

CREATE FUNCTION fn_CalculateTotalCompensation (@Salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @AnnualSalary DECIMAL(10,2) = dbo.fn_CalculateAnnualSalary(@Salary);
    DECLARE @Bonus DECIMAL(10,2) = dbo.fn_CalculateBonus(@Salary);
    RETURN @AnnualSalary + @Bonus;
END;
GO

-- Test
SELECT EmployeeID, FirstName, LastName,
       dbo.fn_CalculateTotalCompensation(Salary) AS TotalCompensation
FROM Employees;
GO

-- =====================================================================
-- Exercise 10: Modify a Nested User-Defined Function
-- Goal: Update fn_CalculateTotalCompensation to use the modified
--       fn_CalculateBonus (already at 15% from Exercise 4).
-- =====================================================================

ALTER FUNCTION fn_CalculateTotalCompensation (@Salary DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @AnnualSalary DECIMAL(10,2) = dbo.fn_CalculateAnnualSalary(@Salary);
    DECLARE @Bonus DECIMAL(10,2) = dbo.fn_CalculateBonus(@Salary); -- now 15%
    RETURN @AnnualSalary + @Bonus;
END;
GO

-- Test
SELECT EmployeeID, FirstName, LastName,
       dbo.fn_CalculateTotalCompensation(Salary) AS TotalCompensation
FROM Employees;
GO

-- =====================================================================
-- Cleanup (optional, for re-running the script)
-- =====================================================================
-- DROP FUNCTION IF EXISTS fn_CalculateTotalCompensation;
-- DROP FUNCTION IF EXISTS fn_CalculateBonus;
-- DROP FUNCTION IF EXISTS fn_GetEmployeesByDepartment;
-- DROP FUNCTION IF EXISTS fn_CalculateAnnualSalary;