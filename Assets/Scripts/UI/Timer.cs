using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] Transform tf;

    private void Update()
    {
        VisualizeTimer(GameManager.Instance.TimeUntilTick / GameManager.Instance.TimeEachTick);
    }

    public void VisualizeTimer(float timeNormalized)
    {
        tf.localScale = new Vector3(Mathf.Round(timeNormalized * 157f) / 157f, 1f, 1f);
    }
}
