namespace Bankomaten;
internal class Program
{
    static void Main()
    {
        BankCommunicator bankCommunicator = new BankCommunicator();
        LogInMenu logInMenu = new LogInMenu(bankCommunicator);

        logInMenu.ShowMenu();
    }
}