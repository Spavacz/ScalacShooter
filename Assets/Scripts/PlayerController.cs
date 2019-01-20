using UnityEngine;

public class PlayerController : MonoBehaviour {
	public PlayerView view;

	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";

	public float speed = 5f;
	public AudioSource audioSource;
	public AudioClip movementAudio;
	public float movementAudioBreak = 0.5f;
	
	private PlayerWeaponController weapon;
	private Rigidbody rig;

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);
	private int playerNumber;
	private float timeToNextAudio = 0;

	private int hp;
	public int maxHp = 5;

	private void Awake() {
		rig = GetComponent<Rigidbody>();
		weapon = GetComponent<PlayerWeaponController>();
		hp = maxHp;
		audioSource = GameObject.Find("shotSfx").GetComponent<AudioSource>();
	}

	private void FixedUpdate() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
		if (inputVector.magnitude > 0 && timeToNextAudio <= 0)
		{
			audioSource.PlayOneShot(movementAudio);
			timeToNextAudio = movementAudioBreak;
		}

		timeToNextAudio -= Time.fixedDeltaTime;
		var moveVector = inputVector * speed * Time.fixedDeltaTime;
		rig.velocity = moveVector;

		if (moveVector != Vector3.zero) {
			var rotation = Quaternion.LookRotation(moveVector, Vector3.up);
			transform.rotation = rotation;
		}
	}

	public void SetPlayer(int playerNumber) {
		this.playerNumber = playerNumber;
		weapon.playerNumber = playerNumber;
		view.Init(GameState.i.Players[playerNumber]);
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
			if (hp == 0) {
				Death(hit.transform.tag);
			}
		}

		if (tag.Equals("Medkit")) {
			hp = maxHp;
			hit.transform.GetComponent<Medkit>().Pickup();
		}
		
		view.SetHp(hp / (float)maxHp);
	}

	private void Death(string deathTag) {
		Debug.Log("Die!!!");
		switch (deathTag) {
			case "Bullet Player 1":
				GameController.score[0]++;
				break;
			case "Bullet Player 2":
				GameController.score[1]++;
				break;
			case "Bullet Player 3":
				GameController.score[2]++;
				break;
			case "Bullet Player 4":
				GameController.score[3]++;
				break;
		}
		GetComponentInParent<PlayerSpawner>().StartSpawn();
		Destroy(gameObject);
	}
}