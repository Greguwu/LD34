using UnityEngine;
using System.Collections;
using DG.Tweening;
using InControl;


public class IntroCamera : MonoBehaviour {

    public static InputDevice currentDevice;
    private bool canStart;
    private CameraFollow followScript;
    private Player playerScript;
    private Rigidbody2D playerBody;
    private GameObject secondCamera;
    private ParticleSystem playerParticles;
    private SpriteRenderer playerSprite;
    private TrailRenderer playerTrail;
    public Animator dripAnimation;
    private AudioSource music1;

    // Use this for initialization
    void Awake () {
        DOTween.Init();
        transform.DOMove(new Vector3(0, -3, -10), 1, false).OnComplete(CanPlay);
        followScript = GetComponent<CameraFollow>();
        playerScript = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>();
        playerBody = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Rigidbody2D>();
        secondCamera = GameObject.FindGameObjectWithTag("Second Camera").transform.gameObject;
        playerParticles = playerBody.GetComponentInChildren<ParticleSystem>();
        playerSprite = playerBody.GetComponentInChildren<SpriteRenderer>();
        playerTrail = playerBody.GetComponentInChildren<TrailRenderer>();
        music1 = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }

    void Start()
    {
        currentDevice = InputManager.ActiveDevice;
        secondCamera.transform.position = new Vector3(secondCamera.transform.position.x, playerScript.transform.position.y - 2, -10);
    }

    // Update is called once per frame
    void Update () {
        if (canStart)
        {
            if (((currentDevice.LeftStickX < 0.15) && (currentDevice.LeftStickX > -0.15)) && ((currentDevice.LeftStickY < 0.15) && (currentDevice.LeftStickY > -0.15)))
            {
                //
            }
            else
            {
                followScript.enabled = true;
                playerBody.gravityScale = 1;
                playerScript.enabled = true;
                enabled = false;
                playerSprite.DOColor(new Color(255, 255, 255, 1), 5).OnComplete(enableTrail);
                music1.Play();
            }
        }
	}

    void CanPlay()
    {
        dripAnimation.enabled = true;
        canStart = true;
    }

    void enableTrail()
    {
        //playerTrail.enabled = true;
    }
}
