using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 starPos;
    public GameObject endpos;
    public float speed = 1.0f;
    public bool press = false;
    public bool unpressed = false;
    public bool goBack = false;
   // Start is called before the first frame update
    void Start()
    {
        starPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, endpos.transform.position);
        if(press)
        {
            DoorUp();
        }
        if(unpressed)
        {
            DoorDown();
        }
    }
    public void Opening()
    {
        unpressed = false;
        press = true;
    }
    public void DoorUp()
    {
        
        float dist = Vector3.Distance(transform.position, endpos.transform.position);
        if(dist > .1f)
        {
            Debug.Log("working");
            transform.position = Vector3.Lerp(transform.position, endpos.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if(goBack)
            {
                press = false;
                unpressed = true;
            }
        }
        
    }
    public void Closing()
    {
        press = false;
        unpressed = true;
    }
    public void DoorDown()
    {
        Debug.Log("desserp");
        float dist = Vector3.Distance(transform.position, starPos);
        if(dist > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, starPos, speed * Time.deltaTime);
        }
        else
        {
            unpressed = false;
        }
    }
}
