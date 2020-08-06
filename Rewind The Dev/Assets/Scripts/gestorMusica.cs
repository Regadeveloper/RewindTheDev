using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gestorMusica : MonoBehaviour
{

    [SerializeField] private AudioClip menú;
    [SerializeField] private AudioClip game;
    [SerializeField] private AudioSource altavoz;
    static private gestorMusica unidad;
    Scene escenaActual;
    // Start is called before the first frame update



    protected virtual void Awake()
    {
        if (unidad == null)
        {
            unidad = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame

    static public void Play(AudioClip mus)
    {
        if (unidad != null)
        {
            if (unidad.altavoz != null)
            {
                unidad.altavoz.Stop();
                unidad.altavoz.clip = mus;
                unidad.altavoz.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown("t"))
        {
            SceneManager.LoadScene(0);
        }
        
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += CargadeNivel;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= CargadeNivel;
    }

    public void CargadeNivel(Scene scene, LoadSceneMode mode)
    {
        escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.buildIndex == 1)
        {
            Play(game);
        }
        else if (escenaActual.buildIndex == 0)
            Play(menú);
    }
}
