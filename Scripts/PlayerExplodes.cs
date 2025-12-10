using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplodes : MonoBehaviour
{
    [SerializeField] private ParticleSystem confettiExplosion;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private AudioSource popSound;
    [SerializeField] private Animator animationController;
    [SerializeField] private AnimationClip ballPop;
    private bool endSequence = false;

    public void Confetti()
    {
        confettiExplosion.transform.position = transform.position;
        confettiExplosion.Play();
    }

    public void Dust()
    {
        dust.transform.position = transform.position;
        dust.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VictoryZone"))
        {
            StartCoroutine(ExplodeSequence());
        }
    }

    private IEnumerator ExplodeSequence()
    {
        if (!endSequence)
        {
            animationController.SetTrigger("ballPop");

            yield return new WaitForSeconds(ballPop.length - 0.05f);
            popSound.Play();
            Confetti();
            Destroy(gameObject);
            endSequence = true;
        }
        else
        {
            StopCoroutine(ExplodeSequence());
            yield return null;
        }
    }
}