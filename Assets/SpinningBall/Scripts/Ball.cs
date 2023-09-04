using System.Collections.Generic;
using UnityEngine;

namespace SpinningBall
{
    public class Ball : MonoBehaviour
    {
        public float bounceStrength = 5.0f;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _rb.velocity = Vector2.up * bounceStrength;

            Platform platform;
            if (collision.collider.gameObject.TryGetComponent<Platform>(out platform))
            {
                if (ColorType == platform.ColorType)
                {
                    Debug.Log("Score up");
                    _colorType = Utilities.GetRandomEnum<ColorType>();
                    _sr.color = GetColorByColorType(_colorType);
                }
                else
                {
                    Debug.Log("Game over");

                }           
            }
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
