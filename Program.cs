using System;
namespace FoodVendingMachine
{
    class Program
    {
        static double cardBalance = 4.00;

        static void Main(string[] args)
        {

            Console.WriteLine("WELCOME.");
            Console.WriteLine("Insert your card.");

            while (!ValidatePIN())
            {
                Console.WriteLine("Incorrect PIN. Try Again.");
            }
            bool continueTransaction = true;
            while (continueTransaction)
            {
                DisplayMenu();
                ProceedSelection();

                Console.WriteLine("Would you like to choose again? (Yes or No): ");
                string answer = Console.ReadLine();
                continueTransaction = (answer == "yes" || answer == "y");
            }
            Console.WriteLine("Thank you for using the vending machine.");
        }
        static bool ValidatePIN()
        {
            int pin = 2245;
            Console.WriteLine("Enter PIN: ");
            int userPIN = Convert.ToInt16(Console.ReadLine());
            return userPIN == pin;
        }
        static void DisplayMenu()
        {
            Console.WriteLine("Snacks and Drinks: ");
            Console.WriteLine("[1]Piattos- $1.29\n[2]VCut - $2.07\n[3]Cheesy - $1.78\n" +
            "[4]Pic A - $3.45\n[5]Royal - $2.99\n[6]Mountain Dew - $2.56\n[7]CANCEL\n");
        }
        static void ProceedSelection()
        {
            Console.WriteLine("Pick your choice: ");
            int itemName = Convert.ToInt16(Console.ReadLine());

            switch (itemName)
            {
                case 1:
                    Receipt("Piattos", 1.29);
                    break;
                case 2:
                    Receipt("Vcut", 2.07);
                    break;
                case 3:
                    Receipt("Chessy", 1.78);
                    break;
                case 4:
                    Receipt("Pic A", 3.45);
                    break;
                case 5:
                    Receipt("Royal", 2.99);
                    break;
                case 6:
                    Receipt("Mountain Dew", 2.56);
                    break;
                case 7:
                    Console.WriteLine("Cancel");
                    break;
                default:
                    Console.WriteLine("Not Available.");
                    break;
            }
        }
        static void Receipt(string itemName, double price)
        {
            if (cardBalance >= price)
            {
                cardBalance -= price;
                Console.WriteLine($"You selected: {itemName} \nPrice: {price}");
                Console.WriteLine($"Remaining Balance: ${cardBalance}");

            }
            else
            {
                Console.WriteLine("Insufficient Funds");
            }
        }
    }
}