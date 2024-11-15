using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    //���C���J�����ƃT�u�J�����̏�������
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera subCamera;

    // Start is called before the first frame update
    private void Start()
    {
        //�Q�[���J�n���̃T�u�J�������I�t�ɂ���
        subCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Enter�L�[���������Ƃ�
        if (Input.GetKeyDown(KeyCode.C))
        {
            //���C���J�����ƃT�u�J������؂�ւ���
            mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
            subCamera.gameObject.SetActive(!subCamera.gameObject.activeSelf);
        }
    }
}
