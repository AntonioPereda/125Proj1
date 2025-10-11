using System.Linq.Expressions;
using UnityEngine;

public class TPScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Teleported!");
            collision.transform.position = new Vector3(8, 6, 36);
        }
    }
}
