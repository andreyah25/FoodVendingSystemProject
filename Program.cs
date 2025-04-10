using System;
using VendingSystem_BusinessDataLogic;

namespace FoodVendingMachine
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunMachine();
        }
        static void RunMachine()
        {
            while (true)
            {
                SelectUser();
            }
        }
        static void SelectUser()
        {
            //MAIN MENU
            Console.WriteLine("WELCOME....");
            Console.WriteLine("Select a user: [1] Admin | [2] Customer | [0] EXIT");
            string user = Console.ReadLine();

            switch (user)
            {
                case "1":
                    RunAdminMode();
                    break;
                case "2":
                    RunCustomerMode();
                    break;
                case "0":
                    Console.WriteLine("Exiting...Thank you!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Selection...");
                    break;
            }
        }
        static void RunAdminMode()
        {
            Console.WriteLine("Enter Admin PIN: ");
            if (int.TryParse(Console.ReadLine(), out int adminPIN) && VendingProcess.ValidateAdminPIN(adminPIN))
            {
                bool continueAdmin = true;

                while (continueAdmin)
                {
                    Console.WriteLine("********************************************");
                    Console.WriteLine("Admin Menu: ");
                    Console.WriteLine("[1] Restock Items\n[2] View Inventory\n[3] Add Snacks\n[4] Remove Snacks\n[5] Search Item\n[6] Exit");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            RestockItems();
                            break;
                        case "2":
                            DisplayInventory();
                            break;
                        case "3":
                            AddSnack();
                            break;
                        case "4":
                            RemoveSnack();
                            break;
                        case "5":
                            SearchSnack();
                            break;
                        case "6":
                            Console.WriteLine("Returning to main menu...");
                            continueAdmin = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Selection.");
                            break;
                    }
                        }
                }
            else
            {
                Console.WriteLine("Incorrect PIN. Access Denied.");
            }
        }
        static void SearchSnack()
        {
            Console.WriteLine("Enter the snack name you want to search for: ");
            string name = Console.ReadLine();
            string result = VendingProcess.SearchSnack(name);
            Console.WriteLine(result);
        } //SEARCH
        static void RemoveSnack()   //DELETE
        {
            Console.WriteLine("**************************************");
            Console.WriteLine("Enter the item you want to remove from the inventory: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Are you sure you want to remove '{name}'? (yes/no): ");
            string confirm = Console.ReadLine();

            if (confirm == "yes" || confirm == "y")
            {
                if (VendingProcess.DeleteSnack(name))
                {
                    Console.WriteLine($"{name} has been removed from the inventory.");
                    DisplayInventory();
                }
                else
                {
                    Console.WriteLine("Item could not be found.");
                }
            }
        }
        static void AddSnack()    //CREATE OR ADD
        {
            Console.WriteLine("Enter snack name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            Console.WriteLine("Enter quantity to add: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }
            if (VendingProcess.AddSnack(name, price, quantity))
            {
                Console.WriteLine($"Successfully added {name} (PHP {price:F2}, Qty: {quantity}) to the menu.");
                DisplayInventory();
            }
            else
            {
                Console.WriteLine("Failed to add snack. Check your input.");
            }
        }
        static void RunCustomerMode()
        {
            Console.WriteLine("Please Insert your Card.");
            while (!AuthenticateUser())
            {
                Console.WriteLine("Incorrect PIN. Try Again.");
            }
            bool continueTransaction = true;
            while (continueTransaction)
            {
                ShowMainCustomerMenu();
                Console.WriteLine("Would you like to do another action? (Yes or No): ");
                string answer = Console.ReadLine().ToLower();
                continueTransaction = (answer == "yes" || answer == "y");
               
                if (!continueTransaction)
                {
                    Console.WriteLine("Ending session adn returning to Main Menu.....");
                    
                    
                }
                Console.WriteLine("*********************************");
            }
        }
        static bool AuthenticateUser()
        {
            Console.WriteLine("Enter PIN: ");
            if (int.TryParse(Console.ReadLine(), out int userPIN))
            {
                return VendingProcess.ValidatePIN(userPIN);
            }
            return false;
        }
        static void ShowMainCustomerMenu()
        {
            Console.WriteLine("Snacks Menu:");
            Console.WriteLine("[1] Snack Menu\n[2] Check Balance\n[3] Add Funds\n[4] Cancel Transaction");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine("****************************************");
            switch (choice)
            {
                case "1":
                    ShowSnackMenu();
                    break;
                case "2":
                    Console.WriteLine($"Your Current Balance is: PHP {VendingProcess.GetBalance():F2}");
                    break;
                case "3":
                    HandleAddingFunds();
                    break;
                case "4":
                    Console.WriteLine("Transaction canceled.");
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        } //READ
        static void ShowSnackMenu()
        {
            Console.WriteLine("Snacks and Drinks:");
            string[] inventory = VendingProcess.GetInventory();
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {inventory[i]}");
            }

            Console.WriteLine("Enter the item number (or type 'exit' to cancel):");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "exit")
            {
                Console.WriteLine("Returning to main menu...");
                return;
            }

            string[] itemDetails = VendingProcess.GetItemDetails(choice);

            if (itemDetails != null)
            {
                string itemName = itemDetails[0];
                if (double.TryParse(itemDetails[1], out double price))
                {
                    if (VendingProcess.IsItemInStock(itemName))
                    {
                        Console.WriteLine($"You selected: {itemName} - PHP {price:F2}");
                        HandlePurchase(itemName, price);
                    }
                    else
                    {
                        Console.WriteLine("Item is out of stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Error reading price.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        static void HandlePurchase(string itemNames, double price)
        {
            var (success, remainingBalance) = VendingProcess.ProcessPurchase(itemNames, price);
            if (success)
            {
                Console.WriteLine($"You purchased {itemNames} for PHP {price:F2}.");
                Console.WriteLine($"Remaining Balance: PHP {remainingBalance:F2}");
            }
            else
            {
                Console.WriteLine("Insufficient funds or item out of stock.");
            }
        }
        static void HandleAddingFunds()
        {
            Console.WriteLine("Enter amount to add: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (VendingProcess.AddFunds(amount))
                {
                    Console.WriteLine($"PHP{amount:F2} added successfully.");
                    Console.WriteLine($"New Balance: PHP{VendingProcess.GetBalance():F2}");
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a positive number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
        static void RestockItems() //UPDATE
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Enter item name to restock: ");
            string itemNames = Console.ReadLine();
            Console.WriteLine("Enter quantity to add: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                if (VendingProcess.RestockItems(itemNames, quantity))
                {
                    Console.WriteLine($"Restocked {quantity} items of {itemNames}.");
                    Console.WriteLine("Successfully Added Stocks....");
                }
                else
                {
                    Console.WriteLine("Invalid item name or quantity.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid quantity.");
            }
        }
        static void DisplayInventory()  //READ
        {
            Console.WriteLine("Current Stocks:");
            string[] inventory = VendingProcess.GetInventory();
            foreach (var item in inventory)
            {
                Console.WriteLine(item);
            }
        }
       
    }
}