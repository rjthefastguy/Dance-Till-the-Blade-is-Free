using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingGuy : MonoBehaviour
{
    public float speed;
    private Transform m_transform;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    public float nextFireTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        m_transform = transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if(chase==true)
            Chase();
        else
            ReturnStartPoint();
        Flip();
    }


    public void StartChase(GameObject target)
    {
        player = target;
        chase = true;
    }


    private void Chase()
    {
        if(Time.time > nextFireTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
            {
                //change speed, shoot, animation
            }
            else
            {
                //reset variables
            }
        }
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }


    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            chase = false;
            if(Time.time > nextFireTime)
            {
                ReturnStartPoint();
            }
        }
        if(col.gameObject.CompareTag("Ground"))
        {
            chase = false;
            if(Time.time > nextFireTime)
            {
                ReturnStartPoint();
            }
        }
    }
}


