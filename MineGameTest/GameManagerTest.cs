using Microsoft.Extensions.Options;
using Moq;

namespace MineGameTest;

public class GameManagerTest
{
    [Fact]
    public void Start()
    {
        var optionsSnapshotMock = GetSettingsMock();
        
        IOutput output = new Output(optionsSnapshotMock.Object);
        IGameManager game = new GameManager(output, optionsSnapshotMock.Object);

        game.Start();
    }

    private Mock<IOptionsSnapshot<Settings>> GetSettingsMock()
    {
        var settingsMock = new Mock<IOptionsSnapshot<Settings>>();
        settingsMock.SetupGet(x => x.Value).Returns(new Settings
        {
            Fields = 10,
            Length = 10,
            Width = 10,
            Mines = 10
        });
        return settingsMock;
    }
}