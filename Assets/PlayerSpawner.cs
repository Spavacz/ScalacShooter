using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    public int playerNumber;
    
    public GameObject playerPrefab;

    private void Start() {
        Spawn();
    }

    public void StartSpawn() {
        StartCoroutine(WaitAndSpawn());
    }

    public IEnumerator WaitAndSpawn() {
        yield return new WaitForSeconds(5f);
        Spawn();
    }

    private void Spawn() {
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerController>();
        player.transform.parent = transform;
        player.SetPlayer(playerNumber);
    }
}
