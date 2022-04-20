using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class lista_objetos : MonoBehaviour
{
    public GameObject[] ObjAGuardar;
    public GameObject[]precio;
    public GameObject a;

    
    GameObject textos;
    int N;

    void Update()
    {
 

    }

    public void lista_compras()
    {
        ObjAGuardar = GameObject.FindGameObjectsWithTag("Objeto");
        precio = GameObject.FindGameObjectsWithTag("precios");

        textos = transform.GetChild(0).gameObject;
        N = ObjAGuardar.Length;


        for (int i = 0; i < N; i++)
        {
            Debug.Log("listo");
            a = Instantiate(textos, transform);
            a.tag = "lista_objetos";
            a.GetComponent<Text>().text = ObjAGuardar[i].name + ":     $" + precio[i].GetComponent<Text>().text;

        }


        textos.SetActive(false);

    }

    public void vaciar()
    {

        GameObject[] lista = GameObject.FindGameObjectsWithTag("lista_objetos");

        foreach (GameObject a in lista)
        {
            GameObject.Destroy(a);
        }

        textos.SetActive(true);


    }
}

    
