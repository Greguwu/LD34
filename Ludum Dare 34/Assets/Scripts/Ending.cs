using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Ending : MonoBehaviour
{

    private CameraFollow followScript;
    private Player playerScript;
    private Rigidbody2D playerBody;
    private SpriteRenderer playerSprite;
    private TrailRenderer playerTrail;
    public AudioSource endSong;
    public AudioSource endFX;
    public GameObject mainCamera;
    public GameObject particlesEnd;

    // Use this for initialization
    void Start()
    {
        followScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        playerScript = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>();
        playerBody = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Rigidbody2D>();
        playerSprite = playerBody.GetComponentInChildren<SpriteRenderer>();
        playerTrail = playerBody.GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void TheEnd()
    {
        endFX.Play();
        followScript.enabled = false;
        playerBody.gravityScale = 0;
        playerBody.velocity = Vector3.zero;
        playerScript.enabled = false;
        playerSprite.GetComponent<Animator>().Play(Animator.StringToHash("entree ocean"));
        playerTrail.time = 1f;
        endSong.Play();
        playerSprite.DOColor(new Color(255, 255, 255, 0), 5);
        mainCamera.transform.DOMove(new Vector3(0, -601.8f, -10), 20);
        particlesEnd.transform.localPosition = new Vector3(-2.0868f, 0.6f, 10);
        particlesEnd.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            TheEnd();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
