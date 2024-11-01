using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ally : MonoBehaviour
{

    public float attackRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = attackRange;
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
            Debug.Log("”ÍˆÍ‚É“ü‚è‚Ü‚µ‚½");
        }
    }
}
