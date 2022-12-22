using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private AudioClip _onDeathSfx;
        
        private bool hasTouched;
        private EnemySpawner _enemySpawner;
        private PlayerSpawner _playerSpawner;
        private void Start()
        {
            _playerSpawner = GetComponentInParent<PlayerSpawner>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Door"))
            {
                other.GetComponentInParent<Gate>().TurnOffColliders();
                var door = other.GetComponent<Door>();
                _playerSpawner.Spawn(door.textValue, door.textSymbol);
            }
            else if (other.CompareTag("FightRing"))
            {
                GameManager.Instance.state = GameManager.PlayerState.Fighting;
                GetComponentInParent<PlayerMovement>().enemySpawner = other.transform;
            }
            else if (other.CompareTag("Enemy"))
            {
                DestroyOnContact(other.gameObject);
            }
            else if (other.CompareTag("Obstacle"))
            {
                AudioSource.PlayClipAtPoint(_onDeathSfx, transform.position);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Finish"))
            {
                other.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(_playerSpawner.GetComponent<PlayerBehaviour>()
                    .FormationCoroutine(transform.parent.childCount));
                CameraFollow.isLevelEnd = true;
            }
            else if(other.CompareTag("RewardStep"))
            {
                transform.parent = null;
            }
            
        }

        void DestroyOnContact(GameObject other)
        {
            if (!hasTouched)
            {
                hasTouched = true;
                Destroy(other);
                AudioSource.PlayClipAtPoint(_onDeathSfx,transform.position);
                Destroy(gameObject);
            }
        }
    }
}
