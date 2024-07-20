using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreatures : MonoBehaviour
{
    public List<CreatureScriptableObject> creaturesTypes = new List<CreatureScriptableObject>();
	[SerializeField] List<CreatureScriptableObject> exsistingCreatures = new List<CreatureScriptableObject>();
	[SerializeField] int maxCreatures;
    [SerializeField] int step;
    [SerializeField] Transform spawnPos;

	private void Start()
	{
		//creaturesTypes = Connection.GetCreatures();
		StartCoroutine(LoadCreaturesOnStart());
	}

	private void OnEnable()
	{
        EventBus.Instance.CheackScore += CheakCurrentCount;
	}
	private void OnDisable()
	{
		EventBus.Instance.CheackScore -= CheakCurrentCount;
	}

	public void CheakCurrentCount()
    {
        if (exsistingCreatures.Count < maxCreatures && creaturesTypes.Count > 0)
        {
			while ((Score.CurrentScore - step * exsistingCreatures.Count) >= step && exsistingCreatures.Count < maxCreatures)
			{
                SpawnCreature(creaturesTypes[Random.Range(0, creaturesTypes.Count)]);
			}
		}
        else
        {
			//EventBus.Instance.CheackScore -= CheakCurrentCount;
		}
        
    }

    private void SpawnCreature(CreatureScriptableObject creature)
    {
        Instantiate(creature.Prefab, new Vector3(spawnPos.position.x + Random.Range(-1f, 1f), spawnPos.position.y + Random.Range(-1f, 1f), 0), Quaternion.identity);
		exsistingCreatures.Add(creature);
        Debug.Log("Spawn " + creature.Name);

	}

	private IEnumerator LoadCreaturesOnStart()
	{
		yield return new WaitForSeconds(3f);
		EventBus.Instance.CheackScore.Invoke();
	}
}
