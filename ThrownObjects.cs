using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjects : MonoBehaviour
{
    public bool thrown = false;
    public EnemyDIE myDeath;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        myDeath = GetComponent<EnemyDIE>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if(rb.velocity.x == 0f)
            {
                thrown = false;
            }
        }
        if(thrown)
        {
            if(collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<EnemyDIE>().Die();
                myDeath.Die();
            }
            if(collision.collider.CompareTag("Button"))
            {
                myDeath.Die();
            }
        }
    }
}
