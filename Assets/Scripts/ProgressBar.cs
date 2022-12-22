using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private TextMeshProUGUI progressPercentageText;

    private Transform _finishLine;
    private Transform _playerController;
    private float _progressPercentage;
    private float _maxDistance;
    private void Start()
    {
        _finishLine = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
        _playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Transform>();
        _maxDistance = _finishLine.position.z - _playerController.position.z;
    }

    void Update()
    {
        progressBarImage.fillAmount = _playerController.position.z / _maxDistance;
    }

    // private void Start()
    // {
    //     _finishLine = GameObject.FindGameObjectWithTag("FinishLine").GetComponent<Transform>();
    //     _maxDistance = _finishLine.transform.position.z;
    //     progressBarImage.material.color = ColorManager.Instance.CurrentColor;
    // }
    //
    // private void Progress()
    // {
    //     if (!GameManager.Instance.HasStarted) return;
    //     var distanceOffset = _maxDistance - _finishLine.position.z;
    //     progressBarImage.fillAmount = _fillAmount;
    //     _fillAmount = distanceOffset / _maxDistance;
    // }
    //
    // private void ProgressText()
    // {
    //     if (!GameManager.Instance.HasStarted) return;
    //     _progressPercentage = Mathf.Clamp(((0.01f - _fillAmount) * -100), 0, 100);
    //     progressPercentageText.text = (int)_progressPercentage + "%";
    // }
}
