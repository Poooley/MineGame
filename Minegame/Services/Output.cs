using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Services;

internal class Output : IOutput
{
    public void SetPlayingField(byte width, double length)
    {
        for (int i = 0; i < length; i++)
        {
            for (int y = 0; y < width; y++)
            {
                Console.Write("X ");
            }
            Console.WriteLine();
        }
    }

    public void Write(string input)
    {
        throw new NotImplementedException();
    }

    public void WriteLine(string input)
    {
        throw new NotImplementedException();
    }
}
