using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class CapturarPantalla2 : MonoBehaviour
{
    public Camera camara;
    RenderTexture renderTex;
    public GameObject imagen;
    Texture2D textura;
    Image previewImage;
    int oldMask;

    void Start()
    {
       camara = camara.GetComponent<Camera>();
       oldMask = camara.cullingMask;
    }

    public void CapturarPant()
    {
        camara.cullingMask = 1 << 0;
        StartCoroutine(Captura());

    }

    //capturar pantalla
    private IEnumerator Captura()
    {
        
        yield return new WaitForEndOfFrame();

        //crear nueva textura y mandar render de la camara a la nueva textura
        renderTex = new RenderTexture(Screen.width, Screen.height, 24);
        camara.targetTexture = renderTex;
        RenderTexture.active = renderTex;
        camara.Render();

        textura = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        textura.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        textura.Apply();

        //asignar la textura a un nuevo sprite
        Image DefaultPreview = imagen.GetComponent<Image>();
        var sprite = Sprite.Create(textura, new Rect(0.0f, 0.0f, textura.width, textura.height), new Vector2(0.5f, 0.5f), 100.0f);

        DefaultPreview.sprite = sprite;

        //vovler a renderizar la camara a la pantalla
        RenderTexture.active = null;
        camara.targetTexture = null;

        //volver a activar todos los filtros de la camara
        camara.cullingMask = oldMask;

    }

    

    
    
}
