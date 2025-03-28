using System;
namespace VendingSystem_BusinessDataLogic
{
    public class VendingProcess
    {
        //balance
        private static double cardBalance = 4.00; 
        private static readonly int correctPIN = 2245; // PIN for authentication

        //Validates PIN
        public static bool ValidatePIN(int userPIN)
        {
            return userPIN == correctPIN;
        }
        //Checks the sufficient balance
        public static bool SufficientBalance(double price)
        {
            return cardBalance >= price;
        }
        //Processes the transaction and deducts balance if needed
        public static (bool success, double remainingBalance)ProcessPurchase(string itemName, double price)
        {
            if (SufficientBalance(price))
            {
                cardBalance -= price;
                return (true, cardBalance);
            }
            return (false, cardBalance);
        }
        //current card balance
        public static double GetBalance()
        {
            return cardBalance;
        }
        // Adds funds to the card balance
        public static bool AddFunds(double amount)
        {
            if (amount > 0)
            {
                cardBalance += amount;
                return true;
            }
            return false;
        }
    }
}

