using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieCheckerboss : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField] private UnityEvent battleWon;
    public int enemyCount;
    public int maxEnemyCount;
    // Start is called before the first frame update
    void Start()
    {
        maxEnemyCount = enemies.Length;
        enemyCount = maxEnemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeInHierarchy == false || enemies[i] == null)
            {
                enemyCount--;
            }
            
        }*/
        if(enemyCount <= 0)
        {
            BattleWon();
        }
    }
    public void CountDown()
    {
        enemyCount--;
    }

    public void BattleWon()
    {
        battleWon?.Invoke();
    }
}
