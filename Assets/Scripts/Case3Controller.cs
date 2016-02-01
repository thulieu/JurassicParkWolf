using UnityEngine;
using System.Collections;

public class Case3Controller : MonoBehaviour {

    public GameObject upSound;
    public GameObject downSound;
    private bool isPlay = true;
    // Use this for initialization
    void Start () {
        StartCoroutine(PlaySound());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator PlaySound()
    {
        if (isPlay)
        {
            upSound.SetActive(true);
            downSound.SetActive(false);
        }
        else
        {
            upSound.SetActive(false);
            downSound.SetActive(true);
        }
        yield return new WaitForSeconds(23);
        isPlay = !isPlay;
        StartCoroutine(PlaySound());
    }
}
