using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class RandomHelper
{
    public static T GetRandomEnumValue<T>(bool exceptNone = false) where T : Enum
    {
        Array enumValues = Enum.GetValues(typeof(T));
        int randomIndex = Random.Range(exceptNone ? 1 : 0, enumValues.Length);
        return (T)enumValues.GetValue(randomIndex);
    }

    public static Vector3 GetRandomPositionInCollider(Collider collider)
    {
        float x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float z = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        
        return new Vector3(x, y, z);
    }

    public static Vector3 GetRandomPositionInCollider(Collider collider, bool excludeX, bool excludeY, bool excludeZ)
    {
        Vector3 min = collider.bounds.min;
        Vector3 max = collider.bounds.max;
    
        float x = excludeX ? collider.transform.position.x : Random.Range(min.x, max.x);
        float y = excludeY ? collider.transform.position.y : Random.Range(min.y, max.y);
        float z = excludeZ ? collider.transform.position.z : Random.Range(min.z, max.z);
    
        return new Vector3(x, y, z);
    }
    
    public static Quaternion GetRandomRotation()
    {
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = Random.Range(0f, 360f);
        
        return new Quaternion(x, y, z, 0f);
    }
}