using UnityEngine;

public class L1Setup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject destroyableWall;
    [SerializeField] private GameObject speedTrap;
    void Start()
    {
        GameObject wallB = Instantiate(destroyableWall, new Vector3(-9.32f, -0.24f, 16.23f), Quaternion.Euler(0, 90, 0));
        wallB.transform.localScale = new Vector3(2.5f, 1.16f, 2f);

        GameObject wallC = Instantiate(destroyableWall, new Vector3(-18.89f, -0.24f, 17.68f), Quaternion.Euler(0, 0, 0));
        wallC.transform.localScale = new Vector3(2.5f, 1.16f, 2f);

        GameObject wallD = Instantiate(destroyableWall, new Vector3(12.82f, -0.24f, -0.13f), Quaternion.Euler(0, 90, 0));
        wallD.transform.localScale = new Vector3(2.5f, 1.16f, 2f);


        //////


        GameObject bumpB = Instantiate(speedTrap, new Vector3(-6.14f, 0.13f, 13.8f), Quaternion.Euler(0, 90, 0));

        GameObject bumpC = Instantiate(speedTrap, new Vector3(-6.142f, 0.13f, -13.348f), Quaternion.Euler(0, 0, 0));
        bumpC.transform.localScale = new Vector3(0.5f, 1, 0.729f);

        GameObject bumpD = Instantiate(speedTrap, new Vector3(7.139f, 0.13f, -6.86f), Quaternion.Euler(0, 90, 0));
        bumpD.transform.localScale = new Vector3(0.5f, 1, 0.711f);
    }

}
