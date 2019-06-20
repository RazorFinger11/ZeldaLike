using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//HOW TO USE IT
//TARGET IS THE OBJECT THAT ZOMBIE WILL FOLLOW AND TRY TO KILL
//HAVE FUN

[RequireComponent(typeof(ZombieModel))]
[RequireComponent(typeof(ZombieView))]
public class ZombieController : MonoBehaviour
{
    [SerializeField] GameObject target;

    ZombieModel zombie;
    ZombieView anim;
    ZombieState state;

    void Start() {
        zombie = this.GetComponent<ZombieModel>();
        anim = this.GetComponent<ZombieView>();
    }

    void Update() {
        switch (state) {
            case ZombieState.Spawn:
                state = zombie.Spawn();
                break;

            case ZombieState.Chase:
                zombie.UpdateDestination(target);
                state = zombie.Chase();
                anim.Chase();
                break;

            case ZombieState.Attack:
                zombie.UpdateDestination(target);
                state = zombie.Attack();
                anim.Attack();
                break;

            case ZombieState.Die:
                break;
        }
    }
}
