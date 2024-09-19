namespace Bankomaten;
class BankAccount
{
    public int Id { get => bankUser.Id;}
    public decimal Balance { get => balance; }
    public string UserName { get => bankUser.Name; }
    private BankUser bankUser;
    private decimal balance;

    public List<IAccountAction> History {get;}

    public BankAccount(BankUser bankUser, decimal balance)
    {
        this.bankUser = bankUser;
        this.balance = balance;
        this.History = new List<IAccountAction>();
    }

    public IAccountActionResult Withdraw(decimal amount, int toId) =>
        IsValidWithdrawal(amount) 
        ? new AccountActionResultSuccess(WithdrawalAction(amount,toId), $"Du förde över {amount} till konto {toId}") 
        : new AccountActionResultFailure(new AccountOverCharged(amount-balance), $"Kunde inte skicka pengarna");

    public IAccountActionResult Withdraw(decimal amount) =>
        IsValidWithdrawal(amount) 
        ? new AccountActionResultSuccess(WithdrawalAction(amount), $"Du tog ut {amount}")
        : new AccountActionResultFailure(new AccountOverCharged(amount-balance), $"Kunde inte ta ut pengarna");
    private AccountTransfer WithdrawalAction(decimal amount, int toId)
    {
        balance -= amount;
        return (AccountTransfer)AddToHistoryAndReturn(new AccountTransfer(amount,toId));
    }
    
    private AccountWithdrawal WithdrawalAction(decimal amount)
    {
        balance -= amount;
        return (AccountWithdrawal)AddToHistoryAndReturn(new AccountWithdrawal(amount));
    }


    public IAccountActionResult Deposit(decimal amount)
    {
        balance += amount;
        return new AccountActionResultSuccess(AddToHistoryAndReturn(new AccountDeposit(amount)), $"Du lade in {amount}");
    }
    public IAccountActionResult Deposit(decimal amount, int IdFrom)
    {
        balance += amount;
        //först läggs historian för detta kontot till
        AddToHistoryAndReturn(new AccountReceive(amount, IdFrom));
        //sen skickar vi tillbaka till den som kallat på fucntionen (från ett annat konto)
        return new AccountActionResultSuccess(new AccountTransfer(amount, Id), $"Du skickade ${amount} till {UserName}");
    }

    private IAccountAction AddToHistoryAndReturn(IAccountAction action)
    {
        History.Add(action);
        return action;
    }

    public bool IsPinwordCorrect(string pin) =>
        bankUser.IsPinwordCorrect(pin);
    
    public bool IsValidWithdrawal(decimal withdrawal) =>
        withdrawal >= 0m && balance - withdrawal >= 0m;
}