using Microsoft.Extensions.Configuration;

namespace Minegame.Services;
public class Config : IConfig
{
    IConfiguration _config;
    byte _fields;
    byte _width;
    byte _mines;
    public Config(IConfiguration config)
    {
        _config = config;
    }

    public byte Fields { get => _config.GetValue<byte>("Fields"); }
    public byte Width { get => _config.GetValue<byte>("Width"); }
    public byte Mines { get => _config.GetValue<byte>("Mines"); }
}