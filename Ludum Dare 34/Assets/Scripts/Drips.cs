using UnityEngine;
using System.Collections;

public class Drips : MonoBehaviour {

    public float scalingFactor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            Debug.Log("hum");
            other.GetComponent<Player>().scalePlayer(scalingFactor);
        }
        Destroy(this.gameObject);
    }
}
