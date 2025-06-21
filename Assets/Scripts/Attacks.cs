using UnityEngine;
 
[CreateAssetMenu(fileName = "Attacks", menuName = "Scriptable Objects/Attacks")]
public class Attacks : ScriptableObject
{
    public Attack[] attacks;
 
    public Attack getRandomAttack()
    {
        if (attacks == null || attacks.Length == 0)
        {
            Debug.LogWarning("No Attacks Available");
            return null;
        }
        int randomIndex = Random.Range(0, attacks.Length);
        return attacks[randomIndex];    
    }
 
}
[System.Serializable]
public class Attack
{
    public string attackName;
    public float minDamage;
    public float MaxDamage;
    public float attackTime;
    public string animationName;
    public string soundName;
    public GameObject hitPatrticlesPrefab;
    public GameObject particlesPrefab;
}
 
 