using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private int playerNumber;

	private void Update() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
		var moveVector = inputVector * speed * Time.deltaTime;
		transform.Translate(moveVector);
	}

	public void SetPlayer(int playerNumber) {
		this.playerNumber = playerNumber;
	}
}