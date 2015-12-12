using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Player playerObject;
    private SpriteRenderer background;
    public float playerEnd;
    public float backParallax;
    private float playerStartY;
    private float backStartY;
    private TrailRenderer tr;
 
    void Awake()
    {
        background = GetComponentInChildren<SpriteRenderer>();
        backStartY = background.transform.localPosition.y;
        playerStartY = playerObject.transform.localPosition.y;
        tr = playerObject.GetComponentInChildren<TrailRenderer>();
        tr.sortingLayerName = "Character";
    }

    void Update()
    {
        transform.position = new Vector3(0, playerObject.transform.position.y -4, -10);
        backParallax = Maths.Remap(playerObject.transform.localPosition.y, backStartY * -1, playerEnd, backStartY, playerStartY);
        background.transform.localPosition = new Vector3(0, backParallax, 10);
    }

}
