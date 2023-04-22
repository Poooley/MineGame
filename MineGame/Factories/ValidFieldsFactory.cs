namespace Minegame.Factories;

public static class ValidFieldsFactory
{
    public static int[] GetFields(MineColumn mineColumn, Settings settings) => mineColumn switch
    {
        MineColumn.Left => Enumerable.Range(0, settings.Length).Select(x => x * settings.Width).ToArray(),
        MineColumn.Right => Enumerable.Range(0, settings.Length).Select(x => x * settings.Width + settings.Width - 1).ToArray(),
        _ => throw new Exception("Not expected mine column")
    };
}
public enum MineColumn
{
    Left,
    Right
}