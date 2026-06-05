using System;

/*======SavingAccount class======*/
class SavingAccount
{
    public string? AccountNumber { get; set; }
    public double Rate { get; set; }
    public double Balance { get; private set; }

    // Function to calculate the balance after the term ends
    public double TotalBalance()
    {
        return Balance + Balance * (Rate / 100);
    }

    // Deposit function
    public void Deposit(double amount)
    {
        if (amount > 0) Balance += amount;
        else Console.WriteLine("Invalid input");
    }

    // Withdraw function
    public void Withdraw(double amount)
    {
        if (amount <= 0) Console.WriteLine("Invalid input");
        else if (Balance >= amount) Balance -= amount;
        else Console.WriteLine("Not enough balance to withdraw.");
    }

    // ToString function
    public override string ToString()
    {
        return "Account Number: " + AccountNumber + "\nRate: " + Rate + "%\nBalance: " + Balance + " GEL";
    }
}

/*======CDAccount class======*/
class CDAccount : SavingAccount
{
    private int termInMonths;

    // Function for determining the interest rate
    public int TermInMonths
    {
        get { return termInMonths; }
        set
        {
            termInMonths = value;
            if (termInMonths >= 3 && termInMonths <= 12) Rate = 2;
            else if (termInMonths > 12 && termInMonths <= 24) Rate = 5;
            else if (termInMonths > 24 && termInMonths <= 36) Rate = 7;
            else
            {
                Console.WriteLine("Invalid input"); return;
            }
        }
    }

    // ToString function
    public override string ToString()
    {
        return base.ToString() + "\nTerm: " + TermInMonths + " months\nTotal balance: " + TotalBalance() + " GEL\n\n";
    }
}

// Program class
class Program
{
    static void Main()
    {
        // List of accounts
        List<CDAccount> Accounts = new List<CDAccount>();

        while (true)
        {
            foreach (var acc in Accounts.OrderByDescending(a => a.Balance)) Console.WriteLine(acc);

            Console.Write("1 - Create new account\n2 - Deposit\n3 - Withdraw\n0 - Exit\nChoose: ");
            string? answer = Console.ReadLine();

            if (answer == "0") // Exit
            {
                break;
            }

            else if (answer == "1") // Create new account
            {
                CDAccount NewAccount = new CDAccount();

                Console.Write("Enter account number: ");
                NewAccount.AccountNumber = Console.ReadLine();

                while (true)
                {
                    Console.Write("Enter term in months (3-36): ");
                    if (int.TryParse(Console.ReadLine(), out int months) && months >= 3 && months <= 36)
                    {
                        NewAccount.TermInMonths = months;
                        break;
                    }
                    else Console.WriteLine("Invalid input. Term must be 3-36.");
                }

                Accounts.Add(NewAccount);
                Console.WriteLine("\nAccount created successfully!\n");
            }
            else if (answer == "2" || answer == "3") // Deposit or Withdraw
            {
                if (Accounts.Count == 0)
                {
                    Console.WriteLine("Please create an account.");
                    continue;
                }

                Console.WriteLine("Enter your account number: ");
                string? AccNumber = Console.ReadLine();
                bool found = false;

                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (AccNumber == Accounts[i].AccountNumber)
                    {
                        found = true;

                        if (answer == "2")
                        {
                            Console.Write("Enter an amount for deposit: ");
                            if (double.TryParse(Console.ReadLine(), out double dep))
                                Accounts[i].Deposit(dep);
                            else Console.WriteLine("Invalid input.");
                        }
                        else if (answer == "3")
                        {
                            Console.Write("Enter an amount to withdraw: ");
                            if (double.TryParse(Console.ReadLine(), out double wit))
                                Accounts[i].Withdraw(wit);
                            else Console.WriteLine("Invalid input.");
                        }
                        break;
                    }
                }

                if (!found) Console.WriteLine("Account not found.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }        
}


