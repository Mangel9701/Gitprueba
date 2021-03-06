using System.Collections;
using System.IO;
using UnityEngine;

public class screenshot1 : MonoBehaviour
{
    public GameObject UI1;

    private IEnumerator ScreenShoot1()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        string name = "Screenshot_prior" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        NativeGallery.SaveImageToGallery(texture, "PRIOR_pictures", name);

        Destroy(texture);

        UI1.SetActive(true);


    }

    public void TakeScreenShot()
    {
        UI1.SetActive(false);
        StartCoroutine(ScreenShoot1());
    }
}
