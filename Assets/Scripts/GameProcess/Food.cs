using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
    [SerializeField] private Node _foodNode;
    [HideInInspector] public Vector3 FoodPosition { get; private set; }

    private Node _food;

    public void SpawnFood(Vector3 position)
    {
        if (_food == null)
        {
            _food = Instantiate(_foodNode, position, Quaternion.identity, transform);
            FoodPosition = position;
        }
        else Debug.Log("Food already spawned!");
    }

    public void DestroyFood()
    {
        if (_food)
        {
            _food.ScaleNode(new Vector3(0, 0, 1));
            Destroy(_food.gameObject,0.4f);
            _food = null;
        }
        else Debug.Log("Nothing to destroy! Food is NULL!");
    }

}
