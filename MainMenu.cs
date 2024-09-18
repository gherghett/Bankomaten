namespace Bankomaten;
static class MainMenu
{
    public static IAccountAction WithdrawMoney(ISecureBankConnection connection)
    {
        decimal withdrawal = InputHandler.GetInt("Hur mkt vill du ta ut?");
        IAccountActionResult result = connection.Withdraw(withdrawal);
        Console.WriteLine(result.Message);
        return result.Action;
    }

    public static IAccountAction DepositMoney(ISecureBankConnection connection)
    {
        decimal deposit = InputHandler.GetInt("Hur mkt vill du sätta in?");
        IAccountActionResult result = connection.Deposit(deposit);
        Console.WriteLine(result.Message);
        return result.Action;
    }

    public static void SendMoney(ISecureBankConnection connection)
    {
        string to = InputHandler.GetString("Vem vill du skicka till?");
        decimal amount = InputHandler.GetInt("Hur mycket vill du skicka?");
        var result = connection.SendMoney(to, amount);
        Console.WriteLine(result.Message);
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

    public static int Show(ISecureBankConnection connection)
    {
        int index = 0;
        UI.DisplayWelcome(connection.Account);
        while (index >= 0)
        {
            //char choice = InputHandler.GetString("T")[0];
            char choice = InputHandler.GetCharOfSet( "[T]a ut pengar, " +
                                        "[S]ätt in pengar, " +
                                        "[K]olla saldo, "+
                                        "eller Sk[I]cka pengar! " +
                                        "([L]ogga ut/[Q]uit)",
                                         ['T', 'S', 'K', 'I', 'L', 'Q']);
            switch (choice)
            {
                case 'T':
                    WithdrawMoney(connection);
                    break;
                case 'S':
                    DepositMoney(connection);
                    break;
                case 'K':
                    UI.DisplayBalance(connection.Account);
                    break;
                case 'I':
                    SendMoney(connection);
                    break;
                case 'L': //logout
                    return -1;
                case 'Q': //exit
                    return -2;
            }
        }
        return index;
    }

}
  