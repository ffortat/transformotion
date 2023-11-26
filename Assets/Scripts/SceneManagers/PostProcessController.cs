using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume;

    public void ApplyProfile(PostProcessReaction reaction)
    {
        volume.profile = reaction.profile;
    }
}
