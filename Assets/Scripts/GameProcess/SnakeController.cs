using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private Snake _snake;

    public void SpawnSnake(Vector3 position) => _snake.SpawnSnake(position);

    public void MoveSnake() => _snake.MoveSnake();

    public void ExtendSnake() => _snake.AddNode();

    public void DestroySnake() => _snake.DestroySnake();

    public Vector3[] GetSnakeCoords() => _snake.GetCoords();

    public Vector3 GetHeadPosition() => _snake.GetHeadPosition();

    public Vector3[] GetBodyPosition() => _snake.GetBodyPosition();
}
