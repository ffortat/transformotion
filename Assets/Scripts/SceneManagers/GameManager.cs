using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PropPlacer propPlacer;
    [SerializeField] CameraChanger cameraChanger;
    [SerializeField] PostProcessController postProcessController;

    KeywordContext currentKeywordContext;

    public void KeywordSelected(Keyword keywordSelected)
    {
        if(currentKeywordContext.IsInitialized())
        {
            if (!CheckKeywordValidityInContext(keywordSelected))
            {
                Debug.LogError("Invalid keyword in current context. No reaction found.");
                return;
            }
        }

        KeywordReaction reaction = keywordSelected.GetReaction(currentKeywordContext);
        if( reaction as LocationReaction != null)
        {
            currentKeywordContext.currentLocation = ((SpawnableReaction)reaction).propReference;
            Apply((SpawnableReaction)reaction);
        }
        if (reaction as PropReaction != null)
        {
            Apply((SpawnableReaction)reaction);
        }
        else if (reaction as CameraReaction != null)
        {
            Apply((CameraReaction)reaction);
        }
        else if (reaction as PostProcessReaction != null)
        {
            Apply((PostProcessReaction)reaction);
        }
    }

    bool CheckKeywordValidityInContext(Keyword keyword)
    {
        return keyword.IsValidInContext(currentKeywordContext);
    }

    void Apply(SpawnableReaction reaction)
    {
        if(propPlacer != null)
        {
            propPlacer.PlaceProp(reaction);
        }
    }

    void Apply(CameraReaction reaction)
    {
        if (cameraChanger != null)
        {
            cameraChanger.Apply(reaction);
        }
    }
    void Apply(PostProcessReaction reaction)
    {
        if (postProcessController != null)
        {
            postProcessController.ApplyProfile(reaction);
        }
    }
}
