using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public Transform player;  // 自機のTransform
    public float acceleration = 3f;  // 加速度
    public float maxSpeed = 8f;  // 最大速度
    public float followDistance = 5f;  // 追尾開始する距離

    private Vector3 velocity = Vector3.zero;  // 敵の現在の速度
    private Vector3 previousPlayerPosition;  // 自機の前回の位置
    private Vector3 moveDirection;  // 追尾方向
    private float distanceTraveled;  // 自機の移動量



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();  // rigidbodyを取得 
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
        // 自機の移動量を計算
        distanceTraveled = Vector3.Distance(player.position, previousPlayerPosition);

        // 自機の移動後、敵が追尾開始する
        if (Vector3.Distance(transform.position, player.position) < followDistance)
        {
            Debug.Log("追尾してます");
            // 敵が自機を追尾する方向を計算
            moveDirection = (player.position - transform.position).normalized;

            // 追尾方向に基づいて加速を加える
            velocity += moveDirection * acceleration * Time.deltaTime;

            // 最大速度に制限
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            // 自機が遠くにいる場合は減速
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * acceleration);
        }

        // 敵を移動
        transform.position += velocity * Time.deltaTime;

        // 自機の位置を更新
        previousPlayerPosition = player.position;
    }
}
