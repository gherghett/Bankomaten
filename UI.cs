
namespace Bankomaten;
class UI
{
    public static void DisplayBalance(BankAccount account) =>
        Console.WriteLine($"Du har {account.Balance} pengar!");

    public static void DisplayWelcome(BankAccount account)
    {
        Console.WriteLine($"Välkommen {account.UserName}");
        DisplayBalance(account);
    }

    internal static void DisplayHistory(BankAccount account)
    {
        foreach(IAccountAction action in account.History)
        {
            Console.WriteLine(action);
        }
    }

}