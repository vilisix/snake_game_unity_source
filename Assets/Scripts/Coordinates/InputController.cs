using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public IDirection Direction { get; private set; }

    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;

    [SerializeField] private float _minDistanceForSwipe = 20f;

    private void OnEnable()
    {
        ResetDirection();
    }

    public void ResetDirection()
    {
        Direction = new Up();
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPosition = touch.position;
                _fingerDownPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDownPosition = touch.position;
                Direction = GetSwipeDirection();
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) Direction = new Up();
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) Direction = new Down();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) Direction = new Left();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) Direction = new Right();
    }

    public bool IsOppositeFor(IDirection currentDirection)
    {
        return (currentDirection is Up && Direction is Down)
                || (currentDirection is Down && Direction is Up)
                || (currentDirection is Left && Direction is Right)
                || (currentDirection is Right && Direction is Left);
    }

    private IDirection GetSwipeDirection()
    {
        if(Vector2.Distance(_fingerDownPosition,_fingerUpPosition) > _minDistanceForSwipe)
        {
            float angle = FindDegree(_fingerDownPosition-_fingerUpPosition);
            if (angle >= 45.0f && angle < 135.0f) return new Up();
            else if (angle >= 135.0f && angle < 225.0f) return new Left();
            else if (angle >= 225.0f && angle < 315.0f) return new Down();
            else return new Right();
        }
        return Direction;
    }

    public static float FindDegree(Vector2 vector)
    {
        float value = (float)((Mathf.Atan2(vector.y, vector.x) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }
}
