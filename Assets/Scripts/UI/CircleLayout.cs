using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CircleLayout : UIBehaviour, ILayoutGroup
{
    [SerializeField]
    private float radius = 100f;
    
    private RectTransform rectTransform;
    private DrivenRectTransformTracker rectTracker;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetLayoutHorizontal()
    {
        rectTracker.Clear();

        if (!rectTransform)
        {
            Awake();
        }

        float TAU = 2f * Mathf.PI;
        float angle = Random.Range(0f, TAU);
        float stepAngle = TAU / rectTransform.childCount;
        
        foreach (RectTransform rectChild in rectTransform)
        {
            rectTracker.Add(this, rectChild, DrivenTransformProperties.SizeDelta | DrivenTransformProperties.AnchoredPosition);
            
            var preferredWidth = LayoutUtility.GetPreferredWidth(rectChild);
            rectChild.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            var preferredHeight = LayoutUtility.GetPreferredHeight(rectChild);
            rectChild.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
            
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Rect rectangle = new Rect(x, y, rectChild.rect.width, rectChild.rect.height);

            rectChild.localPosition = rectangle.center;

            angle += stepAngle;
        }
    }

    public void SetLayoutVertical()
    {
        //Layout is done on the first pass SetLayoutHorizontal
    }
}
