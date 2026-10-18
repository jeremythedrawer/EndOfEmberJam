
using System;

public static class Biology
{
    public const int CELL_COUNT = 64;
    public enum CellType
    {
        Plant, // hexagon shape
        Mammal, // circle shape
        Mold, //bean shape
    }
    [Flags] public enum EnvironmentState
    {
        None = 0,
        Temperature = 1 << 0, // Cell speed
        Light = 1 << 1, // Cell growing
        Humidity = 1 << 3, // Cell pulsing
    }
}
