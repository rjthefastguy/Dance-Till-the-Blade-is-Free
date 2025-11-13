using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Dance : MonoBehaviour
{
    //[SerializeField] TMP_Text health;
    //[SerializeField] TMP_Text heal;
    public Animator anim;
    public float hitpoints;
    [SerializeField] float MaxHitpoints = 5;
    public float cooldownTimeD = 15;
    [SerializeField] public float HealCooling = 30;
    public int revives = 0;
    public Transform CheckPoint = null;
    public Transform Ground = null;
    public float reviveTime = -1;
    public LayerMask enemyLayer;
    [SerializeField] GameObject StunPoint;
    [SerializeField] float attackRangeX;
    [SerializeField] float attackRangeY;
    [SerializeField] float attackRangeZ;
    private bool canRevive = false;
    public int posX = 80;
    // Start is called before the first frame update
    void Start()
    {
        hitpoints = MaxHitpoints;
        anim = GetComponent<Animator>();
        //heal.text = "heal up";
    }
    public void OnStun()
    {
        anim.SetBool("IsStun",true);
    }
    // Update is called once per frame
    void Update()
    {
        if(hitpoints > MaxHitpoints)
        {
            hitpoints = MaxHitpoints;
        }
        if(HealCooling <= 0)
            {        //healing self
                HealCooling = 0;
                //heal.text = "heal up";
                if(Input.GetKeyDown(KeyCode.RightShift))
                {
                    if(hitpoints < MaxHitpoints)
                    {
                        anim.SetBool("IsHeal",true);
                        hitpoints += 1;
                        HealCooling = cooldownTimeD;
                        //heal.transform.position = new Vector3(0,0,0);

                    }
                
                }
            }
            else
            {
                HealCooling -= Time.deltaTime;
                //heal.text = "Heal CoolDown:" + (int) HealCooling;
            }
        if(canRevive == true)
        {
            if(reviveTime <= 0)
            {
                transform.position = new Vector3(CheckPoint.position.x,CheckPoint.position.y +1f,CheckPoint.position.z);
                canRevive = false;
                hitpoints = MaxHitpoints;
                revives -= 1;
            }
            else
            {
                reviveTime -= Time.deltaTime;
            }
        }
        
        //health.text = "Health: " + hitpoints;
    }

    public void endHeal()
    {
        anim.SetBool("IsHeal", false);
    }
    public void endStun()
    {
        anim.SetBool("IsStun", false);
    }
    public void TakeHit(float damage)
   {
       //damage depends on unique enemies
       hitpoints -= damage;
       if(hitpoints <= 0)
       {
           Death();
       }
   }
   public void Death()
    {
        if(revives > 0)
        {
            transform.position = new Vector3(CheckPoint.position.x,CheckPoint.position.y,CheckPoint.position.z -100);
            reviveTime = 3;
            canRevive = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
     public void Stun()
    {
        //check if attack child hits a gameobject with enemy layer in order to know what to attack
        Collider[] enemy = Physics.OverlapBox(StunPoint.transform.position,new Vector3(attackRangeX, attackRangeY, attackRangeZ) /2, Quaternion.identity,enemyLayer);
        foreach (Collider enemyGameObject in enemy)
        {
            if(enemyGameObject.CompareTag("Enemy"))
            {
                Debug.Log("Stunned");
                enemyGameObject.GetComponent<Chase>().nextFireTime = Time.time + 3;
                enemyGameObject.GetComponent<EnemyATK>().nextFireTime = Time.time + 3;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        //Allows to see the range of attack child
        Gizmos.DrawWireCube(StunPoint.transform.position,new Vector3(attackRangeX, attackRangeY, attackRangeZ) /2);
    }
}
