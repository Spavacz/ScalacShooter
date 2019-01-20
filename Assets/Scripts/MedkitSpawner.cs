using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour {
	public GameObject medkitPrefab;

	public List<Vector3> positions;

	private void Start() {
		Spawn();
	}

	public void StartSpawn() {
		StartCoroutine(WaitAndSpawn());
	}

	public IEnumerator WaitAndSpawn() {
		yield return new WaitForSeconds(5f);
		Spawn();
	}

	private void Spawn() {
		var pos = positions[Random.Range(0, positions.Count)];
		var medkit = Instantiate(medkitPrefab, pos, Quaternion.identity);
		medkit.transform.parent = transform;
	}

	private void OnDrawGizmos() {
		positions.ForEach(pos => { Gizmos.DrawWireSphere(pos, 0.3f); });
	}
}