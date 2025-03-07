using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionItem : MonoBehaviour, IDescriptionItem
{
    public DescriptionItemData data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }
}
