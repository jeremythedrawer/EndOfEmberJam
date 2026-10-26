using UnityEngine;
using System;

public static class Biology
{
    public const int CELL_COUNT = 64;
    public enum CellType
    {
        None,
        Plant, // hexagon shape
        Mammal, // circle shape
        Fungus, //bean shape
    }
    [Flags] public enum EnvironmentState
    {
        None = 0,
        Temperature = 1 << 0, // Cell speed
        Light = 1 << 1, // Cell growing
        Humidity = 1 << 3, // Cell pulsing
    }
    public enum ViewType
    {
        None,
        Room,
        Microscope,
        PetriDishTable,
    }
    [Serializable] public struct TriggerCircle
    {
        public Vector3 center;
        public float radius;
    }

}
