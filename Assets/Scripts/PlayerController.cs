using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	public string inputHorizontal = "Horizontal";
	public string inputVertical = "Vertical";

	private void Update() {
		var inputVector = new Vector3(Input.GetAxis(inputHorizontal), 0, Input.GetAxis(inputVertical));
		var moveVector = inputVector * speed * Time.deltaTime;
		transform.Translate(moveVector);
	}
}