using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpinningBall
{
    public class UIGameover : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;
        [SerializeField] private Button _replayBtn;
      

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;



        private void OnEnable()
        {
            GameplayManager.OnGameOver += LoadScoreText;
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= LoadScoreText;
        }
        private void Start()
        {
            LoadScoreText();

            _homeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.GameplayScene);
            });

            _replayBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);
                Loader.Load(Loader.Scene.GameplayScene);
            });          
        }

        private void OnDestroy()
        {
            _replayBtn.onClick.RemoveAllListeners();
            _homeBtn.onClick.RemoveAllListeners();
        }


        private void LoadScoreText()
        {
            _scoreText.text = $"{GameManager.Instance.Score}";
        }

    }
}
