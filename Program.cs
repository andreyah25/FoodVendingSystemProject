using System;
using VendingSystem_BusinessDataLogic; //connection to the other project

namespace FoodVendingMachine
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME.");
            Console.WriteLine("Insert your card.");

            //loop
            while (!AuthenticateUser())
            {
                Console.WriteLine("Incorrect PIN. Try Again.");
            }
            
            bool continueTransaction = true;
            while (continueTransaction)
            {
                DisplayMenu();
                ProcessUserSelection();
                //loop
                Console.WriteLine("*********************************");
                Console.WriteLine("Would you like to choose again? (Yes or No): "); 
                string answer = Console.ReadLine().ToLower();
                continueTransaction = (answer == "yes" || answer == "y");
                Console.WriteLine("*********************************");
            }

            Console.WriteLine("Thank you for purchasing in the vending machine.");
        }

        //PIN authentication
        static bool AuthenticateUser()
        {
            Console.WriteLine("Enter PIN: ");
            if (int.TryParse(Console.ReadLine(), out int userPIN))
            {
                return VendingProcess.ValidatePIN(userPIN);
            }
            return false;
        }
        // Displays menu items
        static void DisplayMenu()
        {
            Console.WriteLine("Snacks and Drinks: ");
            Console.WriteLine("[1] Piattos - $1.29\n[2] VCut - $2.07\n[3] Cheesy - $1.78\n" +
                              "[4] Pic A - $3.45\n[5] Royal - $2.99\n[6] Mountain Dew - $2.56\n" 
                              + "[7] CHECK BALANCE\n[8] ADD FUNDS\n[9] CANCEL");
        }
        // Processes user selection and calls the business logic
        static void ProcessUserSelection()
        {
            Console.WriteLine("Pick your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        HandlePurchase("Piattos", 1.29);
                        break;
                    case 2:
                        HandlePurchase("Vcut", 2.07);
                        break;
                    case 3:
                        HandlePurchase("Cheesy", 1.78);
                        break;
                    case 4:
                        HandlePurchase("Pic A", 3.45);
                        break;
                    case 5:
                        HandlePurchase("Royal", 2.99);
                        break;
                    case 6:
                        HandlePurchase("Mountain Dew", 2.56);
                        break;
                    case 7:
                        Console.WriteLine($"Your current balance is: ${VendingProcess.GetBalance():F2}"); //F2(fixed point number for 2 decimal places)
                        break;
                    case 8:
                        HandleAddingFunds();
                        break;
                    case 9:
                        Console.WriteLine("Transaction Canceled.");
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        //Handles the purchasing logic 
        static void HandlePurchase(string itemName, double price)
        {
            var (success, remainingBalance) = VendingProcess.ProcessPurchase(itemName, price);

            if (success)
            {
                Console.WriteLine($"You purchased {itemName} for ${price:F2}.");
                Console.WriteLine($"Remaining Balance: ${remainingBalance:F2}");
            }
            else
            {
                Console.WriteLine("Insufficient Funds.");
            }
        }
        // Handles adding funds to the balance
        static void HandleAddingFunds()
        {
            Console.WriteLine("Enter amount to add: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (VendingProcess.AddFunds(amount))
                {
                    Console.WriteLine($"${amount:F2} added successfully.");
                    Console.WriteLine($"New Balance: ${VendingProcess.GetBalance():F2}");
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
    }
}
