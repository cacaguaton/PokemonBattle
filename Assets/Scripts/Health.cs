using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class Health : MonoBehaviour
{
    [SerializeField]
    private Slider _healtSlider;
    [SerializeField]
    private float _InitialHealth = 200f;
    [SerializeField]
    private UnityEvent<float> _OnUpdateHealth;
    [SerializeField]
    private UnityEvent _onDefeated;
    [SerializeField]
    private UnityEvent<DamageTarget> _onTakeDamage;
    private float _currentHealth;
    public float CurrentHealth => _currentHealth;
 
    public void InitializeHealth()
    {
        _currentHealth = _InitialHealth;
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        _OnUpdateHealth?.Invoke(_currentHealth / _InitialHealth);
    }
    public void TakeDamage(DamageTarget damageTarget)
    {
        _currentHealth -= damageTarget.damage;
        _onTakeDamage?.Invoke(damageTarget);
        if (_currentHealth < 0)
        {
            _onDefeated?.Invoke();
            _currentHealth = 0;
        }

        UpdateHealth();
    }
}
 
 