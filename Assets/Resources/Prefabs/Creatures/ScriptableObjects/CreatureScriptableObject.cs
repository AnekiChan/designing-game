using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureScriptableObject")]
public class CreatureScriptableObject : ScriptableObject
{
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public GameObject Prefab;
	[field: SerializeField] public int Speed;
}
