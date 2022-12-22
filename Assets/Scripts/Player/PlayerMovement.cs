using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed = 10f;
        [SerializeField] private float thrustSpeed = 10f;
        [HideInInspector] public Transform enemySpawner;
    
        private Vector3 direction;
        private Rigidbody rb;
        private PlayerBehaviour _playerBehaviour;
    

        private void Start()
        {
            _playerBehaviour = GetComponent<PlayerBehaviour>();
        }

        private void Update()
        {
            Move(direction);
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.state == GameManager.PlayerState.Running)
            {
                float horizontal = Input.GetAxis("Horizontal") * horizontalSpeed;
                float vertical = thrustSpeed;
                direction = new Vector3(horizontal, 0, vertical);
            }
        }

        void Move(Vector3 dir)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GameManager.Instance.state = GameManager.PlayerState.Running;
            if (GameManager.Instance.state == GameManager.PlayerState.Running)
                transform.position += dir*Time.deltaTime;
            if (GameManager.Instance.state == GameManager.PlayerState.Fighting)
            {
                _playerBehaviour.MoveToRing(enemySpawner);
            }
        }
    
    
    
    }
}
