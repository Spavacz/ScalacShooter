using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
	public int playerNumber;
	private TextMeshProUGUI text;

	void Start() {
		text = GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		text.text = GameController.score[playerNumber].ToString();
	}
}