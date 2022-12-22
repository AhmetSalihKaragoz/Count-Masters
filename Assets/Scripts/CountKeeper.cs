using System.Numerics;
using UnityEngine;
using TMPro;
using Vector3 = UnityEngine.Vector3;

public class CountKeeper : MonoBehaviour
{
    private TextMeshPro _countText;
    private Transform _player;
    private Vector3 offset;
    

    private void Awake()
    {
        _countText = transform.GetChild(0).GetComponent<TextMeshPro>();
        _player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Transform>();
        offset = transform.position - _player.position;
        _countText.text = _player.transform.childCount.ToString();
    }

    private void Update()
    {
        var pos = offset + _player.position;
        transform.position = pos;
        _countText.text = _player.transform.childCount.ToString();
    }
}
