using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;


public class DescargarModelo : MonoBehaviour
{
    //string Nombre;
    public float progresoDescarga;
    public GameObject modelo = null;
    public string url;
    public AssetBundle bundle1;


    public void Descargar()
    {
        url = this.transform.GetChild(3).GetComponent<Text>().text;
        //Nombre = this.transform.GetChild(5).GetComponent<Text>().text;
        StartCoroutine(downloadObject());
    }


    public IEnumerator downloadObject()
    {
        UnityWebRequest www1 = UnityWebRequestAssetBundle.GetAssetBundle(url);
        var operation = www1.SendWebRequest();

        while (!operation.isDone)
        {
            progresoDescarga = www1.downloadProgress * 100;
            
            yield return null;
        }


       if (operation.isDone)
        {
            bundle1 = DownloadHandlerAssetBundle.GetContent(www1);
            //obtener nombre del asset 
            string rootAssetPath = bundle1.GetAllAssetNames()[0];
            GameObject arObject = bundle1.LoadAsset(rootAssetPath) as GameObject;
            modelo = arObject;
            
        }
  
    }



}



