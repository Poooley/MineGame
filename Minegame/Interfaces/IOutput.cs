using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Interfaces;
interface IOutput
{
    void SetPlayingField(byte width, double length);
    void Write(string input);
    void WriteLine(string input);
}

