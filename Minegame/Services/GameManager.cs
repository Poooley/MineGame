using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Minegame.Services;

internal class GameManager : IGameManager
{
    private readonly IOutput _console;
    private readonly IConfig _configuration;
    public GameManager(IOutput console, IConfig config)
    {
        _console = console;
        _configuration = config;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Game!");
        InitializeGame();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
    private void IsWin()
    {
        
    }
    private void IsLose()
    {
        
    }
    private void IsDraw()
    {
        
    }
    private void InitializeGame()
    {
        Field[] fields = new Field[_configuration.Fields];

        var length = _configuration.Fields / _configuration.Width;

        _console.SetPlayingField(_configuration.Width, length);

        for (int i = 0; i < _configuration.Width; i++) 
        {
            for (int j = 0; j < length; j++)
            {
                _console.Write("X");
            }
            _console.WriteLine("");
        }
    }

}