using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class KeywordReaction : ScriptableObject
{
    KeywordContext context;

    public void CheckContext()
    {

    }
}

struct KeywordContext
{
    AssetReference currentLocation;
}
