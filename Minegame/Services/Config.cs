using Microsoft.Extensions.Configuration;

namespace Minegame.Services;
public class Config : IConfig
{
    readonly byte _width;
    readonly byte _length;
    readonly int _fields;
    readonly byte _mines;
    public Config(IConfiguration config)
    {
        _length = config.GetValue<byte>("Length");
        _width = config.GetValue<byte>("Width");
        _fields = _length * _width;
        _mines = config.GetValue<byte>("Mines");
    }

    public int Fields { get => _fields; }
    public byte Width { get => _width; }
    public byte Length { get => _length; }
    public byte Mines { get => _mines; }
}