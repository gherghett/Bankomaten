namespace Bankomaten;

interface IBankTransactionService
{
    public IAccountActionResult Withdraw(BankAccount account, decimal amount);
    public IAccountActionResult Deposit(BankAccount account, decimal amount);
    public IAccountActionResult SendMoney(BankAccount fromAccount, BankAccount toAccount, decimal amount);
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

    public IAccountActionResult SendMoney(BankAccount fromAccount, BankAccount toAccount, decimal amount)
    {
        IAccountActionResult result = fromAccount.Withdraw(amount);
        if (result is AccountActionResultFailure)
        {
            return result;
        }
        return toAccount.Deposit(amount, fromAccount.Id);
    }
}
