using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerSpawner _playerController;
        private Transform _enemySpawner;
        
        //HorizontalValues
        private readonly float xGap = 0.6f;
        private readonly float yGap = 1.85f;

        //VerticalValues
        private readonly List<float> _xPosList = new();
        private readonly List<float> _yPosList = new();

        private bool isOdd(int num) => num % 2 != 0;
        private void Start()
        {
            _playerController = GetComponent<PlayerSpawner>();
        }

        public void MoveToRing(Transform other)
        {
            transform.position = Vector3.Lerp(transform.position,new Vector3(other.position.x,transform.position.y,other.position.z),Time.deltaTime);
            switch (other.childCount)
            {
                case > 1:
                {
                    var enemyDirection = new Vector3(other.position.x, transform.position.y, other.position.z) - transform.position;
                    for (int i = 0; i < transform.childCount; i++)
                    { 
                        transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation,
                            Quaternion.LookRotation(enemyDirection.normalized,Vector3.up),Time.deltaTime );
                        var Distance = other.GetChild(0).position - transform.GetChild(i).position;
                        if (Distance.magnitude < 8f)  transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            new Vector3(other.GetChild(1).position.x,transform.GetChild(i).position.y,other.GetChild(1).position.z),Time.deltaTime);
                    }
                    break;
                }
                case <= 1:
                    _playerController.FormatCharacters();
                    other.GetChild(0).GetComponent<Ring>().PopRing();
                    GameManager.Instance.state = GameManager.PlayerState.Running;
                    break;
            }
        }
        
        public  IEnumerator FormationCoroutine(int childCount)
        {
            yield return GetXPositions(childCount);
            yield return GetYPositions(childCount);
            yield return SetFormation(childCount);
        }
        
        IEnumerator SetFormation(int childCount)
        {
            _xPosList.Reverse();
            _yPosList.Reverse();
            var oddCounter = 0;
            var loopLimit = 1;
            var loopIndex = 0;
            var startingChildCount = childCount;
            while (childCount > 0)
            {
                var lineWidth = loopLimit - loopIndex;
                { 
                    for (int i = loopIndex; i < loopLimit; i++) 
                    {
                        var pos = new Vector3(_xPosList[i], _yPosList[i], 0);
                        transform.GetChild(i).localPosition = pos;
                    }
                    yield return new WaitForSeconds(0.1f);
                    if (isOdd(oddCounter))
                    {
                        loopIndex = loopLimit;
                        loopLimit += lineWidth+1;
                    }
                    else
                    {
                        loopIndex = loopLimit;
                        loopLimit += lineWidth;
                    }
                    oddCounter++;
                    childCount -= lineWidth;
                    if (childCount < lineWidth * 2)
                    {
                        for (int i = loopIndex; i < startingChildCount; i++)
                        {
                                var pos = new Vector3(_xPosList[i], _yPosList[i], 0);
                                transform.GetChild(i).localPosition = pos;
                        }
                        childCount = 0;
                    }
                    
                }
            }
        
        }

        IEnumerator GetYPositions(int childCount)
        {
            for (int i = 0; i < childCount; i++)
            {
                _yPosList.Add(0);
            }
            var oddCounter = 0;
            var loopIndex = 1;
            var loopLimit = 1;
            var startingChildCount = childCount;
            while (childCount > 0)
            {
                for (int i = 0; i < loopLimit; i++)
                {
                    _yPosList[i] += yGap;
                }

                if (isOdd(oddCounter))
                {
                    loopIndex++;
                }
                loopLimit += loopIndex;
                oddCounter++;
                childCount -= loopIndex;
                if (childCount < loopIndex * 2)
                {
                    for (int i = 0; i < startingChildCount-loopIndex; i++)
                    {
                        _yPosList[i] += yGap;
                    }

                    childCount = 0;
                }
            }

            yield return null;
        }

        IEnumerator GetXPositions(int childCount)
        {
            var oddCounter = 0;
            var loopLimit = 4;
            var loopIndex = 2;
            var startingChildCount = childCount;
            while (childCount > 0)
            {
                var lineWidth = loopLimit - loopIndex;
                if (oddCounter < 2)
                {
                    _xPosList.Add(0);
                    oddCounter++;
                }
                else
                {
                    if (isOdd(lineWidth))
                    {
                        var xOffset = (lineWidth - 1) / 2 * -xGap;
                        for (int i = loopIndex; i < loopLimit; i++)
                        {
                            _xPosList.Add(xOffset);
                            xOffset += xGap;
                        }
                    }
                    else
                    {
                        var xOffset = -lineWidth / 2 * xGap + xGap / 2;
                        for (int i = loopIndex; i < loopLimit; i++)
                        {
                            _xPosList.Add(xOffset);
                            xOffset += xGap;
                        }
                    }
                    if (isOdd(oddCounter))
                    {
                        loopIndex = loopLimit;
                        loopLimit += lineWidth+1;
                    }
                    else
                    {
                        loopIndex = loopLimit;
                        loopLimit += lineWidth;
                    }
                    oddCounter++;
                    childCount -= lineWidth;
                    if (childCount < lineWidth * 2)
                    {
                        lineWidth = childCount;
                        if (isOdd(lineWidth))
                        {
                            var xOffset = (lineWidth - 1) / 2 * -xGap;
                            for (int i = loopIndex; i < startingChildCount; i++)
                            {
                                _xPosList.Add(xOffset);
                                xOffset += xGap;
                            }
                        }
                        else
                        {
                            var xOffset = -lineWidth / 2 * xGap + xGap / 2;
                            for (int i = loopIndex; i < startingChildCount; i++)
                            {
                                _xPosList.Add(xOffset);
                                xOffset += xGap;
                            }
                        }
                        childCount = 0;
                    }
                }
            }

            yield return null;
        }
    }
}


