using System;
using System.Collections;
using System.Runtime.InteropServices;
using Assets.Scripts.Character;
using Assets.Scripts.World;
using UnityEngine;

// Created by:
// Eelco Eikelboom
// S1080542
// Jordi Wolthuis
// s1085303
[RequireComponent(typeof(Character))]
public class CharacterAnimationController : MonoBehaviour {
    private Animator _animator;
    private Rigidbody _body;
    private CharacterWeaponController _weaponController;
    private Character _character;

    [SerializeField]
    public AnimationClip WalkingAnimationClip;
    [SerializeField]
    public AnimationClip AttackAnimationClip;

    // Use this for initialization
    private void Start() {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _weaponController = FindObjectOfType<CharacterWeaponController>();
        _character = GameManager.Instance.Character;
        LoadAnimationEvent();

        
    }

    // Update is called once per frame
    private void Update() {
        _animator.SetBool("inMotion", !_body.IsSleeping());

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            _animator.Play(_weaponController.GetWeaponAnimation());
            _weaponController.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            _animator.Play("characterBlocking");
        else if (Input.GetKeyDown(KeyCode.Z))
            _animator.Play("characterMentalBreakdown");
        
    }

    private void LoadAnimationEvent() {
        if (WalkingAnimationClip != null) {
            WalkingAnimationClip.AddEvent(new AnimationEvent { functionName = "OnCharacterWalk" });
        }
        if (AttackAnimationClip != null){
            AttackAnimationClip.AddEvent(new AnimationEvent { functionName = "OnCharacterAttack" });
        }
    }

    private void OnCharacterWalk() {
        _character.PlayWalkingSound();
    }

    private void OnCharacterAttack(){
        _character.PlayAttackSound();
    }

}