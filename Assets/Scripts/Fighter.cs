using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    public string Name => _name;

    [SerializeField]
    private Health _health;
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private Attacks _attacks;
    public Health Health => _health;
    public Attacks attacks => _attacks;
    public Animator CharacterAnimator => _characterAnimator;

    [SerializeField]
    private UnityEvent _onFighterInitialized;

    [SerializeField]
    private string _winAnimationName = "win";
    public string WinAnimationName => _winAnimationName;
    [SerializeField]
    private string _winSoundName;
    public string WinSoundName => _winSoundName;

    public void InitializerFighter()
    {
        _onFighterInitialized?.Invoke();
    }

 
}