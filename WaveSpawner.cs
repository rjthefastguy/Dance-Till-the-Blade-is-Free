using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
    /*public GameObject[] enemies;
    [SerializeField] private UnityEvent battleWon;
    public int enemyCount;
    public int maxEnemyCount;
    public bool won;
    private BoxCollider2D bc2d;
    CollisionDetector cd;
    
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        cd = GetComponent<CollisionDetector>();
        foreach (EnemyDIE enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
            enemyCount++;
        }
        maxEnemyCount = enemyCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(enemyCount <= 0)
        {
            BattleWon();
        }
    }

    public void StartBattle() 
    {
        Debug.Log("Wave started");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeInHierarchy == false) continue;
            enemies[i].gameObject.SetActive(true);
        }
    }

    public void BattleWon()
    {
        battleWon?.Invoke();
    }
    */
}
