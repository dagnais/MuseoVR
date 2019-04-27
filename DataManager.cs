using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameObject persistentData;
    void Start()
    {
        if(FindObjectOfType<PersistentData>() == null)
            Instantiate(persistentData, transform.position, Quaternion.identity);
    }
}
