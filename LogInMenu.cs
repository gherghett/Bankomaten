namespace Bankomaten;
class LogInMenu
{
    public LogInMenu(BankCommunicator communicator)
    {
        this.bankCommunicator = communicator;
    }
    private BankCommunicator bankCommunicator;
    public void ShowMenu()
    {
        int index = -1;
        while(index > -2)
        {
            // InputHandler.GetString("L, S, Q")[0];
            char choice = InputHandler.GetCharOfSet("[L]ogga in eller ([Q]uit)", ['L', 'Q']);
            switch (choice)
            {
                case 'L':
                    IBankConnection bankConnection = bankCommunicator
                                        .CreateSecureConnection(InputHandler.GetString("Ange användarnamn"),
                                                                InputHandler.GetString("Ange lösenord") );
                    Console.WriteLine(bankConnection.LoginMessage);
                    if (bankConnection is ISecureBankConnection secureConnection)
                    {
                        index = MainMenu.Show(secureConnection);
                    }
                    break;
                case 'S':
                    break;
                case 'Q':
                    return;
            }
        }
    }
}