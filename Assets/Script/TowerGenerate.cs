using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerate : MonoBehaviour
{
    public GameObject towerPrefab; // �z�u����^���[�̃v���n�u
    public LayerMask placementLayer; // �z�u�\�G���A�̃��C���[�}�X�N
    public float gridSize = 1.0f; // �O���b�h�T�C�Y�i��: 1x1�j

    public int gridWidth = 10; // �O���b�h�̉������̃Z����
    public int gridHeight = 10; // �O���b�h�̏c�����̃Z����
    public Color gridColor = Color.green; // �O���b�h���̐F
    public Vector3 gridOrigin = Vector3.zero;

    private Camera mainCamera;
    private GameObject previewTower; // �z�u�O�̃v���r���[�^���[
    private bool isGridVisible = true; // �O���b�h�\���̐؂�ւ�

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

    // �O���b�h�X�i�b�v�@�\
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize; // Y���𖳎�����XZ���ʂő�����
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
                previewTower.GetComponent<Collider>().enabled = false; // �v���r���[���̓R���C�_�[����
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
                Instantiate(towerPrefab, snappedPosition, Quaternion.identity); // �^���[��z�u
                Destroy(previewTower); // �v���r���[�I�u�W�F�N�g��j��
                previewTower = null;
            }
        }
    }

    void ToggleGridVisibility()
    {
        // G�L�[�ŃO���b�h�\���̃I���E�I�t��؂�ւ�
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGridVisible = !isGridVisible;
        }
    }

    void OnDrawGizmos()
    {
        if (!isGridVisible) return;

        Gizmos.color = gridColor;

        // �O���b�h�̊J�n�ʒu
        Vector3 startPosition = gridOrigin;

        // �������̐���`��
        for (int i = 0; i <= gridHeight; i++)
        {
            float zPos = startPosition.z + i * gridSize;
            Vector3 start = new Vector3(startPosition.x, startPosition.y, zPos);
            Vector3 end = new Vector3(startPosition.x + gridWidth * gridSize, startPosition.y, zPos);
            Gizmos.DrawLine(start, end);
        }

        // �c�����̐���`��
        for (int i = 0; i <= gridWidth; i++)
        {
            float xPos = startPosition.x + i * gridSize;
            Vector3 start = new Vector3(xPos, startPosition.y, startPosition.z);
            Vector3 end = new Vector3(xPos, startPosition.y, startPosition.z + gridHeight * gridSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
