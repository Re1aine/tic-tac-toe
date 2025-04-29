using UnityEngine;

public class MenuSceneProvider : IMenuSceneProvider
{
    public Collider SpawnZone { get; }
    public Camera StaticCamera {get; }
    public Camera OrbitCamera { get; }

    public MenuSceneProvider(Collider spawnZone, Camera staticCamera, Camera orbitCamera)
    {
        StaticCamera = staticCamera;
        OrbitCamera = orbitCamera;
        SpawnZone = spawnZone;
    }
}

public interface IMenuSceneProvider
{
    Collider SpawnZone { get; }
    public Camera StaticCamera {get; }
    public Camera OrbitCamera { get; }
}
