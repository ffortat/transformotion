using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] Camera _camera;
    AssetReference currentSkybox;

    public void Apply(CameraReaction reaction)
    {
        if(reaction.cameraFov >0)
        {
            _camera.fieldOfView = reaction.cameraFov;
        }
        if(reaction.skybox != null)
        {
            if(currentSkybox != null)
            {
                currentSkybox.ReleaseAsset();
            }
            currentSkybox = reaction.skybox;


            AsyncOperationHandle handle = currentSkybox.LoadAssetAsync<Material>();
            handle.Completed += Handle_Completed;
        }
    }

    private void Handle_Completed(AsyncOperationHandle handle)
    {
        if(handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
        {
            _camera.GetComponent<Skybox>().material = (Material)currentSkybox.Asset;
        }
    }

    private void OnDestroy()
    {
        if(currentSkybox != null)
        {
            currentSkybox.ReleaseAsset();
        }
    }
}
