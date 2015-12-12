using UnityEngine;
using System.Collections;
using InControl;

public class Player : MonoBehaviour {

    public static InputDevice currentDevice;
    private InControlManager inControlScript;
    public float xJoyAxis;
    public float yJoyAxis;
    public Vector2 playerVel;
    private Rigidbody2D playerBody;

    public float turnSpeed;
    public float maxUpVelocity;
    public float maxDownVelocity;
    public float maxXVelocity;
    public float stickRotation;
    private bool inMovement;

	// Use this for initialization
	void Start () {
        currentDevice = InputManager.ActiveDevice;
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        currentDevice = InputManager.ActiveDevice;

        xJoyAxis = currentDevice.LeftStickX;
        yJoyAxis = currentDevice.LeftStickY;
        stickRotation = Mathf.Atan2(currentDevice.LeftStickY, currentDevice.LeftStickX) * Mathf.Rad2Deg;
        this.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = new Vector3(0, 0, stickRotation - 270);

        if (((currentDevice.LeftStickX < 0.15)&&(currentDevice.LeftStickX > -0.15))&&((currentDevice.LeftStickY < 0.15) && (currentDevice.LeftStickY > -0.15)))
        {
            inMovement = false;
        }
        else
        {
            inMovement = true;
        }
        if (currentDevice.LeftStickY < -0.85)
        {
            maxDownVelocity = 15;
        }
        else
        {
            maxDownVelocity = 10;
        }
    }

    void FixedUpdate()
    {
        playerVel = playerBody.velocity;

        if (inMovement == false)
        {
            playerBody.velocity = Vector3.zero;
        }
        if (inMovement == true)
        {
            playerBody.AddForce(new Vector2(xJoyAxis * turnSpeed, yJoyAxis * turnSpeed));
        }
        if (playerBody.velocity.x > maxXVelocity)
        {
            playerBody.velocity = new Vector3(maxXVelocity, playerBody.velocity.y);
        }
        if (playerBody.velocity.x < -maxXVelocity)
        {
            playerBody.velocity = new Vector3(-maxXVelocity, playerBody.velocity.y);
        }
        if (playerBody.velocity.y < -maxDownVelocity)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, -maxDownVelocity);
        }
        if (playerBody.velocity.y > maxUpVelocity)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, maxUpVelocity);
        }
    }
}
