using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace SpinningBall
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _soundBtn;
        [SerializeField] private Button _musicBtn;


        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _recordText;

        [Header("Images")]
        [SerializeField] private Image _musicBtnIcon;
        [SerializeField] private Image _soundBtnIcon;


        [Header("Colors")]
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _deactiveColor;

        private void OnEnable()
        {
            GameplayManager.OnGameOver += LoadRecordText;
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= LoadRecordText;
        }


        private void Start()
        {
            LoadRecordText();
            UpdateMusicUI();
            UpdateSoundFXUI();

            _playBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
            });


            _musicBtn.onClick.AddListener(() =>
            {
                ToggleMusic();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _soundBtn.onClick.AddListener(() =>
            {
                ToggleSFX();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _soundBtn.onClick.RemoveAllListeners();
            _musicBtn.onClick.RemoveAllListeners();
        }

        private void LoadRecordText()
        {
            _recordText.text = $"BEST {GameManager.Instance.Record}";
        }

        private void ToggleSFX()
        {

            SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
            SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;

            UpdateSoundFXUI();
        }


        private void UpdateSoundFXUI()
        {
            if (SoundManager.Instance.isSoundFXActive)
            {
                _soundBtnIcon.color = _activeColor;
            }
            else
            {
                _soundBtnIcon.color = _deactiveColor;
            }
        }

        private void ToggleMusic()
        {
            SoundManager.Instance.MuteBackground(SoundManager.Instance.isMusicActive);
            SoundManager.Instance.isMusicActive = !SoundManager.Instance.isMusicActive;

            UpdateMusicUI();
        }

        private void UpdateMusicUI()
        {
            if (SoundManager.Instance.isMusicActive)
            {
                _musicBtnIcon.color = _activeColor;
            }
            else
            {
                _musicBtnIcon.color = _deactiveColor;
            }
        }
    }
}
