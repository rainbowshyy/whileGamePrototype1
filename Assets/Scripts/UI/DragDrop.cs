using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    public Action onBegin;
    public Action onEnd;
    public Action onDrop;

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBegin?.Invoke();
        rectTransform.anchoredPosition = eventData.position / UIManager.Instance.Canvas.scaleFactor;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / UIManager.Instance.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        onEnd?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
