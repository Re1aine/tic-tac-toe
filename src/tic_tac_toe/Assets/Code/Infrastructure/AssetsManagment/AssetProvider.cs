using UnityEngine;

public static class AssetProvider
{
    public static T Instantiate<T>(string prefabName) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public static T InstantiateAt<T>(string prefabName, Vector3 position) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
    
    public static T InstantiateAt<T>(string prefabName, Vector3 position, Quaternion quaternion) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, position, quaternion);
    }

    public static T Instantiate<T>(string prefabName, Transform parent) where T : MonoBehaviour
    {
        var prefab = Resources.Load<T>(prefabName);
        return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    }

    public static GameObject InstantiateGameObjectAt(string prefabName, Vector3 position)
    { 
        var prefab = Resources.Load<GameObject>(prefabName);
        return Object.Instantiate(prefab, position, Quaternion.identity);
    }
    public static GameObject InstantiateGameObject(string prefabName)
    { 
        var prefab = Resources.Load<GameObject>(prefabName);
        return Object.Instantiate(prefab);
    }
}
