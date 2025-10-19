using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

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



    public GameObject POWERUP_MANAGER;
    public GameObject LevelPowerupManager;
    public List<Vector3> powerupLocations = new List<Vector3>();
    Dictionary<int, string> powerupEffects = new Dictionary<int, string>();
    powerupManager PowerupManagerScript;
    int frameCount = 0;

    string playerSTATUS = "";
    string cameraSTATUS = "";

    void Start()
    {
        thePlayer = PLAYER.GetComponent<PlayerMovementScript>();
        theCamera = CAMERA.GetComponent<CameraScript>();

        playerSTATUS = thePlayer.getStatus();
        cameraSTATUS = theCamera.getStatus();


        GameObject wallB = Instantiate(destroyableWall, new Vector3(-9.32f, -0.24f, 16.23f), Quaternion.Euler(0, 90, 0));
        wallB.transform.localScale = new Vector3(2.5f, 1.16f, 2f);

        GameObject wallC = Instantiate(destroyableWall, new Vector3(-18.89f, -0.24f, 17.68f), Quaternion.Euler(0, 0, 0));
        wallC.transform.localScale = new Vector3(2.5f, 1.16f, 2f);

        GameObject wallD = Instantiate(destroyableWall, new Vector3(12.82f, -0.24f, -0.13f), Quaternion.Euler(0, 90, 0));
        wallD.transform.localScale = new Vector3(2.5f, 1.16f, 2f);


        //////


        GameObject bumpB = Instantiate(speedTrap, new Vector3(-6.14f, 0.13f, 13.8f), Quaternion.Euler(0, 90, 0));

        GameObject bumpC = Instantiate(speedTrap, new Vector3(-6.142f, 0.13f, -13.348f), Quaternion.Euler(0, 0, 0));
        bumpC.transform.localScale = new Vector3(0.5f, 1, 0.729f);

        GameObject bumpD = Instantiate(speedTrap, new Vector3(7.139f, 0.13f, -6.86f), Quaternion.Euler(0, 90, 0));
        bumpD.transform.localScale = new Vector3(0.5f, 1, 0.711f);


        ///
        ///powerup pooling
        ///

        LevelPowerupManager = Instantiate(POWERUP_MANAGER);
        PowerupManagerScript = LevelPowerupManager.GetComponent<powerupManager>();


        instanciatePowerupSpawns();
        instanciatePowerupEffects();

        enableAndSpawnPowerup();
        enableAndSpawnPowerup();
        enableAndSpawnPowerup();
        enableAndSpawnPowerup();
        enableAndSpawnPowerup();
    }

    void Update()
    {
        playerSTATUS = thePlayer.getStatus();
        cameraSTATUS = theCamera.getStatus();

        frameCount++;
        //Debug.Log(frameCount);
        if (frameCount >= 1000)
        {
            frameCount = 0;
            for (int i = 0; i < PowerupManagerScript.getMaxNumber(); i++)
            {
                if (PowerupManagerScript.pooledPowerups[i].activeSelf == false)
                {   
                    GameObject disabledPowerup = PowerupManagerScript.pooledPowerups[i];
                    powerupLocations.Add(disabledPowerup.transform.position);
                    if (UnityEngine.Random.Range(0, 1) == 0)
                    {
                        enableAndSpawnPowerup();
                    }

                } 

            }

        }





    }

    public void instanciatePowerupSpawns()
    {
        powerupLocations.Add(new Vector3(-15, 1, 50));
        powerupLocations.Add(new Vector3(-12, 1, 50));
        powerupLocations.Add(new Vector3(-9, 1, 50));
        powerupLocations.Add(new Vector3(-6, 1, 50));
        powerupLocations.Add(new Vector3(-3, 1, 50));
        powerupLocations.Add(new Vector3(0, 1, 50));
        powerupLocations.Add(new Vector3(3, 1, 50));
        powerupLocations.Add(new Vector3(6, 1, 50));
        powerupLocations.Add(new Vector3(9, 1, 50));

    }

    public void instanciatePowerupEffects()
    {
        powerupEffects.Add(5, "5");
        powerupEffects.Add(10, "10");
        powerupEffects.Add(15, "15");
        powerupEffects.Add(30, "30");
        powerupEffects.Add(40, "40");
    }

    public void enableAndSpawnPowerup()
    {
        GameObject powerupToSpawn = PowerupManagerScript.returnInactivePooled();
        if (!powerupToSpawn)
        {
            Debug.Log("getting null");
            return;
        }

        int randomWeighted = UnityEngine.Random.Range(1, 100);
        string effect = "";
        if (randomWeighted <= 5) { 
            effect = powerupEffects[5];

        } else if (randomWeighted <= 15){
            effect = powerupEffects[10];

        }
        else if (randomWeighted <= 30) {
            effect = powerupEffects[15];

        } else if (randomWeighted <= 60) {
            effect = powerupEffects[30];

        } else {
            effect = powerupEffects[40];
        }

        PowerupScript selfPowerupScript = powerupToSpawn.GetComponent<PowerupScript>();
        selfPowerupScript.setPowerupEffect(effect);

        int maxSpawnIndex = powerupLocations.Count - 1;
        int randomSpawnIndex = UnityEngine.Random.Range(0, maxSpawnIndex);
        Vector3 randomLocation = powerupLocations[randomSpawnIndex];
        powerupToSpawn.gameObject.transform.position = randomLocation;
        powerupToSpawn.gameObject.SetActive(true);
        powerupLocations.Remove(randomLocation);

    }
    
    

}

    
