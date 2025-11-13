using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieChecker : MonoBehaviour
{
    public GameObject died;
    [SerializeField] private UnityEvent battleWon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(died.activeInHierarchy == false)
        {
            BattleWon();
        }
    }

    public void BattleWon()
    {
        battleWon?.Invoke();
    }
}
