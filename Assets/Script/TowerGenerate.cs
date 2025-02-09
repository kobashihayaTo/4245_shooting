using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerGenerate : MonoBehaviour
{
    public GameObject towerPrefab; // 配置するタワーのプレハブ
    public LayerMask placementLayer; // 配置可能エリアのレイヤーマスク
    public float gridSize = 1.0f; // グリッドサイズ（例: 1x1）

    public int gridWidth = 10; // グリッドの横方向のセル数
    public int gridHeight = 10; // グリッドの縦方向のセル数
    public Color gridColor = Color.green; // グリッド線の色
    public Vector3 gridOrigin = Vector3.zero;

    [SerializeField]
    private Camera mainCamera;

    private GameObject previewTower; // 配置前のプレビュータワー
    private bool isGridVisible = true; // グリッド表示の切り替え

    [SerializeField]
    private SceneSwitter sceneSwitter;

    public int towerCount = 0;  //配置できるタワーの数

    void Start()
    {
        mainCamera = Camera.main;
        Debug.Log(mainCamera == null ? "Main Camera is null" : "Main Camera is set.");

        Debug.Log("TowerPrefab: " + (towerPrefab != null ? "OK" : "null"));
        if (towerPrefab == null)
        {
            Debug.LogError("Tower prefab is not assigned!");
        }
    }

    void Update()
    {
        if (sceneSwitter.GetterIsMode() == false)
        {

            //設置モードだったら
            if (towerCount > 0)
            {
                HandlePlacementPreview();
            }
            HandleTowerPlacement();
            HandleTowerRemoval();  // 削除処理を追加
        }

        ToggleGridVisibility();
    }

    // スクリーン座標をワールド座標に変換
    Vector3 ScreenToWorldPosition(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            return SnapToGrid(hit.point); // ヒットした位置をスナップ処理
        }
        return Vector3.zero; // ヒットしなかった場合は無視
    }

    // グリッドスナップ機能
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        float y = 0.5f; // 高さはそのまま（地面の高さを維持）
        return new Vector3(x, y, z);
    }

    void HandlePlacementPreview()
    {
        if (towerPrefab == null) return;

        // スクリーン座標をグリッドサイズにスナップする
        Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
        if (worldPosition == Vector3.zero) return;

        if (previewTower == null)
        {
            previewTower = Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            previewTower.GetComponent<Collider>().enabled = false; // プレビュー時はコライダー無効
        }
        else
        {
            previewTower.transform.position = worldPosition; // プレビュータワーを位置更新
        }

        // 設置できるかどうかをチェックし、プレビュータワーの色を変更
        if (CanPlaceTower(worldPosition))
        {
            previewTower.GetComponent<Renderer>().material.color = Color.green;  // 設置可能なら緑
        }
        else
        {
            previewTower.GetComponent<Renderer>().material.color = Color.red;    // 設置不可なら赤
        }
    }

    void ChangePreviewTowerColor(Color color)
    {
        if (previewTower != null)
        {
            Renderer renderer = previewTower.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;  // プレビュータワーの色を変更
            }
        }
    }

    // 左クリックでタワーを設置
    void HandleTowerPlacement()
    {
        if (towerCount > 0)
        {
            if (Input.GetMouseButtonDown(0) && previewTower != null)
            {
                Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
                if (worldPosition == Vector3.zero) return;

                // 位置にタワーがすでに存在するかチェック
                if (CanPlaceTower(worldPosition))
                {
                    // タワーを設置
                    Instantiate(towerPrefab, worldPosition, Quaternion.identity);
                    Destroy(previewTower);

                    // 新しくプレビュータワーを作成
                    previewTower = null;

                    towerCount--;
                }
                else
                {
                    // タワーが設置できない場合は何もしない（警告メッセージを表示するなど）
                    Debug.Log("この位置にはタワーを設置できません！");
                }
            }
        }
    }

    // 右クリックでタワーを削除
    void HandleTowerRemoval()
    {
        if (Input.GetMouseButtonDown(1))  // 右クリック
        {
            Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
            if (worldPosition == Vector3.zero) return;

            // マウス位置に対してRaycastを使って削除対象のタワーAを見つける
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
            {
                // ヒットしたオブジェクトが"Tower"タグを持っているかチェック
                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    GameObject targetTower = hit.collider.gameObject; // クリックしたタワーAをターゲット
                    Destroy(targetTower);  // タワーAを削除
                    towerCount++;  // タワーのカウントを増やす
                }
            }
        }
    }



    void ToggleGridVisibility()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGridVisible = !isGridVisible;
        }
    }

    void OnDrawGizmos()
    {
        if (!isGridVisible) return;

        Gizmos.color = gridColor;
        Vector3 startPosition = gridOrigin;

        for (int i = 0; i <= gridHeight; i++)
        {
            float zPos = startPosition.z + i * gridSize;
            Vector3 start = new Vector3(startPosition.x, startPosition.y, zPos);
            Vector3 end = new Vector3(startPosition.x + gridWidth * gridSize, startPosition.y, zPos);
            Gizmos.DrawLine(start, end);
        }

        for (int i = 0; i <= gridWidth; i++)
        {
            float xPos = startPosition.x + i * gridSize;
            Vector3 start = new Vector3(xPos, startPosition.y, startPosition.z);
            Vector3 end = new Vector3(xPos, startPosition.y, startPosition.z + gridHeight * gridSize);
            Gizmos.DrawLine(start, end);
        }
    }

    // 位置にタワーが設置できるかを確認
    bool CanPlaceTower(Vector3 position)
    {
        // 範囲を狭めるために0.1fに変更
        Collider[] colliders = Physics.OverlapSphere(position, gridSize * 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Tower")) // "Tower" タグがあるオブジェクトがすでにある場合
            {
                return false;  // すでにタワーが存在するので設置できない
            }
        }
        return true;  // タワーがない場合は設置可能
    }


    //TowerCountのゲッター
    public int GetterTowerCount()
    {
        return towerCount;
    }
}
