using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    //�̗�
    [SerializeField]
    private float hp = 5;  //�̗�

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbody���擾 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-10.0f, 0.0f, 0.0f); // �l��ݒ�
                                                           //transform.position -= speed * transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(10.0f, 0.0f, 0.0f); // �l��ݒ�
                                                          //transform.position += speed * transform.right * Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider collision)
    {

        //�^�O��EnemyBullet�̃I�u�W�F�N�g��������������{ }
        //���̏������s����
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Enemy");  //�R���\�[����hit Player���\��
            hp -= 1;
        }

        //�̗͂�0�ȉ��ɂȂ�����{}���̏������s����
        if (hp <= 0)
        {
            Destroy(gameObject);  //�Q�[���I�u�W�F�N�g���j�󂳂��
            SceneManager.LoadScene("GameOver");  // �Q�[���N���A��ʂɈڍs����
        }

    }
}
