namespace Minegame.Factories;

public static class KeysFactory
{
    public static IDictionary<EnabledKey, EnabledKeys> GetDefaultKeys()
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

    public static EnabledKeys GetEnabledKeys(IDictionary<EnabledKey, EnabledKeys> enabledKeys, 
        bool leftNotAllowed, bool rightNotAllowed)
    {
        if (leftNotAllowed)
            return enabledKeys[EnabledKey.Left];
        
        if (rightNotAllowed)
            return enabledKeys[EnabledKey.Right];
        
        return enabledKeys[EnabledKey.All];
    }
}

public enum EnabledKey
{
    Left,
    All,
    Right
}