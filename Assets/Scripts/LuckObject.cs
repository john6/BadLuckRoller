using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckObject : MonoBehaviour
{
    [SerializeField] private int luckValue;
    [SerializeField] private string actionText;

    protected void AlterLuck()
    {
        GameManager.instance.AlterLuck(luckValue, GetMessage());
    }

    public string GetMessage()
    {
        return actionText + ": luck" + (luckValue > 0 ? "+" : "-") + Mathf.Abs(luckValue);
    }
}
