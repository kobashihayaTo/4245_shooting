using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountControl : MonoBehaviour
{
    // UI TextéwíËóp
    public Text TextFrame;
    // ï\é¶Ç∑ÇÈïœêî
    private int frame;
    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TextFrame.text = string.Format("{0:00000} frame", frame);
        frame++;
    }
}
