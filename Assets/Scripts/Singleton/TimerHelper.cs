using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
    public class TimerHelper : MonoBehaviour, ISingleton
    {
        
       [EditorButton()]
       public void SetTimerScale(float scale)
        {
            Time.timeScale = scale;
        }

        public Coroutine StartTimer(Action action, float time)
        {
            return StartCoroutine(Timer(action, time));
        }

        public void StopTimer(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
        public void StartTimer<T>(Action<T> action, float time, T param)
        {
            StartCoroutine(Timer(action, time,  param));
        }
        
        public void StartTimer<T>(List<Action<T>> actions, float time, T param)
        {
            StartCoroutine(Timer(actions, time,  param));
        } 
        public void StartTimer<T>(List<Action<T, bool>> actions, float time, T param)
        {
            StartCoroutine(Timer(actions, time,  param));
        }
        
        IEnumerator Timer(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
        
        IEnumerator Timer<T>(Action<T> action, float time, T param)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke(param);
        }
        
        IEnumerator Timer<T>(List<Action<T>> actions, float time, T param)
        {
            foreach (var action in actions)
            {
                yield return new WaitForSeconds(time);
                action?.Invoke(param);
            }
        } 
        IEnumerator Timer<T>(List<Action<T, bool>> actions, float time, T param)
        {
            foreach (var action in actions)
            {
                yield return new WaitForSeconds(time);
                action?.Invoke(param, true);
            }
        }
    }
}