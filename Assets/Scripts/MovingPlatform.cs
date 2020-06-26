using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject _point_A, _point_B;
    [SerializeField]
    float _speed = 1.0f;
    private Vector3 _target;


    // Start is called before the first frame update
    void Start()
    {
        _target = _point_A.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == _point_A.transform.position)
        {
            _target = _point_B.transform.position;
        }
        else if (transform.position == _point_B.transform.position)
        {
            _target = _point_A.transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
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
