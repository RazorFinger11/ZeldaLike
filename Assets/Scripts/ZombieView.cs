using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieView : MonoBehaviour
{
    Animator anim;

    void Start() {
        anim = this.GetComponent<Animator>();
    }

    public void Chase() {
        anim.ResetTrigger("Attack");
        anim.SetBool("Chase", true);
    }

    public void Attack() {
        anim.SetTrigger("Attack");        
    }

    public void Die() {
        anim.SetTrigger("Die");
    }
}