using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class lista_objetos : MonoBehaviour
{
    public GameObject[] ObjAGuardar;
    public GameObject[] precio;
    public GameObject a;

    
    GameObject textos;
    int N;

    void Update()
    {
        //con_colliders

        

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

            a.GetComponent<Text>().text = ObjAGuardar[i].name + ":    " + precio[i].GetComponent<Text>().text;

        }


        textos.SetActive(false);

    }

    public void vaciar()
    {
      /* foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }*/
        for (int i = 0; i < N; i++)
        {
            GameObject.Destroy(textos.gameObject);

        }





    }
}

    
