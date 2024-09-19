namespace Bankomaten;
class LogInMenu
{
    private BankCommunicator bankCommunicator;
    private IBankTransactionService transactionService;
    public LogInMenu(BankCommunicator communicator, IBankTransactionService transactionService)
    {
        this.bankCommunicator = communicator;
        this.transactionService = transactionService;
    }
    public void ShowMenu()
    {
        int index = -1;
        while(index > -2)
        {
            // InputHandler.GetString("L, S, Q")[0];
            char choice = InputHandler.GetString("L, S, Q")[0]; //InputHandler.GetCharOfSet("[L]ogga in eller ([Q]uit)", ['L', 'Q']);
            switch (choice)
            {
                case 'L':
                    IBankConnection bankConnection = bankCommunicator
                                                    .CreateSecureConnection(InputHandler.GetString("Ange användarnamn"),
                                                                            InputHandler.GetString("Ange lösenord"),
                                                                            this.transactionService);
                    Console.WriteLine(bankConnection.LoginMessage);
                    if (bankConnection is ISecureBankConnection secureConnection)
                    {
                        var mainMenu = new MainMenu(secureConnection, transactionService);
                        index = mainMenu.Show();
                    }
                    break;
                case 'S':
                    break;
                case 'Q':
                    return;
            }
        }
    }
}