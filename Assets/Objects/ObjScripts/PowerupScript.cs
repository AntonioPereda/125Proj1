using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int powerupID = -1;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        this.gameObject.SetActive(false);
        //Debug.Log("item is: " + pickupEffect);
        
    }

    public void setID (int i)
    {
        powerupID = i;
    }

}
