using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    public Collider bola;
    public float multiplier;
    public Color color;
    public float score;

    public AudioManager audioManager;
    public VFXManager vfxManager;
    public ScoreManager scoreManager;

    private Renderer rerenderer;
    private Animator animator;

    private void Start()
    {
        rerenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        GetComponent<Renderer>().material.color = color;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == bola)
        {
            Rigidbody bolaRig = bola.GetComponent<Rigidbody>();
            bolaRig.velocity *= multiplier;

            animator.SetTrigger("hit");

            audioManager.PlaySFX(collision.transform.position);

            vfxManager.PlayVFX(collision.transform.position);

            scoreManager.AddScore(score);
        }
    }
}