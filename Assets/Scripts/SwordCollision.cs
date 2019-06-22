using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwordCollision : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            other.gameObject.SendMessage("Damage");
        }
    }
}