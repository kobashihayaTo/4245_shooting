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
    private GameObject playerObj;
    public GameObject canonball;
    private int count = 0;
    //
    Transform playerTr; // �v���C���[��Transform

    //�̗�
    [SerializeField]
    private float hp = 5;  //�̗�

    private bool flag = false;

    [SerializeField] private SceneSwitter sceneSwitter;

    //public float meter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().radius = attackRange;

        // �v���C���[��Transform���擾�i�v���C���[�̃^�O��Player�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //// �v���C���[�Ƃ̋�����0.1f�����ɂȂ����炻��ȏ���s���Ȃ�
        //if (Vector3.Distance(transform.position, playerTr.position) < 0.1f)
        //    return;
        Move();
    }
    private void Move()
    {
        if (sceneSwitter.GetterIsMode() == true)
        {
            //�����Ŕ����Ă���
            RB.velocity = transform.forward * moveVertical;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                RB.velocity = transform.right * moveHorizontal;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                RB.velocity = -transform.right * moveHorizontal;
            }
            if (flag == false)
            {
                //// �v���C���[�Ɍ����Đi��
                //transform.position = Vector3.MoveTowards(
                //    transform.position,
                //    new Vector3(playerTr.position.x, playerTr.position.z),
                //    moveVertical * Time.deltaTime);
                playerObj = SerchTag(gameObject, "Player");

                transform.LookAt(playerObj.transform);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player_ally"))
        {
            flag = true;
            //�߂���player_ally�̕������擾��������
            nearObj = SerchTag(gameObject, "Player_ally");
            //��ԋ߂���player_ally�̕���������
            transform.LookAt(nearObj.transform);

            if (SerchTag(gameObject, "Player_ally"))
            {
                Debug.Log("�͈͂ɓ���܂���");
                count++;
                //�����Ő�����ς��Ēe�̑ł��o�̕ύX
                if (count % 10 == 0)
                {
                    Instantiate(canonball, transform.position, RB.rotation);
                    //
                }
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

    private void OnTriggerExit(Collider other)
    {
        flag= false;

        Debug.Log("�͈͂���o�܂���");
        // transform���擾
        Transform myTransform = this.transform;
        // ���[���h���W����ɁA��]���擾
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f; // ���[���h���W����ɁAx�������ɂ�����]��10�x�ɕύX
        worldAngle.y = 180.0f; // ���[���h���W����ɁAy�������ɂ�����]��10�x�ɕύX
        worldAngle.z = 0.0f; // ���[���h���W����ɁAz�������ɂ�����]��10�x�ɕύX
        myTransform.eulerAngles = worldAngle; // ��]�p�x��ݒ�
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
