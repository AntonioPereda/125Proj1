using Mono.Cecil;
using System.Collections;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    RaycastHit objectHit;
    bool followingPlayer;

    [SerializeField] private GameObject detectionZone;
    [SerializeField] private GameObject PLAYER;
    bool coroutineActive = false;
    //float rotationRate = 0.25f;
    public AudioSource cameraSound;
    Collider detectionZoneCollider;
    float randXDir;
    float randZDir;

    string cameraState = "";

    void Start()
    {
        followingPlayer = false;
        detectionZoneCollider = detectionZone.GetComponent<BoxCollider>();
        setState();
        coroutineActive = true;
        StartCoroutine(lookForPlayer());
        //STATES: SCANNING, FOLLOW-PLAYER
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("isGamePaused") == "false")
        {
            if (coroutineActive == false && followingPlayer == false)
            {
                coroutineActive = true;
             
            } 

            Vector3 rayPosition = transform.position;
            Debug.DrawRay(rayPosition, -transform.forward, Color.red, 3f);
            Physics.Raycast(rayPosition, -transform.forward, out objectHit, float.PositiveInfinity);
            Vector3 newPosition = new Vector3(objectHit.point[0], -2f, objectHit.point[2]);

            if (followingPlayer)
            {
                transform.LookAt(new Vector3(PLAYER.transform.position[0], PLAYER.transform.position[1], PLAYER.transform.position[2]));
                transform.rotation = Quaternion.Euler(-transform.eulerAngles[0], transform.eulerAngles[1] - 180, transform.eulerAngles[2]);
                detectionZone.transform.position = new Vector3(PLAYER.transform.position[0] + 1f, -2f, PLAYER.transform.position[2] - 1f);
            }
            else
            {
                detectionZone.transform.position = newPosition;
            }

        }
        else
        {
            coroutineActive = false;
        }
    }

    public void setFollowPlayer()
    {
        StopCoroutine(lookForPlayer());
        Debug.Log("now following player!");
        followingPlayer = true;
        coroutineActive = false;
        setState();
    }

    public void setLookForPlayer()
    {
        StartCoroutine(lookForPlayer());
        Debug.Log("now looking for player!");
        followingPlayer = false;
        coroutineActive = true;

        var randX = UnityEngine.Random.Range(-78f, 78f);
        var randY = UnityEngine.Random.Range(-30f, 32f);
        var pointToLookAt = new Vector3(randX, 0, randY);
        transform.LookAt(pointToLookAt);
        transform.rotation = Quaternion.Euler(-transform.eulerAngles[0], transform.eulerAngles[1] - 180, transform.eulerAngles[2]);


        setState();
    }

    public void setState()
    {
        if (followingPlayer)
        {
            Debug.Log("setting to follow player");
            detectionZone.gameObject.SetActive(false);
            detectionZoneCollider.excludeLayers = LayerMask.GetMask("Everything");
            cameraState = "FOLLOW-PLAYER";
            cameraSound.Play();
        } else
        {
            Debug.Log("setting to scan for player");
            detectionZone.gameObject.SetActive(true);
            detectionZoneCollider.excludeLayers = LayerMask.GetMask("Nothing");
            cameraState = "SCANNING";
        }

    }

    public string getStatus()
    {
        return cameraState;
    }

    IEnumerator lookForPlayer()
    {
        randXDir = UnityEngine.Random.Range(0.01f, 0.1f);
        randZDir = UnityEngine.Random.Range(0.01f, 0.1f);
        var pointX = objectHit.point[0];
        var pointZ = objectHit.point[2];

        while (true)
        {
                pointX += randXDir;
                pointZ += randZDir;
                var pointToLookAt = new Vector3(pointX, 0, pointZ);
                transform.LookAt(pointToLookAt);
                transform.rotation = Quaternion.Euler(-transform.eulerAngles[0], transform.eulerAngles[1] - 180, transform.eulerAngles[2]);
                
                if (pointX >= 78 || pointX <= -78)
                {
                    if (pointX > 0)
                    {
                        randXDir = UnityEngine.Random.Range(0.01f, 0.1f) * -1;
                    }
                    else
                    {
                        randXDir = UnityEngine.Random.Range(0.01f, 0.1f);
                    }
                }

                if (pointZ >= 30 || pointZ <= -32)
                {
                    if (pointZ > 0)
                    {
                        randZDir = UnityEngine.Random.Range(0.01f, 0.1f) *-1;
                    } else
                    {
                        randZDir = UnityEngine.Random.Range(0.01f, 0.1f);
                    }
                    
                }

                if (UnityEngine.Random.Range(1, 1000) == 1)
                {
                    randXDir = UnityEngine.Random.Range(0.01f, 0.1f) * -Mathf.Sign(randXDir);
                } else if (UnityEngine.Random.Range(1, 1000) == 1000)
                {
                    randZDir = UnityEngine.Random.Range(0.01f, 0.1f) * -Mathf.Sign(randZDir);
                }

            yield return null;
        }

    }
}

/*
 
 78,30.1 - -78, -32
 */