using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Music2Trigger : MonoBehaviour {

    private AudioSource music1;
    private AudioSource music2;
    public string music1Tag;
    public string music2Tag;
    public string newEnvironnement;

    // Use this for initialization
    void Start () {
        music1 = GameObject.FindGameObjectWithTag(music1Tag).GetComponent<AudioSource>();
        music2 = GameObject.FindGameObjectWithTag(music2Tag).GetComponent<AudioSource>();
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == true)
        {
            music1.DOFade(0, 30);
            music2.DOFade(1, 30);
            music2.Play();
            other.GetComponent<Player>().environnement = newEnvironnement;
        }
    }
}
