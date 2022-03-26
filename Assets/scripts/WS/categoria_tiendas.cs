using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class categoria_tiendas : MonoBehaviour
{
    [Serializable]
    public struct Game
    {
        public string Name;
        public string IconUrl;

        public Sprite Icon;
        //public string Description;

        //public string ID;
        //public string Category;
    }

    [SerializeField] Sprite defaultIcon;

    public Game[] allGames;
    public string url;


    void Start()
    {
        

        StartCoroutine(GetGames());
    }

    //fabricacion de botones
    void DrawUI ()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        int N = allGames.Length;

        for (int i = 0; i < N; i++)
        {
            g = Instantiate(buttonTemplate, transform);

            //g.transform.GetChild(1).GetComponent<Text>().text = allGames[i].Description;
            g.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].Icon;
            g.transform.GetChild(2).GetComponent<Text>().text = allGames[i].Name;
        }

        Destroy(buttonTemplate);

    }

    //obtiene los links json
    IEnumerator GetGames()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.chunkedTransfer = false;
        yield return request.Send();

        if (request.isNetworkError) { Debug.Log("Network Error"); }

        else
        {
            if (request.isDone)
            {
                allGames = JsonHelper.GetArray<Game>(request.downloadHandler.text);

                StartCoroutine(GetGameIcones());
            }
        }
    }

    //obtiene los sprites y los dibujo en los botones
   IEnumerator GetGameIcones()
    {

     

        for (int i = 0; i < allGames.Length; i++)
        {
            WWW w = new WWW(allGames[i].IconUrl);
            yield return w;

            if (w.error != null)
            {
                allGames[i].Icon = defaultIcon;
            }
            else
            {
                if (w.isDone)
                {
                    Texture2D tx = w.texture;
                    allGames[i].Icon = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), Vector2.zero, 100f);
                }
            }
        }

        //Dibuja cada boton
        DrawUI();

    }


}
