using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PresurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Box"))
        {
            UnityEngine.Debug.Log("Hit");
            if (other.transform.position.x > (this.transform.position.x - 0.05f) && other.transform.position.x < (this.transform.position.x + 0.05f))
            {
                other.transform.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                Destroy(this);
            }
        }

    }
}
