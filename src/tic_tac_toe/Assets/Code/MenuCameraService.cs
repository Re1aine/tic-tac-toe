using System.Collections;
using UnityEngine;

public class MenuCameraService : ICameraService
{
    private const float SwitchMinDuration = 15f;
    private const float SwitchMaxDuration = 30f;
    
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IMenuSceneProvider _menuSceneProvider;

    private readonly Camera _orbitCamera;
    private readonly Camera _staticCamera;
    
    public MenuCameraService(ICoroutineRunner coroutineRunner, IMenuSceneProvider menuSceneProvider)
    {
        _coroutineRunner = coroutineRunner;
        _menuSceneProvider = menuSceneProvider;
    }

    public void Activate()
    {
        _coroutineRunner.StartCoroutine(SwitchCameraRoutine(), CoroutineScopes.Menu);
    }

    public void DisActivate()
    {
        
    }

    private IEnumerator RotateOrbitCamera(float duration)
    {
        while (duration >= 0)
        {
            _menuSceneProvider.OrbitCamera.transform.RotateAround(Vector3.zero, Vector3.up, 5 * Time.deltaTime);
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    private void SwitchOnOrbitCamera(bool isActive)
    {
        _menuSceneProvider.OrbitCamera.gameObject.SetActive(isActive);
        _menuSceneProvider.StaticCamera.gameObject.SetActive(!isActive);  
    }

    private IEnumerator SwitchCameraRoutine()
    {
        while (true)
        {
            SwitchOnOrbitCamera(true);
            
            yield return _coroutineRunner.StartCoroutine(RotateOrbitCamera(Random.Range(SwitchMinDuration, SwitchMaxDuration)),
                CoroutineScopes.Menu);
            
            SwitchOnOrbitCamera(false);
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }
}

public interface ICameraService
{
    void Activate();
    void DisActivate();
}