-- =====================================================================
-- SQL Exercise - Advanced Concepts (Online Retail Store)
-- Window Functions | GROUPING SETS/CUBE/ROLLUP | CTE | Recursive CTE
-- MERGE | PIVOT/UNPIVOT
-- =====================================================================

-- ---------------------------------------------------------------------
-- Database Schema (assumed, consistent with other exercises)
-- ---------------------------------------------------------------------
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Name VARCHAR(100),
    Region VARCHAR(50)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10, 2)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

INSERT INTO Customers (CustomerID, Name, Region) VALUES
(1, 'Alice', 'North'),
(2, 'Bob', 'South'),
(3, 'Charlie', 'East'),
(4, 'David', 'West'),
(5, 'Eve', 'North');

INSERT INTO Products (ProductID, ProductName, Category, Price) VALUES
(1, 'Laptop', 'Electronics', 1200.00),
(2, 'Smartphone', 'Electronics', 800.00),
(3, 'Tablet', 'Electronics', 600.00),
(4, 'Headphones', 'Accessories', 150.00),
(5, 'Monitor', 'Electronics', 300.00),
(6, 'Keyboard', 'Accessories', 50.00);

INSERT INTO Orders (OrderID, CustomerID, OrderDate) VALUES
(1, 1, '2023-01-15'),
(2, 2, '2023-02-20'),
(3, 3, '2023-03-25'),
(4, 4, '2023-04-30'),
(5, 1, '2023-05-10'),
(6, 1, '2023-06-12'),
(7, 1, '2023-07-01'),
(8, 5, '2023-08-15');

INSERT INTO OrderDetails (OrderDetailID, OrderID, ProductID, Quantity) VALUES
(1, 1, 1, 1),
(2, 2, 2, 2),
(3, 3, 3, 1),
(4, 4, 4, 3),
(5, 5, 5, 2),
(6, 6, 6, 4),
(7, 7, 1, 1),
(8, 8, 2, 1);
GO

-- =====================================================================
-- Exercise 1: Ranking and Window Functions
-- Goal: Top 3 most expensive products in each category using
--       ROW_NUMBER(), RANK(), DENSE_RANK(), OVER(), PARTITION BY.
-- =====================================================================

SELECT
    ProductID,
    ProductName,
    Category,
    Price,
    ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC) AS RowNum,
    RANK()       OVER (PARTITION BY Category ORDER BY Price DESC) AS RankNum,
    DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRankNum
FROM Products;

-- Top 3 per category using ROW_NUMBER (strict top 3, no ties duplicated)
WITH RankedProducts AS (
    SELECT
        ProductID,
        ProductName,
        Category,
        Price,
        ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC) AS RowNum
    FROM Products
)
SELECT ProductID, ProductName, Category, Price
FROM RankedProducts
WHERE RowNum <= 3;


-- =====================================================================
-- Exercise 2: Aggregation with GROUPING SETS, CUBE, and ROLLUP
-- Goal: Total quantity sold by Region and Category.
-- =====================================================================

-- Base join used by all three aggregation queries
-- (Customers -> Orders -> OrderDetails -> Products)

-- 2a. GROUPING SETS: totals by Region, by Category, and by both
SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN Customers c ON c.CustomerID = o.CustomerID
JOIN OrderDetails od ON od.OrderID = o.OrderID
JOIN Products p ON p.ProductID = od.ProductID
GROUP BY GROUPING SETS (
    (c.Region, p.Category),
    (c.Region),
    (p.Category),
    ()
);

-- 2b. ROLLUP: hierarchical subtotals (Region -> Region+Category) + grand total
SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN Customers c ON c.CustomerID = o.CustomerID
JOIN OrderDetails od ON od.OrderID = o.OrderID
JOIN Products p ON p.ProductID = od.ProductID
GROUP BY ROLLUP (c.Region, p.Category);

-- 2c. CUBE: all combinations of Region and Category
SELECT
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN Customers c ON c.CustomerID = o.CustomerID
JOIN OrderDetails od ON od.OrderID = o.OrderID
JOIN Products p ON p.ProductID = od.ProductID
GROUP BY CUBE (c.Region, p.Category);


-- =====================================================================
-- Exercise 3: CTEs and MERGE
-- =====================================================================

-- 3a. Recursive CTE: generate dates from 2025-01-01 to 2025-01-31
WITH DateSequence AS (
    SELECT CAST('2025-01-01' AS DATE) AS CalendarDate
    UNION ALL
    SELECT DATEADD(DAY, 1, CalendarDate)
    FROM DateSequence
    WHERE CalendarDate < '2025-01-31'
)
SELECT CalendarDate
FROM DateSequence
OPTION (MAXRECURSION 100);

-- 3b. MERGE: update existing products or insert new ones from a staging table
CREATE TABLE StagingProducts (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10, 2)
);

INSERT INTO StagingProducts (ProductID, ProductName, Category, Price) VALUES
(1, 'Laptop', 'Electronics', 1100.00),   -- price update for existing product
(7, 'Smartwatch', 'Electronics', 250.00); -- new product to insert

MERGE INTO Products AS target
USING StagingProducts AS source
ON target.ProductID = source.ProductID
WHEN MATCHED THEN
    UPDATE SET
        target.ProductName = source.ProductName,
        target.Category    = source.Category,
        target.Price       = source.Price
WHEN NOT MATCHED BY TARGET THEN
    INSERT (ProductID, ProductName, Category, Price)
    VALUES (source.ProductID, source.ProductName, source.Category, source.Price);

-- Verify
SELECT * FROM Products;


-- =====================================================================
-- Exercise 4: PIVOT and UNPIVOT
-- Goal: Monthly sales quantity per product, pivoted then unpivoted.
-- =====================================================================

-- 4a. Aggregate sales by Product and Month
SELECT
    p.ProductName,
    DATENAME(MONTH, o.OrderDate) AS OrderMonth,
    SUM(od.Quantity) AS TotalQuantity
FROM Orders o
JOIN OrderDetails od ON od.OrderID = o.OrderID
JOIN Products p ON p.ProductID = od.ProductID
GROUP BY p.ProductName, DATENAME(MONTH, o.OrderDate);

-- 4b. PIVOT: rows -> columns (one column per month)
SELECT ProductName, [January], [February], [March], [April], [May], [June], [July], [August]
FROM (
    SELECT
        p.ProductName,
        DATENAME(MONTH, o.OrderDate) AS OrderMonth,
        od.Quantity
    FROM Orders o
    JOIN OrderDetails od ON od.OrderID = o.OrderID
    JOIN Products p ON p.ProductID = od.ProductID
) AS SourceTable
PIVOT (
    SUM(Quantity)
    FOR OrderMonth IN ([January], [February], [March], [April], [May], [June], [July], [August])
) AS PivotTable;

-- 4c. UNPIVOT: pivoted data back into row format
SELECT ProductName, OrderMonth, TotalQuantity
FROM (
    SELECT ProductName, [January], [February], [March], [April], [May], [June], [July], [August]
    FROM (
        SELECT
            p.ProductName,
            DATENAME(MONTH, o.OrderDate) AS OrderMonth,
            od.Quantity
        FROM Orders o
        JOIN OrderDetails od ON od.OrderID = o.OrderID
        JOIN Products p ON p.ProductID = od.ProductID
    ) AS SourceTable
    PIVOT (
        SUM(Quantity)
        FOR OrderMonth IN ([January], [February], [March], [April], [May], [June], [July], [August])
    ) AS PivotTable
) AS PivotedData
UNPIVOT (
    TotalQuantity FOR OrderMonth IN ([January], [February], [March], [April], [May], [June], [July], [August])
) AS UnpivotedTable;


-- =====================================================================
-- Exercise 5: Using a CTE to Simplify a Query
-- Goal: Find all customers who have placed more than 3 orders.
-- =====================================================================

WITH CustomerOrderCounts AS (
    SELECT
        o.CustomerID,
        COUNT(o.OrderID) AS OrderCount
    FROM Orders o
    GROUP BY o.CustomerID
)
SELECT
    c.CustomerID,
    c.Name,
    coc.OrderCount
FROM CustomerOrderCounts coc
JOIN Customers c ON c.CustomerID = coc.CustomerID
WHERE coc.OrderCount > 3;

-- Note: with the sample data above, CustomerID 1 has 4 orders,
-- so it will be the only row returned.