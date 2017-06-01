using Assets.Scripts.Character;
using UnityEngine;

// Created by:
// Eelco Eikelboom
// S1080542
public class CharacterAnimationController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody _body;
    private CharacterWeaponController _weaponController;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _weaponController = FindObjectOfType<CharacterWeaponController>();
    }

    // Update is called once per frame
    void Update() {
        _animator.SetBool("inMotion", !_body.IsSleeping());
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _animator.Play(_weaponController.GetWeaponAnimation());
        else if(Input.GetKeyDown(KeyCode.Mouse1))
            _animator.Play("characterBlocking");
        else if(Input.GetKeyDown(KeyCode.Z))
            _animator.Play("characterMentalBreakdown");
    }
}
