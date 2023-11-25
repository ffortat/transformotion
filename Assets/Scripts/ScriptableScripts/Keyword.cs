using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Keyword", menuName = "ScriptableObjects/Keyword", order = 1)]
public class Keyword : ScriptableObject
{
    public List<KeywordReaction> keywordReaction;
}