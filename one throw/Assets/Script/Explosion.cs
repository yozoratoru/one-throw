using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetTrigger("triggerExplosion");
        AudioManager.Instance.PlayExplosionSound();
    }
}
