using UnityEngine;
using System.Collections;

public class WaterPath : MonoBehaviour {

    private BezierSpline thisSpline;
    public float pathDuration;

	// Use this for initialization
	void Start () {
        thisSpline = GetComponent<BezierSpline>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitEnd(Collider2D other)
    {
        //Debug.Log("wait");
        yield return new WaitForSeconds(pathDuration);
        other.GetComponent<SplineWalker>().enabled = false;
        other.GetComponent<SplineWalker>().progress = 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            other.GetComponent<SplineWalker>().duration = pathDuration;
            other.GetComponent<SplineWalker>().spline = thisSpline;
            other.GetComponent<SplineWalker>().enabled = true;
            StartCoroutine(WaitEnd(other));
        }
    }
}
