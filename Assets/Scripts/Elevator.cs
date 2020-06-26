using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Vector3 _target;
    private Vector3 _home;
    [SerializeField]
    private float _speed;
    private void Awake()
    {
        _target = transform.position;
        _home = transform.position;
    }

    void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void CallElevator(Vector3 pos)
    {
        if (this.transform.position == _home)
        {
            _target = pos;
        }
        else
        {
            _target = _home;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
