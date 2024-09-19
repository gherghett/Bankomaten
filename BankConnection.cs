namespace Bankomaten;

interface IBankConnection
{
    public string LoginMessage {get;}
}

interface ISecureBankConnection : IBankConnection
{
    public BankAccount Account {get;}
    public IAccountActionResult Deposit(decimal deposit);
    public IAccountActionResult SendMoney(string to, decimal amount);
    public IAccountActionResult Withdraw(decimal amount);

}

class SecureBankConnection : ISecureBankConnection
{
    //this list is stand in for a connection to database
    private List<BankAccount> bankAccounts;
    public BankAccount Account {get;}
    public SecureBankConnection(int id, List<BankAccount> bankAccounts, IBankTransactionService bankTransactionService)
    {
        Account = bankAccounts.Where(a=>a.Id ==id).SingleOrDefault();
        this.bankAccounts = bankAccounts.ToList();
        this.transactionService = bankTransactionService;
    }
    
    public string LoginMessage => "Du är inloggad.";

    private IBankTransactionService transactionService;

    public IAccountActionResult Withdraw(decimal amount) => transactionService.Withdraw(Account,amount);

    public IAccountActionResult Deposit(decimal amount) => transactionService.Deposit(Account, amount);

    public IAccountActionResult SendMoney(string to, decimal amount) => transactionService.SendMoney(bankAccounts, Account, to, amount);
}

class FailedBankConnection : IBankConnection
{
    public FailedBankConnection()
    {

    }
    public string LoginMessage => "Inloggning misslyckades";
}