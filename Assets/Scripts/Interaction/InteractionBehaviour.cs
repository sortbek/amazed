using System;
using Assets.Scripts.Character;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Radomly decide what item this prop holds
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Interact(Character actor)
    {
        print(String.Format("{0} interacts with {1}", ToString(), actor.ToString()));
    }
}