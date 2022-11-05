namespace Minegame.Factories;

public static class KeysFactory
{
    public static IDictionary<EnabledKey, EnabledKeys> GetEnabledKeys()
    {
        EnabledKeys leftHandSide = new EnabledKeys()
        {
            Keys = new List<ConsoleKey> { ConsoleKey.RightArrow, ConsoleKey.DownArrow },
            Description = $"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ► ▼ "
        };

        EnabledKeys allFields = new EnabledKeys()
        {
            Keys = new List<ConsoleKey> { ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow },
            Description = $"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ◄ ▼ ► "

        };

        EnabledKeys rightHandSide = new EnabledKeys()
        {
            Keys = new List<ConsoleKey> { ConsoleKey.LeftArrow, ConsoleKey.DownArrow },
            Description = $"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ◄ ▼ "

        };

        return new Dictionary<EnabledKey, EnabledKeys>()
        {
            { EnabledKey.Left, leftHandSide },
            { EnabledKey.All, allFields },
            { EnabledKey.Right, rightHandSide }
        };
    }
}

public enum EnabledKey
{
    Left,
    All,
    Right
}