using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnBoardStable;
    public static event Action OnInputDisabled;
    public static event Action OnInputEnabled;

    public static void BoardStable() => OnBoardStable?.Invoke();
    public static void InputDisabled() => OnInputDisabled?.Invoke();
    public static void InputEnabled() => OnInputEnabled?.Invoke();
    
}
