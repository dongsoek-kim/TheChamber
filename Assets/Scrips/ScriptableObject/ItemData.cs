using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Handleable,
    KeyCard
}

[CreateAssetMenu(fileName = "item", menuName = "New item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public GameObject dropPrefab;


    [Header("KeyCard")]
    public Keycard index;

    [Header("Handleable")]
    public GameObject handlePrefab;

    [Header("Ghost")]
    public GameObject ghostPrefab;
}
