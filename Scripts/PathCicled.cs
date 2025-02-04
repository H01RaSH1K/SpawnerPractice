using System.Collections.Generic;
using UnityEngine;

public class PathCicled : MonoBehaviour
{
    [SerializeField] List<Transform> _wayPoints;

    public Transform GetNextWayPoint(Transform lastWayPoint)
    {
        if (_wayPoints.Count == 0)
            return null;

        if (lastWayPoint == null)
            return _wayPoints[0];

        int lastWayPointIndex = _wayPoints.IndexOf(lastWayPoint);
        int nextWayPointIndex = lastWayPointIndex + 1;
        nextWayPointIndex = nextWayPointIndex >= _wayPoints.Count ? 0 : nextWayPointIndex;
        return _wayPoints[nextWayPointIndex];
    }
}
