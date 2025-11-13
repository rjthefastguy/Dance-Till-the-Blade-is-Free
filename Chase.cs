using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public float moveSpeed;
    public SlimeChase enemy;
    private Transform Transform;
    Rigidbody rb;
    public bool chase = false;
    public bool rest = true;
    public Transform[] patrolPoints;
    int currentPoint = 0;
    private Collider player;
    public float nextFireTime = 0;
    public Vector3 LookDirection;
    public float maxSight;
    public LayerMask Player;
    bool grounded;
    RaycastHit hit;
    public float Height;
    // Start is called before the first frame update
    void Start()
    {
        rest = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        LookDirection =  patrolPoints[currentPoint].position - transform.position;
        Transform = transform;
        Transform.right = LookDirection;
        Transform.Rotate(270.0f, 0.0f, 0.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {

        if(!enemy.grabbed)
        {
            Transform.right = LookDirection;
        }
        
        if(transform.eulerAngles.x < 269 || transform.eulerAngles.x > 271)
        {
            transform.eulerAngles = new Vector3(270, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit,  Height * 0.5f + 0.2f, Player) && hit.collider != null && hit.collider.gameObject.layer == 10;
        Debug.DrawRay(transform.position,LookDirection, Color.green);
        if(chase==true)
        {
            Chasing();
        }
         if(!grounded)
        {
             transform.position = transform.position + new Vector3(0, 1f * Time.deltaTime, 0);
        }
        else
        {
            ReturnStartPoint();

        }
    }
    public void StartChase(Collider target)
    {
        player = target;
        chase = true;
    }

    

    private void Chasing()
    {

        if(Time.time > nextFireTime)
        {
            LookDirection = Helpers.NoY(player.transform.position) - Helpers.NoY(transform.position);
            Transform.position = Vector3.MoveTowards( Helpers.NoY(transform.position), Helpers.NoY(player.transform.position), moveSpeed * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.right, out hit, maxSight, Player) && hit.collider != null && hit.collider.gameObject.layer == 12)
            {
                Debug.Log("I can see you!");
            }
            else
            {
                Debug.Log("can't find you!");
            }
        }
    }

    private void ReturnStartPoint()
    {
        if(Time.time > nextFireTime)
        {
            if(Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .1f)
            {
                currentPoint +=1;
                if(currentPoint >= patrolPoints.Length) currentPoint = 0;
            } 
            else 
            {
                LookDirection = patrolPoints[currentPoint].position - transform.position;
                Transform.position = Vector3.MoveTowards(Transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
            }
        }
            
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            chase = false;
            player = null;
            if(Time.deltaTime > nextFireTime)
            {
                rest = true;
                ReturnStartPoint();
            }
        }
    }
}
