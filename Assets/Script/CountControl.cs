using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountControl : MonoBehaviour
{
    // 表示する変数
    [SerializeField] private TowerGenerate towerGene;
    [SerializeField] private Text remainingCountText; // UIのTextコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        UpdateRemainingCount(); // 初期表示を更新
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRemainingCount(); // 常に残り数を更新
    }

    // 残り配置数を更新するメソッド
    public void UpdateRemainingCount()
    {
        int remainingCount = towerGene.GetterTowerCount();
        remainingCountText.text = "残り配置数: " + remainingCount.ToString();
    }
}
