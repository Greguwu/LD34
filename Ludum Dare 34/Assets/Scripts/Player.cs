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
    }

    void FixedUpdate()
    {
        playerVel = playerBody.velocity;
        playerBody.AddForce(new Vector2(xJoyAxis * turnSpeed, yJoyAxis * turnSpeed));
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
