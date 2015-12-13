using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [HideInInspector]
    public Player playerObject;
    private SpriteRenderer background;
    public float playerEnd;
    private float backParallax;
    private float playerStartY;
    private float backStartY;
    private TrailRenderer tr;
    private GameObject normalMap;
    public float normalMapScrollSpeed;

    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>();
        background = GetComponentInChildren<SpriteRenderer>();
        normalMap = GetComponentInChildren<MeshRenderer>().transform.gameObject;
        backStartY = background.transform.localPosition.y;
        playerStartY = playerObject.transform.localPosition.y;
        tr = playerObject.GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        transform.position = new Vector3(0, playerObject.transform.position.y -2, -10);
        backParallax = Maths.Remap(playerObject.transform.localPosition.y, playerStartY, playerEnd, backStartY, backStartY * -1);
        background.transform.localPosition = new Vector3(0, backParallax, 10);
        normalMap.transform.localPosition -= new Vector3(0, normalMapScrollSpeed/100, 0);
        tr.sortingLayerName = "Character";
    }

}
