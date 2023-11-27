using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CircleLayout : UIBehaviour, ILayoutGroup
{
    [SerializeField]
    private float radius = 100f;
    
    private float TAU = 2f * Mathf.PI;
    
    private RectTransform rectTransform;
    private Dictionary<RectTransform, Coroutine> childrenPosition;
    private DrivenRectTransformTracker rectTracker;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        childrenPosition = new Dictionary<RectTransform, Coroutine>();
    }

    public void SetLayoutHorizontal()
    {
        rectTracker.Clear();

        if (!rectTransform)
        {
            Awake();
        }
        
        foreach (var (rect, coroutine) in childrenPosition)
        {
            StopCoroutine(coroutine);
        }
        childrenPosition.Clear();

        float angle = Random.Range(0f, TAU);
        float stepAngle = TAU / rectTransform.childCount;
        
        foreach (RectTransform rectChild in rectTransform)
        {
            rectTracker.Add(this, rectChild, DrivenTransformProperties.SizeDelta | DrivenTransformProperties.AnchoredPosition);
            
            var preferredWidth = LayoutUtility.GetPreferredWidth(rectChild);
            rectChild.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            var preferredHeight = LayoutUtility.GetPreferredHeight(rectChild);
            rectChild.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
            
            childrenPosition.Add(rectChild, StartCoroutine(RotateRect(rectChild, angle)));

            angle += stepAngle;
        }
    }

    private void PositionChild(RectTransform rect, float angle)
    {
        if (rect)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Rect rectangle = new Rect(x, y, rect.rect.width, rect.rect.height);

            rect.localPosition = rectangle.center;
        }
    }
    
    IEnumerator RotateRect(RectTransform rect, float angle)
    {
        float localAngle = angle;
        while (true)
        {
            float angleDelta = TAU * Time.deltaTime / 150;
            
            PositionChild(rect, localAngle);
            
            yield return null;
            
            localAngle += angleDelta;
        }
    }

    public void SetLayoutVertical()
    {
        //Layout is done on the first pass SetLayoutHorizontal
    }
}
