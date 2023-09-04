using UnityEngine;

namespace SpinningBall
{
    public class Platform: MonoBehaviour
    {

        [SerializeField] private ColorType _colorType;
        public ColorType ColorType { get => _colorType; }
    }
}
