﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// How a type of enemy is supposed to act
/// </summary>
public interface IEnemyBehavior
{
    /// <summary>
    /// What the enemy wil do in a single frame update
    /// </summary>
    void Act();

    void OnDead();
}
