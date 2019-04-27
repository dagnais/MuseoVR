using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPathController : MonoBehaviour
{
    [Tooltip ("Con este id indicamos el orden en que se deben ejecutar los puntos del camino")]
    public int id;

    [Header("Eventos")]
    public bool isEvent;
    public enum TypeEvent
    {
        loadScene,
        playAudio,
        playAnimation
    }
    [Tooltip ("El evento se accionará cuando el personaje entre en este punto de ruta")]
    public TypeEvent typeEvent;
    [Tooltip ("El valor que requiere el evento seleccionado")]
    public string parameter;
    [Header ("Si lo requiere el evento")]
    public bool notMove;
    public bool notRotate;

    #region variables privadas
    private bool _changeIndex;
    private PlayerController _player;
    private PathManager _pathManager;
    private AudioSource _audio;
    private Animator _anim;
    private bool _isEventRunning;
    #endregion

    void Start()
    {
        _pathManager = FindObjectOfType<PathManager>();
        if (_pathManager == null)
            Debug.LogError("No se encontro PathManager");

        if (typeEvent == TypeEvent.playAudio)
            _audio = GetComponent<AudioSource>();
        if (typeEvent == TypeEvent.playAnimation)
        {
            _anim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (_isEventRunning)
            EventPlayAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<PlayerController>();

            if (_pathManager.currentIndex == _pathManager.paths.Length - 1)
                return;

            if(!_changeIndex)
                _pathManager.currentIndex++;

            _changeIndex = true;

            if (notMove)
                _player.isMove = false;

            if (notRotate)
                _player.isRotate = false;

            if(isEvent)
                SelectEventType();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _changeIndex = false;
        }
    }

    void SelectEventType()
    {
        switch (typeEvent)
        {
            case TypeEvent.playAudio:
                    _audio.Play();
                    StartCoroutine(EventPlayAudio());
                break;
            case TypeEvent.playAnimation:
                _anim.enabled = true;
                _isEventRunning = true;
                break;
            case TypeEvent.loadScene:
                if (parameter == string.Empty)
                {
                    Debug.LogError("Se requiere el nombre de la escena a cargar");
                    return;
                }
                PersistentData data = FindObjectOfType<PersistentData>();
                Scene scene = SceneManager.GetActiveScene();
                for (int i = 0; i < data.nameScenes.Length; i++)
                {
                    if (data.nameScenes[i] == scene.name)
                    {
                        data.indexScenes[i] = id+1;
                    }
                }
                SceneManager.LoadScene(parameter);
                break;
        }
    }

    IEnumerator EventPlayAudio()
    {
        yield return new WaitForSeconds(_audio.clip.length);
        FinishEvent();
    }

    void EventPlayAnimation()
    {
        if (parameter == string.Empty)
        {
            Debug.LogError("Se requiere el nombre de la animacion de idle(salida)");
            return;
        }
           
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName(parameter))
        {
            _anim.enabled = false;
            _isEventRunning = false;
            FinishEvent();
        }
    }

    void FinishEvent()
    {
        if (notMove)
            _player.isMove = true;

        if (notRotate)
            _player.isRotate = true;
    }
}
