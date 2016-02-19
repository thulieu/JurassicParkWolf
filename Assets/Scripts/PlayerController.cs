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
    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capCol;


    public GameObject bow;
    public float aimingWeight;

    //IK stuff
    public Transform spine;
    public float aimingZ = 213.46f;
    public float aimingX = -65.93f;
    public float aimingY = -20.1f;
    public float point = 30f;

    //Arrow
    public Transform wepon;
    public GameObject arrow;

    //Movement
    public float speed = 4;
    public float turnSpeed = 5;
    Vector3 directionPos;
    Vector3 lookPos;

    //
    public PhysicMaterial zfriction;
    public PhysicMaterial mfriction;

    // Use this for initialization
    void Start () {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        capCol = GetComponent<CapsuleCollider>();
        run = false;
        diveForward = false;
        isAim = true;
        bow.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

       
       

        //anim.SetFloat("inputH", inputH);
        //anim.SetFloat("inputV", inputV);
        //anim.SetBool("run", run);
        //anim.SetBool("diveForward", diveForward);


        //float moveX = inputH * 20f * Time.deltaTime;
        //float moveZ = inputV * 50f * Time.deltaTime;


        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    run = true;
        //}else{
        //    run = false;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    //navMeshAgent.Stop();
        //    diveForward = true;

        //}else {
        //    diveForward = false;
        //    //navMeshAgent.Resume();
        //}


        //if (moveZ < 0f)
        //{
        //    moveX = 0f;

        //}else if (run)
        //{
        //    moveX *= 3f;
        //    moveZ *= 3f;
        //}

        //rbody.velocity = new Vector3(moveX, 0f, moveZ);

        Ray ray = new Ray(m_Cam.position, m_Cam.forward);

        lookPos = ray.GetPoint(100);
        HandleFriction();
    }

    void FixedUpdate() {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        //rbody.AddRelativeForce(((transform.right * inputH) + (transform.forward * inputV)) * speed/Time.deltaTime);

        directionPos = transform.position + m_Cam.forward * 100f;
        Vector3 dir = directionPos - transform.position;
        dir.y = 0;

        rbody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);

        AimWepon();
        AttackAnimation();
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetBool("run", run);
        anim.SetBool("diveForward", diveForward);
    }

    void HandleFriction() {
        if (inputH == 0 && inputV == 0)
        {
            capCol.material = mfriction;
        }
        else
        {
            capCol.material = zfriction;
        }       
    }

    void AttackAnimation() {
        if (isAim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDrawArrow = true;
                anim.SetBool("isDrawArrow", isDrawArrow);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDrawArrow = false;
                anim.SetBool("isDrawArrow", isDrawArrow);
                GameObject Arrow = Instantiate(arrow, wepon.position, wepon.rotation) as GameObject;
                //Arrow.transform.rotation = new Quaternion(360f, Arrow.transform.rotation.y, Arrow.transform.rotation.z, Arrow.transform.rotation.w);
                Arrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 700f));
            }
        } 
    }

    void AimWepon() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAim = true;
            anim.SetBool("isAim", isAim);
            bow.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isAim = false;
            anim.SetBool("isAim", isAim);
            bow.SetActive(false);
        }
    }

    void LateUpdate() {
        aimingWeight = Mathf.MoveTowards(aimingWeight, (isAim)? 1.0f : 0.0f, Time.deltaTime * 5);

        Vector3 normalState = new Vector3(0,0,-2f);
        Vector3 aimingState = new Vector3(0, 0, 0f);

        Vector3 pos = Vector3.Lerp(normalState, aimingState, aimingWeight);

        m_Cam.transform.localPosition = pos;

        if (isAim)
        {
            Vector3 eulerAngleOffset = Vector3.zero;
            eulerAngleOffset = new Vector3(aimingX, aimingY, aimingZ);
            Ray ray = new Ray(m_Cam.position, m_Cam.forward);
            Vector3 lookPostion = ray.GetPoint(point);

            //spine.LookAt(lookPostion);
            spine.Rotate(eulerAngleOffset, Space.Self);
        }
    }
}
