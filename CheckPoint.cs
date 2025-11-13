using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Dance Players;
    public int checker = 1;
    public int Chrevives;
    private Transform m_transform;
    [Header("Textures")]
    public GameObject obolisk;
    public Material Light;
    [SerializeField] GameObject obpart;
    void Start()
    {
        m_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(checker >= 0)
            {
                Players.revives = Chrevives;
                Players.CheckPoint = m_transform;
                obolisk.GetComponent<Renderer>().material = Light;
                obpart.SetActive(true);

            }
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
           checker -= 1;
           Players.CheckPoint = m_transform;
        }
    }

}
