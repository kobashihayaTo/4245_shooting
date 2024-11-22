using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody RB;
    //�G�̉����x
    public float moveHorizontal = 0f;
    //�G�̏c���x
    public float moveVertical = 0f;
    //�͈͂ɓ�������
    public float attackRange = 0.0f;
    public Transform Player_ally;
    public GameObject canonball;
    private int count = 0;
    //�̗�
    [SerializeField]
    private float hp = 5;  //�̗�
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        //�����Ŕ����Ă���
        RB.velocity = transform.forward * moveVertical;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RB.velocity = -transform.right * moveHorizontal;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RB.velocity = transform.right * moveHorizontal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player_ally"))
        {

            transform.LookAt(Player_ally);
            count++;

            //�����Ő�����ς��Ēe�̑ł��o�̕ύX
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, RB.rotation);
                //Debug.Log("�͈͂ɓ���܂���");
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        //�^�O��EnemyBullet�̃I�u�W�F�N�g��������������{}���̏������s����
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("hit Player");  //�R���\�[����hit Player���\��
            hp -= 1;
        }

        //�̗͂�0�ȉ��ɂȂ�����{}���̏������s����
        if (hp <= 0)
        {
            Destroy(gameObject);  //�Q�[���I�u�W�F�N�g���j�󂳂��
            //SceneManager.LoadScene("GameClear");  // �Q�[���N���A��ʂɈڍs����
        }
    }
}
