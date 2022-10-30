using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace Minegame.Services;

internal class GameManager : IGameManager
{
    private readonly IOutput _console;
    private MySettings _settings;
    private Field[] fields;
    private byte currentRow = 0;

    public GameManager(IOutput console, IOptionsSnapshot<MySettings> settings = null)
    {
        _console = console;
        _settings = settings.Value;
    }

    public void Start()
    {
        Console.Clear();
        InitializeGame();
    }

    public void Stop()
    {
        Console.Write("\nNoch eine Runde? (j/n)? ");
        
        if (!Console.ReadLine().ToLower().Equals("n"))
        {
            InitializeGame();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    public void Move()
    {
        while (true)
        {
            Console.Clear();
            _console.SetPlayingField(currentRow, fields);
            var userInput = _console.GetUserInput();

            var curPos = (currentRow * _settings.Width) + userInput;
            fields[curPos].IsFlagged = true;

            if (fields[curPos].IsMine)
                IsLose();

            if (currentRow == _settings.Length - 1)
                IsWin();

            currentRow++;
        }

    }
    private void IsWin()
    {
        SetFinishText("Sie haben gewonnen! Glückwunsch!");
        Stop();
    }
    private void IsLose()
    {
        SetFinishText("Sie haben leider verloren! Versuchen Sie es erneut!");
        Stop();
    }

    private void SetFinishText(string text)
    {
        Console.Clear();
        _console.SetPlayingField((byte)(_settings.Length - 1), fields, true);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n----- {text} -----");
        Console.ForegroundColor = ConsoleColor.Gray;

    }

    private void InitializeGame()
    {
        fields = InitializeFields();
        currentRow = 0;
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
}