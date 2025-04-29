using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private FigureType _figureType;

    public FigureType FigureType => _figureType;
}

public enum FigureType
{
    Circle = 0,
    Cross = 1
}