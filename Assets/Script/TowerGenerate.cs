using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerate : MonoBehaviour
{
    public GameObject towerPrefab; // 配置するタワーのプレハブ
    public LayerMask placementLayer; // 配置可能エリアのレイヤーマスク
    public float gridSize = 1.0f; // グリッドサイズ（例: 1x1）

    public int gridWidth = 10; // グリッドの横方向のセル数
    public int gridHeight = 10; // グリッドの縦方向のセル数
    public Color gridColor = Color.green; // グリッド線の色
    public Vector3 gridOrigin = Vector3.zero;

    private Camera mainCamera;
    private GameObject previewTower; // 配置前のプレビュータワー
    private bool isGridVisible = true; // グリッド表示の切り替え

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - //

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlacementPreview();
        HandleTowerPlacement();
        ToggleGridVisibility();
    }

    // グリッドスナップ機能
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize; // Y軸を無視してXZ平面で揃える
        return new Vector3(x, position.y, z);
    }

    void HandlePlacementPreview()
    {
        if (towerPrefab == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            Vector3 snappedPosition = SnapToGrid(hit.point);

            if (previewTower == null)
            {
                previewTower = Instantiate(towerPrefab, snappedPosition, Quaternion.identity);
                previewTower.GetComponent<Collider>().enabled = false; // プレビュー時はコライダー無効
            }
            else
            {
                previewTower.transform.position = snappedPosition;
            }
        }
        else
        {
            if (previewTower != null)
            {
                Destroy(previewTower);
            }
        }
    }

    void HandleTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && previewTower != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
            {
                Vector3 snappedPosition = SnapToGrid(hit.point);
                Instantiate(towerPrefab, snappedPosition, Quaternion.identity); // タワーを配置
                Destroy(previewTower); // プレビューオブジェクトを破棄
                previewTower = null;
            }
        }
    }

    void ToggleGridVisibility()
    {
        // Gキーでグリッド表示のオン・オフを切り替え
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGridVisible = !isGridVisible;
        }
    }

    void OnDrawGizmos()
    {
        if (!isGridVisible) return;

        Gizmos.color = gridColor;

        // グリッドの開始位置
        Vector3 startPosition = gridOrigin;

        // 横方向の線を描画
        for (int i = 0; i <= gridHeight; i++)
        {
            float zPos = startPosition.z + i * gridSize;
            Vector3 start = new Vector3(startPosition.x, startPosition.y, zPos);
            Vector3 end = new Vector3(startPosition.x + gridWidth * gridSize, startPosition.y, zPos);
            Gizmos.DrawLine(start, end);
        }

        // 縦方向の線を描画
        for (int i = 0; i <= gridWidth; i++)
        {
            float xPos = startPosition.x + i * gridSize;
            Vector3 start = new Vector3(xPos, startPosition.y, startPosition.z);
            Vector3 end = new Vector3(xPos, startPosition.y, startPosition.z + gridHeight * gridSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
