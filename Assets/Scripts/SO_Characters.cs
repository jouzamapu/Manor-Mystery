using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
public class SO_Characters : ScriptableObject
{
    [SerializeField] Sprite characterSprite;
    public Sprite CharacterSprite => characterSprite;
    [SerializeField] string characterName;
    public string CharacterName => characterName;
}
