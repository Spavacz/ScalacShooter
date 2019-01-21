using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LobbyPlayer : MonoBehaviour {
	public int playerNumber;
	public HeroList HeroList;
	public int chosenHero;
	public TextMeshProUGUI heroName;
	public Image heroImage;

	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";
	private const string INPUT_CONFIRM = "Fire1Player";
	private const string INPUT_BACK = "Fire2Player";

	private string InputConfirm => INPUT_CONFIRM + (inputNumber + 1);
	private string InputBack => INPUT_BACK + (inputNumber + 1);
	private string InputHorizontal => INPUT_HORIZONTAL + (inputNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (inputNumber + 1);

	private bool playerChanged;

	private enum PlayerState {
		NO_PLAYER,
		JOINED,
		READY
	}

	private PlayerState playerState;

	private int inputNumber = -1;

	private void Awake() {
		heroName.text = "";
		heroImage.color = Color.clear;
	}

	private void Start() {
		if (GameState.i.ReadyPlayers.Count > 0) {
			var heroesName = GameState.i.ReadyPlayers.Find(p => p.playerNumber == playerNumber).heroName;
			if (heroesName != null) {
				chosenHero = HeroList.Heroes.FindIndex(h => h.name.Equals(heroesName));
			}			
			GameState.i.ReadyPlayers = new List<GameState.PlayerHero>();
		}
	}

	private void Update() {
		inputNumber = GameState.i.GetPlayerInputNumber(playerNumber);
		if (inputNumber != -1) {
			UpdateState();
		}
	}

	private void UpdateState() {
		switch (playerState) {
			case PlayerState.NO_PLAYER:
				JoinPlayer();
				break;
			case PlayerState.JOINED when Input.GetButtonDown(InputBack):
				LeavePlayer();
				break;
			case PlayerState.JOINED when Input.GetButtonDown(InputConfirm):
				ReadyPlayer();
				break;
			case PlayerState.JOINED:
				ChooseHero();
				break;
			case PlayerState.READY when Input.GetButtonDown(InputBack):
				NotReadyPlayer();
				break;
		}
	}

	private void JoinPlayer() {
		if (inputNumber >= 0) {
			playerState = PlayerState.JOINED;
		}
	}

	private void LeavePlayer() {
		playerState = PlayerState.NO_PLAYER;
		inputNumber = -1;
		GameState.i.RemovePlayerController(playerNumber);

		heroImage.color = Color.clear;
		heroName.text = "";
	}

	private void ReadyPlayer() {
		playerState = PlayerState.READY;
		heroImage.color = Color.green;
		var hero = HeroList.Heroes[chosenHero];
		GameState.i.addHero(hero, playerNumber);
	}

	private void NotReadyPlayer() {
		playerState = PlayerState.JOINED;
		GameState.i.removeHero(playerNumber);
		heroImage.color = Color.white;
	}

	private void ChooseHero() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));

		if (inputVector == Vector3.zero || inputVector.x > -0.2 && inputVector.x < 0.2) {
			playerChanged = false;
		} else if ((inputVector.x < -0.8 || inputVector.x > 0.8) && !playerChanged) {
			playerChanged = true;
			chosenHero += inputVector.x > 0 ? 1 : -1;
			if (chosenHero < 0) {
				chosenHero = HeroList.Heroes.Count - 1;
			} else if (chosenHero >= HeroList.Heroes.Count) {
				chosenHero = 0;
			}
		}

		heroName.text = HeroList.Heroes[chosenHero].name;
		heroImage.sprite = HeroList.Heroes[chosenHero].heroImage;
		heroImage.color = Color.white;
	}
}