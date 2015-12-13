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
    private SpriteRenderer playerSprite;
    private TrailRenderer playerTrail;

    public float turnSpeed;
    public float maxUpVelocity;
    public float maxDownVelocity;
    public float scaleSpeedFactor;
    public float maxXVelocity;
    private float stickRotation;
    private bool inMovement;
    private float velocityRemap;
    private float lastStickRotation = 0;
    public float spriteMinimumSize;
    private float startSpriteMinimumSize;
    public float spriteMaximumSize;
    public float trailScale;
    private CameraFollow mainCamera;

    // Use this for initialization
    void Start () {
        currentDevice = InputManager.ActiveDevice;
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerTrail = GetComponentInChildren<TrailRenderer>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        startSpriteMinimumSize = spriteMinimumSize;
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
            playerSprite.transform.eulerAngles = new Vector3(0, 0, lastStickRotation);
        }
        else
        {
            inMovement = true;
            playerSprite.transform.eulerAngles = new Vector3(0, 0, stickRotation - 270);
            lastStickRotation = stickRotation - 270;
        }

        if (currentDevice.LeftStickY < 1)
        {
            velocityRemap = Maths.Remap(yJoyAxis, 1, -1, 1, maxDownVelocity + (playerBody.transform.localScale.x * scaleSpeedFactor));
        }

        if (inMovement == true)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 1);
        }

        spriteMinimumSize = Maths.Remap(mainCamera.backParallax, mainCamera.backStartY, mainCamera.backStartY*-1, startSpriteMinimumSize, spriteMaximumSize);
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, spriteMinimumSize, spriteMaximumSize), Mathf.Clamp(transform.localScale.y, spriteMinimumSize, spriteMaximumSize), 1);
        playerTrail.startWidth = Mathf.Clamp(transform.localScale.x * trailScale, 0.1f, 1);
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
