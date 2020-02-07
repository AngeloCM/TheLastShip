using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public enum ControlScheme
    {
        classic,
        frontline,
        frontlineBeta,
    }

    public static ControlScheme CurrentControlScheme = ControlScheme.classic;

    public static bool InvertYLook;

    public static bool IsPaused;
}
