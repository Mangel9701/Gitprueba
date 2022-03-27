using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.ReplayKit;

public class Recorder : MonoBehaviour
{
   public Camera camara;
    int oldMask;

    void Start()
    {
        camara = camara.GetComponent<Camera>();
        oldMask = camara.cullingMask;
    }

    public void Initialise()
    {
        camara.cullingMask = 1 << 0;
        ReplayKitManager.Initialise();
    }
    public void StartRecording()
    {
        //camara.cullingMask = 1 << 0;
        ReplayKitManager.StartRecording();
        print("grabando");
    }

    public void StopRecording()
    {
        ReplayKitManager.StopRecording();
        print("stop");
        camara.cullingMask = oldMask;

    }


    public void SavePreview() //Saves preview to gallery
    {
        StartCoroutine(Time());
    }

   


    public IEnumerator Time()
    {
        yield return  new WaitForSeconds(3);

        if (ReplayKitManager.IsPreviewAvailable())
        {
            ReplayKitManager.SavePreview((error) =>
            {
                Debug.Log("Saved preview to gallery with error : " + ((error == null) ? "null" : error));
            });
            string path = ReplayKitManager.GetPreviewFilePath();
            Debug.Log(path);
        }
        else
        {
            Debug.LogError("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
        }
    }

}
