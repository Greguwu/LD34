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
        }
    }

    void HurtPlayer(Collision2D other)
    {
        Debug.Log("wesh");
        hasCollidedOnce = true;
        other.gameObject.GetComponent<Player>().scalePlayer(-downScalingFactor);
    }
}
