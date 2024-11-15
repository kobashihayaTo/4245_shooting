using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    //メインカメラとサブカメラの情報をそれぞれ変数に入れる
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera subCamera;

    // Start is called before the first frame update
    private void Start()
    {
        //ゲーム開始時はサブカメラをオフにしておく
        subCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Enterキーを押したとき
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //メインカメラとサブカメラを切り替える
            mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
            subCamera.gameObject.SetActive(!subCamera.gameObject.activeSelf);
        }
    }
}
