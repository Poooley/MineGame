using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minegame.Interfaces;
interface IOutput
{
    int GetUserInput(int curPos);
    byte GetUserInputFirstRound();
    public void SetPlayingField(int currentRow, Field[] fields, bool showMines = false);
}

