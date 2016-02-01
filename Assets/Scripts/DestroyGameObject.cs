using UnityEngine;
using System.Collections;

public class DestroyGameObject : MonoBehaviour {

    public float destroyTime = 5.0f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
	}
}
