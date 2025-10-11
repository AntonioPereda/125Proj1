using UnityEngine;

public class DestroyableWallScript : MonoBehaviour
{
    [SerializeField] private GameObject destroyableWall;

    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))

        {
            PlayerMovementScript playerMovement = collision.gameObject.GetComponent<PlayerMovementScript>();
            if (playerMovement.returnPlayerVelocity() > 5.0)
            {
                Debug.Log("Destroyed!");
                Destroy(gameObject);
            }

        }
    }
}
