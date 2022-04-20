using System.Collections;
using System.IO;
using UnityEngine;

public class screenshot : MonoBehaviour
{
    public GameObject [] UI_1;

    private IEnumerator ScreenShoot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        string name = "Screenshot_prior_precios" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        NativeGallery.SaveImageToGallery(texture, "PRIOR_pictures", name);

        Destroy(texture);

        foreach (GameObject a in UI_1) { a.SetActive(true); }

        // FLECHA.SetActive(true);


    }

    public void TakeScreenShot_list()
    {
        foreach (GameObject a in UI_1 ) { a.SetActive(false); }
        //FLECHA.SetActive(false);
        StartCoroutine(ScreenShoot());
    }
}
