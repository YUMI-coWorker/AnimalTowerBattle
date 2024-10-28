using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanel : MonoBehaviour
{
    public GameObject playEffect;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        playEffect.GetComponent<ParticleSystem>().Play();
    }

    private void OnDisable()
    {
        playEffect.GetComponent<ParticleSystem>().Stop();
    }
}
