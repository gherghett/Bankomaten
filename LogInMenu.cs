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
    public IBankConnection ShowMenu()
    {
        while (true)
        {
            // InputHandler.GetString("L, S, Q")[0];
            char choice = InputHandler.GetCharOfSet("[L]ogga in eller ([Q]uit)", ['L', 'Q']);
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
                        return secureConnection;
                    }
                    break;
                case 'S':
                    break;
                case 'Q':
                    return new FailedBankConnection();
            }
        }
    }
}