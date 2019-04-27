using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [Header ("Nombre de las escenas intervinientes")]
    public string[] nameScenes;
    public int[] indexScenes;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        indexScenes = new int[nameScenes.Length];
    }
}
