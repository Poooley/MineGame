# MineGame

## Instructions
1. Use the numpad to enter a number between 1 and 15 to make your first move.
2. Use the arrow keys to move around the board and avoid the mines.

## Gameplay
By default, the game has the following settings:

* Difficulty level: Medium (7% mines)
* Board size: 15 x 2
* Number of mines: 10

You can change the game settings by editing the appsettings.json file. The following settings are available:

```json
{
  "Length": 10,
  "Width": 15,
  "Mines": 10
}
```

## Building the Game

To build the MineGame, you'll need to have .NET 7.0 installed on your system.

1. Clone the repository using git clone
2. Navigate to the root directory of the project: cd MineGame
3. Restore the project dependencies: dotnet restore
4. Build the project in release mode: dotnet build -c Release
5. Navigate to the MineGame directory: cd MineGame
6. Run the game: dotnet run