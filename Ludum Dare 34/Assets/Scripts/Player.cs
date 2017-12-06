using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour {

    private float xJoyAxis;
    private float yJoyAxis;
    [HideInInspector]
    public Vector2 playerVel;
    private Rigidbody2D playerBody;
    private SpriteRenderer playerSprite;
    private TrailRenderer playerTrail;
    //private ParticleSystem playerParticles;

    public float turnSpeed;
    public float maxUpVelocity;
    public float maxDownVelocity;
    public float scaleSpeedFactor;
    public float maxXVelocity;
    private float stickRotation;
    private bool inMovement;
    public float velocityRemap;
    private float lastStickRotation = 0;
    public float spriteMinimumSize;
    private float startSpriteMinimumSize;
    public float spriteMaximumSize;
    private float startSpriteMaximumSize;
    //private float startSpriteMaximumSize;
    public float trailScale;
    private CameraFollow mainCamera;

	private Animator animator;

    //LES SONS DU JEU EN TABLEAU COMMME UN POOORC
    [HideInInspector]
    public AudioSource sourceAudio;
	public AudioClip[] collisonsVille;
	public AudioClip[] collisonsForet;
	public AudioClip[] collisonsPlage;

	public AudioClip[] plopsVille;
	public AudioClip[] plopsForet;
	public AudioClip[] plopsPlage;

	//là ya tous les sons in puis les sons out, on fonctionne à l'envers
	public AudioClip[] sonsIN;
	public AudioClip[] sonsOUT;

	private int randomRange;

	//IMPORTANT : DANS QUEL ENVIRONNEMENT ON EST :ville, foret, littoral, ocean
	public string environnement = "ville";
    private float volumeSounds = 0.7f;

    // Use this for initialization
    void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerTrail = GetComponentInChildren<TrailRenderer>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        startSpriteMinimumSize = spriteMinimumSize;
        startSpriteMaximumSize = spriteMaximumSize;
        //startSpriteMaximumSize = spriteMaximumSize;
		animator = playerSprite.GetComponent<Animator>();
		//playerParticles = GetComponentInChildren<ParticleSystem>();
		sourceAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        xJoyAxis = Input.GetAxis("Horizontal Axis");
        yJoyAxis = Input.GetAxis("Vertical Axis");
        stickRotation = Mathf.Atan2(yJoyAxis, xJoyAxis) * Mathf.Rad2Deg;

        if (((xJoyAxis < 0.15)&&(xJoyAxis > -0.15))&&((yJoyAxis < 0.15) && (yJoyAxis > -0.15)))
        {
            inMovement = false;
            //playerSprite.transform.eulerAngles = new Vector3(0, 0, lastStickRotation);
        }
        else
        {
            inMovement = true;
            //playerSprite.transform.eulerAngles = new Vector3(0, 0, stickRotation - 270);
            //playerSprite.transform.eulerAngles = new Vector3(0, 0, Maths.Remap(stickRotation, -180, 0, -70, 70));
            lastStickRotation = stickRotation - 270;
        }

        if (yJoyAxis < 1)
        {
            velocityRemap = Maths.Remap(yJoyAxis, 1, -1, 1, maxDownVelocity + (playerBody.transform.localScale.x * scaleSpeedFactor));
        }

        if (inMovement == true)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 1);
        }

        spriteMinimumSize = Maths.Remap(mainCamera.backParallax, mainCamera.backStartY, mainCamera.backStartY*-1, startSpriteMinimumSize, startSpriteMaximumSize);
        spriteMaximumSize = Maths.Remap(mainCamera.backParallax, mainCamera.backStartY, mainCamera.backStartY * -1, startSpriteMaximumSize, startSpriteMaximumSize + 0.5f);
        //spriteMaximumSize = Maths.Remap(mainCamera.backParallax, mainCamera.backStartY, mainCamera.backStartY*-1, startSpriteMaximumSize, spriteMaximumSize * 2);
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
        //transform.localScale += new Vector3(newScale, newScale, 0);
        transform.DOScale(transform.localScale.x + newScale, 1);
    }

    public void playAnim(string animAJouer)
    {
		animator.Play( Animator.StringToHash( animAJouer ) );
    }

    public void colliSon()
    {
    	//Ici on regarde dans quel environnement on se trouve
    	switch (environnement)
    	{
    		case "ville":
				randomRange = Random.Range(0, collisonsVille.Length);
		        if (collisonsVille[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(collisonsVille[randomRange], volumeSounds);
		        }
    		break;

			case "foret":
				randomRange = Random.Range(0, collisonsForet.Length);
		        if (collisonsForet[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(collisonsForet[randomRange], volumeSounds);
		        }
			break;    	
				
			case "ocean":
			randomRange = Random.Range(0, collisonsPlage.Length);
		        if (collisonsPlage[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(collisonsPlage[randomRange], volumeSounds);
		        }
    		break;
    	}
    }

	public void playPlop()
    {

    	//Ici on regarde dans quel environnement on se trouve
    	switch (environnement)
    	{
    		case "ville":
				randomRange = Random.Range(0, plopsVille.Length);
		        if (plopsVille[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(plopsVille[randomRange], volumeSounds);
		        }
    		break;

			case "foret":
				randomRange = Random.Range(0, plopsForet.Length);
		        if (plopsForet[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(plopsForet[randomRange], volumeSounds);
		        }
			break;    	
				
			case "ocean":
			randomRange = Random.Range(0, plopsPlage.Length);
		        if (plopsPlage[randomRange] != null)
		        {
					sourceAudio.PlayOneShot(plopsPlage[randomRange], volumeSounds);
		        }
    		break;
    	}
    }

	public void playSonInPath()
	{
		//Ici on regarde dans quel environnement on se trouve
    	switch (environnement)
    	{
    		case "ville":
				sourceAudio.PlayOneShot(sonsIN[0], volumeSounds);
    		break;

			case "foret":
				sourceAudio.PlayOneShot(sonsIN[1], volumeSounds);
			break;    	
				
			case "ocean":
				sourceAudio.PlayOneShot(sonsIN[2], volumeSounds);
    		break;
    	}
	}

	public void playSonOutPath()
	{
		//Ici on regarde dans quel environnement on se trouve
    	switch (environnement)
    	{
    		case "ville":
				sourceAudio.PlayOneShot(sonsOUT[0], volumeSounds);
    		break;

			case "foret":
				sourceAudio.PlayOneShot(sonsOUT[1], volumeSounds);
			break;    	
				
			case "ocean":
				sourceAudio.PlayOneShot(sonsOUT[2], volumeSounds);
    		break;
    	}
	}
}
