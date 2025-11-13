using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeChase : MonoBehaviour
{
    public Chase enemy;
    public GameObject Mcamera;
    public float maxSwingDistance;
    public LayerMask Player;
    public bool grabbed = false;
    Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        if(!grabbed)
        {
            rb.constraints = RigidbodyConstraints.None;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, maxSwingDistance, Player) && hit.collider != null && hit.collider.gameObject.layer == 12)
            {
                enemy.StartChase(hit.collider);
                Debug.Log("Run!");
            }
        }
        if(grabbed)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            enemy.LookDirection = transform.position - Mcamera.transform.position;
        }
        
    }

}
