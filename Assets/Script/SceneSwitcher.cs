using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitter : MonoBehaviour
{
    public bool IsMode = false; // false���z�u���[�h true���v���C���[�h

    // Start is called before the first frame update
    void Start()
    {
        IsMode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ModeChenge()
    {
        IsMode = true;
    }

    public bool GetterIsMode()
    {
        return IsMode;
    }

}
