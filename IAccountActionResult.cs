namespace Bankomaten;

public interface IAccountAction
{
}
public record AccountAction(decimal Amount) : IAccountAction
{
    public DateTime Date { get; init; } = DateTime.Now;
}
record AccountActionFail() : IAccountAction;
public record AccountOverCharged(decimal Amount) :  AccountAction(Amount)
{
    public override string ToString() => $"{Date} Ett utdrag kunde inte genomföras på grund av ett undeskott på {Amount}";
}
public record AccountWithdrawal(decimal Amount) : AccountAction(Amount)
{
    public override string ToString() => $"{Date} Ett utdrag på {Amount}";
}
public record AccountDeposit(decimal Amount) : AccountAction(Amount)
{
    public override string ToString() => $"{Date} En insättning på {Amount}";
}
public record AccountTransfer(decimal Amount, int ToId) : AccountAction(Amount)
{
    public override string ToString() => $"{Date} En överföring på {Amount} till {ToId}";
}
public record AccountReceive(decimal Amount,  int FromId) : AccountAction(Amount)
{
    public override string ToString() => $"{Date} En insättning från {FromId} på {Amount}";
}
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