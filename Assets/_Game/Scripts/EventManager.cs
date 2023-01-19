using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    #region UpdateHealthUI
    public delegate void UpdateHealthUI(int health);
    public static event UpdateHealthUI onUpdateHealthUI;
    public static void InvokeUpdateHealthUI(int health) => onUpdateHealthUI?.Invoke(health);
    #endregion
}
