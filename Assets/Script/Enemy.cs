using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Rigidbody RB;
    public float moveHorizontal = 0f;   // �G�̉����x
    public float moveVertical = 0f;     // �G�̏c���x
    public float attackRange = 0.0f;    // �͈͂ɓ�������
    public int wave = 0;                // �E�F�[�u��

    // ��ԋ߂��I�u�W�F�N�g
    private GameObject nearObj;
    private GameObject playerObj;
    public GameObject canonball;
    private int count = 0;
    Transform playerTr; // �v���C���[��Transform

    [SerializeField] private float hp = 5; // �̗�
    [SerializeField] private float maxHp = 5; // �ő�HP���C���X�y�N�^�[�Őݒ�\��
    private bool flag = false;
    // �V�[���̐؂�ւ��p
    [SerializeField] private SceneSwitter sceneSwitter;
    // �ړ��͈̔͂��邽�߂ɕK�v
    [SerializeField] private Transform enemy_pos;
    [SerializeField] private int interval = 10;
    [SerializeField] private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().radius = attackRange;

        // �v���C���[��Transform���擾�i�v���C���[�̃^�O��Player�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        // HP�Q�[�W�̏�����
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (sceneSwitter.GetterIsMode() == true)
        {
            // �����Ŕ����Ă���
            RB.velocity = transform.forward * moveVertical;
            if (enemy_pos.position.x >= -4.0f)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    RB.velocity = transform.right * moveHorizontal;
                }
            }
            if (enemy_pos.position.x <= 4.0f)
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    RB.velocity = -transform.right * moveHorizontal;
                }
            }

            if (flag == false)
            {
                playerObj = SerchTag(gameObject, "Player");
                transform.LookAt(playerObj.transform);
            }
        }
    }

    // HP�Q�[�W�̍X�V���\�b�h��ǉ�
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = hp / maxHp; // �ő�HP���g�p
            Debug.Log("HP�Q�[�W�X�V: " + healthBar.fillAmount); // �f�o�b�O���b�Z�[�W
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (sceneSwitter.IsMode == true)
        {
            if (other.CompareTag("Tower"))
            {
                Debug.Log("�ʂ��Ă邪��");
                flag = true;
                // �߂���player_ally�̕������擾��������
                nearObj = SerchTag(gameObject, "Tower");
                // ��ԋ߂���player_ally�̕���������
                transform.LookAt(nearObj.transform);

                if (SerchTag(gameObject, "Tower"))
                {
                    Debug.Log("�͈͂ɓ���܂���");
                    count++;
                    // �����Ő�����ς��Ēe�̑łԊu�̕ύX
                    if (count % interval == 0)
                    {
                        Instantiate(canonball, transform.position, RB.rotation);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // �^�O��EnemyBullet�̃I�u�W�F�N�g��������������{ }
        // ���̏������s����
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("hit Player");  //�R���\�[����hit Player���\��
            hp -= 1;
            UpdateHealthBar(); // HP�Q�[�W���X�V
        }

        //�̗͂�0�ȉ��ɂȂ�����{}���̏������s����
        if (hp <= 0)
        {
            Destroy(gameObject);  //�Q�[���I�u�W�F�N�g���j�󂳂��

            switch (wave)
            {
                case 1:
                    SceneManager.LoadScene("GameScene_Wave2");  // wave2�V�[���Ɉڍs����
                    break;
                case 2:
                    SceneManager.LoadScene("GameScene_Wave3");  // wave3�V�[����ʂɈڍs����
                    break;
                case 3:
                    SceneManager.LoadScene("GameScene_Wave4");  // wave4�V�[����ʂɈڍs����
                    break;
                case 4:
                    SceneManager.LoadScene("GameScene_Wave5");  // wave5�V�[����ʂɈڍs����
                    break;
                case 5:
                    SceneManager.LoadScene("GameClear");  // �Q�[���N���A��ʂɈڍs����
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        flag = false;

        Debug.Log("�͈͂���o�܂���");
        // transform���擾
        Transform myTransform = this.transform;
        // ���[���h���W����ɁA��]���擾
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f;                    // ���[���h���W����ɁAx�������ɂ�����]��10�x�ɕύX
        worldAngle.y = 180.0f;                  // ���[���h���W����ɁAy�������ɂ�����]��10�x�ɕύX
        worldAngle.z = 0.0f;                    // ���[���h���W����ɁAz�������ɂ�����]��10�x�ɕύX
        myTransform.eulerAngles = worldAngle;   // ��]�p�x��ݒ�
    }

    // �w�肳�ꂽ�^�O�̒��ōł��߂����̂��擾
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;               // �����p�ꎞ�ϐ�
        float nearDis = 0;              // �ł��߂��I�u�W�F�N�g�̋���
        //string nearObjName = "";      //�I�u�W�F�N�g����
        GameObject targetObj = null;    //�I�u�W�F�N�g

        // �^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
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
