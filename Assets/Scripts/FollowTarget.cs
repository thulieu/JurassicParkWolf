﻿using UnityEngine;

public abstract class FollowTarget : MonoBehaviour {

    [SerializeField]
    public Transform target;

    [SerializeField]
    private bool autoTargetPlayer = true;


	// Use this for initialization
	virtual protected void Start () {
        if (autoTargetPlayer)
        {
            FindTargetPlayer();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (autoTargetPlayer && (target == null || !target.gameObject.activeSelf))
        {
            FindTargetPlayer();
        }
        if (target != null && (target.GetComponent<Rigidbody>() != null && !target.GetComponent<Rigidbody>().isKinematic))
        {
            Follow(Time.deltaTime);
        }
	}


    public void FindTargetPlayer() {
        if (target == null)
        {
            GameObject targetObj = GameObject.FindGameObjectWithTag("player");
            if (targetObj)
            {
                SetTarget(targetObj.transform);
            }
        }
    }

    public virtual void SetTarget(Transform newTransform) {
        target = newTransform;
    }

    public Transform Target { get { return this.target; } }
    protected abstract void Follow(float deltaTime);

}
