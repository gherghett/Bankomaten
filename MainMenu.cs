namespace Bankomaten;
class MainMenu
{
    private ISecureBankConnection connection;
    private IBankTransactionService bankTransactionService;
    public MainMenu(ISecureBankConnection secureBankConnection, IBankTransactionService bankTransactionService)
    {
        this.connection = secureBankConnection;
        this.bankTransactionService = bankTransactionService;
    }
    public IAccountAction WithdrawMoney()
    {
        decimal withdrawal = InputHandler.GetInt("Hur mkt vill du ta ut?");
        IAccountActionResult result = connection.Withdraw(withdrawal);
        Console.WriteLine(result.Message);
        return result.Action;
    }

    public IAccountAction DepositMoney()
    {
        decimal deposit = InputHandler.GetInt("Hur mkt vill du sätta in?");
        IAccountActionResult result = connection.Deposit(deposit);
        Console.WriteLine(result.Message);
        return result.Action;
    }

    public void SendMoney()
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

    public int Show()
    {
        int index = 0;
        UI.DisplayWelcome(connection.Account);
        while (index >= 0)
        {
            //char choice = InputHandler.GetString("T")[0];
            char choice = InputHandler.GetCharOfSet( "[T]a ut pengar, " +
                                        "[S]ätt in pengar, " +
                                        "[K]olla saldo, "+
                                        "Se [H]istoria, " +
                                        "eller Sk[I]cka pengar! " +
                                        "([L]ogga ut/[Q]uit)",
                                         ['T', 'S', 'K','H', 'I', 'L', 'Q']);
            switch (choice)
            {
                case 'T':
                    WithdrawMoney();
                    break;
                case 'S':
                    DepositMoney();
                    break;
                case 'K':
                    UI.DisplayBalance(connection.Account);
                    break;
                case 'H':
                    UI.DisplayHistory(connection.Account);
                    break;
                case 'I':
                    SendMoney();
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
  