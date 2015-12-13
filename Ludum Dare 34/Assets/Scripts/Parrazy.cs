using UnityEngine;
using System.Collections;

public class Parrazy : MonoBehaviour {

    public float coupedecale;

    void Start() {
        
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position += new Vector3 (0, coupedecale, 0);
    }
}