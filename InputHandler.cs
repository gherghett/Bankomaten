namespace Bankomaten;
class InputHandler
{
    public static int GetInt(string prompt)
    {
        while (true)
        {
            Prompt(prompt);
            int output;
            if (int.TryParse(Console.ReadLine(), out output))
            {
                return output;
            }
            Console.WriteLine("skriv en int.");
        }
    }
    public static char GetChar(string prompt)
    {
        Prompt(prompt);
        char output;
        do
        {
            output = char.ToUpper(Console.ReadKey(true).KeyChar);
        }
        while (!char.IsLetterOrDigit(output));

        return output;

    }
    public static char GetCharOfSet(string prompt, char[] acceptedChars)
    {
        while (true)
        {
            char output;
            output = GetChar(prompt);
            Console.Write(output + "\n");
            if (Contains(output, acceptedChars))
                return output;
            else
                Console.WriteLine("Felaktig input");
        }

    }
    public static bool Contains(char c, char[] chars)
    {
        for (int i = 0; i < chars.Length; i++)
        {
            if (c == chars[i])
            {
                return true;
            }
        }
        return false;
    }
    public static string GetString(string prompt)
    {
        Prompt(prompt);
        return Console.ReadLine();
    }
    private static void Prompt(string prompt) =>
        Console.Write($"{prompt}: ");

}