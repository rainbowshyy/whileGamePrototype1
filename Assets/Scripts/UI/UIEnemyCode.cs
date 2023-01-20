using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEnemyCode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void PlaceOnScreen(Vector3 pos, Vector2 offset)
    {
        GetComponent<RectTransform>().anchoredPosition = (Vector2)Camera.main.WorldToScreenPoint(pos) / UIManager.Instance.Canvas.scaleFactor + offset;
    }
}
