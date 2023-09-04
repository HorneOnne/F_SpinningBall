using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpinningBall
{
    public class Spin : MonoBehaviour
    {
        private bool isRotating = false;
        [SerializeField] private float rotateDuration = 1.0f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                TurnLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                TurnRight();
            }
        }

        public void TurnLeft()
        {
            if (!isRotating)
            {
                StartCoroutine(RotateCoroutine(45));
            }
        }

        public void TurnRight()
        {
            if (!isRotating)
            {
                StartCoroutine(RotateCoroutine(-45));
            }
        }


        private IEnumerator RotateCoroutine(float rotAngle = -45f)
        {
            isRotating = true;

            float targetRotation = transform.eulerAngles.z + rotAngle;
            float startRotation = transform.eulerAngles.z;
            float startTime = Time.time;

            while (Time.time - startTime < rotateDuration)
            {
                float t = (Time.time - startTime) / rotateDuration;
                float newRotation = Mathf.Lerp(startRotation, targetRotation, t);
                transform.eulerAngles = new Vector3(0, 0, newRotation);
                yield return null;
            }

            transform.eulerAngles = new Vector3(0, 0, targetRotation);
            isRotating = false;
        }
    }
}
