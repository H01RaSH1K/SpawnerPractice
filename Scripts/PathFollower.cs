using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private CicledPath _path;

    private Mover _mover;
    private Transform _nextWayPoint;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        SetNextWayPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _nextWayPoint)
            SetNextWayPoint();
    }

    private void SetNextWayPoint()
    {
        _nextWayPoint = _path.GetNextWayPoint(_nextWayPoint);
        _mover.SetTarget(_nextWayPoint);
    }
}
