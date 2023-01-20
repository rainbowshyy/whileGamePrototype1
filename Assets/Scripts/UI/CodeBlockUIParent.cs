using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CodeBlockUIParent : MonoBehaviour
{
    public static Action<Inputs> onChange;

    [SerializeField] CodeBlockDropSlot dropSlot;
    [SerializeField] Inputs input;

    public void PlaceBlock(int index, GameObject block)
    {
        block.GetComponent<RectTransform>().SetParent(transform);
        block.GetComponent<RectTransform>().SetSiblingIndex(index);
        UpdateDropZones();
        block.GetComponent<CodeBlockUIElement>().UpdateText();
        block.GetComponent<RectTransform>().localScale = Vector3.one;
        CheckEmpty();
    }

    public void UpdateDropZones()
    {
        foreach (CodeBlockUIElement c in GetComponentsInChildren<CodeBlockUIElement>())
        {
            c.DestroyDropSlots();
            c.CreateDropSlots();
        }
    }

    public void CheckEmpty()
    {
        dropSlot.enabled = transform.childCount <= 0;
        onChange?.Invoke(input);
    }

    public List<CodeBlock> GetCode()
    {
        List<CodeBlock> codeCurrent = new();
        foreach (CodeBlockUIElement codeBlock in GetComponentsInChildren<CodeBlockUIElement>())
        {
            codeCurrent.Add(codeBlock.code);
        }
        return codeCurrent;
    }

    public void Clear()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
}
