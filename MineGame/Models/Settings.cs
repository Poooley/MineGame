using Microsoft.Extensions.Configuration;

namespace Minegame.Models;
public class Settings
{
    public int Fields { get; set; }
    public byte Width { get; set; }
    public byte Length { get; set; }
    public byte Mines { get; set; }
}