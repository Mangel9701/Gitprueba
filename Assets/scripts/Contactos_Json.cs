using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class Contactos_Json : MonoBehaviour
{

    [Serializable]
    public struct Game
    {
        public string Tipo;
        public Sprite Icon;
        public string IconUrl;
        public string Color;
    }
    public int N = 10;
    public GameObject g;
    public Game[] allGames;

    [SerializeField] Sprite defaultIcon;

    void Start()
    {
        StartCoroutine(GetGames()); 
    }

    void DrawUI()
    {
        GameObject buttontemplate = transform.GetChild(0).gameObject;

        N = allGames.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttontemplate, transform);

            g.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].Icon;//"Game "+i 
            g.transform.GetChild(1).GetComponent<Text>().text = allGames[i].Color;//"Game "+i 
            g.transform.GetChild(2).GetComponent<Text>().text = allGames[i].Tipo;

            g.name = i.ToString();
        }
        Destroy(buttontemplate); 
    }

    IEnumerator GetGames()
    {
        string url = "https://prueba-ra.s3.us-west-1.amazonaws.com/botones_texto.json ";   
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.chunkedTransfer = false;
        yield return request.Send();

        if (request.isNetworkError) { }

        else
        {
            if (request.isDone)
            {
                allGames = JsonHelper.GetArray<Game>(request.downloadHandler.text);
                
                StartCoroutine(GetGameIcones());
            }
        }
    }

    IEnumerator GetGameIcones()
    {
        for (int i=0;i < allGames.Length; i++)
        {
            WWW w = new WWW(allGames[i].IconUrl);
            yield return w;

            if(w.error != null){
                allGames[i].Icon = defaultIcon;
            } else {
                if (w.isDone)
                {
                    Texture2D tx = w.texture;
                    allGames[i].Icon = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero, 100f);
 
                }
            }
        }

        DrawUI();
        
    }
}