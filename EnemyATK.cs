using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyATK : MonoBehaviour
{
    [SerializeField] float damage;
    public Dance PlayerD;
    public float nextFireTime = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<Dance>();
        if(Time.time > nextFireTime)
        {

            if (player)
            {
                player.TakeHit(damage);
            }
        }
    }

}
