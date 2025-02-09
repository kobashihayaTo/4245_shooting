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

    [SerializeField]
    private SceneSwitter sceneSwitter;

    public int towerCount = 0;  //�z�u�ł���^���[�̐�

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

            //�ݒu���[�h��������
            if (towerCount > 0)
            {
                HandlePlacementPreview();
            }
            HandleTowerPlacement();
            HandleTowerRemoval();  // �폜������ǉ�
        }

        ToggleGridVisibility();
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

        // �ݒu�ł��邩�ǂ������`�F�b�N���A�v���r���[�^���[�̐F��ύX
        if (CanPlaceTower(worldPosition))
        {
            previewTower.GetComponent<Renderer>().material.color = Color.green;  // �ݒu�\�Ȃ��
        }
        else
        {
            previewTower.GetComponent<Renderer>().material.color = Color.red;    // �ݒu�s�Ȃ��
        }
    }

    void ChangePreviewTowerColor(Color color)
    {
        if (previewTower != null)
        {
            Renderer renderer = previewTower.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;  // �v���r���[�^���[�̐F��ύX
            }
        }
    }

    // ���N���b�N�Ń^���[��ݒu
    void HandleTowerPlacement()
    {
        if (towerCount > 0)
        {
            if (Input.GetMouseButtonDown(0) && previewTower != null)
            {
                Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
                if (worldPosition == Vector3.zero) return;

                // �ʒu�Ƀ^���[�����łɑ��݂��邩�`�F�b�N
                if (CanPlaceTower(worldPosition))
                {
                    // �^���[��ݒu
                    Instantiate(towerPrefab, worldPosition, Quaternion.identity);
                    Destroy(previewTower);

                    // �V�����v���r���[�^���[���쐬
                    previewTower = null;

                    towerCount--;
                }
                else
                {
                    // �^���[���ݒu�ł��Ȃ��ꍇ�͉������Ȃ��i�x�����b�Z�[�W��\������Ȃǁj
                    Debug.Log("���̈ʒu�ɂ̓^���[��ݒu�ł��܂���I");
                }
            }
        }
    }

    // �E�N���b�N�Ń^���[���폜
    void HandleTowerRemoval()
    {
        if (Input.GetMouseButtonDown(1))  // �E�N���b�N
        {
            Vector3 worldPosition = ScreenToWorldPosition(Input.mousePosition);
            if (worldPosition == Vector3.zero) return;

            // �}�E�X�ʒu�ɑ΂���Raycast���g���č폜�Ώۂ̃^���[A��������
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
            {
                // �q�b�g�����I�u�W�F�N�g��"Tower"�^�O�������Ă��邩�`�F�b�N
                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    GameObject targetTower = hit.collider.gameObject; // �N���b�N�����^���[A���^�[�Q�b�g
                    Destroy(targetTower);  // �^���[A���폜
                    towerCount++;  // �^���[�̃J�E���g�𑝂₷
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

    // �ʒu�Ƀ^���[���ݒu�ł��邩���m�F
    bool CanPlaceTower(Vector3 position)
    {
        // �͈͂����߂邽�߂�0.1f�ɕύX
        Collider[] colliders = Physics.OverlapSphere(position, gridSize * 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Tower")) // "Tower" �^�O������I�u�W�F�N�g�����łɂ���ꍇ
            {
                return false;  // ���łɃ^���[�����݂���̂Őݒu�ł��Ȃ�
            }
        }
        return true;  // �^���[���Ȃ��ꍇ�͐ݒu�\
    }


    //TowerCount�̃Q�b�^�[
    public int GetterTowerCount()
    {
        return towerCount;
    }
}
