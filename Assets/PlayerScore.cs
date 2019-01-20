using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {
	public int playerNumber;
	private Text text;

	void Start() {
		text = GetComponent<Text>();
	}

	void Update() {
		text.text = GameController.score[playerNumber].ToString();
	}
}