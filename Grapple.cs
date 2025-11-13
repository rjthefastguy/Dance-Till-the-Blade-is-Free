using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] LayerMask ropeLayerMask;
    public float distance = 90.0f;
    public LineRenderer line;
    public DistanceJoint2D rope;
    public Vector2 lookDirection;
    bool checker = true;
    public bool Trigger = false;

    //public bool trigger = false;

    void Start()
    {
        rope = gameObject.GetComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();

        rope.enabled = false;
        line.enabled = false;
    }

    void Update()
    {
        line.SetPosition(0, transform.position);

        lookDirection = transform.position - transform.position; 

        if (Input.GetMouseButtonDown(1) && checker == true)
        {
            Debug.Log("press");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, distance, ropeLayerMask);

            if (hit.collider != null && hit.collider.gameObject.layer == 8)
            {
                //grappling hits
                checker = false;
                SetRope(hit);
            }
        }
        else if (Input.GetMouseButtonUp(1) && checker == false)
        {
            checker = true;
            DestroyRope();
        }
    }

    void SetRope(RaycastHit2D hit)
    {
        rope.enabled = true;
        rope.connectedAnchor = hit.transform.position;

        line.enabled = true;
        line.SetPosition(1, hit.transform.position);
    }

    void DestroyRope()
    {
        rope.enabled = false;
        line.enabled = false;
    }

    //triggerenter
    //check if it is a grapple
    //if it is have the look direction at it 
}
