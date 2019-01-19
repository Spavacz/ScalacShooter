using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public Transform playersContainer;
	public GameObject playerPrefab;

	public int numberOfPlayers = 4;
	public List<Vector3> spawnPoints;

	private void Start() {
		for (var i = 0; i < numberOfPlayers; i++) {
			SpawnPlayer(i);
		}
	}

	private void SpawnPlayer(int playerNumber) {
		var player = Instantiate(playerPrefab, playersContainer).GetComponent<PlayerController>();
		player.transform.localPosition = spawnPoints[playerNumber % spawnPoints.Count];
		player.SetPlayer(playerNumber);
	}
}