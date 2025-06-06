
public abstract class Ability
{ 
    protected float Cooldown;
    protected bool IsCanUse = true;

    public virtual void Use()
    {
        
    }
}

public enum AbilityId
{
    Bomb
}