using System;
using System.Collections.Generic;

namespace ExpenseTracker
{
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        
        static void Main(string[] args)
        {
            Console.Title = "Expense Tracker";
            
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    AddTransaction();
                }
                else if (choice == "2")
                {
                    ViewTransactions();
                }
                else if (choice == "3")
                {
                    ShowSummary();
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Try again.");
                }
            }
        }
        
        static void ShowMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("===== EXPENSE TRACKER MENU =====");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View All Transactions");
            Console.WriteLine("3. View Summary");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
        }
        
        static void AddTransaction()
        {
            Console.WriteLine("");
            Console.WriteLine("--- ADD NEW TRANSACTION ---");
            
            Console.Write("Title: ");
            string title = Console.ReadLine();
            
            Console.Write("Amount: ");
            string amountText = Console.ReadLine();
            decimal amount = decimal.Parse(amountText);
            
            Console.Write("Type (Income or Expense): ");
            string type = Console.ReadLine();
            
            Console.Write("Category: ");
            string category = Console.ReadLine();
            
            Transaction transaction = new Transaction();
            transaction.Id = transactions.Count + 1;
            transaction.Title = title;
            transaction.Amount = amount;
            transaction.Type = type;
            transaction.Category = category;
            transaction.Date = DateTime.Now;
            
            transactions.Add(transaction);
            
            Console.WriteLine("Transaction added!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ViewTransactions()
        {
            Console.WriteLine("");
            Console.WriteLine("--- ALL TRANSACTIONS ---");
            
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | ${t.Amount} | {t.Type} | {t.Category}");
            }
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        
        static void ShowSummary()
        {
            Console.WriteLine("");
            Console.WriteLine("--- FINANCIAL SUMMARY ---");
            
            decimal totalIncome = 0;
            decimal totalExpense = 0;
            
            foreach (var t in transactions)
            {
                if (t.Type.ToLower() == "income")
                {
                    totalIncome += t.Amount;
                }
                else
                {
                    totalExpense += t.Amount;
                }
            }
            
            decimal balance = totalIncome - totalExpense;
            
            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expense: ${totalExpense}");
            Console.WriteLine($"Balance: ${balance}");
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
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