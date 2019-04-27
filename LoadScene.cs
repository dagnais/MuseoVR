using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    public bool isAutomatic;
    public float time;

    void Start()
    {
        if (isAutomatic)
        {
            if(time>0)
                Invoke("Load", time);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
