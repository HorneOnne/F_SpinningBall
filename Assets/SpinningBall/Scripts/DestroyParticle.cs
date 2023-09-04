using UnityEngine;

namespace SpinningBall
{
    public class DestroyParticle : MonoBehaviour
    {
        private ParticleSystem _destroyPs;

        private void Awake()
        {
            _destroyPs = GetComponent<ParticleSystem>();
        }

        public void SetColor(Color color)
        {
            var mainModule = _destroyPs.main;
            mainModule.startColor = color;
        }
        public void Play(float destroyTime)
        {
            _destroyPs.Play();

            Destroy(this.gameObject, destroyTime);
        }
    }
}
