using UnityEngine;
using System.Collections;

namespace SpinningBall
{
    public class Platform: MonoBehaviour
    {
        [SerializeField] private ColorType _colorType;
        public ColorType ColorType { get => _colorType; }
        [SerializeField] private Transform _centerPoint;


        private Vector3 initialPosition;
        private Vector3 targetDirection;
        private float moveSpeed = 1f; // Adjust this value for the desired speed.
        private float moveDuration = 0.25f; // Duration of the initial movement in seconds.

        public void PlayAnimation()
        {
            initialPosition = transform.localPosition;
            targetDirection = (initialPosition - _centerPoint.localPosition).normalized;
            StartCoroutine(MoveInDirectionAndReturn());
        }


        private IEnumerator MoveInDirectionAndReturn()
        {
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                // Move the object in the specified direction over time.
                transform.localPosition += targetDirection * moveSpeed * Time.deltaTime;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Calculate the remaining time for the return movement.
            float returnDuration = moveDuration; // Use the same duration for return.
            float remainingReturnTime = returnDuration;

            while (remainingReturnTime > 0f)
            {
                // Move the object back to the initial position over time.
                transform.localPosition = Vector3.Lerp(initialPosition, transform.localPosition, 1f - (remainingReturnTime / returnDuration));

                remainingReturnTime -= Time.deltaTime;
                yield return null;
            }

            // Ensure the object reaches its initial position exactly.
            transform.localPosition = initialPosition;
        }
    }
}
