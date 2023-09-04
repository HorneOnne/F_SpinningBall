using System.Collections.Generic;
using UnityEngine;

namespace SpinningBall
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _bounceStrength = 5.0f;
        [SerializeField] private DestroyParticle _destroyPSPrefab;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;


        [SerializeField] private ColorType _colorType;
        public ColorType ColorType { get => _colorType; }

        private Dictionary<ColorType, string> _colorDict = new Dictionary<ColorType, string>()
        {
            {ColorType.CrimsonRed, "#FF6860" },
            {ColorType.PeachSorbet, "#FFB380" },
            {ColorType.EmeraldGreen, "#29E072" },
            {ColorType.AquaSky, "#20DAE0" },
            {ColorType.CharcoalGray, "#404040" },
            {ColorType.OceanBlue, "#2286DE" },
            {ColorType.RoyalPlum, "#912BE1" },
            {ColorType.ElectricPink, "#EE3FDD" },
        };


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();   
        }

        private void Start()
        {
            GameplayManager.OnPlaying += SetActiveDynamicPhysicBall;
        }

        private void PlayVFX()
        {
            var destroyPs = Instantiate(_destroyPSPrefab, transform.position, Quaternion.identity);
            destroyPs.SetColor(GetColorByColorType(ColorType));
            destroyPs.Play(1f);

            Destroy(this.gameObject);
        }

        private void SetActiveDynamicPhysicBall()
        {
            _rb.isKinematic = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (GameplayManager.Instance.CurrentState != GameplayManager.GameState.PLAYING) return;

            _rb.velocity = Vector2.up * _bounceStrength;
            Platform platform;
            if (collision.collider.gameObject.TryGetComponent<Platform>(out platform))
            {
                if (ColorType == platform.ColorType)
                {
                    SoundManager.Instance.PlaySound(SoundType.ScoreUp, false);

                    platform.PlayAnimation();
                    _colorType = Utilities.GetRandomEnum<ColorType>();
                    _sr.color = GetColorByColorType(_colorType);

                    GameManager.Instance.ScoreUp();
                }
                else
                {
                    SoundManager.Instance.PlaySound(SoundType.Destroyed, false);
                    platform.PlayAnimation();

                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                    PlayVFX();

                    
                }           
            }
        }

       
     
        private void OnDestroy()
        {
            GameplayManager.OnPlaying -= SetActiveDynamicPhysicBall;
        }



        #region Utilities

        private Color GetColorByColorType(ColorType colorType)
        {
            if(_colorDict.ContainsKey(colorType))
            {
                return HexToColor(_colorDict[colorType]);
            }
            return Color.clear;
        }

        private static Color HexToColor(string hex)
        {
            // Remove the '#' character if present
            hex = hex.TrimStart('#');

            // Check if the hex code is valid
            if (hex.Length != 6)
            {
                Debug.LogError("Invalid hex color code. It should be 6 characters long.");
                return Color.white; // Return white as a default color
            }

            // Parse the hex code to RGB values
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            // Create and return the Color object
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f, 1.0f);
        }
        #endregion
    }
}
