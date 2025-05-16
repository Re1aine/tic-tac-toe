using UnityEngine;

[CreateAssetMenu(fileName = "new TimerStaticData", menuName = "StaticData/new TimerStaticData")]
public class TimerStaticData : ScriptableObject
{
    public Color NormalColor = Color.white;
    public Color WarningColor = Color.yellow;
    public Color DangerColor = Color.red;
    public float RoundDuration;
    public float DangerThreshold;
    public float WarningThreshold;
}