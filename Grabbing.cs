using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Animator anim;
    public LayerMask Grabbable;
    public ThirdPersonCam Camera;
    RaycastHit hit;
    public float maxSwingDistance;
    public GameObject shoulderHold;
    public Transform Home = null;
    public GameObject m_camera;
    private float moveTime = 0;
    public bool GrabIt;
    private Rigidbody m_Rigidbody;
    public float Speed;
    public float m_Thrust;
    public bool Throwing = false;
    [SerializeField] GameObject StunPoint;
    [SerializeField] float attackRangeX;
    [SerializeField] float attackRangeY;
    [SerializeField] float attackRangeZ;
    Collider lassoedThing;
    public float targetTime = 0.67f;
    private bool windUp = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GrabIt)
        {
            moveTime += Speed * Time.deltaTime;
            lassoedThing.transform.position = Vector3.Slerp(lassoedThing.transform.position, shoulderHold.transform.position, moveTime);
        }
        if(moveTime >= 0.01f)
        {
            Throwing = true;
        }
        if(windUp)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                OnGrab();
            }
        }
    }

    void OnGrab()
    {

        Collider[] Grabbed = Physics.OverlapBox(StunPoint.transform.position,new Vector3(attackRangeX, attackRangeY, attackRangeZ) /2, Quaternion.identity, Grabbable);
        
        if(Grabbed.Length > 0)
        {
            anim.SetBool("IsGrabbing", true);
            Home = Grabbed[0].transform.parent;
            Grabbed[0].transform.SetParent(transform);
            Grabbed[0].enabled = false;
            GrabIt = true;
            lassoedThing = Grabbed[0];
            lassoedThing.GetComponent<SlimeChase>().grabbed = true;
            Camera.aiming = true;
        }
        else if(!Throwing)
        {
            Debug.Log("nothing...");
        }
        if(Throwing)
        {
            windUp = true;
            anim.SetBool("IsThrowing", true);
            if (targetTime <= 0.0f)
            {
                GrabIt = false;
                Throwing = false;
                targetTime = 0.67f;
                moveTime = 0;
                lassoedThing.GetComponent<ThrownObjects>().thrown = true;
                m_Rigidbody = lassoedThing.GetComponent<Rigidbody>();
                lassoedThing.transform.SetParent(Home);
                Home = null;
                m_Rigidbody.AddForce(m_camera.transform.forward * m_Thrust);
                lassoedThing.GetComponent<SlimeChase>().grabbed = false;
                lassoedThing.enabled = true;
                Camera.basic = true;
                lassoedThing = null;
                windUp = false;
            }

        }
    }     

    public void endGrab()
    {
        anim.SetBool("IsThrowing", false);
    }
    public void IdleGrab()
    {
        anim.SetBool("IsGrabbing", false);
        anim.SetBool("IsIdleGrab", true);
    }
}
