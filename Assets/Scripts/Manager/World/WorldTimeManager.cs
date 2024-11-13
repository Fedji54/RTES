using System;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldTimeManager : MonoBehaviour
    {
        public Action<bool> OnGamePausedChanged;
        public Action<float> OnGameSpeedChanged;
        public Action<int, int> OnTimeChanged;
        public Action<int, int, int> OnDateChanged;

        private bool _paused = true;

        [SerializeField] private float _timeSpeedMultiplier = 600f;
        [SerializeField] private float _gameSpeed = 1f;
        [SerializeField] private float _slowStateMultiplier = 0.5f;
        [SerializeField] private float _fastStateMultiplier = 2f;
        [SerializeField] private float _veryFastStateMultiplier = 4f;
        [Header("Time Values")]
        [SerializeField] private int _secondsInMinute = 60;
        [SerializeField] private int _minutesInHour = 60;
        [SerializeField] private int _hoursInDay = 24;
        [SerializeField] private int _daysInMonth = 30;
        [SerializeField] private int _monthsInYear = 12;
        [Header("Move this to data config")]
        [SerializeField] private int _startingHour = 9;
        [SerializeField] private int _startingMinute = 25;
        [SerializeField] private int _startingDay = 2;
        [SerializeField] private int _startingMonth = 5;
        [SerializeField] private int _startingYear = 3947;

        private GameSpeedState _gameSpeedState;
        private float _second;
        private int _minute;
        private int _hour;
        private int _day;
        private int _month;
        private int _year;

        public float GameSpeed => _gameSpeed;
        public float Second => _second;
        public int Minute => _minute;
        public int Hour => _hour;
        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
        public bool Paused => _paused;

        public void Initialize()
        {
            _gameSpeedState = GameSpeedState.Normal;
            _hour = _startingHour;
            _minute = _startingMinute;
            _day = _startingDay;
            _month = _startingMonth;
            _year = _startingYear;
            OnGamePausedChanged?.Invoke(_paused);
            OnGameSpeedChanged?.Invoke(_gameSpeed);
            OnTimeChanged?.Invoke(_hour, _minute);
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        private void Update()
        {
            if (_paused)
            {
                return;
            }
            _second += _timeSpeedMultiplier * Time.deltaTime;
            if (_second >= _secondsInMinute)
            {
                _second -= _secondsInMinute;
                AddMinute();
            }
        }

        public void AccelerateTimeScale()
        {
            switch (_gameSpeedState)
            {
                case GameSpeedState.Slow:
                    _gameSpeedState = GameSpeedState.Normal;
                    _gameSpeed = 1f;
                    break;
                case GameSpeedState.Normal:
                    _gameSpeedState = GameSpeedState.Fast;
                    _gameSpeed = _fastStateMultiplier;
                    break;
                case GameSpeedState.Fast:
                    _gameSpeedState = GameSpeedState.VeryFast;
                    _gameSpeed = _veryFastStateMultiplier;
                    break;
                case GameSpeedState.VeryFast:
                    _gameSpeedState = GameSpeedState.Slow;
                    _gameSpeed = _slowStateMultiplier;
                    break;
            }
            Time.timeScale = _gameSpeed;
            OnGameSpeedChanged?.Invoke(_gameSpeed);
        }

        public void DecelerateTimeScale()
        {
            switch (_gameSpeedState)
            {
                case GameSpeedState.Slow:
                    _gameSpeedState = GameSpeedState.VeryFast;
                    _gameSpeed = _veryFastStateMultiplier;
                    break;
                case GameSpeedState.Normal:
                    _gameSpeedState = GameSpeedState.Slow;
                    _gameSpeed = _slowStateMultiplier;
                    break;
                case GameSpeedState.Fast:
                    _gameSpeedState = GameSpeedState.Normal;
                    _gameSpeed = 1f;
                    break;
                case GameSpeedState.VeryFast:
                    _gameSpeedState = GameSpeedState.Fast;
                    _gameSpeed = _fastStateMultiplier;
                    break;
            }
            Time.timeScale = _gameSpeed;
            OnGameSpeedChanged?.Invoke(_gameSpeed);
        }

        public void TogglePause()
        {
            if (_paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            _paused = true;
            // other logic
            OnGamePausedChanged?.Invoke(_paused);
        }

        public void UnpauseGame()
        {
            _paused = false;
            // other logic
            OnGamePausedChanged?.Invoke(_paused);
        }

        public void AddMinute(int amount = 1)
        {
            _minute += amount;
            while (_minute >= _minutesInHour)
            {
                _minute -= _minutesInHour;
                AddHour();
            }
            OnTimeChanged?.Invoke(_hour, _minute);
        }

        public void AddHour(int amount = 1)
        {
            _hour += amount;
            while (_hour >= _hoursInDay)
            {
                _hour -= _hoursInDay;
                AddDay();
            }
            OnTimeChanged?.Invoke(_hour, _minute);
        }

        public void AddDay(int amount = 1)
        {
            _day += amount;
            while (_day > _daysInMonth)
            {
                _day -= _daysInMonth;
                AddMonth();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddMonth(int amount = 1)
        {
            _month += amount;
            while (_month > _monthsInYear)
            {
                _month -= _monthsInYear;
                AddYear();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddYear(int amount = 1)
        {
            _year += amount;
            OnDateChanged?.Invoke(_day, _month, _year);
        }
    }
}