using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Character;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour {

    private Animator _animator;
    private Rigidbody _body;

    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        _animator.SetBool("inMotion", !_body.IsSleeping());
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _animator.Play("characterAttacking");
        else if(Input.GetKeyDown(KeyCode.Mouse1))
            _animator.Play("characterBlocking");
        else if(Input.GetKeyDown(KeyCode.Z))
            _animator.Play("characterMentalBreakdown");
    }
}
