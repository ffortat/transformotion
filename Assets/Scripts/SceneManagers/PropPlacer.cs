using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PropPlacer : MonoBehaviour
{
    List<AssetReferenceGameObject> loadedProps = new List<AssetReferenceGameObject>();

    [SerializeField] Transform PropParent;

    public void PlaceProp(SpawnableReaction reaction)
    {
        if(loadedProps.Find(x => x.AssetGUID == reaction.propReference.AssetGUID) != null)
        {
            return;
        }

        loadedProps.Add(reaction.propReference);

        AsyncOperationHandle handle = reaction.propReference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;
    }

    private void Handle_Completed(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate((GameObject)handle.Result, PropParent);
        }
        else
        {
            Debug.LogError("AssetReference failed to load.");
        }
    }

    private void RemoveProp(SpawnableReaction previousReaction)
    {
        int referenceIndex = loadedProps.FindIndex(x => x.AssetGUID == previousReaction.propReference.AssetGUID);
        if(referenceIndex != -1)
        {
            AssetReferenceGameObject reference = loadedProps[referenceIndex];
            
        loadedProps.Remove(reference);
        if (reference != null)
        {
            reference.ReleaseAsset();
        }
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