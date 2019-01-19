using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
	public void Pickup() {
		GetComponentInParent<MedkitSpawner>().StartSpawn();
		Destroy(gameObject);
	}
}
