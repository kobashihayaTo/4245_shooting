using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public Transform player;  // ���@��Transform
    public float acceleration = 3f;  // �����x
    public float maxSpeed = 8f;  // �ő呬�x
    public float followDistance = 5f;  // �ǔ��J�n���鋗��

    private Vector3 velocity = Vector3.zero;  // �G�̌��݂̑��x
    private Vector3 previousPlayerPosition;  // ���@�̑O��̈ʒu
    private Vector3 moveDirection;  // �ǔ�����
    private float distanceTraveled;  // ���@�̈ړ���



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbody���擾 
        rb.velocity = velocity;

        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
        }
        previousPlayerPosition = player.position;
    }


    // Update is called once per frame
    void Update()
    {
        // ���@�̈ړ��ʂ��v�Z
        distanceTraveled = Vector3.Distance(player.position, previousPlayerPosition);

        // ���@�̈ړ���A�G���ǔ��J�n����
        if (Vector3.Distance(transform.position, player.position) < followDistance)
        {
            Debug.Log("�ǔ����Ă܂�");
            // �G�����@��ǔ�����������v�Z
            moveDirection = (player.position - transform.position).normalized;

            // �ǔ������Ɋ�Â��ĉ�����������
            velocity += moveDirection * acceleration * Time.deltaTime;

            // �ő呬�x�ɐ���
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            // ���@�������ɂ���ꍇ�͌���
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * acceleration);
        }

        // �G���ړ�
        transform.position += velocity * Time.deltaTime;

        // ���@�̈ʒu���X�V
        previousPlayerPosition = player.position;
    }
}
