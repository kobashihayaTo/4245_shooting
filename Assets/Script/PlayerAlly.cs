using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : MonoBehaviour
{

    public float attackRange = 0.0f;

    public Transform Enemy;
    public GameObject canonball;
    private int count = 0;

<<<<<<< Updated upstream:Assets/Script/PlayerAlly.cs
    //体力
    [SerializeField]
    private float hp = 5;  //体力

=======
    private GameObject CurrentPlacingTower;
    [SerializeField] private Camera PlayerCamera;

    //[SerializeField]
    //private CircleCollider2D circleCollider2D;
>>>>>>> Stashed changes:Assets/Script/Player_ally.cs

    // Start is called before the first frame update
    void Start()
    {
        //circleCollider2D.radius = attackRange;
        GetComponent<SphereCollider>().radius = attackRange;

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPlacingTower != null)
        {
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(camray, out HitInfo, 100f, PlacementCollideMask))
            {
                CurrentPlacingTower.transform.position = HitInfo.point;
            }

            if (Input.GetMouseButtonDown(0) && HitInfo.collider.gameObject != null)
            {
                BoxCollider TowerCollider = CurrentPlacingTower.gameObject.GetComponent<BoxCollider>();
                TowerCollider.isTrigger = true;

                Vector3 BoxCenter = CurrentPlacingTower.gameObject.transform.position + TowerCollider.center;
                Vector3 HalfExtents = TowerCollider.size / 2;
                if (!Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity, PlacementCheckMask, QueryTriggerInteraction.Ignore))
                {
                    TowerCollider.isTrigger = false;
                    CurrentPlacingTower = null;
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            transform.LookAt(Enemy);
            count++;

            //ここで数字を変えて弾の打つ感覚の変更
            if (count % 10 == 0)
            {
                Instantiate(canonball, transform.position, Quaternion.identity);
                //Debug.Log("範囲に入りました");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("範囲から出ました");
        }
    }

<<<<<<< Updated upstream:Assets/Script/PlayerAlly.cs
    private void OnTriggerEnter(Collider collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{}内の処理が行われる
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Player");  //コンソールにhit Playerが表示
            hp -= 1;
        }

        //体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);  //ゲームオブジェクトが破壊される
        }
    }
=======
    public void SetTowerPlace(GameObject tower)
    {
        CurrentPlactingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }

>>>>>>> Stashed changes:Assets/Script/Player_ally.cs
}
