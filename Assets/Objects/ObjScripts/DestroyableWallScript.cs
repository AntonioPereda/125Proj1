using UnityEngine;

public class DestroyableWallScript : MonoBehaviour
{
    [SerializeField] private GameObject destroyableWall;
    public AudioSource wallDestroyed;

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
                wallDestroyed.Play();
                Destroy(gameObject);
            }

        }
    }
}
