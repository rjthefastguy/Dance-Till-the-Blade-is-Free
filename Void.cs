using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] float damage;
    public GameObject Dance;
    public Dance Dancing;
    public GameObject TP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collision)
    {
        Dance.transform.position = TP.transform.position;
        if(collision.CompareTag("Player"))
        {
            
            // if(Dancing.CheckPoint != null)
            // {
            //     Debug.Log("hi");
            //     Dance.transform.position = new Vector3(Dancing.CheckPoint.position.x,Dancing.CheckPoint.position.y +1,Dancing.CheckPoint.position.z);
            //     Dancing.TakeHit(damage);
            // }
            // else
            // {
            //     Debug.Log("hi2");
            //     Dance.transform.position = new Vector3(Dancing.Ground.position.x,Dancing.Ground.position.y +1,Dancing.Ground.position.z);
            //     Dancing.TakeHit(damage);
            // }
        }
         if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Collider>().GetComponent<EnemyDIE>();
        }
    }
}
