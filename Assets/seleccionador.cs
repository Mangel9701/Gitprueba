using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seleccionador : MonoBehaviour
{
    public GameObject uno;
    public GameObject dos;
    public GameObject tres;

    GameObject [] objetos;

    // Start is called before the first frame update
    void Start()
    {
        objetos[0] = uno;
        objetos[1] = dos;
        objetos[2] = tres;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseOver()
    {
        
    }
}
