using UnityEngine;
using System.Collections;

public class ThrowSpear : MonoBehaviour {

    public GameObject arrow;
    float throwSpeed = 1000f;
    public GameObject wepon;
    private GameObject throwThis;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire1"))
        {
            StartCoroutine("Arrow");
        }
    }

    IEnumerator Arrow() {
        yield return new WaitForSeconds(0.7f);
        throwThis = Instantiate(arrow, new Vector3(transform.position.x + 0.3f, 1.4f, transform.position.z - 0.161f), new Quaternion(wepon.transform.rotation.x, wepon.transform.rotation.y, wepon.transform.rotation.z, wepon.transform.rotation.w)) as GameObject;
        throwThis.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, throwSpeed));
    }
}
