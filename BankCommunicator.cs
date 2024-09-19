namespace Bankomaten;

class BankCommunicator
{
    private List<BankAccount> accounts;

    public BankCommunicator()
    {
        List<string> userNames = ["bert", "tilda", "frida", "bollen"];
        List<string> pinCodes = ["1", "12", "34", "45"];
        List<decimal> balances = [1234m, 100m, 500m, 10000m];
        accounts = new();
        for(int i = 0; i<balances.Count; i++)
        {
            accounts.Add(new BankAccount( new BankUser(userNames[i], pinCodes[i]), balances[i]));
        };
    }

    public IBankConnection CreateSecureConnection(string userName, string pinword, IBankTransactionService transactionService)
    {
        BankAccount? loggedInAccount = AreCorrectCredentials(userName, pinword);
        if (loggedInAccount != null)
        {
            return new BankConnectionBuilder()
                       .SetAccount(loggedInAccount)
                       .SetAccounts(accounts)
                       .SetTransActionService(transactionService)
                       .Build();
        }
        else
            return new FailedBankConnection();

    }

    private BankAccount? AreCorrectCredentials(string userName, string pinword ) =>
        accounts.Where(a => a.UserName == userName).SingleOrDefault(a => a.IsPinwordCorrect(pinword));
}

interface IBankConnectionBuilder {}
interface IBankConnectionBuilderExpectingLoggedInAccount
{
    public IBankConnectionBuilderExpectingAccountsDB SetAccount(BankAccount? loggedInAccount);
}
interface IBankConnectionBuilderExpectingAccountsDB
{
    public IBankConnectionBuilderExpectingTransactionService SetAccounts(List<BankAccount> accounts);
}
interface IBankConnectionBuilderExpectingTransactionService
{
    public IBankConnectionBuilderReady SetTransActionService(IBankTransactionService transactionService);
}
interface IBankConnectionBuilderReady
{
    public IBankConnection Build();
}
class BankConnectionBuilder : IBankConnectionBuilder,
                              IBankConnectionBuilderExpectingLoggedInAccount,
                              IBankConnectionBuilderExpectingAccountsDB,
                              IBankConnectionBuilderExpectingTransactionService,
                              IBankConnectionBuilderReady
{
    private int id;
    private List<BankAccount> accounts;
    private IBankTransactionService bankTransactionService;
    public IBankConnectionBuilderExpectingAccountsDB SetAccount(BankAccount loggedInAccount)
    {
        id = loggedInAccount.Id;
        return this as IBankConnectionBuilderExpectingAccountsDB;
    }

    public IBankConnectionBuilderExpectingTransactionService SetAccounts(List<BankAccount> accounts)
    {
        this.accounts = accounts;
        return this as IBankConnectionBuilderExpectingTransactionService;
    }

    IBankConnectionBuilderReady IBankConnectionBuilderExpectingTransactionService.SetTransActionService(IBankTransactionService transactionService)
    {
        this.bankTransactionService = transactionService;
        return this as IBankConnectionBuilderReady;
    }
    public IBankConnection Build()
    {
        return new SecureBankConnection(id, accounts, bankTransactionService);
    }
}