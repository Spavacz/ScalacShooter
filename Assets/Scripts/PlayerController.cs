using UnityEngine;
public class PlayerController : MonoBehaviour {
	public float speed = 5f;
	public LayerMask unwalkableLayer;
	private const string INPUT_HORIZONTAL = "Horizontal Player ";
	private const string INPUT_VERTICAL = "Vertical Player ";
	private float radius = 0.5f;

	
	private Rigidbody rig;

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private int playerNumber;

	private void Awake() {
		rig = GetComponent<Rigidbody>();
	}

	private void Update() {
		var inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical));
		var moveVector = inputVector * speed * Time.deltaTime;

		rig.velocity = moveVector;

//		var hits = Physics.RaycastAll(transform.position, moveVector, radius + moveVector.magnitude, unwalkableLayer);
//		if (hits.Length > 0) {
//			foreach (var hit in hits) {
//				if (hit.transform == transform) {
//					continue;
//				}
//				if (hit.transform.CompareTag("Wall") || hit.transform.CompareTag("Player")) {
//					Debug.Log("X");
//					var newVector = moveVector.normalized * (hit.distance - radius);
//					if (newVector.magnitude < moveVector.magnitude) {
//						moveVector = newVector;
//					}
//				}
//			}
//		}

		//transform.position += moveVector;
	}

	public void SetPlayer(int playerNumber) {
		this.playerNumber = playerNumber;
	}
}