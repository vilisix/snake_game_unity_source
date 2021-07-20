using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private FoodController _foodController;

    public UnityEvent EatEvent;
    public UnityEvent DeathEvent;
    public UnityEvent RestartGameEvent;

    private void Awake()
    {
        StartGame();
    }

    public void StartGame()
    {
        Vector3 snakeCoordinates = new Vector3(0,0,0);
        _snakeController.SpawnSnake(snakeCoordinates);
        _foodController.SpawnFood(GetFoodSpawnPosition());
    }

    public void GameStep()
    {
        //move snake
        _snakeController.MoveSnake();

        //check for eat condition
        if (_snakeController.GetHeadPosition() == _foodController.GetFoodPosition())
        {
            _snakeController.ExtendSnake();
            _foodController.DestroyFood();
            _foodController.SpawnFood(GetFoodSpawnPosition());

            EatEvent.Invoke();
        }

        //check for death condition
        if (_snakeController.GetBodyPosition().Contains(_snakeController.GetHeadPosition()))
        {
            _snakeController.DestroySnake();
            _foodController.DestroyFood();

            DeathEvent.Invoke();

            Invoke("RestartGame", 2.0f);
        }

    }
    private void RestartGame()
    {
        RestartGameEvent.Invoke();
    }

    private Vector3 GetFoodSpawnPosition()
    {
        return GameCoordinates.FindFreeSpace(_snakeController.GetSnakeCoords());
    }
}
