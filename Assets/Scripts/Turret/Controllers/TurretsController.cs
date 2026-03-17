using System;
using System.Collections.Generic;
using Game.Turret;
using Zenject;

public class TurretsController : ITurretsController, IInitializable, ITickable, IDisposable
{
    private readonly List<ITurret> _turrets;

    public TurretsController()
    {
        _turrets = new List<ITurret>();
    }

    public void Dispose()
    {
        foreach (ITurret turret in _turrets)
            turret.Dispose();
    }

    public void Initialize()
    {
    }

    public void Register(ITurret turret)
    {
        _turrets.Add(turret);
    }

    public void Tick()
    {
        foreach (ITurret turret in _turrets)
            turret.Update();
    }
}
