using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collider))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private PathCicled _path;
    [SerializeField] private float _wayPointThreshold;
    
    private float _wayPointThresholdSquared;

    private Mover _mover;
    private Transform _nextWayPoint;

    private void Awake()
    {
        _wayPointThresholdSquared = _wayPointThreshold * _wayPointThreshold;
        _mover = GetComponent<Mover>();
        SetNextWayPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        float distanceToWayPointSquared = (other.transform.position - _nextWayPoint.position).sqrMagnitude;

        if (distanceToWayPointSquared < _wayPointThresholdSquared)
            SetNextWayPoint();
    }

    private void SetNextWayPoint()
    {
        _nextWayPoint = _path.GetNextWayPoint(_nextWayPoint);
        _mover.SetTarget(_nextWayPoint);
    }
}
