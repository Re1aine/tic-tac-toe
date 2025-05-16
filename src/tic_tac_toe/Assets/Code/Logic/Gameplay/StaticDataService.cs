using UnityEngine;

public class StaticDataService : IStaticDataService
{
    public TankStaticData TankStaticData { get; }
    public ProjectileStaticData ProjectileStaticData { get; }
    public TimerStaticData TimerStaticData { get; }
    public FigureStaticData FigureStaticData { get; }

    public StaticDataService()
    {
        TankStaticData = Resources.Load<TankStaticData>("StaticData/TankStaticData");
        ProjectileStaticData = Resources.Load<ProjectileStaticData>("StaticData/ProjectileStaticData");
        TimerStaticData = Resources.Load<TimerStaticData>("StaticData/TimerStaticData");
        FigureStaticData = Resources.Load<FigureStaticData>("StaticData/FigureStaticData");
    }
}

public interface IStaticDataService
{
    TankStaticData TankStaticData { get; }
    ProjectileStaticData ProjectileStaticData { get; }
    TimerStaticData TimerStaticData { get; }
    FigureStaticData FigureStaticData { get; }
}