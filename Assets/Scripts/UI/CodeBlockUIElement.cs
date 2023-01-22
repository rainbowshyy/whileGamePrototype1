using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(DragDrop))]
public class CodeBlockUIElement : MonoBehaviour
{
    private DragDrop dragDrop;

    [SerializeField] private GameObject dropSlotPrefab;
    [SerializeField] private Transform dropSlotParent;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spriteActive;

    public CodeBlock code;

    private bool placed;

    private void Awake()
    {
        dragDrop = GetComponent<DragDrop>();

        dragDrop.onBegin += Begin;
    }

    public void UpdateText()
    {
        text.text = code.ShowSyntax();
    }

    public void Begin()
    {
        DestroyDropSlots();
        if (placed)
        {
            placed = false;

            CodeBlockUIParent parent = transform.parent.gameObject.GetComponent<CodeBlockUIParent>();

            transform.SetParent(UIManager.Instance.Canvas.transform);
            dragDrop.rectTransform.anchorMin = Vector2.zero;
            dragDrop.rectTransform.anchorMax = Vector2.zero;

            parent.CheckEmpty();
            parent.UpdateDropZones();

            HighLight(false);
        }
    }

    public void CreateDropSlots()
    {
        //must be child of CodeBlockUIParent

        placed = true;

        var slot = Instantiate(dropSlotPrefab, dropSlotParent);
        slot.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
        slot.transform.localPosition = new Vector3(0f, 0f, 0f);
        slot.GetComponent<CodeBlockDropSlot>().SetData(transform.GetSiblingIndex(), transform.parent.gameObject.GetComponent<CodeBlockUIParent>());
        if (transform.GetSiblingIndex() <= 0)
        {
            slot.transform.localScale = new Vector3(1f, 2f, 1f);
        }


        slot = Instantiate(dropSlotPrefab, dropSlotParent);
        slot.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
        slot.transform.localPosition = new Vector3(0f, 0f, 0f);
        slot.GetComponent<CodeBlockDropSlot>().SetData(transform.GetSiblingIndex() + 1, transform.parent.gameObject.GetComponent<CodeBlockUIParent>());
        if (transform.GetSiblingIndex() >= transform.parent.childCount - 1)
        {
            slot.transform.localScale = new Vector3(1f, 2f, 1f);
        }
    }

    public void DestroyDropSlots()
    {
        foreach (Transform child in dropSlotParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void HighLight(bool active)
    {
        UpdateText();
        if (active)
        {
            image.sprite = spriteActive;
            text.text += " <color=#FF0000>(next)</color>";
        }
        else
        {
            image.sprite = spriteDefault;
        }
    }
}