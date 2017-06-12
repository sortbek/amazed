using Assets.Scripts.Character;
using UnityEngine;

// Created by:
// Eelco Eikelboom
// S1080542
public class CharacterAnimationController : MonoBehaviour {
    public Animator Animator;
    private Rigidbody _body;
    private CharacterWeaponController _weaponController;

    // Use this for initialization
    private void Start() {
        Animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
        _weaponController = FindObjectOfType<CharacterWeaponController>();
    }

    // Update is called once per frame
    private void Update() {
        Animator.SetBool("inMotion", !_body.IsSleeping());

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Animator.Play(_weaponController.GetWeaponAnimation());
            _weaponController.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            Animator.Play("characterBlocking");
        else if (Input.GetKeyDown(KeyCode.Z))
            Animator.Play("characterMentalBreakdown");
    }
}