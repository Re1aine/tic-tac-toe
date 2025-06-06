using System;
using System.Collections;
using UnityEngine;
using VContainer;

public class SafeContainer : MonoBehaviour, IPausable, IRaycastable
{
    public event Action<SafeContainer> Destroyed;
    
    private IPauseService _pauseService;
    
    private bool _isPaused;

    [Inject]
    public void Construct(IPauseService pauseService)
    {
        _pauseService = pauseService;
    }
    
    private void Start() => _pauseService.Add(this);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Figure>(out var figure)) 
            figure.FigureModificator = FigureModificator.UnForcable;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Figure>(out var figure)) 
            figure.FigureModificator = FigureModificator.None;
    }

    public Coroutine StartMoveTo(Vector3 from, Vector3 to, float speed)
    {
        return StartCoroutine(MoveTo(from, to, speed));
    }

    private IEnumerator MoveTo(Vector3 from, Vector3 to, float speed)
    {
        float journeyLength = Vector3.Distance(from, to);
        float elapsedTime = 0f;

        while (true)
        {
            yield return new WaitWhile(() => _isPaused);
            
            elapsedTime += Time.deltaTime;
            
            float distanceCovered = elapsedTime * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(from, to, fractionOfJourney);
            
            if (fractionOfJourney >= 1f) break;

            yield return null;
        }
    }

    public void Pause() => _isPaused = true;

    public void UnPause() => _isPaused = false;
    
    public void Destroy()
    {
        Destroyed?.Invoke(this);
        _pauseService.Remove(this);
        Destroy(gameObject);
    }
}