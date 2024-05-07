using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureScriptableObject", menuName = "ScriptbleObjects/Creatures")]
public class CreatureScriptableObject : ScriptableObject
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public GameObject Prefab;
	[field: SerializeField] public int Speed;
}
