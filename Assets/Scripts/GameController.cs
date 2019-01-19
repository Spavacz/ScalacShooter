using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public Transform playersContainer;
	public GameObject playerPrefab;
	public GameObject playerSpawnerPrefab;

	public List<Vector3> spawnPoints;

	private void Start() {
		var numberOfPlayers = GameState.i.Players.Count;
		for (var i = 0; i < numberOfPlayers; i++) {
			CreatePlayerSpawner(i);
		}
	}

	private void CreatePlayerSpawner(int playerNumber) {
		var spawner = Instantiate(playerSpawnerPrefab, playersContainer).GetComponent<PlayerSpawner>();
		spawner.transform.position = spawnPoints[playerNumber];
		spawner.playerNumber = playerNumber;
	}
	
//	private void SpawnPlayer(int playerNumber) {
//		var player = Instantiate(playerPrefab, playersContainer).GetComponent<PlayerController>();
//		player.transform.localPosition = spawnPoints[playerNumber % spawnPoints.Count];
//		player.SetPlayer(playerNumber);
//	}
}