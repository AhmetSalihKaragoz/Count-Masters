using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static bool isLevelEnd = false;
    [SerializeField] private float smoothSpeed;
    
    private Transform player;
    private Vector3 offset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Transform>();
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (!isLevelEnd)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, offset + player.position,smoothSpeed);
            transform.position = newPos;
        }
        else
        {
            smoothSpeed = 0.1f;
            var endPos = Vector3.Lerp(transform.position,new Vector3(player.position.x+4,player.GetChild(0).position.y+9.5f,player.position.z-7),smoothSpeed);
            var endRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(40, -30, 0),smoothSpeed);
            transform.position = endPos;
            transform.rotation = endRot;
        }
    }
}
