using UnityEngine;
using System.Collections;
using UnityEditor;

public class FreeCameraLook : Pivot {

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float turnSpeed = 1.5f;

    [SerializeField]
    private float turnSmoothing = .1f;

    [SerializeField]
    private float tiltMax = 75f;

    [SerializeField]
    private float tiltMix = 45f;

    [SerializeField]
    private bool lockCursor = false;

    private float lookAngle;
    private float tiltAngle;

    private const float LookDistance = 100f;

    private float smoothX = 0;
    private float smoothY = 0;
    private float smoothXvelocity = 0;
    private float smoothYvelocity = 0;


    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = lockCursor;

        cam = GetComponentInChildren<Camera>().transform;
        pivot = cam.parent;

    }

    // Update is called once per frame
    protected override void Update () {

        base.Update();
        HandleRotationMovement();
        if (lockCursor && Input.GetButtonUp("0"))
        {
            Cursor.visible = lockCursor;
        }
	}

    void onDisable() {
        Cursor.visible = false;
    }

    protected override void Follow(float deltaTime)
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, deltaTime * moveSpeed);
    }

    void HandleRotationMovement() {

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, x, ref smoothXvelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, y, ref smoothYvelocity, turnSmoothing);

        }
        else {
            smoothX = x;
            smoothY = y;
        }

        lookAngle += smoothX * turnSpeed;

        transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

        tiltAngle -= smoothY * turnSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMix, tiltMax);

        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);


    }
}
