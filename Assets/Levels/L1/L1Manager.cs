using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class L1Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject destroyableWall;
    [SerializeField] private GameObject speedTrap;

    [SerializeField] private GameObject PLAYER;
    PlayerMovementScript thePlayer;

    [SerializeField] private GameObject POWERUP;

    [SerializeField] private GameObject CAMERA;
    CameraScript theCamera;

    public AudioSource wallDestroyed;



    public GameObject POWERUP_MANAGER;
    public GameObject LevelPowerupManager;
    public List<Vector3> powerupLocations = new List<Vector3>();
    powerupManager PowerupManagerScript;

    float followTimer = 0;
    List<GameObject> allWalls = new List<GameObject>();

    string playerSTATUS = "";
    string cameraSTATUS = "";

    void Start()
    {
        PlayerPrefs.SetString("isGamePaused", "false");
        thePlayer = PLAYER.GetComponent<PlayerMovementScript>();
        theCamera = CAMERA.GetComponent<CameraScript>();

        playerSTATUS = thePlayer.getStatus();
        cameraSTATUS = theCamera.getStatus();

        GameObject wallA = Instantiate(destroyableWall, new Vector3(-0.33f, -0.24f, 27.6f), Quaternion.Euler(0, 0, 0));
        wallA.transform.localScale = new Vector3(4.6f, 1.8f, 2f);
        allWalls.Add(wallA);


        GameObject wallB = Instantiate(destroyableWall, new Vector3(-9.32f, -0.24f, 16.23f), Quaternion.Euler(0, 90, 0));
        wallB.transform.localScale = new Vector3(2.5f, 3.6f, 2f);
        allWalls.Add(wallB);

        GameObject wallC = Instantiate(destroyableWall, new Vector3(-18.89f, -0.24f, 17.68f), Quaternion.Euler(0, 0, 0));
        wallC.transform.localScale = new Vector3(2.5f, 3.6f, 2f);
        allWalls.Add(wallC);

        GameObject wallD = Instantiate(destroyableWall, new Vector3(12.82f, -0.24f, -0.13f), Quaternion.Euler(0, 90, 0));
        wallD.transform.localScale = new Vector3(2.5f, 3.6f, 2f);
        allWalls.Add(wallD);

        GameObject wallE = Instantiate(destroyableWall, new Vector3(34.14f, -0.24f, 9.41f), Quaternion.Euler(0, 0, 0));
        wallE.transform.localScale = new Vector3(6f, 3.61f, 4.17f);
        allWalls.Add(wallE);

        GameObject wallF = Instantiate(destroyableWall, new Vector3(20.73f, -0.24f, 9.41f), Quaternion.Euler(0, 0, 0));
        wallF.transform.localScale = new Vector3(6f, 3.61f, 4.17f);
        allWalls.Add(wallF);

        GameObject wallG = Instantiate(destroyableWall, new Vector3(62.17f, -0.24f, 21.64f), Quaternion.Euler(0, 90, 0));
        wallG.transform.localScale = new Vector3(6f, 3.61f, 4.17f);
        allWalls.Add(wallG);

        GameObject wallH = Instantiate(destroyableWall, new Vector3(50.28f, -0.24f, -23.47f), Quaternion.Euler(0, 90, 0));
        wallH.transform.localScale = new Vector3(4.5f, 3.61f, 4.17f);
        allWalls.Add(wallH);

        GameObject wallI = Instantiate(destroyableWall, new Vector3(-47.12f, -0.24f, 12.78f), Quaternion.Euler(0, 0, 0));
        wallI.transform.localScale = new Vector3(5.2f, 3.61f, 4.17f);
        allWalls.Add(wallI);

        GameObject wallJ = Instantiate(destroyableWall, new Vector3(-57.75f, -0.24f, -18.9f), Quaternion.Euler(0, 0, 0));
        wallJ.transform.localScale = new Vector3(5.2f, 3.61f, 4.17f);
        allWalls.Add(wallJ);



        //////


        GameObject bumpB = Instantiate(speedTrap, new Vector3(-6.14f, 0.13f, 13.8f), Quaternion.Euler(0, 90, 0));

        GameObject bumpC = Instantiate(speedTrap, new Vector3(-6.142f, 0.13f, -13.348f), Quaternion.Euler(0, 0, 0));
        bumpC.transform.localScale = new Vector3(0.5f, 1, 0.729f);

        GameObject bumpD = Instantiate(speedTrap, new Vector3(7.139f, 0.13f, -6.86f), Quaternion.Euler(0, 90, 0));
        bumpD.transform.localScale = new Vector3(0.5f, 1, 0.711f);

        GameObject bumpE = Instantiate(speedTrap, new Vector3(35.35f, 0.13f, 28.2f), Quaternion.Euler(0, 0, 0));
        bumpE.transform.localScale = new Vector3(0.5f, 1, 1.1f);

        GameObject bumpF = Instantiate(speedTrap, new Vector3(50.15f, 0.13f, -17.03f), Quaternion.Euler(0, 0, 0));
        bumpF.transform.localScale = new Vector3(1f, 1, 1.18f);

        GameObject bumpG = Instantiate(speedTrap, new Vector3(-36.64f, 0.13f, -25.51f), Quaternion.Euler(0, 0, 0));
        bumpG.transform.localScale = new Vector3(1f, 1, -0.68f);

        GameObject bumpH = Instantiate(speedTrap, new Vector3(-59.7f, 0.13f, -1.98f), Quaternion.Euler(0, 0, 0));
        bumpH.transform.localScale = new Vector3(1f, 1, 1.37f);

        GameObject bumpI = Instantiate(speedTrap, new Vector3(-48f, 0.13f, 28.4f), Quaternion.Euler(0, 0, 0));
        bumpI.transform.localScale = new Vector3(1f, 1, 1.15f);





        ///
        ///powerup pooling
        ///

        LevelPowerupManager = Instantiate(POWERUP_MANAGER);
        PowerupManagerScript = LevelPowerupManager.GetComponent<powerupManager>();


        instanciatePowerupSpawns();

    }

    void Update()
    {

        foreach (var wall in allWalls)
        {
            if (wall == null)
            {
                wallDestroyed.Play();
                allWalls.Remove(wall);
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PlayerPrefs.GetString("isGamePaused") == "false")
            {
                Time.timeScale = 0f; //pause the game
                PlayerPrefs.SetString("isGamePaused", "true");
                UnityEngine.SceneManagement.SceneManager.LoadScene(5, LoadSceneMode.Additive);
            } else
            {
                Time.timeScale = 1f; //unpause the game
                PlayerPrefs.SetString("isGamePaused", "false");
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(5);

            }
        }

        if (PlayerPrefs.GetString("isGamePaused") == "false")
        {
            playerSTATUS = thePlayer.getStatus();
            cameraSTATUS = theCamera.getStatus();

        }

        if (cameraSTATUS == "FOLLOW-PLAYER")
        {
            if (followTimer == 0) { startPowerupCollection(); }

            followTimer += Time.deltaTime;
            if (followTimer >= 30)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            }

            int collectedPowerups = 0;
            for (int i = 0; i < PowerupManagerScript.getMaxNumber(); i++)
            {
                if (PowerupManagerScript.pooledPowerups[i].activeSelf == false) { collectedPowerups += 1; }

                if (collectedPowerups == PowerupManagerScript.getMaxNumber())
                {
                    theCamera.setLookForPlayer();
                    followTimer = 0;
                }
            }

        }





    }

    public void instanciatePowerupSpawns()
    {
        
        powerupLocations.Add(new Vector3(28.01f, 2, 4.59f));
        powerupLocations.Add(new Vector3(74, 2, 27));
        powerupLocations.Add(new Vector3(46, 2, 20));
        powerupLocations.Add(new Vector3(21, 2, 26));
        powerupLocations.Add(new Vector3(14.8f, 2, 27.8f));
        powerupLocations.Add(new Vector3(14.8f, 2, -1.87f));
        powerupLocations.Add(new Vector3(-55, 2, 17));
        powerupLocations.Add(new Vector3(-25, 2, 4));
        powerupLocations.Add(new Vector3(18.6f, 2, -18.3f));
        powerupLocations.Add(new Vector3(54f, 2, -7f));  
        
        /*
        powerupLocations.Add(new Vector3(-15, 1, 50));
        powerupLocations.Add(new Vector3(-12, 1, 50));
        powerupLocations.Add(new Vector3(-9, 1, 50));
        powerupLocations.Add(new Vector3(-6, 1, 50));
        powerupLocations.Add(new Vector3(-3, 1, 50));
        powerupLocations.Add(new Vector3(0, 1, 50));
        powerupLocations.Add(new Vector3(3, 1, 50));
        powerupLocations.Add(new Vector3(6, 1, 50));
        powerupLocations.Add(new Vector3(9, 1, 50));
        */
    }

    public Vector3 closestItemToPlayer()
    {   
        Vector3 closestItem = powerupLocations[0];
        Vector3 playerPosition = PLAYER.transform.position;
        float shortestDistance = Vector3.Distance(closestItem, playerPosition);

        foreach(Vector3 potentialLocation in powerupLocations)
        {
            float newDistance = Vector3.Distance(potentialLocation, playerPosition);

            if (newDistance < shortestDistance)
            {
                closestItem = potentialLocation;
                shortestDistance = newDistance;
            }
        }

        return closestItem;
    }



    public void startPowerupCollection()
    {
        for (int i = 0; i < PowerupManagerScript.getMaxNumber(); i++)
        {
            if (PowerupManagerScript.pooledPowerups[i].activeSelf == false)
            {
                GameObject disabledPowerup = PowerupManagerScript.pooledPowerups[i];
                if (disabledPowerup.transform.position != new Vector3(0, 0, 0))
                {
                    powerupLocations.Add(disabledPowerup.transform.position);
                }
            }

            if (UnityEngine.Random.Range(0, 1) == 0)
            {
                enableAndSpawnPowerup();
            }

        }
    }

    public void enableAndSpawnPowerup()
    {
        GameObject powerupToSpawn = PowerupManagerScript.returnInactivePooled();
        if (!powerupToSpawn)
        {
            Debug.Log("getting null");
            return;
        }

        PowerupScript selfPowerupScript = powerupToSpawn.GetComponent<PowerupScript>();

        Vector3 randomLocation = closestItemToPlayer();

        powerupToSpawn.gameObject.transform.position = randomLocation;
        powerupToSpawn.gameObject.SetActive(true);
        powerupLocations.Remove(randomLocation);

    }

    
    

}

    
