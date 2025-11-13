using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public Door elevator;
    public float speed = 1.0f;
    public GameObject Dance;
    public GameObject endpos;
    public bool dance = false;
    [SerializeField] private UnityEvent _ElevatorReached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dance)
        {
            Dance.transform.position = new Vector3(transform.position.x ,transform.position.y +2,transform.position.z);
        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(transform.position.x ,transform.position.y +2,transform.position.z);
            collision.transform.SetParent(transform);
            dance = true;
            elevator.Opening();

        }
    }

     public void OnTriggerStay(Collider collision)
    {
        float dist = Vector3.Distance(transform.position, endpos.transform.position);
        if(dist > .1f)
        {
            //hi
        }
        else
        {
            dance = false;
            collision.transform.SetParent(null);
            _ElevatorReached.Invoke();
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
