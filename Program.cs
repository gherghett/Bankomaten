namespace Bankomaten;
internal class Program
{
    static void Main()
    {
        BankCommunicator bankCommunicator = new BankCommunicator();
        IBankTransactionService transactionService = new BankTransactionService();
        AppController app = new AppController(bankCommunicator, transactionService);
        app.Run();
    }
}