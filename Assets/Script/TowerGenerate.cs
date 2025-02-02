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

    private SceneSwitter sceneSwitter;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandlePlacementPreview();
        HandleTowerPlacement();
        ToggleGridVisibility();
        //CanPlaceTower();
        Debug.Log("反応はしてる");
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
    }

    void HandleTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && previewTower != null)
        {
            Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
            if (worldPosition == Vector3.zero) return;

            // タワーを設置
            Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            Destroy(previewTower);

            // 新しくプレビュータワーを作成
            previewTower = Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            previewTower.GetComponent<Collider>().enabled = false; // プレビュー用なのでコライダーを無効化
            //previewTower = null;
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

    bool CanPlaceTower(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, gridSize * 0.4f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Tower")) // "Tower" タグがあるオブジェクトをチェック
            {
                return false;
            }
        }
        return true;
    }
}
