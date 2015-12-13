using UnityEngine;
using System.Collections;
using InControl;

public class Player : MonoBehaviour {

    public static InputDevice currentDevice;
    private InControlManager inControlScript;
    private float xJoyAxis;
    private float yJoyAxis;
    [HideInInspector]
    public Vector2 playerVel;
    private Rigidbody2D playerBody;

    public float turnSpeed;
    public float maxUpVelocity;
    public float maxDownVelocity;
    public float maxXVelocity;
    private float stickRotation;
    private bool inMovement;
    private float velocityRemap;
    private float lastStickRotation = 0;
    public float spriteMinimumSize;
    public float spriteMaximumSize;

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

        if (((currentDevice.LeftStickX < 0.15)&&(currentDevice.LeftStickX > -0.15))&&((currentDevice.LeftStickY < 0.15) && (currentDevice.LeftStickY > -0.15)))
        {
            inMovement = false;
            this.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = new Vector3(0, 0, lastStickRotation);
        }
        else
        {
            inMovement = true;
            this.GetComponentInChildren<SpriteRenderer>().transform.eulerAngles = new Vector3(0, 0, stickRotation - 270);
            lastStickRotation = stickRotation - 270;
        }

        if (currentDevice.LeftStickY < 1)
        {
            velocityRemap = Maths.Remap(yJoyAxis, 1, -1, 1, maxDownVelocity);
        }

        if (inMovement == true)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 1);
        }

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, spriteMinimumSize, spriteMaximumSize), Mathf.Clamp(transform.localScale.y, spriteMinimumSize, spriteMaximumSize), 1);
    }

    void FixedUpdate()
    {
        playerVel = playerBody.velocity;
        playerBody.AddForce(new Vector2(xJoyAxis * turnSpeed, yJoyAxis * turnSpeed));
        playerBody.velocity = new Vector2(Mathf.Clamp(playerBody.velocity.x, -maxXVelocity, maxXVelocity), Mathf.Clamp(playerBody.velocity.y, -velocityRemap, maxUpVelocity));   
    }

    public void scalePlayer(float newScale)
    {
        transform.localScale += new Vector3(newScale, newScale, 0);
    }
}
