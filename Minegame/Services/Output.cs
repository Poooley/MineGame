using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Services;

internal class Output : IOutput
{
    private readonly IConfig _config;
    public Output(IConfig config)
    {
        _config = config;
    }

    public byte GetUserInput()
    {
        var min = 1;
        while (true)
        {
            Console.Write($"\nWas ist Ihr nächster Zug? Geben Sie eine Zahl zwischen {min} und {_config.Width} ein. ");
            if (byte.TryParse(Console.ReadLine(), out byte input))
            {
                if (input >= min && input <= _config.Width)
                {
                    return --input;
                }
            }
        }
    }

    public void SetPlayingField(byte currentRow, Field[] fields, bool showMines = false)
    {
        currentRow++;
        Console.WriteLine("Das Spielfeld" + (showMines ? " inkl. Minen war:" : ":") + "\n");
        for (int row = 0; row < currentRow; row++)
        {
            for (int column = 0; column < _config.Width; column++)
            {
                var curPos = (row * _config.Width) + column;
                
                if (fields[curPos].IsFlagged && fields[curPos].IsMine)
                    Console.Write("T ");
                else if (fields[curPos].IsFlagged)
                    Console.Write("O ");
                else if (showMines && fields[curPos].IsMine)
                    Console.Write("M ");
                else
                    Console.Write("X ");
            }
            Console.WriteLine();
        }
    }
}
