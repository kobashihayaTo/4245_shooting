using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 3.0f;

    private Animator animator = null;
    [SerializeField] private SceneSwitter scene;
    [SerializeField] private Transform player_pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // rigidbody���擾
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        // �A�j���[�V�����p�t���O�Ǘ�
        if (scene.IsMode == true)
        {
            animator.SetBool("IsActive", true);
        }
        else
        {
            animator.SetBool("IsActive", false);
        }

        if (player_pos.position.x >= -4.0f)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                Debug.Log("���@�����͂��Ă�");
                transform.position -= speed * transform.right * Time.deltaTime;
            }
        }
        if (player_pos.position.x <= 4.0f)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                Debug.Log("�E�@�����͂��Ă�");
                transform.position += speed * transform.right * Time.deltaTime;
            }
        }
        Debug.Log("velo:" + player_pos.position.x);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // �^�O��EnemyBullet�̃I�u�W�F�N�g��������������{ }
        // ���̏������s����
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");  // �Q�[���N���A��ʂɈڍs����
        }
    }
}
