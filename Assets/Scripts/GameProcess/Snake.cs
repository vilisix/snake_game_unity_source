using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour
{
    [SerializeField] private Node _snakeNode;
    [SerializeField] private InputController _input;

    private Node _head;
    private List<Node> _body;

    private Vector3 _lastNodePosition;
    private IDirection _lastNodeDirection;

    //====================================ON AWAKE(WHEN INSTANTIATING)================================

    public void SpawnSnake(Vector3 position)
    {
        _head = Instantiate(_snakeNode, position, Quaternion.identity, transform);

        _body = new List<Node>();
    }

    //====================================MOVING WHOLE SNAKE==========================================

    public void MoveSnake()
    {
        //========= taking input direction from inputController
        if(_input.Direction != null && _input.Direction != _head.Direction && _input.IsOppositeFor(_head.Direction) == false)
        {
            _head.SetDirection(_input.Direction);
        }
        //========= setting direction and position of last node(for adding a new one)
        if (_body.Count == 0)
        {
            _lastNodeDirection = _head.Direction;
            _lastNodePosition = _head.NodePosition;
        }
        else
        {
            _lastNodeDirection = _body[_body.Count - 1].Direction;
            _lastNodePosition = _body[_body.Count - 1].NodePosition;
        }
        //========= moving head and all body

        if(WillMirrored(_head))
        {
            _head.MoveAndDestroyNode();
            _head = CreateIncomingClone(_head);
        }

        _head.MoveNode();

        for(int i=0; i < _body.Count; i++)
        {
            if (WillMirrored(_body[i]))
            {
                _body[i].MoveAndDestroyNode();
                _body[i] = CreateIncomingClone(_body[i]);
            }
            _body[i].MoveNode();
        }
        //========= changing body nodes directions
        if (_body.Count > 0)
        {
            for (int i = _body.Count - 1; i > 0; i--)
            {
                _body[i].SetDirection(_body[i - 1].Direction);
            }
            _body[0].SetDirection(_head.Direction);
        }

    }

    //====================================ADDING NODE TO A SNAKE'S TAIL===============================

    public void AddNode()
    {
        _body.Add(Instantiate(_snakeNode, _lastNodePosition, Quaternion.identity, transform));
        _body[_body.Count - 1].SetDirection(_lastNodeDirection);
    }

    //=============================RETURNING VECTORS OF SNAKE NODES===================================

    public Vector3[] GetCoords()
    {
        Vector3[] headArray = { GetHeadPosition() };
        return GetBodyPosition().Concat(headArray).ToArray();
    }

    public Vector3 GetHeadPosition()
    {
        return _head.NodePosition;
    }

    public Vector3[] GetBodyPosition()
    {
        return _body.Select(x => x.NodePosition).ToArray();
    }

    //====================================DESTROYING A SNAKE===========================================

    public void DestroySnake()
    {
        _head.ScaleNode(new Vector3(0, 0, 1));
        Destroy(_head.gameObject, 0.4f);
        _head = null;
        foreach (var node in _body)
        {
            node.ScaleNode(new Vector3(0, 0, 1));
            Destroy(node.gameObject, 0.4f);
        }
        _body.Clear();
    }

    private bool WillMirrored(Node node)
    {
        return GameCoordinates.IsNodeLeavingCircle(node);
    }

    private Node CreateIncomingClone(Node node)
    {
        Node clone = Instantiate(_snakeNode, GameCoordinates.GetCloneCoordinates(node), Quaternion.identity, transform);
        clone.ScaleImmidiatly();
        clone.SetDirection(node.Direction);
        return clone;
    }
}
