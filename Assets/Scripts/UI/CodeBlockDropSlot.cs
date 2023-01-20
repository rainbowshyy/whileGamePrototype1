using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CodeBlockDropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int dropChildIndex;
    [SerializeField] private CodeBlockUIParent codeBlockUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CodeBlockUIElement>() != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().onDrop?.Invoke();
            codeBlockUI.PlaceBlock(dropChildIndex, eventData.pointerDrag);
        }
    }

    public void SetData(int dropChildIndexParam, CodeBlockUIParent codeBlockUIParam)
    {
        dropChildIndex = dropChildIndexParam;
        codeBlockUI = codeBlockUIParam;
    }
}
