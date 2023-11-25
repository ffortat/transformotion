using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class KeywordReaction : ScriptableObject
{
    public KeywordContext context;
}
[System.Serializable]
public struct KeywordContext
{
    public AssetReference currentLocation;

    public override bool Equals(object obj)
    {
        return obj is KeywordContext context && context.currentLocation.AssetGUID == currentLocation.AssetGUID;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(currentLocation);
    }
    public bool IsInitialized()
    {
        return currentLocation != null;
    }
}
