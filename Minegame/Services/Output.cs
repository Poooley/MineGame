using Microsoft.Extensions.Options;
using Minegame.Factories;
using System.Data.Common;

namespace Minegame.Services;
public class Output : IOutput
{
    private readonly Settings _settings;
    private readonly int[] leftFields;
    private readonly int[] rightFields;   
    private readonly IDictionary<EnabledKey, EnabledKeys> enabledKeys = KeysFactory.GetDefaultKeys();
    
    public Output(IOptionsSnapshot<Settings> settings = null)
    {
        _settings = settings.Value;
        leftFields = ValidFieldsFactory.GetFields(MineColumn.Left, _settings);
        rightFields = ValidFieldsFactory.GetFields(MineColumn.Right, _settings);
    }
    
    public int GetUserInput(int curPos)
    {
        bool leftNotAllowed = leftFields.Any(x => x == curPos);
        bool rightNotAllowed = rightFields.Any(x => x == curPos);

        EnabledKeys keys = KeysFactory.GetEnabledKeys(enabledKeys, leftNotAllowed, rightNotAllowed);
        
        while (true)
        {
            Console.WriteLine(keys.Description);
            
            // get Cursor keys input
            var key = Console.ReadKey(true);

            if (!keys.Keys.Contains(key.Key))
                continue;
            
            return key.Key switch
            {
                ConsoleKey.RightArrow => curPos + 1 + _settings.Width,
                ConsoleKey.LeftArrow => curPos - 1 + _settings.Width,
                ConsoleKey.DownArrow => curPos + _settings.Width,
                _ => throw new Exception("Unhandled key"),
            };
        }
    }

    public byte GetUserInputFirstRound()
    {
        var min = 1;
        while (true)
        {
            Console.Write($"\nWas ist Ihr nächster Zug? Geben Sie eine Zahl zwischen {min} und {_settings.Width} ein. ");
            if (byte.TryParse(Console.ReadLine(), out byte input))
            {
                if (input >= min && input <= _settings.Width)
                {
                    return --input;
                }
            }
        }
    }

    public void SetPlayingField(int currentRow, Field[] fields, bool showMines = false)
    {
        SetGameText(showMines);

        currentRow++;

        for (int row = 0; row < currentRow; row++)
        {
            Console.Write("\t");
            for (int column = 0; column < _settings.Width; column++)
            {
                var curPos = (row * _settings.Width) + column;

                if (fields[curPos].IsFlagged && fields[curPos].IsMine)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("T ");
                }
                else if (fields[curPos].IsFlagged)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("O ");
                }
                else if (showMines && fields[curPos].IsMine)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("M ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("X ");
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }

    private void SetGameText(bool showMines)
    {
        var difficultyInPercent = Math.Round((double)_settings.Mines / _settings.Fields * 100, 0);

        Difficulty difficulty = DifficultyFactory.GetDifficulty(difficultyInPercent);

        Console.ForegroundColor = difficulty.Color;
        Console.WriteLine($"Eingestellter Schwierigkeitsgrad: {difficulty.Name} {difficultyInPercent}%");
        Console.WriteLine($"(M: {_settings.Mines}, F: {_settings.Fields})\n\n");
        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine("Das Spielfeld" + (showMines ? " inkl. Minen war:" : ":") + "\n");
    }
}
