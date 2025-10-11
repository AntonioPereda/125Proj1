using System.Linq.Expressions;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovementScript playerMovement;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Speedbump!");
            playerMovement = collision.gameObject.GetComponent<PlayerMovementScript>();
            playerMovement.halfMovement();
        }
    }
}
