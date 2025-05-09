using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UIFactory : IUIFactory
{
    private readonly IObjectResolver _objectResolver;

    private GameplayUI _gameplayUI;
    
    public UIFactory(IObjectResolver objectResolver) => 
        _objectResolver = objectResolver;

    public GameplayUI CreateHud()
    {
        var prefab = Resources.Load<GameplayUI>("UI/GameplayUI");
        var hud = _objectResolver.Instantiate(prefab);

        _gameplayUI = hud;
        
        return hud;
    }

    public GameObject CreateAbilityWindow()
    {
        var abilityPanel = AssetProvider.InstantiateGameObject("AbilityPanel");
        return abilityPanel;
    }

    public ResultWindow CreateResultWindow()
    {
        var prefab = Resources.Load<ResultWindow>("UI/ResultWindow");
        var resultWindow = _objectResolver.Instantiate(prefab, _gameplayUI.Root);
        resultWindow.SetResultText();
        return resultWindow;
    }

    public PauseWindow CreatePauseWindow()
    {
        var prefab = Resources.Load<PauseWindow>("UI/PauseWindow");
        return _objectResolver.Instantiate(prefab, _gameplayUI.Root);
    }
}

public interface IUIFactory
{
    GameplayUI CreateHud();

    GameObject CreateAbilityWindow();

    ResultWindow CreateResultWindow();

    PauseWindow CreatePauseWindow();
}