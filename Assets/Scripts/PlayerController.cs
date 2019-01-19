using UnityEngine;

public class PlayerController : MonoBehaviour {
	public PlayerView view;

	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";

	public float speed = 5f;

	private PlayerWeaponController weapon;
	private Rigidbody rig;

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private int playerNumber;

	private int hp;
	public int maxHp = 5;

	private void Awake() {
		rig = GetComponent<Rigidbody>();
		weapon = GetComponent<PlayerWeaponController>();
		hp = maxHp;
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

	private void OnCollisionEnter(Collision hit) {
		var tag = hit.transform.tag;
		if (tag.Contains("Bullet Player ") && tag != "Bullet Player " + (playerNumber + 1)) {
			hp--;
			view.SetHp(hp / (float)maxHp);
			if (hp == 0) {
				Death();
			}
		}
	}

	private void Death() {
		Debug.Log("Die!!!");
		Destroy(gameObject);
	}
}