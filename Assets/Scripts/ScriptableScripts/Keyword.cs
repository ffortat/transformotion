using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Keyword", menuName = "ScriptableObjects/Keyword", order = 1)]
public class Keyword : ScriptableObject
{
    public List<KeywordReaction> keywordReaction;
    public KeywordReaction defaultReaction;

    public bool IsValidInContext(KeywordContext contextToMatch)
    {
        if (keywordReaction.Count == 0) return true; //No need to check conditions when no conditional reaction

        bool isValid = false;
        foreach (KeywordReaction reaction in keywordReaction)
        {
            if(reaction.context.currentLocation == null || reaction.context.Equals(contextToMatch))
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }

    public KeywordReaction GetReaction(KeywordContext context)
    {
        for (int i = 0; i < keywordReaction.Count; i++)
        {
            if (keywordReaction[i].context.Equals(context))
            {
                return keywordReaction[i];
            }
        }

        return defaultReaction;
    }
}