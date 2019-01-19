using UnityEngine;

public class PlayerController : MonoBehaviour {
	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";

	public float speed = 5f;

	private PlayerWeaponController weapon;
	private Rigidbody rig;

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private int playerNumber;

	private void Awake() {
		rig = GetComponent<Rigidbody>();
		weapon = GetComponent<PlayerWeaponController>();
	}

	private void Update() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
		var moveVector = inputVector * speed * Time.deltaTime;
		rig.velocity = moveVector;

		if (moveVector != Vector3.zero) {
			var rotation = Quaternion.LookRotation(moveVector, Vector3.up);
			transform.rotation = rotation;
		}
	}

	public void SetPlayer(int playerNumber) {
		this.playerNumber = playerNumber;
		weapon.playerNumber = playerNumber;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Vector3 targetForward = transform.rotation * Vector3.forward * 2;
		Gizmos.DrawLine(transform.position, transform.position + targetForward);
	}
}