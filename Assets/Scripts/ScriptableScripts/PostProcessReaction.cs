using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PostProcessReaction", menuName = "ScriptableObjects/PostProcessReaction", order = 1)]
public class PostProcessReaction : KeywordReaction
{
    public VolumeProfile profile;
}
