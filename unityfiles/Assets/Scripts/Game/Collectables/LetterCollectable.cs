﻿using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Arguments for letter collected event
/// </summary>
public class LetterCollectedArgs : EventArgs {
    public string Letter { get; set; }
}

public class LetterCollectable : Collectable {
    public static event EventHandler<LetterCollectedArgs> OnLetterCollected;

    [SerializeField]
    private TMP_Text letterComponent;

    private MoveToGuiElement moveToGuiElement;

    public string LetterString {
        get => letterComponent.text;
    }

    private void OnDestroy() {
        LetterCollectedArgs letterEventArgs = new LetterCollectedArgs();
        letterEventArgs.Letter = this.LetterString;
        LetterCollectable.OnLetterCollected?.Invoke(this, letterEventArgs);
    }

    private void Awake() {
        this.letterComponent.SetText(SudoRandomLetterGenerator.Instance.GenerateLetter());
        if (TryGetComponent(out moveToGuiElement)) {
            moveToGuiElement.FindTarget<LettersCollectedGUI>();
        }
    }

}