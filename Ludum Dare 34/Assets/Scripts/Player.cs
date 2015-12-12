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
        if (inMovement == true)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 1);
            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.4f, 1), Mathf.Clamp(transform.localScale.y, 0.4f, 1), 1);
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
        playerBody.velocity = new Vector2(Mathf.Clamp(playerBody.velocity.x, -maxXVelocity, maxXVelocity), Mathf.Clamp(playerBody.velocity.y, -maxDownVelocity, maxUpVelocity));   
    }

    public void scalePlayer(float newScale)
    {
        transform.localScale += new Vector3(newScale, newScale, 0);
    }
}
