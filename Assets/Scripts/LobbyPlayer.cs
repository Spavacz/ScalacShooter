using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LobbyPlayer : MonoBehaviour {
	public int playerNumber = 1;
	public float timeToChangeHero = 1;
	public HeroList HeroList = null;
	public int chosenHero = 0;
	public Text heroName;
	public Image heroImage;

	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";
	private const string INPUT_CONFIRM = "Fire1Player";
	private const string INPUT_BACK = "Fire2Player";
	private const string INPUT_START = "Start";

	private string InputConfirm => INPUT_CONFIRM + (playerNumber + 1);
	private string InputBack => INPUT_BACK + (playerNumber + 1);
	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private float changeHeroTimer = 1;
	private bool joined = false;
	private bool ready = false;

	void setReady() {
		if (joined && Input.GetButtonDown(InputConfirm)) {
			joined = false;
			ready = true;
			GameState.i.addHero(HeroList.Heroes[chosenHero].name, HeroList.Heroes[chosenHero].heroImage, playerNumber);
		}

		if (ready && Input.GetButtonDown(InputBack)) {
			joined = true;
			ready = false;
			GameState.i.removeHero(HeroList.Heroes[chosenHero].name);
		}
	}

	void changeHero() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
		if (playerNumber == 3 && Input.GetKeyDown(KeyCode.LeftArrow)) {
			inputVector = new Vector3(-1, 0, 0);
		} else if (playerNumber == 3 && Input.GetKeyDown(KeyCode.RightArrow)) {
			inputVector = new Vector3(1, 0, 0);
		}

		if (inputVector.x < -0.3 && changeHeroTimer <= 0) {
			if (chosenHero == 0) {
				chosenHero = HeroList.Heroes.Count - 1;
			} else {
				chosenHero -= 1;
			}

			changeHeroTimer = 1;
		} else if (inputVector.x > 0.3 && changeHeroTimer <= 0) {
			if (chosenHero == HeroList.Heroes.Count - 1) {
				chosenHero = 0;
			} else {
				chosenHero += 1;
			}

			changeHeroTimer = timeToChangeHero;
		}

		heroName.text = HeroList.Heroes[chosenHero].name;
		heroImage.sprite = HeroList.Heroes[chosenHero].heroImage;
		heroImage.color = new Color(255, 255, 255, 255);
	}

	void joinGame() {
		bool joinGame = Input.GetButtonDown(InputConfirm);
		bool leaveGame = Input.GetButtonDown(InputBack);

		if (!ready && joinGame) {
			joined = true;
		} else if (joined && leaveGame) {
			joined = false;
			heroImage.color = new Color(255, 255, 255, 0);
			heroName.text = "";
		}
	}

	void changeScene() {
		bool start = Input.GetButtonDown(INPUT_START);
		if (start && GameState.i.Players.Count >= 2) {
			SceneManager.LoadScene("SampleScene");
		}
	}

	// Update is called once per frame
	void Update() {
		changeHeroTimer -= Time.deltaTime;
		setReady();
		joinGame();
		changeScene();
		if (joined) {
			changeHero();
		}
	}
}