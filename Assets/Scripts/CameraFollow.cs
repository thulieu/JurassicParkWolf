using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


    //The camera will follow this target 
    public Transform target;

    public float height;
    public float radius;
    public float angle;
    public float rotationalSpeed;
    public float offSetY;


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target.position);

        float cameraX = target.position.x + (radius * Mathf.Cos(angle));
        float cameraY = target.position.y + height;
        float cameraZ = target.position.z + (radius * Mathf.Sign(angle));

        transform.position = new Vector3(cameraX, cameraY, cameraZ);

        if (Input.GetKey(KeyCode.A))
        {
            angle = angle - rotationalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            angle = angle + rotationalSpeed * Time.deltaTime;
        }
	}
}
