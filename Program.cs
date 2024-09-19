namespace Bankomaten;
internal class Program
{
    static void Main()
    {
        BankCommunicator bankCommunicator = new BankCommunicator();
        IBankTransactionService transactionService = new BankTransactionService();
        LogInMenu logInMenu = new LogInMenu(bankCommunicator, transactionService);
        logInMenu.ShowMenu();
    }
}