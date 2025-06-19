using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
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

    public void InitializerFighter()
    {
        _onFighterInitialized?.Invoke();
    }

 
}