using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public void Destroy()
    {
        GameObject[] Objetos = GameObject.FindGameObjectsWithTag("Objeto");


        foreach (GameObject Objeto in Objetos)
        GameObject.Destroy(Objeto);
    }
  

 }

    
     

