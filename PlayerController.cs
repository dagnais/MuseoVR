using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PathManager pathManager;
    public bool isMove, isRotate;
    public float speed=10;
    public int indexStart;

    void Start()
    {
        PersistentData data = FindObjectOfType<PersistentData>();
        Scene scene = SceneManager.GetActiveScene();
        for (int i = 0; i < data.nameScenes.Length; i++)
        {
            if (data.nameScenes[i] == scene.name)
            {
                indexStart = data.indexScenes[i];
                pathManager.currentIndex = indexStart;
            }
        }

        if(indexStart != 0)
            transform.position = pathManager.GetPositionIndex(indexStart);
    }

    void Update()
    {
        if (isMove)
            MoveToPath();

        if (isRotate)
            RotateToPath();
    }
    void MoveToPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathManager.GetPositionIndex(pathManager.currentIndex), Time.deltaTime * speed);
    }
    void RotateToPath()
    {
        Quaternion rot = Quaternion.LookRotation(pathManager.GetPositionIndex(pathManager.currentIndex));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speed);
    }

    float GetDistancePaths()
    {
        return Vector3.Distance(pathManager.GetPositionIndex(pathManager.currentIndex),
            pathManager.GetPositionIndex(pathManager.currentIndex - 1));
    }

    
}
