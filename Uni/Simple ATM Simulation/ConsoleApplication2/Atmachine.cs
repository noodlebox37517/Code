/*
 * ATM with transactions including; Balance , Withdraw and Transfer
 * Author:  Thomas Fleming
 * Date:    september, 2012
 * ID N8589119
 */

using System;
namespace ATM {
    class ATM {
        static double[] amounts = new double[] { 0, 901.45, 450.00, 1500.00, 2000.00 };
        static string[] accountName = new string[] { "Exit", "Savings Account", "Debit Card", "Credit Card", "Line of Credit" };

        //Main() executes initial function and provides a base, 
        //aswell as request transaction type and then calls option()
        static void Main() {
            int option;
            string[] transactionName = new string[] { "Exit", "Balance", "Withdraw", "Transfer", };
            do {
                Console.Write("please select a Transaction");
                Console.WriteLine();
                for (int i = 0; i <= 3; i++) {
                    Console.Write("{0}.  {1}", i, transactionName[i]);
                    Console.WriteLine();
                }
                string actions = Console.ReadLine();
                option = int.Parse(actions);
                OptionSelect(option);
            } while (option != 0);
            Console.Write("Thank you for using this ATM");
            Console.ReadKey();
        }
        //-----------------------------------------------------------------------
        //DisplayBalance() firstly calls DateShow() to show date then writes value of account and which account its is using the whichAccount parameter
        // returns to main()
        static void DisplayBalance(int whichAccount) {
            DateShow();
            Console.Write("Balance of {0} is {1:C}", accountName[whichAccount], amounts[whichAccount]);
            Console.ReadLine();
            Main();
        }
        //-----------------------------------------------------------------------
        //WithdrawAmount() to withdraw from the specified account using the whichAccount parameter
        // then calls TransError to check if action possible if return true calculates and updates change in amounts, then calls DispesnseCash()

        static void WithdrawAmount(int whichAccount) {
            Console.Write("Please enter amount:");
            string amountString = Console.ReadLine();
            double amountVar = double.Parse(amountString);
            if (TransError(whichAccount, amountVar) == true) {
                amounts[whichAccount] = amounts[whichAccount] - amountVar;
                DispenseCash(amountVar);
                FeeCalc(whichAccount, 1);
                DisplayBalance(whichAccount);
            }
        }
        //-----------------------------------------------------------------------
        //DispenseCash() calculates how much oh each note is to be given out usinf a do while loop,
        //which checks if 50 can be diveded into the amount determined by the parameter amount,
        //without any remainder if not it then adds a 20 note and msubtracts 20 from the total.
        //then prints the amount of notes furthermore checking if there are any 50s
        static void DispenseCash(double amount) {
            int twenties = 0;
            double fifties = 0;
            do {
                twenties++;
                amount = amount - 20;
            } while (amount % 50 != 0);
            fifties = amount / 50;

            Console.Write("Please collect {0:C} consisting of", (amount + twenties * 20));
            Console.WriteLine();
            Console.Write(" {0}   $20.00 notes", twenties);
            Console.WriteLine();
            if (fifties > 0) {
                Console.Write(" {0}   $50.00 notes", fifties);
                Console.WriteLine();
            }
        }
        //-----------------------------------------------------------------------
        //TransferAmount() the parameters fromAccount and Toaccount denote the account of which the transfer is beetween,
        //requests transfer amount then using the int of this using an if statement calls TransError()
        //which if shows no error returns true, then calls FeeCalc and calculates the new balances of the pree specified accounts
        // calls date show and reports the new balances 
        static void TransferAmount(int fromAccount, int toAccount) {
            Console.Write(" how much would you like to transfer from {0} to {1}", accountName[fromAccount], accountName[toAccount]);
            string amountTemp = Console.ReadLine();
            Console.WriteLine();
            double transferAmount = double.Parse(amountTemp);
            if (TransError(fromAccount, transferAmount) == true) {
                FeeCalc(fromAccount, 2);
                amounts[fromAccount] = amounts[fromAccount] - transferAmount;
                amounts[toAccount] = amounts[toAccount] + transferAmount;
                DateShow();
                Console.Write("Balance of {0} is {1:C}", accountName[fromAccount], amounts[fromAccount]);
                Console.WriteLine();
                Console.Write("Balance of {0} is {1:C}", accountName[toAccount], amounts[toAccount]);
                Console.WriteLine();
            }
        }
        //-----------------------------------------------------------------------
        //DateShow() displays the current date
        static void DateShow() {
            DateTime date = DateTime.Now;
            Console.WriteLine("Current Date: {0} ", date);
        }
        //-----------------------------------------------------------------------
        //TransferAmount() calculates the fees depending on which parameters given whichAccount to determine accounts,
        // and feeTypes to determine which fee is used, furthermore as an int more feetypes could be added easily
        // uses math.round to round upto the nearest cent value
        static void FeeCalc(int whichAccount, int feeType) {
            int[] AccountFee = new int[] { 0, 0, 2, 3, 0 };
            if (feeType == 1) {
                double FeeAmount = 00.10;
                if (whichAccount == AccountFee[whichAccount]) {
                    amounts[whichAccount] = (amounts[whichAccount] - FeeAmount);
                    amounts[whichAccount] = Math.Round((amounts[whichAccount]), 2);
                } else {
                    FeeAmount = 00.50;
                    amounts[whichAccount] = (amounts[whichAccount] - FeeAmount);
                    amounts[whichAccount] = Math.Round((amounts[whichAccount]), 2);
                }

            }
        }
        //-----------------------------------------------------------------------
        //OptionSelect() using the option parameter uses if statments to determine which method to call
        static void OptionSelect(int option) {
            if (option == 1) {
                BalanceAccount();

            } else if (option == 2) {
                WithdrawAccount();


            } else if (option == 3) {
                TransferAccount();
            }
        }
        //-----------------------------------------------------------------------
        //TransError() parameters determine whichaccount is used, amountVar is the amount to be used when checking for the Error,
        //minbalance is the minumum balance acceptable for these accounts without fees, using an if statement
        //checks to make sure the account balance will be above the minBalance, the returns false if error occured and true if it did not
        static bool TransError(int whichAccount, double amountVar) {
            const double minBalance = 10.00;
            if (amounts[whichAccount] - amountVar < minBalance) {
                Console.Write("Sorry, {0:C} would leave the balance below $10.00", amountVar);
                Console.WriteLine();
                WithdrawAmount(whichAccount);
                return false;
            } else {
                return true;
            }
        }
        //-----------------------------------------------------------------------
        //Exit() exits the current application of the prgram if menu thenexits gracefully, if transaction the to menu
        //exitType dectates which type of exit is to be used 
        static void Exit(int exitType) {
                Console.Write("Thank you for using this ATM");
                Console.ReadKey();
            if (exitType == 0) {
                Console.Write("Transaction Teminated");
                Main();
            }
        }
        //-----------------------------------------------------------------------
        //AccountsList() writes accountlist to window
        static void AccountsList() {
            Console.WriteLine();
            for (int i = 0; i <= 4; i++) {
                Console.Write("{0}.  {1}", i, accountName[i]);
                Console.WriteLine();
            }
        }
        //-----------------------------------------------------------------------
        //BalanceAccount() request which account to use and then calls DisplayBalance
        static void BalanceAccount() {
            Console.Write("Select Account:");
            AccountsList();
            string Account = Console.ReadLine();
            int Accountnum = int.Parse(Account);
            Exit(Accountnum);
            DisplayBalance(Accountnum);
        }
        //-----------------------------------------------------------------------
        //WithdrawAccount() request which account to use and then calls WithdrawAmount()
        static void WithdrawAccount() {
            Console.Write("Select Account");
            AccountsList();
            string Account = Console.ReadLine();
            int Accountnum = int.Parse(Account);
            Exit(Accountnum);
            WithdrawAmount(Accountnum);
        }
        //-----------------------------------------------------------------------
        //TransferAccount() request which accounts to use and then calls TransferAmount
        static void TransferAccount() {
            Console.Write("Select origin Account:");
            Console.WriteLine();
            AccountsList();
            string AccountTemp = Console.ReadLine();
            int fromAccount = int.Parse(AccountTemp);
            Exit(fromAccount);
            Console.WriteLine();
            Console.Write("Select Destination Account:");
            AccountsList();
            AccountTemp = Console.ReadLine();
            int toAccount = int.Parse(AccountTemp);
            Exit(toAccount);
            Console.WriteLine();
            TransferAmount(fromAccount, toAccount);
        }
    }
}


