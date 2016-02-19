using UnityEngine;
using System.Collections;
using System;

public class RandomMovement : MonoBehaviour {

    public Transform target;
    public GameObject allosaurus;
    private Animation animation;

    // movement
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

    // sound clip
    public AudioClip damage, roar, run, dead;

    // setting
    CharacterController cc;
    AudioSource audio;
    Rigidbody rig;
    float health = 100f;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        animation = allosaurus.GetComponent<Animation>();
        //CreateTargetPoint();
    }


    // Update is called once per frame
    void Update () {
        if (target != null && health > 0f)
        {
            transform.GetComponent<NavMeshAgent>().destination = target.position;
            animation.Play("Allosaurus_Run", PlayMode.StopAll);
        }
        else
        {
            transform.GetComponent<NavMeshAgent>().destination = transform.position;
        }
        //else
        //{
        //    if (timeSwitch <= 0)
        //    {
        //        timeSwitch = 10;
        //        CreateTargetPoint();
        //    }
        //    else
        //    {
        //        allosaurus.GetComponent<Animation>().Play("Allosaurus_Walk", PlayMode.StopAll);
        //        transform.GetComponent<NavMeshAgent>().destination = new Vector3(tarX, 0, tarZ);
        //        timeSwitch -= 1 * Time.deltaTime;
        //    }
        //}
        if (rig.velocity.magnitude > 1f && audio.isPlaying == false)
        {
            audio.volume = UnityEngine.Random.Range(0.8f, 1f);
            audio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            audio.Play();
        }

    }

    private void RemoveHealth(float dame) {
        health -= dame;
        if (health <= 0f)
        {
            animation.Play("Allosaurus_Die", PlayMode.StopAll);
        }
        else {
            animation.Play("Allosaurus_Hit01", PlayMode.StopAll);
            audio.PlayOneShot(damage);
            StartCoroutine(WaitAnimationComple("Allosaurus_Hit01"));
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    private IEnumerator WaitAnimationComple(string aniName) {
        yield return new WaitForSeconds(animation[aniName].length);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (health > 0f)
        {
            if (collision.gameObject.tag == "arrow")
            {
                RemoveHealth(15f);
            }
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
