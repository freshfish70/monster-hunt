﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioListner : AudioListner {
    [SerializeField] 
    private Sound attackPlayerSound;

    private void Awake() {
        SubscribeToEvents();
    }

    private void SubscribeToEvents() {
        EnemyBehaviour.EnemyBehaviourStateChangeEvent += CallbackEnemyBehaviourStateChangeEvent;
    }

    private void CallbackEnemyBehaviourStateChangeEvent(object o, EnemyBehavourChangeArgs args) {
        if (args.NewBehaviourState == EnemyBehaviour.BehaviourState.ATTACK) {
            PlaySound(attackPlayerSound);
        }
    }
}