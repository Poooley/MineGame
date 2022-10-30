using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Interfaces;
interface IOutput
{
    byte GetUserInput();
    public void SetPlayingField(byte currentRow, Field[] fields, bool showMines = false);
}

