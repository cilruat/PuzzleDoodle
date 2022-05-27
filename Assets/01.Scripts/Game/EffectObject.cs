using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    private ParticleSystem _ps;
    private IDisposable _observer;

    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();

        _observer = Observable.EveryGameObjectUpdate()
            .Select(_ => _ps.IsAlive(true))
            .DistinctUntilChanged()
            .Where(isAlive => isAlive == false)
            .Subscribe(_ => 
                {
                    Destroy(gameObject);
                    _observer.Dispose(); 
                }).AddTo(gameObject);
    }
}
