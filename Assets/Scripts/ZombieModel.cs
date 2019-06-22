using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState { Spawn, Chase, Attack, Die }

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieModel : MonoBehaviour {
    NavMeshAgent agent;
    bool alive = true;
    float timer;

    void Start() {
        agent = this.GetComponent<NavMeshAgent>();
        timer = Time.time + 2f;
    }

    public ZombieState Spawn() {
        if (timer > Time.time) {
            return ZombieState.Spawn;
        }

        return ZombieState.Chase;
    }

    public void UpdateDestination(GameObject target) {
        agent.SetDestination(target.transform.position);
        transform.LookAt(agent.destination);
    }

    public ZombieState Chase() {
        agent.isStopped = false;

        if (agent.remainingDistance <= agent.stoppingDistance) {
            timer = Time.time + 1f;
            return ZombieState.Attack;
        }

        return ZombieState.Chase;
    }

    public ZombieState Attack() {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        if (timer > Time.time) {
            return ZombieState.Attack;
        }

        return ZombieState.Chase;
    }

    public ZombieState Die() {
        if (alive) {
            agent.isStopped = true;
            alive = false;
            timer = Time.time + 1.2f;
        }
        else if (timer < Time.time) {
            Destroy(this.gameObject);
        }

        return ZombieState.Die;
    }
}
