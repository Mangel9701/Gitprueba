using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calculadora_1 : MonoBehaviour
{
    public GameObject[] ObjAGuardar;
    public GameObject[] precios;
    float suma;
    public Text tuscompras;
    public GameObject a;
    GameObject textos;
    public GameObject carta;

    void Update()
    {
        precios = GameObject.FindGameObjectsWithTag("precios");
        ObjAGuardar = GameObject.FindGameObjectsWithTag("Objeto");
    }

    public void calculado() {

        int N = precios.Length;

        for (int i = 0; i < N; i++)
        {

            suma += float.Parse(precios[i].GetComponent<Text>().text);

        }

        tuscompras.text = "Tus compras " + ": $" + suma.ToString();

    }

    public void encerado()
    {
        suma = 0;
    }
    
}




