using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [Tooltip ("Almacena todos los puntos del camino que debe recorrer el personaje")]
    public PlayerPathController[] paths;
    public int currentIndex = 0;

    void Awake()
    {
        paths = FindObjectsOfType<PlayerPathController>();
    }

    /// <summary>
    /// Toma el index actual y te devuelve la posicion de ese punto de ruta   
    /// </summary>
    public Vector3 GetPositionIndex(int index)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].id == index)
            {
                return paths[i].gameObject.transform.position;
            }
        }
        Debug.LogError("Punto de ruta no encontrado :"+ index);
        return Vector3.zero;
    }
}
