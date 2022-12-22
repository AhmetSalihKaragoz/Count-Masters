using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject characterPrefab;
        [SerializeField] private AudioClip _onSpawnSfx;
        [Range(0f,1f)] [SerializeField] private float _distanceFactor, radius;

        private int _spawnCount;
        private Transform player;

        private void Awake()
        {
            DOTween.SetTweensCapacity(1000,100);
            _spawnCount = transform.childCount;
            player = transform;
        }
    
        public void Spawn(int count, char symbol)
        {
            switch (symbol)
            {
                case '+':
                    _spawnCount = count;
                    StartCoroutine(SpawnCoroutine());
                    AudioSource.PlayClipAtPoint(_onSpawnSfx,transform.position);
                    break;
                case 'x':
                    _spawnCount = transform.childCount*(count-1);
                    StartCoroutine(SpawnCoroutine());
                    AudioSource.PlayClipAtPoint(_onSpawnSfx,transform.position);
                    break;
            }
        }
    
        public void FormatCharacters()
        {
            for (int i = 0; i < player.childCount; i++)
            {
                var x = (float)(_distanceFactor * Math.Sqrt(i) * Math.Cos(i * radius));
                var z = (float)(_distanceFactor * Math.Sqrt(i) * Math.Sin(i * radius));

                var newPos = new Vector3(x, 0, z);
                var child = player.GetChild(i);
                child.DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);
                child.rotation = quaternion.Euler(new Vector3(0,0,0));
            }
        }
    
        private IEnumerator SpawnCoroutine()
        {
            var wait = new WaitForSeconds(0.05f);
            InstantiateCharacters();
            yield return wait;
            FormatCharacters();
        }
        void InstantiateCharacters()
        {
            for (var i = 0; i < _spawnCount; i++)
            {
                Instantiate(characterPrefab, transform.position, Quaternion.identity, transform);
            }
        }

    }
}
