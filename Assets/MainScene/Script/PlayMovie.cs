using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

    public MovieTexture movie;
	// Use this for initialization
	void Start () {
        Renderer r = new Renderer();
        GetComponent<Renderer>().material.mainTexture = movie;
		movie.loop = true;
        movie.Play();
	}
}
