using System;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldTimeManager : MonoBehaviour
    {
        public Action<bool> OnPauseStateChanged;
        public Action<int, int> OnTimeChanged;
        public Action<int, int, int> OnDateChanged;

        private bool _paused = true;

        [SerializeField] private float _timeSpeedMultiplier = 600f;
        [SerializeField] private float _timeScale = 1f;
        [SerializeField] private float _slowedMultiplier = 0.5f;
        [SerializeField] private float _acceleratedMultiplier = 2f;
        [SerializeField] private float _superAcceleratedMultiplier = 4f;

        private TimeScaleState _currentScaleState;
        private float _second;
        private int _minute;
        private int _hour;
        private int _day;
        private int _month;
        private int _year;

        public float TimeScale => _timeScale;
        public float Second => _second;
        public int Minute => _minute;
        public int Hour => _hour;
        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
        public bool Paused => _paused;

        public void Initialize()
        {
            _currentScaleState = TimeScaleState.Normal;
        }

        public void HandleUpdate()
        {
            if (_paused)
            {
                return;
            }
            _second += _timeSpeedMultiplier * Time.deltaTime;
            if (_second >= 60f)
            {
                _second -= 60f;
                AddMinute();
            }
        }

        public void AccelerateTimeScale()
        {
            switch (_currentScaleState)
            {
                case TimeScaleState.Slowed:
                    _currentScaleState = TimeScaleState.Normal;
                    _timeScale = 1f;
                    break;
                case TimeScaleState.Normal:
                    _currentScaleState = TimeScaleState.Accelerated;
                    _timeScale = _acceleratedMultiplier;
                    break;
                case TimeScaleState.Accelerated:
                    _currentScaleState = TimeScaleState.SuperAccelerated;
                    _timeScale = _superAcceleratedMultiplier;
                    break;
                case TimeScaleState.SuperAccelerated:
                    _currentScaleState = TimeScaleState.Slowed;
                    _timeScale = _slowedMultiplier;
                    break;
            }
            Time.timeScale = _timeScale;
        }

        public void DecelerateTimeScale()
        {
            switch (_currentScaleState)
            {
                case TimeScaleState.Slowed:
                    _currentScaleState = TimeScaleState.SuperAccelerated;
                    _timeScale = _superAcceleratedMultiplier;
                    break;
                case TimeScaleState.Normal:
                    _currentScaleState = TimeScaleState.Slowed;
                    _timeScale = _slowedMultiplier;
                    break;
                case TimeScaleState.Accelerated:
                    _currentScaleState = TimeScaleState.Normal;
                    _timeScale = 1f;
                    break;
                case TimeScaleState.SuperAccelerated:
                    _currentScaleState = TimeScaleState.Accelerated;
                    _timeScale = _acceleratedMultiplier;
                    break;
            }
            Time.timeScale = _timeScale;
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
            OnPauseStateChanged?.Invoke(_paused);
        }

        public void UnpauseGame()
        {
            _paused = false;
            // other logic
            OnPauseStateChanged?.Invoke(_paused);
        }

        public void AddMinute(int amount = 1)
        {
            _minute += amount;
            while (_minute >= 60)
            {
                _minute -= 60;
                AddHour();
            }
            OnTimeChanged?.Invoke(_minute, _hour);
        }

        public void AddHour(int amount = 1)
        {
            _hour += amount;
            while (_hour >= 24)
            {
                _hour -= 24;
                AddDay();
            }
            OnTimeChanged?.Invoke(_minute, _hour);
        }

        public void AddDay(int amount = 1)
        {
            _day += amount;
            while (_day > 30)
            {
                _day -= 30;
                AddMonth();
            }
            OnDateChanged?.Invoke(_day, _month, _year);
        }

        public void AddMonth(int amount = 1)
        {
            _month += amount;
            while (_month > 12)
            {
                _month -= 12;
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