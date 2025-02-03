using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _target;

    private void Update()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
