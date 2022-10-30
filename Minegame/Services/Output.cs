﻿using Microsoft.Extensions.Configuration;
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
    private readonly MySettings _settings;
    public Output(IOptionsSnapshot<MySettings> settings = null)
    {
        _settings = settings.Value;
    }

    public byte GetUserInput()
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

    public void SetPlayingField(byte currentRow, Field[] fields, bool showMines = false)
    {
        SetGameText(showMines);

        currentRow++;

        for (int row = 0; row < currentRow; row++)
        {
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
            > 20 => new Difficulty() { Name = "Schwer", Color = ConsoleColor.Red },
            > 10 => new Difficulty() { Name = "Mittel", Color = ConsoleColor.Magenta },
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
