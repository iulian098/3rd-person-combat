using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    [SerializeField] string animationName;
    [SerializeField] float transitionDuration;
    [SerializeField] int comboStateIndex = -1;
    [SerializeField] float comboAttackTime;
    [SerializeField] float forceTime;
    [SerializeField] float force;

    public string AnimationName => animationName;
    public float TransitionDuration => transitionDuration;
    public int ComboStateIndex => comboStateIndex;
    public float ComboAttackTime => comboAttackTime;
    public float ForceTime => forceTime;
    public float Force => force;

}
