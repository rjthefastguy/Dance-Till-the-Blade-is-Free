using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseControl : MonoBehaviour
{
    public FlyingGuy enemy;


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.CompareTag("Player"))
        {
            //print(enemy.GetComponent<FlyingGuy>());
            enemy.StartChase(otherObject.gameObject);//
        }
    }
}
