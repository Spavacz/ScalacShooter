using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour {
	[System.Serializable]
	public struct PlayerHero {
		public string heroName;
		public Sprite heroImage;
		public int playerNumber;
	}

	public static GameState i;

	public List<PlayerHero> ReadyPlayers;

	private readonly int[] playerInputControllers = {-1, -1, -1, -1};

	private void Awake() {
		if (i == null) {
			i = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void AddPlayerController(int inputControllerNumber) {
		var playerNumber = GetEmptyPlayerNumber();
		if (playerNumber >= 0 && !playerInputControllers.Contains(inputControllerNumber)) {
			playerInputControllers[playerNumber] = inputControllerNumber;
		}
	}

	public void RemovePlayerController(int playerNumber) {
		playerInputControllers[playerNumber] = -1;
	}

	private int GetEmptyPlayerNumber() {
		for (var playerNumber = 0; playerNumber < 4; playerNumber++) {
			if (playerInputControllers[playerNumber] == -1) {
				return playerNumber;
			}
		}

		return -1;
	}

	public bool IsAllPlayersReady() {
		return ReadyPlayers.Count > 1 && ReadyPlayers.Count == GetPlayersCount();
	}

	public int GetPlayersCount() {
		return playerInputControllers.Count(c => c != -1);
	}

	public int GetPlayerInputNumber(int playerNumber) {
		return playerInputControllers[playerNumber];
	}


	public void addHero(HeroList.HeroData hero, int playerNumber) {
		PlayerHero newHero;
		newHero.heroName = hero.name;
		newHero.heroImage = hero.heroImage;
		newHero.playerNumber = playerNumber;

		ReadyPlayers.Add(newHero);
	}

	public void removeHero(int playerNumber) {
		var index = ReadyPlayers.FindIndex(h => h.playerNumber.Equals(playerNumber));
		if (index >= 0) {
			ReadyPlayers.RemoveAt(index);
		}
	}
}