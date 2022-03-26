using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class Botones_linea : MonoBehaviour
{

    [Serializable]
    public struct Game
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public string IconUrl;
        public string ID;
        public string Category;
    }
    private string url = "https://prueba-ra.s3.us-west-1.amazonaws.com/botones_din.json";

    public int N = 10;
    public GameObject[] g;

    public Game[] allGames;
    public GameObject[] URLs;

    [SerializeField] Sprite defaultIcon;

    private position_panel position_panel;
    float pos_y_panel;
    private int quantity = 4;
    private int updatedQuantity = 0;
    private int actualQuantity = 0;

    GameObject buttontemplate;

    public List<GameObject> Models;
    private int[] alreadyDone;



    private MultipleObjectPlacement MultipleObjectPlacement;
    public GameObject modelo = null;

    void Start()
    {
        buttontemplate = transform.GetChild(0).gameObject;
        StartCoroutine(GetGames());
        position_panel = FindObjectOfType<position_panel>();
        MultipleObjectPlacement = FindObjectOfType<MultipleObjectPlacement>();
    }

    private void Update()
    {
        //pos_y_panel = position_panel.posicionY;
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
            g[i].transform.GetChild(1).GetComponent<Text>().text = allGames[i].Description;//URL
            g[i].transform.GetChild(2).GetComponent<Image>().sprite = allGames[i].Icon;
            g[i].transform.GetChild(3).GetComponent<Text>().text = allGames[i].ID;
            g[i].transform.GetChild(4).GetComponent<Text>().text = i + "";
            

            g[i].name = i.ToString();
            
        }

        Destroy(buttontemplate);

    }

    IEnumerator GetGames()
    {
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

        alreadyDone = new int[allGames.Length];

        for (int i = 0; i < quantity; i++)
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
        DrawUI();

    }

    IEnumerator updateGamesIcones()
    {
        int cantidad = (int)(pos_y_panel / 186);
        updatedQuantity = quantity + (cantidad * 2);
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
            g[i].transform.GetChild(2).GetComponent<Image>().sprite = allGames[i].Icon;
        }

    }
}

