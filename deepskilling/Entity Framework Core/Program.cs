using RetailInventory.Data;

var context = new AppDbContext();
var products = context.Products.ToList();
Console.WriteLine($"Loaded {products.Count} products.");
