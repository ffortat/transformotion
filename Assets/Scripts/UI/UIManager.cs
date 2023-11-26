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
    [SerializeField] CanvasGroup resetGroup;
    [SerializeField] CanvasGroup warningGroup;

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

    public void ShowReset()
    {
        resetGroup.DOFade(1, 1).SetEase(Ease.InQuart).OnComplete(
            () =>
            {
                resetGroup.interactable = true;
                resetGroup.blocksRaycasts = true;
            });
    }

    public void ShowWarning()
    {
        warningGroup.DOFade(1, 0.5f).SetEase(Ease.InQuart).OnComplete(
            () =>
            {
                warningGroup.interactable = true;
                warningGroup.blocksRaycasts = true;
            });
    }

    public void HideWarning()
    {
        warningGroup.interactable = false;
        warningGroup.blocksRaycasts = false;
        warningGroup.DOFade(0, 0.5f).SetEase(Ease.InQuart);
    }
}
