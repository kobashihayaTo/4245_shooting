using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : MonoBehaviour
{
    public float attackRange = 0.0f;
    public Transform Enemy;
    public GameObject canonball;
    private int count = 0;
    private SceneSwitter sceneSwitter;
    //�̗�
    [SerializeField] private float hp = 5;  //�̗�

    // Start is called before the first frame update
    void Start()
    {
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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�͈͂ɓ�����");
            transform.LookAt(Enemy);
            count++;

            //�����Ő�����ς��Ēe�̑ł��o�̕ύX
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, Quaternion.identity);
                Debug.Log("�����Ă��");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�͈͂���o�܂���");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //�^�O��EnemyBullet�̃I�u�W�F�N�g��������������{}���̏������s����
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Player");  //�R���\�[����hit Player���\��
            hp -= 1;
        }

        //�̗͂�0�ȉ��ɂȂ�����{}���̏������s����
        if (hp <= 0)
        {
            Destroy(gameObject);  //�Q�[���I�u�W�F�N�g���j�󂳂��
        }
    }
}