using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniManager : MonoBehaviour
{
    private static readonly int isIdle = Animator.StringToHash("isIdle");
    private static readonly int isRunning = Animator.StringToHash("isRunning");
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private Animator _animator;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation();
    }

    void SetAnimation()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.PlayerState.Running:
                _animator.SetBool(isRunning, true);
                break;
            case GameManager.PlayerState.Idle:
                _animator.SetBool(isIdle,true);
                break;
            case GameManager.PlayerState.Fighting:
                _animator.SetBool(isWalking, true);
                break;
        }
    }
}
