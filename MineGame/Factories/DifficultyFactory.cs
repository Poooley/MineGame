namespace Minegame.Factories;

public static class DifficultyFactory
{
    public static Difficulty GetDifficulty(double difficultyInPercent)
    {
        return difficultyInPercent switch
        {
            > 10 => new Difficulty() { Name = "Schwer", Color = ConsoleColor.Red },
            > 5 => new Difficulty() { Name = "Mittel", Color = ConsoleColor.Magenta },
            _ => new Difficulty() { Name = "Leicht", Color = ConsoleColor.Yellow },
        };
    }
}