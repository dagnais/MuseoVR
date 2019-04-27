using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMolino : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;

    void Update()
    {
        target.Rotate(direction*Time.deltaTime);
    }
}
