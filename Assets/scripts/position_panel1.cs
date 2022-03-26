using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class position_panel1 : MonoBehaviour
{
    //public GameObject panel;
    public RectTransform rect;
    

    public float posicionY;

    public float posicionX;

    public float posicionY_1;

    public float posicionX_1;

    void Update()
    {
        posicionY = rect.anchoredPosition.y;
        posicionY_1 = rect.anchoredPosition.y;
        //posicionX = rect2.anchoredPosition.x;
       // posicionX_1 = rect4.anchoredPosition.x;

        //Debug.Log(posicionY +  " " + posicionX);
    }
}
