namespace Bankomaten;
class BankUser
{
    private PIN pin;
    public string Name { get => userName; }
    private string userName;
    private int id;
    public int Id { get => id; }
    private static int currentId = 0;
    public BankUser(string userName, string newPin)
    {
        this.userName = userName;
        this.pin = new PIN(newPin);
        this.id = currentId++;
    }
    public bool IsPinwordCorrect(string pin) =>
        this.pin.Is_PIN(pin);
    
}