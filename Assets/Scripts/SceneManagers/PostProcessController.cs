using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] VolumeProfile profile;
    public PostProcessReaction TestPP;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ApplyProfile(TestPP);
        }
    }

    void ApplyProfile(PostProcessReaction reaction)
    {
        ColorAdjustments colorAdjustments;
        profile.TryGet<ColorAdjustments>(out colorAdjustments);

        colorAdjustments.saturation.value = reaction.saturationValue;
    }
}
