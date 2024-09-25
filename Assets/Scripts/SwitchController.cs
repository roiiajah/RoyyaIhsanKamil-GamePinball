using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchController : MonoBehaviour
{
    private enum SwitchState
    {
        Off,
        On,
        Blink
    }

    public Collider bola;
    public Material offMaterial;
    public Material onMaterial;
    public ScoreManager scoreManager;
    public AudioManager audioOn;
    public AudioManager audioOff;
    public VFXManager vfxManager;

    private SwitchState state;
    private Renderer rerenderer;
    public float score;

    private void Start()
    {
        rerenderer = GetComponent<Renderer>();

        Set(false);

        StartCoroutine(BlinkTimerStart(5));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == bola)
        {
            Toggle();
        }
        
        vfxManager.PlayVFX(GetComponent<Collider>().transform.position);
    }

    private void Set(bool active)
    {
        if(active == true)
        {
            state = SwitchState.On;
            rerenderer.material = onMaterial;
            StopAllCoroutines();
            
        }
        else
        {
            state = SwitchState.Off;
            rerenderer.material = offMaterial;
            StartCoroutine(BlinkTimerStart(5));
            
        }
    }

    private void Toggle()
    {
        if (state == SwitchState.On)
        {
            Set(false);
            audioOn.PlaySFX(GetComponent<Collider>().transform.position);
        }
        else
        {
            Set(true);
            audioOff.PlaySFX(GetComponent<Collider>().transform.position);
        }
        scoreManager.AddScore(score);
    }

    private IEnumerator Blink(int times)
    {
        state = SwitchState.Blink;

        for (int i = 0; i < times; i++)
        {
            rerenderer.material = onMaterial;
            yield return new WaitForSeconds(0.5f);
            
            rerenderer.material = offMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        state = SwitchState.Off;
        
        StartCoroutine(BlinkTimerStart(5));
    }

    private IEnumerator BlinkTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Blink(2));
    }
}
