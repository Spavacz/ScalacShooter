using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour {
	public GameObject medkitPrefab;

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
		var medkit =Instantiate(medkitPrefab, transform.position, Quaternion.identity);
		medkit.transform.parent = transform;
	}
}