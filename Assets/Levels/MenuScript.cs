using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    private string input;

    [SerializeField] private GameObject PLAYER;
    [SerializeField] public TMP_InputField fovInput;
    [SerializeField] public TMP_InputField sensInput;
    public void changeScene(int sceneID)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
    }

    private void Start()
    {

    }

    public void updatePlayerFOV()
    {

        float newFOV = float.Parse(fovInput.text);
        PlayerMovementScript playerScript = PLAYER.gameObject.GetComponent<PlayerMovementScript>();
        playerScript.setPlayerFOV(newFOV);
        PlayerPrefs.SetFloat("cameraFOV", newFOV);
    }

    public void updatePlayerSens()
    {

        float newSens = float.Parse(sensInput.text);
        PlayerMovementScript playerScript = PLAYER.gameObject.GetComponent<PlayerMovementScript>();
        playerScript.setPlayerSens(newSens);
        Debug.Log(newSens);
        PlayerPrefs.SetFloat("playerSens", newSens);
    }

}
