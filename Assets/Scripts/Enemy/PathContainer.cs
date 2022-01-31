using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathContainer : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _pathPoints;

    private int _currentPointIndex = 0;

    public int TotalPoints => _pathPoints != null ? _pathPoints.Count : 0;
    
    public Vector3 CurrentPoint()
    {
        if (_pathPoints == null || _pathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        
        return _pathPoints[_currentPointIndex].position;
    }
    
    public Vector3 NextPoint()
    {
        if (_pathPoints == null || _pathPoints.Count == 0)
        {
            return Vector3.zero;
        }
        
        _currentPointIndex = (_currentPointIndex + 1) % _pathPoints.Count;
        return _pathPoints[_currentPointIndex].position;
    }
}
