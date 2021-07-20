using System;
using UnityEngine;

public class Up : IDirection
{
    public Vector3 GetDirection() => new Vector3(0, GameCoordinates.Step , 0);
}

public class Down : IDirection
{
    public Vector3 GetDirection() => new Vector3(0, -GameCoordinates.Step, 0);
}

public class Left : IDirection
{
    public Vector3 GetDirection() => new Vector3(-GameCoordinates.Step, 0, 0);
}

public class Right : IDirection
{
    public Vector3 GetDirection() => new Vector3(GameCoordinates.Step, 0, 0);
}