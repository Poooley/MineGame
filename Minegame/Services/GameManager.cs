using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;

namespace Minegame.Services;

public class GameManager : IHostedService
{
    private readonly IOutput _console;
    private int _currentRow = 0;
    private readonly Settings _settings;
    private Field[] _fields;
    
    public GameManager(IOutput console, IOptionsSnapshot<Settings> settings = null)
    {
        _console = console;
        _settings = settings.Value;
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    public async Task StartAsync(CancellationToken cancellationToken)
    {
       Start:
       await Task.Factory.StartNew(InitializeGame, cancellationToken, 
            TaskCreationOptions.LongRunning, TaskScheduler.Default);

        Console.Write("\nNoch eine Runde? (j/n)? ");

        if (!Console.ReadLine().ToLower().Equals("n"))
            goto Start;
        else
            Console.WriteLine("Drücke Ctrl+C um das Programm zu schließen");
    }
    
    private void InitializeGame()
    {
        _fields = InitializeFields();
        _currentRow = 0;

        Move();
    }
    private Field[] InitializeFields()
    {
        var fields = new Field[_settings.Fields];

        // Initialize fields
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i] = new Field();
        }

        var mines = _settings.Mines;
        var random = new Random();

        // Set all mines to fields
        while (mines > 0)
        {
            var field = random.Next(0, _settings.Fields);
            if (!fields[field].IsMine)
            {
                fields[field].IsMine = true;
                mines--;
            }
        }

        return fields;
    }
    public void Move(bool isFirstRound = true)
    {
        int lastPos = 0;
        while (true)
        {
            Console.Clear();

            _console.SetPlayingField(_currentRow, _fields);
            
            var curPos = isFirstRound ? _console.GetUserInputFirstRound() : _console.GetUserInput(lastPos);
            
            lastPos = curPos;
            

            _fields[curPos].IsFlagged = true;

            if (_fields[curPos].IsMine)
            {
                IsLose();
                break;
            }

            if (_currentRow == _settings.Length - 1)
            {
                IsWin();
                break;
            }

            isFirstRound = false;
            _currentRow++;
        }
    }
    private void IsWin() => SetFinishText("Sie haben gewonnen! Glückwunsch!");
    private void IsLose() => SetFinishText("Sie haben leider verloren! Versuchen Sie es erneut!");
    private void SetFinishText(string text)
    {
        Console.Clear();
        _console.SetPlayingField(_settings.Length - 1, _fields, true);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n----- {text} -----");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}