using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ally : MonoBehaviour
{

    public float attackRange = 0.0f;

    //[SerializeField]
    //private CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        //circleCollider2D.radius = attackRange;
        GetComponent<SphereCollider>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("îÕàÕÇ…ì¸ÇËÇ‹ÇµÇΩ");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("îÕàÕÇ©ÇÁèoÇ‹ÇµÇΩ");
        }
    }

}
