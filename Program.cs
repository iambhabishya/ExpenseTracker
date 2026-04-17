using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExpenseTracker
{
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        static string dataFilePath = "transactions.txt";
        static decimal monthlyBudget = 0;
        
        static void Main(string[] args)
        {
            Console.Title = "💰 Advanced Expense Tracker";
            Console.OutputEncoding = Encoding.UTF8;
            
            // Load saved data
            LoadData();
            
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AddTransaction();
                        break;
                    case "2":
                        ViewTransactions();
                        break;
                    case "3":
                        ShowSummary();
                        break;
                    case "4":
                        DeleteTransaction();
                        break;
                    case "5":
                        EditTransaction();
                        break;
                    case "6":
                        SearchTransactions();
                        break;
                    case "7":
                        ShowCategoryBreakdown();
                        break;
                    case "8":
                        ShowMonthlyReport();
                        break;
                    case "9":
                        SetBudget();
                        break;
                    case "10":
                        ExportToCSV();
                        break;
                    case "11":
                        SaveData();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n✅ Data saved successfully!");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        Console.Clear();
                        break;
                    case "0":
                        SaveData();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n👋 Thank you for using Expense Tracker!");
                        Console.WriteLine("💾 Data saved automatically. Goodbye!");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(2000);
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Invalid choice! Try again.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1500);
                        Console.Clear();
                        break;
                }
            }
        }
        
        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════╗");
            Console.WriteLine("║   💰 EXPENSE TRACKER - MAIN MENU 💰      ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\n📝 TRANSACTIONS:");
            Console.WriteLine("  1. ➕ Add Transaction");
            Console.WriteLine("  2. 📋 View All Transactions");
            Console.WriteLine("  3. 📊 View Summary");
            Console.WriteLine("  4. ❌ Delete Transaction");
            Console.WriteLine("  5. ✏️  Edit Transaction");
            
            Console.WriteLine("\n🔍 ANALYSIS:");
            Console.WriteLine("  6. 🔎 Search Transactions");
            Console.WriteLine("  7. 📈 Category Breakdown");
            Console.WriteLine("  8. 📅 Monthly Report");
            Console.WriteLine("  9. 💵 Set Monthly Budget");
            
            Console.WriteLine("\n💾 DATA:");
            Console.WriteLine("  10. 📤 Export to CSV");
            Console.WriteLine("  11. 💾 Save Data");
            Console.WriteLine("  0. 🚪 Exit");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n👉 Enter your choice: ");
            Console.ResetColor();
        }
        
        static void AddTransaction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║   ➕ ADD NEW TRANSACTION       ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\n📝 Title: ");
            string title = Console.ReadLine();
            
            Console.Write("💵 Amount: $");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("❌ Invalid amount! Enter a positive number: $");
                Console.ResetColor();
            }
            
            Console.WriteLine("\n📊 Type:");
            Console.WriteLine("  1. Income");
            Console.WriteLine("  2. Expense");
            Console.Write("Choose (1 or 2): ");
            string typeChoice = Console.ReadLine();
            string type = typeChoice == "1" ? "Income" : "Expense";
            
            Console.WriteLine("\n📁 Category:");
            Console.WriteLine("  1. 🍔 Food");
            Console.WriteLine("  2. 🚗 Transport");
            Console.WriteLine("  3. 🏠 Bills");
            Console.WriteLine("  4. 🛍️  Shopping");
            Console.WriteLine("  5. 🎮 Entertainment");
            Console.WriteLine("  6. 💊 Health");
            Console.WriteLine("  7. 📚 Education");
            Console.WriteLine("  8. 💰 Salary");
            Console.WriteLine("  9. 📦 Other");
            Console.Write("Choose (1-9): ");
            string categoryChoice = Console.ReadLine();
            
            string category = categoryChoice switch
            {
                "1" => "Food",
                "2" => "Transport",
                "3" => "Bills",
                "4" => "Shopping",
                "5" => "Entertainment",
                "6" => "Health",
                "7" => "Education",
                "8" => "Salary",
                _ => "Other"
            };
            
            Console.Write("\n📅 Date (leave blank for today): ");
            string dateInput = Console.ReadLine();
            DateTime date = string.IsNullOrEmpty(dateInput) ? DateTime.Now : DateTime.Parse(dateInput);
            
            Transaction transaction = new Transaction
            {
                Id = transactions.Count > 0 ? transactions.Max(t => t.Id) + 1 : 1,
                Title = title,
                Amount = amount,
                Type = type,
                Category = category,
                Date = date
            };
            
            transactions.Add(transaction);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Transaction added successfully!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ViewTransactions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          📋 ALL TRANSACTIONS                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            
            if (transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No transactions yet! Add one to get started.");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            Console.WriteLine("\n┌────┬─────────────────────┬──────────┬──────────┬─────────────┬────────────┐");
            Console.WriteLine("│ ID │ Title               │ Amount   │ Type     │ Category    │ Date       │");
            Console.WriteLine("├────┼─────────────────────┼──────────┼──────────┼─────────────┼────────────┤");
            
            foreach (var t in transactions.OrderByDescending(x => x.Date))
            {
                ConsoleColor color = t.Type.ToLower() == "income" ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.Write($"│ {t.Id,-2} │ {t.Title,-19} │ ");
                Console.ForegroundColor = color;
                Console.Write($"${t.Amount,-8:N2}");
                Console.ResetColor();
                Console.Write($" │ {t.Type,-8} │ {t.Category,-11} │ {t.Date:MM/dd/yyyy} │\n");
            }
            
            Console.WriteLine("└────┴─────────────────────┴──────────┴──────────┴─────────────┴────────────┘");
            
            Console.WriteLine($"\n📊 Total Transactions: {transactions.Count}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ShowSummary()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║    💰 FINANCIAL SUMMARY 💰         ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            
            decimal totalIncome = transactions.Where(t => t.Type.ToLower() == "income").Sum(t => t.Amount);
            decimal totalExpense = transactions.Where(t => t.Type.ToLower() == "expense").Sum(t => t.Amount);
            decimal balance = totalIncome - totalExpense;
            
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"💵 Total Income:    ${totalIncome:N2}");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"💸 Total Expense:   ${totalExpense:N2}");
            
            Console.WriteLine("");
            Console.ForegroundColor = balance >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"💰 Current Balance: ${balance:N2}");
            Console.ResetColor();
            
            if (balance < 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  WARNING: You're in deficit!");
                Console.ResetColor();
            }
            
            // Budget tracking
            if (monthlyBudget > 0)
            {
                DateTime now = DateTime.Now;
                decimal monthExpense = transactions
                    .Where(t => t.Type.ToLower() == "expense" && 
                                t.Date.Month == now.Month && 
                                t.Date.Year == now.Year)
                    .Sum(t => t.Amount);
                
                decimal remaining = monthlyBudget - monthExpense;
                decimal percentUsed = (monthExpense / monthlyBudget) * 100;
                
                Console.WriteLine("\n┌─────────────────────────────────────┐");
                Console.WriteLine("│  📊 MONTHLY BUDGET STATUS           │");
                Console.WriteLine("├─────────────────────────────────────┤");
                Console.WriteLine($"│  Budget:        ${monthlyBudget,-18:N2}│");
                Console.WriteLine($"│  Spent:         ${monthExpense,-18:N2}│");
                Console.ForegroundColor = remaining >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"│  Remaining:     ${remaining,-18:N2}│");
                Console.ResetColor();
                Console.WriteLine($"│  Used:          {percentUsed,-18:N1}% │");
                Console.WriteLine("└─────────────────────────────────────┘");
                
                if (percentUsed > 80)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n⚠️  You've used more than 80% of your budget!");
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void DeleteTransaction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║   ❌ DELETE TRANSACTION        ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            
            if (transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No transactions to delete!");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            // Show transactions
            Console.WriteLine("");
            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | ${t.Amount} | {t.Type}");
            }
            
            Console.Write("\n🔢 Enter Transaction ID to delete (0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (id == 0)
                {
                    Console.Clear();
                    return;
                }
                
                var transaction = transactions.FirstOrDefault(t => t.Id == id);
                if (transaction != null)
                {
                    transactions.Remove(transaction);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Transaction deleted successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Transaction not found!");
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void EditTransaction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║   ✏️  EDIT TRANSACTION         ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            
            if (transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No transactions to edit!");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            Console.WriteLine("");
            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | ${t.Amount} | {t.Type}");
            }
            
            Console.Write("\n🔢 Enter Transaction ID to edit (0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (id == 0)
                {
                    Console.Clear();
                    return;
                }
                
                var transaction = transactions.FirstOrDefault(t => t.Id == id);
                if (transaction != null)
                {
                    Console.Write($"\nNew Title (current: {transaction.Title}): ");
                    string newTitle = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newTitle)) transaction.Title = newTitle;
                    
                    Console.Write($"New Amount (current: ${transaction.Amount}): $");
                    string amountInput = Console.ReadLine();
                    if (decimal.TryParse(amountInput, out decimal newAmount)) transaction.Amount = newAmount;
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Transaction updated successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Transaction not found!");
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void SearchTransactions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║   🔎 SEARCH TRANSACTIONS       ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\n🔍 Enter search term: ");
            string searchTerm = Console.ReadLine().ToLower();
            
            var results = transactions.Where(t => 
                t.Title.ToLower().Contains(searchTerm) || 
                t.Category.ToLower().Contains(searchTerm)
            ).ToList();
            
            if (results.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No matching transactions found!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Found {results.Count} transaction(s):");
                Console.ResetColor();
                Console.WriteLine("");
                
                foreach (var t in results)
                {
                    Console.WriteLine($"ID: {t.Id} | {t.Title} | ${t.Amount} | {t.Type} | {t.Category}");
                }
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ShowCategoryBreakdown()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║   📈 CATEGORY BREAKDOWN            ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            
            var expenses = transactions.Where(t => t.Type.ToLower() == "expense");
            
            if (!expenses.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No expenses to analyze!");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            var categoryTotals = expenses
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(t => t.Amount) })
                .OrderByDescending(x => x.Total);
            
            decimal totalExpense = expenses.Sum(t => t.Amount);
            
            Console.WriteLine("\n┌─────────────────┬────────────┬────────────┐");
            Console.WriteLine("│ Category        │ Amount     │ Percentage │");
            Console.WriteLine("├─────────────────┼────────────┼────────────┤");
            
            foreach (var item in categoryTotals)
            {
                decimal percentage = (item.Total / totalExpense) * 100;
                Console.WriteLine($"│ {item.Category,-15} │ ${item.Total,-9:N2} │ {percentage,9:N1}% │");
                
                // Simple text bar chart
                int barLength = (int)(percentage / 5);
                Console.Write("│ ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(new string('█', barLength));
                Console.ResetColor();
                Console.WriteLine(new string(' ', 20 - barLength) + "                 │");
            }
            
            Console.WriteLine("└─────────────────┴────────────┴────────────┘");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ShowMonthlyReport()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║   📅 MONTHLY REPORT                ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            
            var monthlyData = transactions
                .GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:00}",
                    Income = g.Where(t => t.Type.ToLower() == "income").Sum(t => t.Amount),
                    Expense = g.Where(t => t.Type.ToLower() == "expense").Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.Month);
            
            Console.WriteLine("\n┌─────────┬────────────┬────────────┬────────────┐");
            Console.WriteLine("│ Month   │ Income     │ Expense    │ Balance    │");
            Console.WriteLine("├─────────┼────────────┼────────────┼────────────┤");
            
            foreach (var month in monthlyData)
            {
                decimal balance = month.Income - month.Expense;
                Console.Write($"│ {month.Month} │ ");
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"${month.Income,-9:N2}");
                Console.ResetColor();
                Console.Write(" │ ");
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"${month.Expense,-9:N2}");
                Console.ResetColor();
                Console.Write(" │ ");
                
                Console.ForegroundColor = balance >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"${balance,-9:N2}");
                Console.ResetColor();
                Console.WriteLine(" │");
            }
            
            Console.WriteLine("└─────────┴────────────┴────────────┴────────────┘");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void SetBudget()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║   💵 SET MONTHLY BUDGET            ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\n💰 Enter monthly budget: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal budget) && budget > 0)
            {
                monthlyBudget = budget;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Monthly budget set to ${budget:N2}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Invalid amount!");
                Console.ResetColor();
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ExportToCSV()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║   📤 EXPORT TO CSV                 ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();
            
            if (transactions.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠️  No transactions to export!");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            string fileName = $"ExpenseTracker_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("ID,Title,Amount,Type,Category,Date");
                    foreach (var t in transactions)
                    {
                        writer.WriteLine($"{t.Id},{t.Title},{t.Amount},{t.Type},{t.Category},{t.Date:yyyy-MM-dd}");
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Data exported successfully to: {fileName}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error exporting data: {ex.Message}");
                Console.ResetColor();
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void SaveData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(dataFilePath))
                {
                    writer.WriteLine(monthlyBudget);
                    foreach (var t in transactions)
                    {
                        writer.WriteLine($"{t.Id}|{t.Title}|{t.Amount}|{t.Type}|{t.Category}|{t.Date:yyyy-MM-dd HH:mm:ss}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error saving data: {ex.Message}");
                Console.ResetColor();
            }
        }
        
        static void LoadData()
        {
            if (!File.Exists(dataFilePath))
                return;
            
            try
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                if (lines.Length > 0)
                {
                    decimal.TryParse(lines[0], out monthlyBudget);
                    
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] parts = lines[i].Split('|');
                        if (parts.Length == 6)
                        {
                            transactions.Add(new Transaction
                            {
                                Id = int.Parse(parts[0]),
                                Title = parts[1],
                                Amount = decimal.Parse(parts[2]),
                                Type = parts[3],
                                Category = parts[4],
                                Date = DateTime.Parse(parts[5])
                            });
                        }
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Loaded {transactions.Count} transactions from previous session.");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error loading data: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
    
    class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}