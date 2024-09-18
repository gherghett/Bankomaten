namespace Bankomaten;

public interface IAccountAction
{
    decimal Amount { get; }
}
class AccountActionFail : IAccountAction
{
    public decimal Amount {get;}
}
public record AccountOverCharged(decimal Amount) :  IAccountAction;
public record AccountWithdrawal(decimal Amount) : IAccountAction;
public record AccountDeposit(decimal Amount) : IAccountAction;
public record AccountTransfer(decimal Amount, int ToId) : IAccountAction;
public record AccountReceive(decimal Amount, int FromId) : IAccountAction;
interface IAccountActionResult 
{
    public string Message {get;}
    public IAccountAction Action {get;}
}
class AccountActionResultSuccess : IAccountActionResult
{
    public AccountActionResultSuccess(IAccountAction action, string message)
    {
        this.message = message;
        this.action = action;
    }
    private string message;
    private IAccountAction action;
    public string Message => message;

    public IAccountAction Action => action;
}

class AccountActionResultFailure : IAccountActionResult
{
    public AccountActionResultFailure(IAccountAction action, string message)
    {
        this.message = message;
    }
    private IAccountAction action;
    private string message;
    public string Message => message;
    public IAccountAction Action => action;

}