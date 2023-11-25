using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "PropReaction", menuName = "ScriptableObjects/PropReaction")]
public class PropReaction : KeywordReaction
{
    public AssetReferenceGameObject propReference;
    public Vector3 position;
    public Quaternion rotation;
}
