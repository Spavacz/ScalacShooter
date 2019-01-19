using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {
	public List<GameObject> projectilePrefabs;

	public Transform spawnPoint;
	public Transform weaponRotator;

	public int playerNumber;

	private const string INPUT_HORIZONTAL = "Horizontal Aim Player ";
	private const string INPUT_VERTICAL = "Vertical Aim Player ";

	public float speed = 500f;

	private float currentAttackCooldown = 0;
	public float attackCooldown = 1f;

	private string InputHorizontal => INPUT_HORIZONTAL + (playerNumber + 1);
	private string InputVertical => INPUT_VERTICAL + (playerNumber + 1);

	private Vector3 inputVector;

	private void Update() {
		inputVector = new Vector3(Input.GetAxis(InputHorizontal), 0, Input.GetAxis(InputVertical)).normalized;
		if (inputVector != Vector3.zero) {
			var rotation = Quaternion.LookRotation(inputVector, Vector3.up);
			weaponRotator.rotation = rotation;
			if (currentAttackCooldown <= 0) {
				currentAttackCooldown = attackCooldown;
				FireWeapon(inputVector);
			}
		}

		currentAttackCooldown -= Time.deltaTime;
	}

	private void FireWeapon(Vector3 inputVector) {
		//var rotation = Quaternion.LookRotation(inputVector, Vector3.up);
		var projectile = Instantiate(projectilePrefabs[playerNumber], spawnPoint.position, weaponRotator.rotation).GetComponent<Rigidbody>();
//		projectile.AddForce(inputVector * speed);

		projectile.AddForce(projectile.transform.forward * speed);
	}
	
	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		var targetForward = inputVector * 2;
		Gizmos.DrawLine(transform.position, transform.position + targetForward);
	}
}