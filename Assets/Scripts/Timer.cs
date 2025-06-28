using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Animator _animatior;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private List<TimerData> _timerDataList;
    private string _animationName = "TimerShowSeconds";
    [SerializeField]
    private UnityEvent _onTimerEnd;
    private Coroutine _timerCorutine;

    public void StartTimer(int index)
    {
        _timerCorutine = StartCoroutine(TimerCoroutine(index));
    }

    private IEnumerator TimerCoroutine(int index)
    {
        while (index >= 0)
        {
            _image.sprite = _timerDataList[index].sprite;
            _animatior.Play(_animationName);
            SoundManager.instance.Play(_timerDataList[index].soundName);
            yield return new WaitForSeconds(1f);
            index--;
        }
        _onTimerEnd?.Invoke();
        _timerCorutine = null;

    }
    public void StopTImer()
    {
        if (_timerCorutine != null)
        {
            StopCoroutine(_timerCorutine);
            _timerCorutine = null;
        }
        _image.sprite = null;
    }
}

[System.Serializable]
public class TimerData
{
    public Sprite sprite;
    public string soundName;
}
