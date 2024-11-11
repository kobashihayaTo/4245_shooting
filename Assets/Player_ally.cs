using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ally : MonoBehaviour
{

    public float attackRange = 0.0f;

    public Transform Enemy;
    public GameObject canonball;
    private int count = 0;

    private int flag = 0;

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

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
           
            transform.LookAt(Enemy);
            count++;
            
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, Quaternion.identity);
                Debug.Log("îÕàÕÇ…ì¸ÇËÇ‹ÇµÇΩ");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            flag = 0;
            Debug.Log("îÕàÕÇ©ÇÁèoÇ‹ÇµÇΩ");
        }
    }

}
