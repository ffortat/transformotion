using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private TextMeshProUGUI keywordPrefab;
    
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = container.GetComponent<CanvasGroup>();
    }

    public void DisplayKeywords(List<Keyword> keywords, Action<Keyword> callback)
    {
        canvasGroup.alpha = 0;
        
        foreach (var keyword in keywords)
        {
            var keywordInstance = Instantiate(keywordPrefab, container);
            keywordInstance.text = keyword.keywordName;
            keywordInstance.GetComponent<Button>().onClick.AddListener(() =>
            {
                ClearKeywords();
                callback(keyword);
            });
        }

        canvasGroup.DOFade(1, 2.5f);
    }
    
    private void ClearKeywords()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
