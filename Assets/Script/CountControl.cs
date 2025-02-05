using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountControl : MonoBehaviour
{
    // �\������ϐ�
    [SerializeField] private TowerGenerate towerGene;
    [SerializeField] private Text remainingCountText; // UI��Text�R���|�[�l���g

    // Start is called before the first frame update
    void Start()
    {
        UpdateRemainingCount(); // �����\�����X�V
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRemainingCount(); // ��Ɏc�萔���X�V
    }

    // �c��z�u�����X�V���郁�\�b�h
    public void UpdateRemainingCount()
    {
        int remainingCount = towerGene.GetterTowerCount();
        remainingCountText.text = "�c��z�u��: " + remainingCount.ToString();
    }
}
