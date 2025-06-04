using UnityEngine;

public abstract class Ability
{ 
    protected float Cooldown;
    protected bool IsCanUse = true;
    
    public virtual void Use() => Debug.Log("Ability used");
}

public enum AbilityId
{
    Bomb
}