using System;
using UnityEngine;

public class NodeMover : MonoBehaviour
{
    [SerializeField] private float _delta;
    private Vector3 _target;

    private bool _destroyAfterMoving = false;

    private void Awake()
    {
        _target = this.transform.position;
    }

    public void MoveTo(Vector3 target)
    {
        _target = target;
    }

    public void DestroyAfterMoving() => _destroyAfterMoving = true;

    private void FixedUpdate()
    {
        if (transform.position != _target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _delta);
        }
        else
        {
            transform.position = _target;
            if (_destroyAfterMoving) Destroy(this.gameObject);
        }
    }
}
