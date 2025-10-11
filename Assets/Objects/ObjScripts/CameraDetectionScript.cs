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

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(watchingCamera.gameObject.name);
            CAM.setFollowPlayer();

        }
    }
}
