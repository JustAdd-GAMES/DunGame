// DamageData.cs
using UnityEngine;

public struct DamageData
{
    public float amount;          // Base damage amount
    public GameObject source;     // Who dealt the damage
    public GameObject target;     // Who received the damage
    public Vector3 hitPoint;      // World position of impact

    public bool isCritical;       // Critical hit flag

    // Constructor for easy initialization
    public DamageData(float amount, GameObject source, GameObject target, Vector3 hitPoint, bool isCritical = false)
    {
        this.amount = amount;
        this.source = source;
        this.target = target;
        this.hitPoint = hitPoint;
        this.isCritical = isCritical;
    }
}
