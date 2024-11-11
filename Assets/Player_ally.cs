using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ally : MonoBehaviour
{

    public float attackRange = 0.0f;

    public Transform Enemy;
    public GameObject canonball;
    private int count = 0;

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
            
            //‚±‚±‚Å”š‚ğ•Ï‚¦‚Ä’e‚Ì‘Å‚ÂŠ´Šo‚Ì•ÏX
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, Quaternion.identity);
                Debug.Log("”ÍˆÍ‚É“ü‚è‚Ü‚µ‚½");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("”ÍˆÍ‚©‚ço‚Ü‚µ‚½");
        }
    }

}
