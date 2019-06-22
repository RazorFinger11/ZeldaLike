using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieSwarm : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject target;
    [SerializeField] Transform[] spawnPoints;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Swarm();
        }
    }

    void Swarm() {
        if (spawnPoints.Length > 0) {
            for (int i = 0; i < spawnPoints.Length; i++) {
                GameObject go = Instantiate(zombiePrefab, spawnPoints[i].position, Quaternion.identity);
                go.GetComponent<ZombieController>().Target = target;
            }
        }
    }
}
