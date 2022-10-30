using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Services;

internal class Output : IOutput
{
    private readonly Settings _settings;
    int[] leftFields;
    int[] rightFields;
    public Output(IOptionsSnapshot<Settings> settings = null)
    {
        _settings = settings.Value;
        leftFields = Enumerable.Range(0, _settings.Length).Select(x => (x * _settings.Width)).ToArray();
        rightFields = Enumerable.Range(0, _settings.Length).Select(x => ((x * _settings.Width) + _settings.Width -1)).ToArray();
    }
        
    public int GetUserInput(int curPos)
    {
        bool leftNotAllowed = leftFields.Any(x => x == curPos);
        bool rightNotAllowed = rightFields.Any(x => x == curPos);

        while (true)
        {
            var allowedKeys = new List<ConsoleKey>();
            if (leftNotAllowed)
            {
                Console.Write($"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ► ▼ ");
                allowedKeys.Add(ConsoleKey.RightArrow);
                allowedKeys.Add(ConsoleKey.DownArrow);
            }
            else if (rightNotAllowed)
            {
                Console.Write($"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ◄ ▼ ");
                allowedKeys.Add(ConsoleKey.LeftArrow);
                allowedKeys.Add(ConsoleKey.DownArrow);
            }
            else
            {
                Console.Write($"\nWas ist Ihr nächster Zug? Pfeiltasten erlaubt: ◄ ▼ ► ");
                allowedKeys.Add(ConsoleKey.RightArrow);
                allowedKeys.Add(ConsoleKey.LeftArrow);
                allowedKeys.Add(ConsoleKey.DownArrow);
            }

            // get Cursor keys input
            var key = Console.ReadKey(true);

            if (!allowedKeys.Contains(key.Key))
                continue;

            return key.Key switch
            {
                ConsoleKey.RightArrow => curPos + 1 + _settings.Width,
                ConsoleKey.LeftArrow => curPos - 1 + _settings.Width,
                ConsoleKey.DownArrow => curPos + _settings.Width,
                _ => curPos
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

        var difficulty = difficultyInPercent switch
        {
            > 10 => new Difficulty() { Name = "Schwer", Color = ConsoleColor.Red },
            > 5 => new Difficulty() { Name = "Mittel", Color = ConsoleColor.Magenta },
            _ => new Difficulty() { Name = "Leicht", Color = ConsoleColor.Yellow },
        };

        Console.ForegroundColor = difficulty.Color;
        Console.WriteLine($"Eingestellter Schwierigkeitsgrad: {difficulty.Name} {difficultyInPercent}%");
        Console.WriteLine($"(M: {_settings.Mines}, F: {_settings.Fields})\n\n");
        Console.ForegroundColor = ConsoleColor.Gray;

        Console.WriteLine("Das Spielfeld" + (showMines ? " inkl. Minen war:" : ":") + "\n");
    }

    class Difficulty
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
