using UnityEngine;

public interface IPoolable
{
    public bool IsActive { get; }

    public void Activate(Vector3 position, Quaternion rotation);
    public void Deactivate();
}
