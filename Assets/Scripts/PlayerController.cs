using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;
    private Animator anim;
    private float inputH;
    private float inputV;
    private Rigidbody rbody;
    private bool run;
    private bool diveForward;
    private bool isAim;
    private bool isDrawArrow;


    public GameObject bow;
    public float aimingWeight;

    // Use this for initialization
    void Start () {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        run = false;
        diveForward = false;
        isAim = true;
        bow.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAim = true;
            anim.SetBool("isAim", isAim);
            bow.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            isAim = false;
            anim.SetBool("isAim", isAim);
            bow.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click down");
            isDrawArrow = true;
            anim.SetBool("isDrawArrow", isDrawArrow);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawArrow = false;
            anim.SetBool("isDrawArrow", isDrawArrow);
        }

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("run", run);
        anim.SetBool("diveForward", diveForward);
       

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }else{
            run = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            diveForward = true;

        }else {
            diveForward = false;
        }


        if (moveZ < 0f)
        {
            moveX = 0f;

        }else if (run)
        {
            moveX *= 3f;
            moveZ *= 3f;
        }

        rbody.velocity = new Vector3(moveX, 0f, moveZ);
    }

    void LateUpdate() {
        aimingWeight = Mathf.MoveTowards(aimingWeight, (isAim)? 1.0f : 0.0f, Time.deltaTime * 5);

        Vector3 normalState = new Vector3(0,0,-2f);
        Vector3 aimingState = new Vector3(0, 0, 0.3f);

        Vector3 pos = Vector3.Lerp(normalState, aimingState, aimingWeight);

        m_Cam.transform.localPosition = pos;
    }
}
