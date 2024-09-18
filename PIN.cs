namespace Bankomaten;
class PIN
{
    private string readonly _pin;

    public bool Is_PIN(string pin)
    {
        return _pin == pin;
    }
}