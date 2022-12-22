using UnityEngine;

namespace Obstacles
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private float turnSpeed;

        private void Update()
        {
            transform.Rotate(0,turnSpeed*Time.deltaTime,0,Space.Self);
        }
    }
}
