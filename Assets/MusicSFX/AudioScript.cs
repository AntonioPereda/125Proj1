using JetBrains.Annotations;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    public AudioSource mainMenuTheme;
    public AudioSource inGameTheme;
    public AudioSource goTheme;
    public UnityEngine.SceneManagement.Scene gameScene;
    public UnityEngine.SceneManagement.Scene goScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioScript Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
         
         goScene = SceneManager.GetSceneByName("GameOverScene");
         inGameTheme.Pause();
         goTheme.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        gameScene = SceneManager.GetSceneByBuildIndex(1);
        goScene = SceneManager.GetSceneByName("GameOverScene");

        if (mainMenuTheme.isPlaying && gameScene.isLoaded == true)
        {
            Debug.Log("stopping " + mainMenuTheme.name);
            Debug.Log("playing " + inGameTheme.name);
            mainMenuTheme.Pause();
            inGameTheme.Play();
        }

        if (PlayerPrefs.GetString("isGamePaused") == "false" && inGameTheme.isPlaying == false)
        {
            inGameTheme.Play();
        }
        //Debug.Log(inGameTheme.isPlaying);

        if(PlayerPrefs.GetString("isGamePaused") == "true" && inGameTheme.isPlaying == true)
        {
            Debug.Log("stopping music");
            inGameTheme.Pause();
        }

        if (goScene.isLoaded == true)
        {
            inGameTheme.Stop();
            goTheme.Play();
        }



    }
}
