using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

namespace Obstacles
{
    public class Whirlwind : MonoBehaviour
    {
        [SerializeField] private float turnSpeed;
        private bool isRight => gameObject.transform.position.x > -6;

        private void Start()
        {
            if (isRight)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }

        private void Update()
        {
            transform.Rotate(0,turnSpeed*Time.deltaTime,0,Space.Self);
        }
        
        void MoveRight()
        {
            transform.DOMove(new Vector3(-6, 0, transform.position.z), 2.5f).SetEase(Ease.Linear).OnStepComplete(MoveLeft);
        }

        void MoveLeft()
        {
            transform.DOMove(new Vector3(6, 0, transform.position.z), 2.5f).SetEase(Ease.Linear).OnStepComplete(MoveRight);
        }
    }
}
