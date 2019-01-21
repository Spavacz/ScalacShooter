using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public static int[] score = new int[4];
	
	public Transform playersContainer;
	public GameObject playerPrefab;
	public GameObject playerSpawnerPrefab;

	public List<Vector3> spawnPoints;

	private void Start() {
		var numberOfPlayers = GameState.i.ReadyPlayers.Count;
		for (var i = 0; i < numberOfPlayers; i++) {
			CreatePlayerSpawner(i);
		}
	}

	private void CreatePlayerSpawner(int playerNumber) {
		var spawner = Instantiate(playerSpawnerPrefab, playersContainer).GetComponent<PlayerSpawner>();
		spawner.transform.position = spawnPoints[playerNumber];
		spawner.playerNumber = playerNumber;
	}
	
	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		spawnPoints.ForEach(pos => { Gizmos.DrawWireSphere(pos, 0.3f); });
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("Lobby");
		}
	}
}