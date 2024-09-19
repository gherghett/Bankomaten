namespace Bankomaten;
class AppController
{
    private readonly BankCommunicator bankCommunicator;
    private readonly IBankTransactionService transactionService;

    public AppController(BankCommunicator bankCommunicator, IBankTransactionService transactionService)
    {
        this.bankCommunicator = bankCommunicator;
        this.transactionService = transactionService;
    }

    public void Run()
    {
        while (true)
        {
            LogInMenu logInMenu = new LogInMenu(bankCommunicator, transactionService);
            IBankConnection bankConnection = logInMenu.ShowMenu();

            if (bankConnection is ISecureBankConnection secureConnection)
            {
                MainMenu mainMenu = new MainMenu(secureConnection, transactionService);
                int result = mainMenu.Show();
                
                if (result == -2) // User quit
                {
                    break;
                }
            }
        }
    }
}
