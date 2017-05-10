using Assets.Scripts.Character;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private float _interactionRadius;
    private readonly Character _character;


    public CharacterInteraction(Character character)
    {
        _character = character;
        _interactionRadius = 10.0f;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        Collider[] NearbyGameObjects = Physics.OverlapSphere(
            _character.transform.position,
            _interactionRadius
            );

        foreach (var go in NearbyGameObjects)
        {
            print(go.ToString());
        }

        // Check if prop is nearby (withtin x distance)
        // https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
        // Check if gameobjects of layermask 'Props' are within the sphere

        // If multiple prop are nearby sort by how close they are

        // Show on screen 'Search {propname}'

        // If prop has not been interacted with continue
        // If 'E' is pressed try to interact with the prop
        // If inventory is full drop gathered item on the ground
        // Else put item in inventory
        // Notify user what happened on screen:
        // '{itemname} found'
        // If inventory full also: 'Inventory is full, the {itemname} is dropped on the ground'
    }
}