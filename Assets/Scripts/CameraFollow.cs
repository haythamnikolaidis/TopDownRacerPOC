using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform Camera;

    public Transform Player;

    public float FollowDistance = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Camera = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.position = Player.position + new Vector3(0,FollowDistance,0);
        Camera.LookAt(Player);
    }
}
