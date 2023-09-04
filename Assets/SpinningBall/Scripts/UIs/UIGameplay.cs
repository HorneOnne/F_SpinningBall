using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpinningBall
{
    public class UIGameplay : CustomCanvas
    {
        public static event System.Action OnTurnLeftBtnClicked;
        public static event System.Action OnTurnRightBtnClicked;

        [Header("Buttons")]
        [SerializeField] private Button _leftBtn;
        [SerializeField] private Button _rightBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;



        private void OnEnable()
        {
            GameManager.OnScoreUp += LoadScoreText;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= LoadScoreText;
        }


        private void Start()
        {
            LoadScoreText();

            _leftBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Turn, false);

                OnTurnLeftBtnClicked?.Invoke();
            });

            _rightBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Turn, false);

                OnTurnRightBtnClicked?.Invoke();
            });

        }

        private void OnDestroy()
        {
            _leftBtn.onClick.RemoveAllListeners();
            _rightBtn.onClick.RemoveAllListeners();
        }



        private void LoadScoreText()
        {
            _scoreText.text = $"SCORE {GameManager.Instance.Score}";
        }
    }
}
