using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;
    private Coroutine _autoPushCoroutine;

    public void Stop()
    {
        Debug.Log("Push");
        StopCoroutine(_autoPushCoroutine);
        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }

    public void Play()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        _particleSystem.Play();
        _autoPushCoroutine = StartCoroutine(AutoPushCoroutine());
    }

    private IEnumerator AutoPushCoroutine()
    {
        yield return new WaitUntil(() => _particleSystem.isStopped);
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {

    }
}
