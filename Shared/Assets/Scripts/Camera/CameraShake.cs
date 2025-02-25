using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraShake : MonoBehaviour
    {

        protected float shakeTimer;
        protected float shakeAmount;

        protected Vector3 cameraPos;

        protected void Start()
        {
            cameraPos = transform.position;
        }

        protected void Update()
        {
            if (shakeTimer >= 0)
            {
                //Circular shake
                Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
                transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
                shakeTimer -= Time.deltaTime;
            }

            // Return camera to original position after shaking
            else
            {
                transform.position = cameraPos;
            }
        }

        public virtual void ShakeCamera(float shakePower, float shakeDuration)
        {
            shakeAmount = shakePower;
            shakeTimer = shakeDuration;
        }
    }
}