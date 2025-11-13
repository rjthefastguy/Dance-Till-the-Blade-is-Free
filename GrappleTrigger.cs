using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTrigger : MonoBehaviour
{
    public Grappling DanceS;
    public GameObject Dance;
    public GameObject Grapple;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            DanceS.LookDirection =  Grapple.transform.position - Dance.transform.position;
            Debug.Log("Go time ");
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Stop");
            DanceS.LookDirection = Dance.transform.position;
        }
    }
}
