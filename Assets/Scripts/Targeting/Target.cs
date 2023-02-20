using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Action<Target> OnDestroyed;

    bool isVisible;

    public bool IsVisible => isVisible;

    private void OnDestroy() {
        OnDestroyed?.Invoke(this);
    }

    private void OnBecameVisible() {
        isVisible = true;
    }

    private void OnBecameInvisible() {
        isVisible = false;
    }
}
