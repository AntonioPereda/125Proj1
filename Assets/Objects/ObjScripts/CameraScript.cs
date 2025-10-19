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

    string cameraState = "";

    void Start()
    {
        followingPlayer = false;
        setState();
        StartCoroutine(lookForPlayer());
        //STATES: SCANNING, FOLLOW-PLAYER
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayPosition = transform.position;
        Debug.DrawRay(rayPosition, -transform.forward, Color.red, 3f);
        Physics.Raycast(rayPosition, -transform.forward, out objectHit, float.PositiveInfinity);
        Vector3 newPosition = new Vector3(objectHit.point[0], -2f, objectHit.point[2]);

        if (followingPlayer)
        {
            transform.LookAt( new Vector3(PLAYER.transform.position[0], PLAYER.transform.position[1], PLAYER.transform.position[2]) );
            transform.rotation = Quaternion.Euler(-transform.eulerAngles[0], transform.eulerAngles[1]-180, transform.eulerAngles[2]);
            detectionZone.transform.position = new Vector3(PLAYER.transform.position[0] + 1f, -2f, PLAYER.transform.position[2]-1f);
        }
        else
        {
            detectionZone.transform.position = newPosition;
        }

    }

    public void setFollowPlayer()
    {
        StopCoroutine(lookForPlayer());
        Debug.Log("now following player!");
        followingPlayer = true;
        setState();
    }

    public void setState()
    {
        if (followingPlayer)
        {
            cameraState = "FOLLOW-PLAYER";
        } else
        {
            cameraState = "SCANNING";
        }

    }

    public string getStatus()
    {
        return cameraState;
    }

    IEnumerator lookForPlayer()
    {
        while (true) {
            float currentY = transform.eulerAngles[1];
            //Debug.Log(transform.eulerAngles[1]);
            transform.rotation = Quaternion.Euler(transform.eulerAngles[0], currentY + 0.25f, transform.eulerAngles[2]);
            /*
            int randX = UnityEngine.Random.Range(-21, 18);
            int randZ = UnityEngine.Random.Range(-16, 31);

            Vector3 endPos = new Vector3(randX, 0, randZ);

            Vector3 startPos = objectHit.point;
            Vector3 directionToLook = endPos - startPos;
            Quaternion rotationToLook = Quaternion.LookRotation(directionToLook);


            float elapsedTime = 0f;
            const float rotationDuration = 5f;
            while (elapsedTime < rotationDuration)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLook, elapsedTime/rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.rotation = rotationToLook;
            */
            yield return null;
                }
    }
}
