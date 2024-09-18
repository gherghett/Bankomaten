namespace Bankomaten;
class PIN
{
    private readonly string  _pin;

    public PIN(string pin)
    {
        _pin = pin;
    }

    public bool Is_PIN(string pin)
    {
        return _pin == pin;
    }
}