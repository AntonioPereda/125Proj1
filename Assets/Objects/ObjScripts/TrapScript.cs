using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Audio;

public class TrapScript : MonoBehaviour
{
    public AudioSource tripSound;
    const float startTime = 13.75f;
    const float stopAfterTime = 1.0f;
    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovementScript playerMovement;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Speedbump!");
            playerMovement = collision.gameObject.GetComponent<PlayerMovementScript>();
            playerMovement.halfMovement();
            tripSound.time = startTime;
            tripSound.Play();
            StartCoroutine(StopAudioAfterDelay());
        }


    }

    IEnumerator StopAudioAfterDelay()
    {
        yield return new WaitForSeconds(stopAfterTime);
        tripSound.Stop();
    }
}
