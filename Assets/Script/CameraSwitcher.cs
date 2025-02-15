using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    //メインカメラとサブカメラの情報を入れる
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera subCamera;
    [SerializeField] private SceneSwitter sceneSwitter;

    // Start is called before the first frame update
    private void Start()
    {
        //ゲーム開始時のサブカメラをオフにする
        mainCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // モードが切り替わった時
        if (sceneSwitter.GetterIsMode() == true)
        {
            //メインカメラとサブカメラを切り替える
            //mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
            mainCamera.gameObject.SetActive(false);
            subCamera.gameObject.SetActive(true);
        }
        else if (sceneSwitter.GetterIsMode() == false)
        {
            //subCamera.gameObject.SetActive(!subCamera.gameObject.activeSelf);
            subCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }
    }
}
