using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TankStaticData", menuName = "StaticData/new TankStaticData")]
public class TankStaticData : ScriptableObject
{
    public int Cooldown;
    public int ShootForce;
    
}