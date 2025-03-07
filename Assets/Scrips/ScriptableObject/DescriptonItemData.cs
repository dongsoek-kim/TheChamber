using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Descriptionitem", menuName = "New Descriptionitem")]
public class DescriptionItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
}

