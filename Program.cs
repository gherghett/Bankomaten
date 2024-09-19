namespace Bankomaten;
internal class Program
{
    static void Main()
    {
        //this dependency will be used to try to establish a ISecureBankConnection, which can in turn be used
        //make changes to the simulated database
        BankCommunicator bankCommunicator = new BankCommunicator();
        //This interface has the methods needed to make changes on the logged in account, and is injected later 
        //into the ISecureBankConnection object
        IBankTransactionService transactionService = new BankTransactionService();
    
        AppController app = new AppController(bankCommunicator, transactionService);
        app.Run();
    }
}