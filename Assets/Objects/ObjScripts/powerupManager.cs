using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class powerupManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> pooledPowerups;
    const int noOfPowerups = 5;
    public static powerupManager powerupPoolManager;
    public GameObject POWERUP; 

    void Awake()
    {
        powerupPoolManager = this;
    }

    void Start()
    {
        pooledPowerups = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < noOfPowerups; i++)
        {
            
            temp = Instantiate(POWERUP);
            PowerupScript tempScript = temp.gameObject.GetComponent<PowerupScript>();
            tempScript.setID(i);
            temp.SetActive(false);
            temp.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pooledPowerups.Add(temp);
        }

    }

    public void recivePowerupObject(GameObject powerup)
    {
        POWERUP = powerup;
    }

    public GameObject returnInactivePooled()
    {
        for(int i = 0; i < noOfPowerups; i++)
        {
            if (!(pooledPowerups[i].activeSelf))
            {
                return pooledPowerups[i];
            }
        }
        return null;
    }

    public int getMaxNumber()
    {
        return noOfPowerups;
    }
}
