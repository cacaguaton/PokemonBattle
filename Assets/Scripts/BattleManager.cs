using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int _numberOffFighters = 2;
    [SerializeField]
    private UnityEvent _onBattleStopped;
    [SerializeField]
    private UnityEvent _onBattleFinished;
    [SerializeField]
    private UnityEvent _onBattleStarted;
    private List<Fighter> _Fighters = new List<Fighter>();
    private Coroutine _BattleCouroutine;

    private DamageTarget _damageTarget = new DamageTarget();
    public void AddFighter(Fighter fighter)
    {
        MessageFrame.Instance.ShowMessage($"{fighter.name} has joined the battle!");
        _Fighters.Add(fighter);
        CheckFIghters();
    }
    public void RemoveFighter(Fighter fighter)
    {
        _Fighters.Remove(fighter);
        if (_BattleCouroutine != null)
        {
            StopCoroutine(_BattleCouroutine);
            _BattleCouroutine = null;

        }
        _onBattleStopped?.Invoke();
    }
    private void CheckFIghters()
    {
        if (_Fighters.Count < _numberOffFighters)
        {
            return;
        }
        _onBattleStarted?.Invoke();
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
            MessageFrame.Instance.ShowMessage($"{attacker.name} attacks with {attack.attackName}");
            attacker.CharacterAnimator.Play(attack.animationName);
            SoundManager.instance.Play(attack.soundName);
            Instantiate(attack.particlesPrefab, attacker.transform.position, Quaternion.identity);
            GameObject attackParticles = Instantiate(attack.particlesPrefab, attacker.transform.position, Quaternion.identity);
            attackParticles.transform.SetParent(attacker.transform);
            yield return new WaitForSeconds(attack.attackTime);
            float damage = Random.Range(attack.minDamage, attack.MaxDamage);
            GameObject defendParticles = Instantiate(attack.hitPatrticlesPrefab, defender.transform.position, Quaternion.identity);
            defendParticles.transform.SetParent(defender.transform);
            Instantiate(attack.hitPatrticlesPrefab, defender.transform);
            _damageTarget.SetDamageTarget(damage, defender.transform);
            defender.Health.TakeDamage(_damageTarget);
            if (defender.Health.CurrentHealth <= 0)
            {
                RemoveFighter(defender);
                if (_Fighters.Count == 1)
                {
                    yield return new WaitForSeconds(1f);
                    EndBattle(_Fighters[0]);
                }

            }
            yield return new WaitForSeconds(1f);
        }
        EndBattle(_Fighters[0]);
    }

    private void EndBattle(Fighter winner)
    {
        winner.transform.LookAt(Camera.main.transform);
        MessageFrame.Instance.ShowMessage($"{winner.Name} wins the battle!");
        SoundManager.instance.Play(winner.WinSoundName);
        winner.CharacterAnimator.Play(winner.WinAnimationName);
        _onBattleFinished?.Invoke();
    }
}