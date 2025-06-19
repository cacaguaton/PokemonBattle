using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
 
public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int _numberOffFighters = 2;
    [SerializeField]
    private UnityEvent _onfightersReady;
    [SerializeField]
    private UnityEvent _onBattleFinished;
    [SerializeField]
    private UnityEvent _onBattleStarted;
    private List<Fighter> _Fighters = new List<Fighter>();
    private Coroutine _BattleCouroutine;
    public void AddFighter(Fighter fighter)
    {
        _Fighters.Add(fighter);
        CheckFIghters();
    }
    public void RemoveFighter(Fighter fighter)
    {
        _Fighters.Remove(fighter);
        if (_BattleCouroutine != null)
            StopCoroutine(_BattleCouroutine);
        _BattleCouroutine = null;
    }
    private void CheckFIghters()
    {
        if (_Fighters.Count < _numberOffFighters)
        {
            return;
        }
        _onfightersReady?.Invoke();
        StartBattle();
    }
    public void StartBattle()
    {
        foreach (Fighter fighter in _Fighters)
        {
            fighter.InitializerFighter();
        }
        _BattleCouroutine = StartCoroutine(BattleCoroutine());
    }
    private IEnumerator BattleCoroutine()
    {
        _onBattleStarted?.Invoke();
        while (_Fighters.Count > 1)
        {
            Fighter attacker = _Fighters[Random.Range(0, _Fighters.Count)];
            Fighter defender = attacker;
            while (defender == attacker)
            {
                defender = _Fighters[Random.Range(0, _Fighters.Count)];
            }
            attacker.transform.LookAt(defender.transform);
            defender.transform.LookAt(attacker.transform);
            Attack attack = attacker.attacks.getRandomAttack();
            attacker.CharacterAnimator.Play(attack.animationName);
            SoundManager.instance.Play(attack.soundName);
            yield return new WaitForSeconds(attack.attackTime);
            defender.Health.TakeDamage(Random.Range(attack.minDamage, attack.MaxDamage));
            if (defender.Health.CurrentHealth <= 0)
            {
                _Fighters.Remove(defender);
               
            }
            yield return new WaitForSeconds(1f);
        }
        _onBattleFinished?.Invoke();
    }
}