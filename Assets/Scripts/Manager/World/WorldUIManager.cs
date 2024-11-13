using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldUIManager : MonoBehaviour
    {
        [Header("Game Speed")]
        [SerializeField] private GameObject _gameSpeedWindow;
        [SerializeField] private GameObject _gamePausedIcon;
        [SerializeField] private TMP_Text _gameSpeedText;
        [Header("Time")]
        [SerializeField] private GameObject _timeWindow;
        [SerializeField] private TMP_Text _timeText;
        [Header("Date")]
        [SerializeField] private GameObject _dateWindow;
        [SerializeField] private TMP_Text _dateText;
        [Header("Interactable Action")]
        [SerializeField] private GameObject _interactableActionWindow;
        //[SerializeField] private TMP_Text _interactableText;
        [Header("Interactable Message")]
        [SerializeField] private GameObject _interactableMessageWindow;
        [SerializeField] private TMP_Text _interactableMessageText;

        public void Initialize()
        {
            WorldManager.StaticInstance.TimeManager.OnGamePausedChanged += OnGamePausedChanged;
            WorldManager.StaticInstance.TimeManager.OnGameSpeedChanged += OnGameSpeedChanged;
            WorldManager.StaticInstance.TimeManager.OnTimeChanged += OnTimeChanged;
            WorldManager.StaticInstance.TimeManager.OnDateChanged += OnDateChanged;
            WorldManager.StaticInstance.CameraManager.OnInteractableChanged += OnInteractableChanged;
        }

        public void ToggleGameSpeedVisible(bool visible)
        {
            _gameSpeedWindow.SetActive(visible);
        }

        private void OnGamePausedChanged(bool paused)
        {
            _gamePausedIcon.SetActive(paused);
        }

        private void OnGameSpeedChanged(float speed)
        {
            _gameSpeedText.text = $"x{speed:0.##}";
        }

        private void OnTimeChanged(int hour, int minute)
        {
            _timeText.text = $"{hour:00}:{minute:00}";
        }

        private void OnDateChanged(int day, int month, int year)
        {
            _dateText.text = $"{day:00}.{month:00}.{year:0000}";
        }

        private void OnInteractableChanged(Interactable interactable)
        {
            if (interactable != null)
            {
                _interactableMessageText.text = $"{interactable.Config.ActionName} {interactable.Config.DisplayName}";
                _interactableMessageWindow.SetActive(true);
            }
            else
            {
                _interactableMessageWindow.SetActive(false);
                _interactableMessageText.text = string.Empty;
            }
        }

        private void OnDestroy()
        {
            WorldManager.StaticInstance.TimeManager.OnGamePausedChanged -= OnGamePausedChanged;
            WorldManager.StaticInstance.TimeManager.OnGameSpeedChanged -= OnGameSpeedChanged;
            WorldManager.StaticInstance.TimeManager.OnTimeChanged -= OnTimeChanged;
            WorldManager.StaticInstance.TimeManager.OnDateChanged -= OnDateChanged;
            WorldManager.StaticInstance.CameraManager.OnInteractableChanged -= OnInteractableChanged;
        }
    }
}