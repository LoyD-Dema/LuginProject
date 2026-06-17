using System;
using UnityEngine;

/// <summary>
/// A collection of GLOBAL EVENTS to regulate combat.
/// </summary>
public static class CombatEvents
{
    public static Action<int> OnEnemyKilled;
    public static Action<XPPickUp> OnExperiencePickUp;
}
