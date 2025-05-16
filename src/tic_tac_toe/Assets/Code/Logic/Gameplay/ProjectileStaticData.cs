using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ProjectileStaticData", menuName = "StaticData/new ProjectileStaticData")]
public class ProjectileStaticData : ScriptableObject
{
    public List<GameObject> prefabs;
    public int Lifetime;
    public Color Color;
}