using System;
using UnityEngine;
using System.Linq;


public class GameCoordinates
{
    public static float Step { get; private set; }  = 0.5f;
    public static float GameFieldRadius { get; private set; } = 4.25f;
    public static float DotRadius { get; private set; } = Step / 2;

    public static Vector3 GetRandomCoordinates()
    {
        Vector3 coordinates;
        do
        {
            float x = Mathf.Round(UnityEngine.Random.Range(-8, 9));
            float y = Mathf.Round(UnityEngine.Random.Range(-8, 9));
            coordinates = new Vector3(x * Step, y * Step, 0);
        } while (IsOutOfBounds(coordinates));
        return coordinates;
    }

    public static Vector3 FindFreeSpace(Vector3[] obstacles)
    {
        Vector3 _freeSpace;
        do {
            _freeSpace = GetRandomCoordinates();
        } while (obstacles.Contains(_freeSpace));
        return  _freeSpace;
    }

    private static bool IsOutOfBounds(Vector3 position)
    {
        return position.magnitude > (GameFieldRadius - DotRadius);
    }

    public static bool IsNodeLeavingCircle(Node node)
    {
        return node.GetNodeNextPosition().magnitude >= (GameFieldRadius + DotRadius);
    }

    public static Vector3 GetCloneCoordinates(Node node)
    {
        return Vector3.Reflect(node.NodePosition, node.Direction.GetDirection().normalized);
    }

}
