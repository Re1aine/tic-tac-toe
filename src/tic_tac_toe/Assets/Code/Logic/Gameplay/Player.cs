using UnityEngine;
using VContainer;

public class Player : MonoBehaviour
{
    private Camera _camera;
    private IGameplaySceneProvider _sceneProvider;
    private IFigureFactory _figureFactory;
    
    private void Awake() => _camera = Camera.main;

    [Inject]
    private void Construct(IGameplaySceneProvider sceneProvider, IFigureFactory figureFactory)
    {
        _sceneProvider = sceneProvider;
        _figureFactory = figureFactory;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IRaycastable raycastable))
            {
                float heightRelease = hit.transform.position.y + _sceneProvider.GameField.SpawnZone.transform.position.y;
                ReleaseFigure(new Vector3(hit.point.x, heightRelease, hit.point.z));
            }
        }
    }

    private void ReleaseFigure(Vector3 position) => 
        _figureFactory.CreateFigure(position, RandomHelper.GetRandomRotation());
}