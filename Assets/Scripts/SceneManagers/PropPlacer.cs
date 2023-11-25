using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PropPlacer : MonoBehaviour
{
    List<AssetReferenceGameObject> loadedProps = new List<AssetReferenceGameObject>();

    public void PlaceProp(SpawnableReaction reaction)
    {
        loadedProps.Add(reaction.propReference);

        AsyncOperationHandle handle = reaction.propReference.LoadAssetAsync<GameObject>();
        handle.Completed += (AsyncOperationHandle x) => { Handle_Completed(x, reaction.position, reaction.rotation); };
    }

    private void Handle_Completed(AsyncOperationHandle handle, Vector3 position, Quaternion rotation)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate((GameObject)handle.Result, position, rotation);
        }
        else
        {
            Debug.LogError("AssetReference failed to load.");
        }
    }

    private void OnDestroy()
    {
        foreach (AssetReferenceGameObject obj in loadedProps)
        {
            obj.ReleaseAsset();
        }
    }
}