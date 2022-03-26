using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.ReplayKit;

public class recorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ReplayKitManager.IsRecordingAPIAvailable())
            ReplayKitManager.Initialise();
    }

    public void Grabar()
    {
        ReplayKitManager.StartRecording();
    }

    public void Parar()
    {
        ReplayKitManager.StopRecording();
        
    }

    public void Guardar() //Saves preview to gallery
    {
        if (ReplayKitManager.IsPreviewAvailable())
        {
            ReplayKitManager.SavePreview((error) =>
            {
                Debug.Log("Saved preview to gallery with error : " + ((error == null) ? "null" : error));
            });
        }
        else
        {
            Debug.LogError("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
        }
    }



}
