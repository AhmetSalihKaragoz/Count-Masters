using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _playerSpawner;


    private void Start()
    {
        _playerSpawner = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Transform>();
    }
    private void Update()
    {
        if (GameManager.Instance.state != GameManager.PlayerState.Fighting) return;
              MoveToPlayer(); 
    }
    
    void MoveToPlayer()
    {
        
        var playerDirection = new Vector3(_playerSpawner.position.x, transform.position.y, _playerSpawner.position.z) - transform.position;
        for (int i = 1; i < transform.childCount; i++)
        {
             // transform.GetChild(i).rotation = 
             //     Quaternion.Slerp(transform.GetChild(i).rotation,
             //         Quaternion.LookRotation(playerDirection.normalized,Vector3.up),Time.deltaTime );
            transform.GetChild(i).LookAt(new Vector3(_playerSpawner.position.x,-_playerSpawner.position.y,_playerSpawner.position.z));
            var Distance = _playerSpawner.GetChild(0).position - transform.GetChild(i).position;
            if (Distance.magnitude < 8f)
            {
                transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                    new Vector3(_playerSpawner.GetChild(0).position.x,transform.GetChild(i).position.y,_playerSpawner.GetChild(0).position.z),Time.deltaTime);
            }
        }
    }
}
