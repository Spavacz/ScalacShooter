using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyControler : MonoBehaviour {
	private void Update() {
		if (Input.GetButtonDown("Fire1Player1")) {
			JoinPlayer(0);
		} else if (Input.GetButtonDown("Fire1Player2")) {
			JoinPlayer(1);
		} else if (Input.GetButtonDown("Fire1Player3")) {
			JoinPlayer(2);
		} else if (Input.GetButtonDown("Fire1Player4")) {
			JoinPlayer(3);
		}

		StartGame();
	}

	private void JoinPlayer(int inputNumber) {
		GameState.i.AddPlayerController(inputNumber);
	}

	private static void StartGame() {
		if (GameState.i.IsAllPlayersReady())
			SceneManager.LoadScene("SampleScene");
	}
}