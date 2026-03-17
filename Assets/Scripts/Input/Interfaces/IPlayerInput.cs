using System;
using UnityEngine;

namespace Common.Input
{
    public interface IPlayerInput
    {
        event Action<Vector2> OnFirstTouch;
        event Action<Vector2> OnMouseDown;
        event Action OnSpacePress;
        event Action OnSpaceRelease;

        Vector2 TouchFirstPosition { get; }
        Vector2 MousePositionValue { get; }
    }
}