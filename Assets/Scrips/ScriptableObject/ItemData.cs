using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Handleable,
    Consumable,
}

[Serializable]
public class ItemDataConsumable
{
    public float value;
}

[CreateAssetMenu(fileName = "item", menuName = "New item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;


    [Header("Consumable")]
    public ItemDataConsumable oilfill;

    [Header("Handleable")]
    public GameObject handlePrefab;

    [Header("Ghost")]
    public GameObject ghostPrefab;
}
