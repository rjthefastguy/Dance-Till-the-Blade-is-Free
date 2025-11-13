using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDIE : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        particle.transform.position = transform.position;
        if(transform.position.y <= -18.5f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("He died somehow...");
        particle.SetActive(true);
        gameObject.SetActive(false);
    }
}
