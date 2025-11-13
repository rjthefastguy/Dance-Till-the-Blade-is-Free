using UnityEngine;
using UnityEngine.Events;
public class CollisionDetector : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private UnityEvent _colliderEntered;
    [SerializeField] private UnityEvent _colliderExit;
    public bool stayDown;
    Collider enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Pressed",true);
            anim.SetBool("Idle",true);
            _colliderEntered?.Invoke();
        }
        if(col.gameObject.CompareTag("Enemy"))
        {
            if(col.collider.GetComponent<ThrownObjects>().thrown == true)
            {
                anim.SetBool("Pressed",true);
                anim.SetBool("Idle",true);
                _colliderEntered?.Invoke();
            }
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(!stayDown)
            {
                anim.SetBool("Pressed",false);
                anim.SetBool("Idle",false);
                _colliderExit?.Invoke();
            }
        }
        if(col.gameObject.CompareTag("Enemy"))
        {
            if(col.collider.GetComponent<ThrownObjects>().thrown == true)
            {
                if(!stayDown)
                {
                    anim.SetBool("Pressed",false);
                    anim.SetBool("Idle",false);
                    _colliderExit?.Invoke();
                }
            }
        }
    }
    

}
