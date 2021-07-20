using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour
{
    [SerializeField] private Food _food;

    public void SpawnFood(Vector3 position) => _food.SpawnFood(position);

    public void DestroyFood() => _food.DestroyFood();

    public Vector3 GetFoodPosition() => _food.FoodPosition;
}
