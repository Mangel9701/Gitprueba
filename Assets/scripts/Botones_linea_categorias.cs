using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class Botones_linea_categorias : MonoBehaviour
{

    [Serializable]
    public struct Game
    {
        public string Name;
        public Sprite Icon;
        public string IconUrl;
        public string ID;
    }
    public int N;
    public GameObject[] g;

    public Game[] allGames;
    public GameObject[] URLs;

    [SerializeField] Sprite defaultIcon;

    private position_panel position_panel;
    float pos_x_panel;
    private int quantity = 4;
    private int updatedQuantity = 0;
    private int actualQuantity = 0;

    GameObject buttontemplate;

    void Start()
    {
        buttontemplate = transform.GetChild(0).gameObject;
        StartCoroutine(GetGames());
        position_panel = FindObjectOfType<position_panel>();
    }

    private void Update()
    {
        pos_x_panel = position_panel.posicionX;
        StartCoroutine(updateGamesIcones());
    }

    void DrawUI()
    {
        N = allGames.Length;
        g = new GameObject[N];

        for (int i = 0; i < N; i++)
        {
            g[i] = (GameObject)Instantiate(buttontemplate, transform);
            g[i].transform.GetChild(0).GetComponent<Text>().text = allGames[i].Name;
            g[i].transform.GetChild(1).GetComponent<Image>().sprite = allGames[i].Icon;
            g[i].transform.GetChild(2).GetComponent<Text>().text = allGames[i].ID;
            g[i].name = i.ToString();
        }
        Destroy(buttontemplate);
    }

    IEnumerator GetGames()
    {
        string url = "https://prueba-ra.s3.us-west-1.amazonaws.com/botones_category.json";   
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
        for (int i=0;i < quantity; i++)
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

    IEnumerator updateGamesIcones()
    {
        double val = ( pos_x_panel - 431.37 ) * -1;

        int cantidad = (int)(val / 95);

        updatedQuantity = quantity + cantidad ;


        if (updatedQuantity > allGames.Length)
        {
            updatedQuantity = allGames.Length;
        }

        if (updatedQuantity > actualQuantity & cantidad != 0)
        {
            actualQuantity = updatedQuantity;
            for (int i = quantity; i < updatedQuantity; i++)
            {
                if (allGames[i].Icon == null)
                {
                    
                    WWW w = new WWW(allGames[i].IconUrl);
                    yield return w;

                    if (w.error != null)
                    {
                        allGames[i].Icon = defaultIcon;
                        Debug.Log(w.error);
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
            }
        }
        
        AddToUI(actualQuantity);

    }

    void AddToUI(int actualQuantity)
    {
        for (int i = 0; i < actualQuantity; i++)
        {
            g[i].transform.GetChild(1).GetComponent<Image>().sprite = allGames[i].Icon;
        }

    }
}
