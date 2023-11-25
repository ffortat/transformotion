using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CameraReaction", menuName = "ScriptableObjects/CameraReaction")]
public class CameraReaction : KeywordReaction
{
    public float cameraFov = -1;
    public AssetReference skybox;
}
