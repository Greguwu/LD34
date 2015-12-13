using UnityEngine;
using System.Collections;

public class Drips : MonoBehaviour {

    public Sprite[] dripsSprites;
    public float scalingFactor;
    private int randomRange;
    public Vector2 movementDirection;
    private Rigidbody2D dripBody;
    private bool inArea = false;
    private GameObject playerObject;
    private Vector3 previousScale;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;
        randomRange = Random.Range(0, dripsSprites.Length);
        if (dripsSprites[randomRange] != null)
        {
            GetComponent<SpriteRenderer>().sprite = dripsSprites[randomRange];
        }
        dripBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerObject.transform.position.y <= transform.position.y + 9.5f)
        {
            inArea = true;
        }

        if (inArea)
        {
            dripBody.velocity = new Vector3(Mathf.Clamp(movementDirection.x, -10, 10), Mathf.Clamp(movementDirection.y, -10, 0), 0);
        }
        else if (!inArea)
        {
            dripBody.velocity = Vector3.zero;
        }
	}

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            other.GetComponent<Player>().scalePlayer(scalingFactor);
            previousScale = other.GetComponent<Player>().transform.localScale;
            other.GetComponent<Player>().playAnim("Grow");
            other.GetComponent<Player>().transform.localScale = previousScale;
			other.gameObject.GetComponent<Player>().playPlop();
        }
        if ((other.GetComponent<Player>() == true)||(other.GetComponent<Obstacles>() == true))
        {
            Destroy(this.gameObject);
        }
    }
}
