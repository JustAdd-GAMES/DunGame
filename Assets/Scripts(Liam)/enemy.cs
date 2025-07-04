using UnityEngine;


public abstract class Enemy : ScriptableObject
{
    public enum EnemyType
    {
        Melee,
        Ranged,
        Boss
    }
    public int EnemyId;
    public string EnemyName;
    public float EnemyHealth;
    public float EnemyDamage;
    public float EnemySpeed;
    public float EnemyDifficulty;
    public Sprite EnemyIcon;
    public EnemyType Type;

}