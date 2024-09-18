using System.IO.Compression;
using System.Net.Security;

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

    public IBankConnection CreateSecureConnection(string userName, string pinword)
    {
        BankAccount? loggedInAccount = AreCorrectCredentials(userName, pinword);
        if(loggedInAccount != null)
            return new SecureBankConnection(loggedInAccount.Id, accounts);
        else
            return new FailedBankConnection();
    }

    private BankAccount? AreCorrectCredentials(string userName, string pinword ) =>
        accounts.Where(a => a.UserName == userName).SingleOrDefault(a => a.IsPinwordCorrect(pinword));

}