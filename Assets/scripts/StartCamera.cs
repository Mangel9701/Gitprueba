using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.ReplayKit;

public class StartCamera : MonoBehaviour
{
 
    public void Initialise()
    {
        ReplayKitManager.Initialise();
    }
    public void StartRecording()
    {
        
        ReplayKitManager.StartRecording();
        print("grabando");
    }

    public void StopRecording()
    {
        ReplayKitManager.StopRecording();
        print("stop");

    }

    public void Tiempo()
    {
        StartCoroutine(Timer());
    }
    
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void Tiempo2()
    {
        StartCoroutine(Timer2());
    }

    public void SavePreview() //Saves preview to gallery
    {
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

    public IEnumerator Timer2()
    {
        yield return new WaitForSeconds(5);
    }


}