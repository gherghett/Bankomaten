namespace Bankomaten;

interface IBankTransactionService
{
    public IAccountActionResult Withdraw(BankAccount account, decimal amount);
    public IAccountActionResult Deposit(BankAccount account, decimal amount);
    public IAccountActionResult SendMoney(List<BankAccount> accounts, BankAccount fromAccount, string toAccount, decimal amount);
}
class BankTransactionService : IBankTransactionService
{
    public IAccountActionResult Withdraw(BankAccount account, decimal amount)
    {
        return account.Withdraw(amount);
    }

    public IAccountActionResult Deposit(BankAccount account, decimal amount)
    {
        return account.Deposit(amount);
    }

    public IAccountActionResult SendMoney(List<BankAccount> accounts, BankAccount fromAccount, string toUserName, decimal amount)
    {
        BankAccount toAccount = accounts.Where(a=>a.UserName == toUserName).SingleOrDefault();
        if (toAccount == null)
        {
            return new AccountActionResultFailure(new AccountActionFail(), $"Kunde inte hitta kontot att skicka till");
        }
        IAccountActionResult sendingResult = fromAccount.Withdraw(amount, toAccount.Id);
        if( sendingResult is AccountActionResultFailure )
        {
            return sendingResult;
        }
        else
        {
            toAccount.Deposit(amount, fromAccount.Id);
        }
        return sendingResult;
    }
}
