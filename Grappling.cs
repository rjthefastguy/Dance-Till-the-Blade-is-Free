using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{

    [Header("References")]
    public LineRenderer lr;
    public Animator anim;
    public Transform gunTip, player;
    public LayerMask whatIsGrappleable;
    public Vector3 LookDirection;
    public Movement Dance;
    public bool IsGrappling = false;

    [Header("Swinging")]
    public float maxSwingDistance;
    public SpringJoint joint;
    private Vector3 swingPoint;

    [Header("Input")]
    private Vector3 currentGrapplePosition;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnGrapple()
    {
        IsGrappling = !IsGrappling;
        if(IsGrappling) StartSwing();
        if(!IsGrappling) StopSwing();
    }
    // Update is called once per frame
    void Update()
    {
        if(Dance.Swinging)
        {
            Dance.moveSpeed = Dance.SwingSpeed;
        }
        if(!Dance.Swinging)
        {
            Dance.moveSpeed = Dance.startingSpeed;
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwing()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, LookDirection, out hit, maxSwingDistance, whatIsGrappleable))
        {
            Dance.Swinging = true;
            Debug.Log("hit " + hit.collider.name);
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            //the distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            anim.SetBool("IsGrapple",true);
        }
    }

    void StopSwing()
    {
        Dance.Swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
        anim.SetBool("IsGrapple",false);
    }

    void DrawRope()
    {
        //if not grappling, don't draw rope
        if(!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}
