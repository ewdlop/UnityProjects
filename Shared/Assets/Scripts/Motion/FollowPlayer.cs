using UnityEngine;

namespace Assets.Scripts.Motion
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform player;
        public float speedX;
        public float speedY;
        public float speedZ;
        public Transform planet;

        Rigidbody rb;
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(speedX, speedY, speedZ), ForceMode.VelocityChange);
        }

        protected virtual void FixedUpdate()
        {
            Vector3 centripetalForce = Mathf.Pow(rb.linearVelocity.magnitude, 2) / (planet.position - transform.position).magnitude * (planet.position - transform.position).normalized * Time.fixedDeltaTime;
            rb.AddForce(centripetalForce, ForceMode.VelocityChange);
        }

        protected virtual void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.up) * Quaternion.AngleAxis(90f, Vector3.down);
        }
    }
}