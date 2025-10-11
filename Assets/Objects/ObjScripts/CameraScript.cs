using UnityEngine;

public class CameraScript : MonoBehaviour
{
    RaycastHit objectHit;
    bool followingPlayer;

    [SerializeField] private GameObject detectionZone;
    [SerializeField] private GameObject PLAYER;

    void Start()
    {
        //Instantiate(detectionZone);
        followingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer)
        {
            //Debug.Log("followingPlayer");
            transform.LookAt(PLAYER.transform.position);
            //Debug.Log(-transform.eulerAngles[1] + transform.eulerAngles[1]);
            transform.rotation = Quaternion.Euler(-transform.eulerAngles[0], transform.eulerAngles[1], transform.eulerAngles[2]);
        }

        Vector3 rayPosition = transform.position - new Vector3(0, 0, 0);
        Debug.DrawRay(rayPosition, -transform.forward, Color.red, 3f);
        Physics.Raycast(rayPosition, -transform.forward, out objectHit, float.PositiveInfinity);
        Vector3 newPosition = new Vector3(objectHit.point[0], 0, objectHit.point[2]);
        detectionZone.transform.position = newPosition;

    }

    public void setFollowPlayer()
    {
        Debug.Log("now following player!");
        followingPlayer = true;
    }
}
