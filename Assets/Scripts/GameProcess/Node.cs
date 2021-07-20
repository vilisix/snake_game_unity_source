using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    private NodeMover _mover;
    private NodeScaler _scaler;
    [HideInInspector]public Vector3 NodePosition { get; private set; }
    [HideInInspector]public IDirection Direction { get; private set; }

    private void Awake()
    {
        NodePosition = this.transform.position;

        if (TryGetComponent<NodeMover>(out _mover) == false)
        {
            Debug.Log("NodeMover not founded! Creating...");
            _mover = this.gameObject.AddComponent<NodeMover>();
            _mover.MoveTo(this.transform.position);
        }

        if (TryGetComponent<NodeScaler>(out _scaler) == false)
        {
            Debug.Log("NodeScaler not founded! Creating...");
            _scaler = this.gameObject.AddComponent<NodeScaler>();
            _scaler.ScaleTo(this.transform.localScale);
        }
    }

    public void SetDirection(IDirection direction)
    {
        Direction = direction;
    }

    public Vector3 GetNodeNextPosition()
    {
        return NodePosition + Direction.GetDirection();
    }

    public void MoveNode()
    {
        NodePosition += Direction.GetDirection();
        _mover.MoveTo(NodePosition);
    }

    public void MoveAndDestroyNode()
    {
        MoveNode();
        _mover.DestroyAfterMoving();
    }

    public void ScaleNode(Vector3 target)
    {
        _scaler.ScaleTo(target);
    }

    public void ScaleImmidiatly() => _scaler.ScaleImmediatly();
}
