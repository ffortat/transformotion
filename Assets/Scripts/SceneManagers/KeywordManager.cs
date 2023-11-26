using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordManager : MonoBehaviour
{
    [SerializeField]
    List<Keyword> keywords = new List<Keyword>();
    [SerializeField]
    List<Keyword> locationKeywords = new List<Keyword>();

    public int wordCountTotal = 10;
    int wordCount = 0;

    public List<Keyword> GetNextKeywords(bool hasLocation)
    {
        List<Keyword> result = new List<Keyword>();
        if(!hasLocation)
        {
            result = new List<Keyword>(locationKeywords);
            return result;
        }

        int wordCountToGive = 3; //UnityEngine.Random.Range(3, 5);
        for( int i = 0; i < wordCountToGive; i++ ) 
        {
            int pick = UnityEngine.Random.Range(0, keywords.Count);
            result.Add(keywords[pick]);
            keywords.RemoveAt(pick);
        }
        return result;
    }

    public bool IsDone()
    {
        return wordCount == wordCountTotal;
    }
}
