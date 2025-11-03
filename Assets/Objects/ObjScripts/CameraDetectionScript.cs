using UnityEngine;

public class CameraDetectionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject watchingCamera;
    CameraScript CAM;

    private void Start()
    {
        CAM = watchingCamera.gameObject.GetComponent<CameraScript>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player found");
            CAM.setFollowPlayer();

        }
    }
}
