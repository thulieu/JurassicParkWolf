using UnityEngine;
using System.Collections;
using System;

public class RandomMovement : MonoBehaviour {

    public Transform target;

    public float minTarX = 1;
    public float maxTarX = 10;
    public float minTarZ = 1;
    public float maxTarZ = 10;
    public float tarX;
    public float tarZ;
    public float dampX;
    public float dampZ;
    public bool death = false;
    public float timeSwitch = 10;

    CharacterController cc;
    AudioSource audio;
    Rigidbody rig;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        CreateTargetPoint();
	}


    // Update is called once per frame
    void Update () {
        if (target != null)
        {
            transform.GetComponent<NavMeshAgent>().destination = target.position;
        }
        else
        {
            if (timeSwitch <= 0 )
            {
                timeSwitch = 10;
                CreateTargetPoint();
            }
            else
            {
                transform.GetComponent<NavMeshAgent>().destination = new Vector3(tarX, 0, tarZ);
                timeSwitch -= 1 * Time.deltaTime;
            }
        }
        if (rig.velocity.magnitude > 1f && audio.isPlaying == false)
        {
            audio.volume = UnityEngine.Random.Range(0.8f, 1f);
            audio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            audio.Play();
        }

    }


    private void CreateTargetPoint()
    {
        
        dampX = UnityEngine.Random.Range(40f, 40f);
        dampZ = UnityEngine.Random.Range(40f, 40f);

        tarX = transform.position.x + dampX;
        tarZ = transform.position.z + dampZ;
        //tarX = UnityEngine.Random.Range(minTarX, maxTarX) - dampX;
        //tarZ = UnityEngine.Random.Range(minTarZ, maxTarZ) - dampZ;
    }
}
