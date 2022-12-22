using System;
using UnityEngine;
using DG.Tweening;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnCount;
    [Range(0f,1f)] [SerializeField] private float _distanceFactor, radius;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
        }
        FormatCharacters();
    }
    void FormatCharacters()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = (float)(_distanceFactor * Math.Sqrt(i) * Math.Cos(i * radius));
            var z = (float)(_distanceFactor * Math.Sqrt(i) * Math.Sin(i * radius));

            var newPos = new Vector3(x, 0, z);
            var child = transform.GetChild(i);
            child.DOLocalMove(newPos, 1f).SetEase(Ease.OutBack);
        }
    }
}
