using System;
using System.Threading.Tasks;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    const bool lightOn = true;
    const bool lightOff = false;
    bool isLightOn = true;
    int frameCount = 1;
    void Start()
    {
        this.gameObject.SetActive(lightOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLightOn && PlayerPrefs.GetString("isGamePaused") == "false")
        {
            frameCount++;
            if (frameCount >= 1000)
            {
                float chance = UnityEngine.Random.Range(0, 100f);
                if (chance >= 50)
                {
                    Debug.Log("turing light off");
                    turnLightOff();
                }
                else
                {
                    Debug.Log("light remains on");
                    frameCount = 0;
                }
            }

        } 

    }

    void turnLightOff()
    {
        this.gameObject.SetActive(lightOff);
        isLightOn = false;
        Invoke("turnLightOn", 10);
        Debug.Log("light is off");
    }

    void turnLightOn()
    {
        this.gameObject.SetActive(lightOn);
        isLightOn = true;
        Debug.Log("light back on");
        frameCount = 0;
    }

}
