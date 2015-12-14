using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour
{

    private CameraFollow followScript;
    private Player playerScript;
    private Rigidbody2D playerBody;
    private SpriteRenderer playerSprite;
    private TrailRenderer playerTrail;

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
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            followScript.enabled = false;
            playerBody.gravityScale = 0;
            playerBody.velocity = Vector3.zero;
            playerScript.enabled = false;
            playerSprite.GetComponent<Animator>().Play(Animator.StringToHash("entree ocean"));
            playerTrail.time = 1f;
            GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }
    }
}
