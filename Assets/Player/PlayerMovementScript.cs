using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovementScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody playerBody;
    float playerSpeed = 0.025f;
    Vector3 prevPosition;
    float currentVelocity;

    string currentStatus = "OK";
    //STATES: "OK", "0.5-SPEED", "DETECTED"
    void Start()
    {   
        playerBody = GetComponent<Rigidbody>();
        prevPosition = playerBody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float currentV3 = Mathf.Abs(playerBody.transform.position[0] + playerBody.transform.position[1] + playerBody.transform.position[2]);
        float currentP3 = Mathf.Abs(prevPosition[0] + prevPosition[1] + prevPosition[2]);

        currentVelocity = Mathf.Abs((currentV3 - currentP3) / Time.deltaTime);
        prevPosition = playerBody.transform.position;
        //Debug.Log(currentVelocity);

        //move FB
        transform.Translate(transform.forward * Input.GetAxis("Vertical") * playerSpeed, Space.World);
        //move LR
        transform.Translate(transform.right * Input.GetAxis("Horizontal") * playerSpeed, Space.World);

        //y camera angle rotation based on player movement
        //lock from Z rotating so it doesnt fall over
        float mouseX = Input.GetAxis("Mouse X") * 300f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 300f * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.left, mouseY);
        Vector3 playerRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(playerRotation.x, playerRotation.y, 0f);


    }

    public void halfMovement()
    {
        playerSpeed = 0.0125f;
        //Debug.Log("player speed halfed");
        if (currentStatus != "DETECTED")
        {
            currentStatus = "0.5-SPEED";
        }
        Invoke("returnNormalSpeed", 3);

    }

    public void returnNormalSpeed()
    {
        playerSpeed = 0.025f;
        if (currentStatus != "DETECTED")
        {
            currentStatus = "OK";
        }
    }

    public float returnPlayerVelocity()
    {
        return currentVelocity;
    }

    public string getStatus()
    {
        return currentStatus;
    }

    public void setPlayerStatus(string state)
    {
        currentStatus = state;
    }
}
       