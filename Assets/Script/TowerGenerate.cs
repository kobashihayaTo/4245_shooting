using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerGenerate : MonoBehaviour
{
    public GameObject towerPrefab; // �z�u����^���[�̃v���n�u
    public LayerMask placementLayer; // �z�u�\�G���A�̃��C���[�}�X�N
    public float gridSize = 1.0f; // �O���b�h�T�C�Y�i��: 1x1�j

    public int gridWidth = 10; // �O���b�h�̉������̃Z����
    public int gridHeight = 10; // �O���b�h�̏c�����̃Z����
    public Color gridColor = Color.green; // �O���b�h���̐F
    public Vector3 gridOrigin = Vector3.zero;

    [SerializeField]
    private Camera mainCamera;

    private GameObject previewTower; // �z�u�O�̃v���r���[�^���[
    private bool isGridVisible = true; // �O���b�h�\���̐؂�ւ�

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
        Debug.Log("�����͂��Ă�");
    }

    // �X�N���[�����W�����[���h���W�ɕϊ�
    Vector3 ScreenToWorldPosition(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            return SnapToGrid(hit.point); // �q�b�g�����ʒu���X�i�b�v����
        }
        return Vector3.zero; // �q�b�g���Ȃ������ꍇ�͖���
    }

    // �O���b�h�X�i�b�v�@�\
    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        float y = 0.5f; // �����͂��̂܂܁i�n�ʂ̍������ێ��j
        return new Vector3(x, y, z);
    }

    void HandlePlacementPreview()
    {
        if (towerPrefab == null) return;

        // �X�N���[�����W���O���b�h�T�C�Y�ɃX�i�b�v����
        Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
        if (worldPosition == Vector3.zero) return;

        if (previewTower == null)
        {
            previewTower = Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            previewTower.GetComponent<Collider>().enabled = false; // �v���r���[���̓R���C�_�[����
        }
        else
        {
            previewTower.transform.position = worldPosition; // �v���r���[�^���[���ʒu�X�V
        }
    }

    void HandleTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && previewTower != null)
        {
            Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
            if (worldPosition == Vector3.zero) return;

            // �^���[��ݒu
            Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            Destroy(previewTower);

            // �V�����v���r���[�^���[���쐬
            previewTower = Instantiate(towerPrefab, worldPosition, Quaternion.identity);
            previewTower.GetComponent<Collider>().enabled = false; // �v���r���[�p�Ȃ̂ŃR���C�_�[�𖳌���
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
            if (col.gameObject.CompareTag("Tower")) // "Tower" �^�O������I�u�W�F�N�g���`�F�b�N
            {
                return false;
            }
        }
        return true;
    }
}
