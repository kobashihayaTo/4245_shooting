using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody RB;
    // 敵の横速度
    public float moveHorizontal = 0f;
    // 敵の縦速度
    public float moveVertical = 0f;
    // 範囲に入ったら
    public float attackRange = 0.0f;
    // ウェーブ数
    public int wave = 0;

    //一番近いオブジェクト
    private GameObject nearObj;
    private GameObject playerObj;
    public GameObject canonball;
    private int count = 0;
    Transform playerTr; // プレイヤーのTransform

    [SerializeField] private float hp = 5; // 体力
    private bool flag = false;
    // シーンの切り替え用
    [SerializeField] private SceneSwitter sceneSwitter;
    // 移動の範囲するために必要
    [SerializeField] private Transform enemy_pos;
    [SerializeField]private int interval = 10;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().radius = attackRange;

        // プレイヤーのTransformを取得（プレイヤーのタグをPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //// プレイヤーとの距離が0.1f未満になったらそれ以上実行しない
        //if (Vector3.Distance(transform.position, playerTr.position) < 0.1f)
        //    return;
        Move();
    }
    private void Move()
    {
        if (sceneSwitter.GetterIsMode() == true)
        {
            //自動で迫ってくる
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
                //// プレイヤーに向けて進む
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

        if (sceneSwitter.IsMode == true)
        {

            if (other.CompareTag("Tower"))
            {
                Debug.Log("通ってるがな");
                flag = true;
                //近くのplayer_allyの方向を取得し続ける
                nearObj = SerchTag(gameObject, "Tower");
                //一番近くのplayer_allyの方向を向く
                transform.LookAt(nearObj.transform);

                if (SerchTag(gameObject, "Tower"))
                {
                    Debug.Log("範囲に入りました");
                    count++;
                    //ここで数字を変えて弾の打つ間隔の変更
                    if (count % interval == 0)
                    {
                        Instantiate(canonball, transform.position, RB.rotation);
                        //
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{ }
        //内の処理が行われる
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("hit Player");  //コンソールにhit Playerが表示
            hp -= 1;
        }

        //体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);  //ゲームオブジェクトが破壊される

            switch (wave)
            {
                case 1:

                    SceneManager.LoadScene("GameScene_Wave2");  // wave2シーンに移行する

                    break;

                case 2:

                    SceneManager.LoadScene("GameScene_Wave3");  // wave3シーン画面に移行する

                    break;

                case 3:

                    SceneManager.LoadScene("GameScene_Wave4");  // wave4シーン画面に移行する

                    break;

                case 4:

                    SceneManager.LoadScene("GameScene_Wave5");  // wave5シーン画面に移行する

                    break;

                case 5:

                    SceneManager.LoadScene("GameClear");  // ゲームクリア画面に移行する

                    break;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        flag = false;

        Debug.Log("範囲から出ました");
        // transformを取得
        Transform myTransform = this.transform;
        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = myTransform.eulerAngles;
        worldAngle.x = 0.0f; // ワールド座標を基準に、x軸を軸にした回転を10度に変更
        worldAngle.y = 180.0f; // ワールド座標を基準に、y軸を軸にした回転を10度に変更
        worldAngle.z = 0.0f; // ワールド座標を基準に、z軸を軸にした回転を10度に変更
        myTransform.eulerAngles = worldAngle; // 回転角度を設定
    }

    //指定されたタグの中で最も近いものを取得
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
