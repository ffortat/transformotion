using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PropPlacer propPlacer;
    [SerializeField] CameraChanger cameraChanger;
    [SerializeField] PostProcessController postProcessController;
    [SerializeField] KeywordManager keywordManager;


    List<Keyword> chosenKeywords = new List<Keyword>();

    KeywordContext currentKeywordContext;
    bool wasContextUpdated = false;

    private void Start()
    {
        StartCoroutine(WaitForNextKeyword());
    }

    IEnumerator WaitForNextKeyword(float waitTime = 5.0f)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        var keywords = keywordManager.GetNextKeywords(currentKeywordContext.currentLocation != null);
        KeywordSelected(keywords[UnityEngine.Random.Range(0, keywords.Count)]);
    }

    public void KeywordSelected(Keyword keywordSelected)
    {
        Debug.Log("Selected : " + keywordSelected.name);

        KeywordReaction reaction = keywordSelected.GetReaction(currentKeywordContext);
        ApplyReaction(reaction);

        /*
        if(wasContextUpdated)
        {
            foreach (var keyword in chosenKeywords)
            {
                UpdateReactions();
            }
            wasContextUpdated = false;
        }*/

        if(!keywordManager.IsDone())
        {
            StartCoroutine(WaitForNextKeyword());
        }
    }

    void UpdateReactions()
    {
        foreach (var keyword in chosenKeywords)
        {
            if(keyword.keywordReaction.Count == 0)
            {
                continue;
            }
            KeywordReaction reaction = keyword.GetReaction(currentKeywordContext);
            ApplyReaction(reaction);
        }
    }

    bool CheckKeywordValidityInContext(Keyword keyword)
    {
        return keyword.IsValidInContext(currentKeywordContext);
    }

    void ApplyReaction(KeywordReaction reaction)
    {
        if (reaction as LocationReaction != null)
        {
            currentKeywordContext.currentLocation = ((SpawnableReaction)reaction).propReference;
            wasContextUpdated = true;
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

    void Apply(SpawnableReaction reaction, SpawnableReaction previousReaction = null)
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
