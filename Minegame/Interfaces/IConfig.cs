namespace Minegame.Interfaces
{
    public interface IConfig
    {
        int Fields { get; }
        byte Length { get; }
        byte Mines { get; }
        byte Width { get; }
    }
}