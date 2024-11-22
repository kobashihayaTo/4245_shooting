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
    //��ԋ߂��I�u�W�F�N�g
    private GameObject nearObj;
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
        if (GameObject.FindGameObjectWithTag("Player_ally"))
        {
            //�߂���player_ally�̕������擾��������
            nearObj = SerchTag(gameObject, "Player_ally");
        }

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
        //��ԋ߂���player_ally�̕���������
        transform.LookAt(nearObj.transform);
        if (SerchTag(gameObject,"Player_ally" ))
        {
           
            count++;

            //�����Ő�����ς��Ēe�̑ł��o�̕ύX
            if (count % 600 == 0)
            {
                Instantiate(canonball, transform.position, RB.rotation);
                //Debug.Log("�͈͂ɓ���܂���");
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        //�^�O��EnemyBullet�̃I�u�W�F�N�g��������������{ }
        //���̏������s����
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

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player_ally"))
        {
            Debug.Log("�͈͂���o�܂���");
        }
    }

    //�w�肳�ꂽ�^�O�̒��ōł��߂����̂��擾
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //�����p�ꎞ�ϐ�
        float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
        //string nearObjName = "";    //�I�u�W�F�N�g����
        GameObject targetObj = null; //�I�u�W�F�N�g

        //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
            //�ꎞ�ϐ��ɋ������i�[
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //�ł��߂������I�u�W�F�N�g��Ԃ�
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
