using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {

    public float downScalingFactor;
    private bool hasCollidedOnce;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D other)
    {
        if (!hasCollidedOnce)
        {
            HurtPlayer(other);
			//previousScale = other.gameObject.GetComponent<Player>().transform.localScale;
            other.gameObject.GetComponent<Player>().playAnim("collision");
            other.gameObject.GetComponent<Player>().colliSon();
            //other.gameObject.GetComponent<Player>().transform.localScale = previousScale;
        }
    }

    void HurtPlayer(Collision2D other)
    {
        hasCollidedOnce = true;
        other.gameObject.GetComponent<Player>().scalePlayer(-downScalingFactor);
    }
}
