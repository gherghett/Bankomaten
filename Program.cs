namespace Bankomaten;
internal class Program
{
    static int AttemptLogIn(string pin, List<string> pinCodes)
    {
        int index;
        index = pinCodes.IndexOf(pin);
        if (index < 0)
        {
            Console.WriteLine("Fel, försök igen.");
        }
        return index;
    }

    static void DisplayBalance(int accountBalance) =>
        Console.WriteLine("Du har " + accountBalance + " pengar!");

    static void DisplayWelcome(int index, int accountBalance)
    {
        Console.WriteLine("Välkommen kund nr " + index);
        DisplayBalance(accountBalance);
    }

    public static int WithdrawMoney(int accountBalance)
    {
        int withdrawal = InputHandler.GetInt("Hur mkt vill du ta ut?");
        if (IsValidWithdrawal(accountBalance, withdrawal))
        {
            Console.WriteLine($"Du har tagit ut {withdrawal}");
            return accountBalance - withdrawal;
        }
        Console.WriteLine("Invalid amount.");
        return accountBalance;
    }

    public static bool IsValidWithdrawal(int accountBalance, int withdrawal) =>
        withdrawal > 0 && accountBalance - withdrawal > 0;

    public static int DepositMoney(int accountBalance)
    {
        int amount = InputHandler.GetInt("Hur mkt vill du sätta in?");
        if (amount < 0)
        {
            Console.WriteLine("Ogiltig mängd pengar");
            return accountBalance;
        }
        Console.WriteLine($"{amount} pengar har satts in");
        return amount + accountBalance;
    }

    public static void SendMoney(List<int> balance, int from)
    {
        int to = GetUser("Vem vill du skicka till?", balance.Count);
        int amount = InputHandler.GetInt("Hur mycket vill du skicka");
        if (balance[from] >= amount)
        {
            balance[to] += amount;
            balance[from] -= amount; 
            Console.WriteLine("Din överföring är klar.");
        }
        else
        {
            Console.WriteLine("Det gick inte att skicka. Det finns inte tillräckligt med pengar");
        }
    }

    public static int GetUser(string prompt, int usersLength)
    {
        while (true)
        {
            int userId = InputHandler.GetInt(prompt);
            if (userId > usersLength || userId < 0)
            {
                Console.WriteLine($"Användare ska vara mellan 0 - {usersLength - 1}");
            }
            else
            {
                return userId;
            }
        }
    }

    public static int MainMenu(int index, List<int> balance)
    {
        while (index >= 0)
        {
            char choice = InputHandler.GetCharOfSet( "[T]a ut pengar, " +
                                        "[S]ätt in pengar, " +
                                        "[K]olla saldo, "+
                                        "eller Sk[I]cka pengar! " +
                                        "([L]ogga ut/[Q]uit)",
                                         ['T', 'S', 'K', 'I', 'L', 'Q']);
            switch (choice)
            {
                case 'T':
                    balance[index] = WithdrawMoney(balance[index]);
                    break;
                case 'S':
                    balance[index] = DepositMoney(balance[index]);
                    break;
                case 'K':
                    DisplayBalance(balance[index]);
                    break;
                case 'I':
                    SendMoney(balance, index);
                    break;
                case 'L': //logout
                    return -1;
                case 'Q': //exit
                    return -2;
            }
        }
        return index;
    }

    public static int LogInMenu(List<string> pinCodes, List<int> balance)
    {
        int index = -1;
        while(index < 0)
        {
            char choice = InputHandler.GetCharOfSet("[L]ogga in eller [S]kapa nytt konto ([Q]uit)", ['L', 'S', 'Q']);
            switch (choice)
            {
                case 'L':
                    index = AttemptLogIn(InputHandler.GetString("skriv in PIN"), pinCodes);
                    break;
                case 'S':
                    pinCodes.Add(GetUniquePIN("Skriv in PIN för ditt nya konto", pinCodes));
                    balance.Add(0);
                    index = pinCodes.Count - 1;
                    break;
                case 'Q':
                    return -2;
            }
        }
        return index;
    }

    public static string GetUniquePIN(string prompt, List<string> pinCodes)
    {
        string newPin = "";
        while(string.IsNullOrWhiteSpace(newPin))
        {
            newPin = InputHandler.GetString(prompt);
            foreach(string pinCode in pinCodes)
            {
                if(newPin == pinCode)
                {
                    Console.WriteLine("Välj ett annat PIN. Och... em, testa inte ange detta pinnet, inget kommer hända ändå.");
                    newPin = "";
                    break;
                }
            }
        }
        return newPin;
    }


    static void Main()
    {
        List<string> pinCodes = ["1", "12", "34", "45"];
        List<int> balance = [1234, 100, 500, 10000];

        int userId = -1; // -1 betyder utloggad
        while(userId != -2) // -2 beyder att prorammet ska avslutas
        {
            while(userId == -1)
            {
                userId = LogInMenu(pinCodes, balance);
            }
            while(userId >= 0)
            {
                DisplayWelcome(userId, balance[userId]);

                userId = MainMenu(userId, balance);
            }
        }
    }
}