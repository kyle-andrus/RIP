﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeEffect : MonoBehaviour
{
    [SerializeField]
    private float Duration;
    private float Timer = 0.0f;

    [SerializeField]
    private AnimationCurve Curve;

    private bool Active = false;
    private Vector3 InitialPosition;

    void Update()
    {
        if (Active)
        {
            Timer += Time.deltaTime;
            if (Timer <= Duration)
            {
                gameObject.transform.localPosition = InitialPosition + Random.insideUnitSphere * Curve.Evaluate(Timer/Duration);
            }
            else
            {
                Timer = 0f;
                gameObject.transform.localPosition = InitialPosition;
                Active = false;
            }
        }
    }

    public void Activate()
    {
        InitialPosition = gameObject.transform.localPosition;
        Active = true;
    }
}
