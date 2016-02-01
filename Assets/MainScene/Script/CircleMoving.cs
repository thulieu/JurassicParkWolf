using UnityEngine;
using System.Collections;

public class CircleMoving : MonoBehaviour {
	public GameObject directionWall;
	float timeCounter = 0;

	float speed;
	float width;
	float height;
	// Use this for initialization
	void Start () {
		speed = 0.42f;
		width = 20f;
		height = 20;

	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed;

		float x = Mathf.Cos (timeCounter) * width;
		float z = Mathf.Sin (timeCounter) * height;

		transform.position = new Vector3 (x, 18.5f, z);
	
	}

	IEnumerator ChangeDirection(){

		yield return new WaitForSeconds (14);
		var newWall = Instantiate (directionWall, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		
		newWall.transform.position = new Vector3 (21.59f, 18.5f, -1.4f);
		
		newWall.transform.localScale = new Vector3 (20, 20, 2);
	}

	void OnTriggerEnter(Collider other) {
		height = -height;
		StartCoroutine (ChangeDirection ());
		Destroy (other);

	}
}
