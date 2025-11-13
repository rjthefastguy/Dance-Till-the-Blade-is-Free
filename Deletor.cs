using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletor : MonoBehaviour
{
    public GameObject died;
    public GameObject self;
    public float blowUpTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(died.activeInHierarchy == false)
        {
            if(blowUpTimer <= 0)
            {
                DestroyGameObject();
            }
            else
            {
                blowUpTimer -= Time.deltaTime;
            }
        }
    }
    void DestroyGameObject()
    {
        Destroy(self);
    }
}
