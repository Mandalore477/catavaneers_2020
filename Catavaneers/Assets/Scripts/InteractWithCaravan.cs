﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCaravan : MonoBehaviour
{
    Transform caravan_tf;
    Transform part_slot_tf;

    [SerializeField] string Place_Part_str = "Place_Part_P1"; //replace P1 in inspecter with P2, P3, P4 acordingly

    Caravan caravan;

    CharacterControl char_control;

    void Start()
    {
        char_control = GetComponent<CharacterControl>();
    }

    /*
    Purpose:                Interact with caravan (Add and Remove Parts).
    Effects:                Effects from AddToCaravan() and RemoveFromCaravan().
    Input/Output:           Keyboard input "E" to add & "R" to remove. Output N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_control (Class InteractWithCaravan), 
                            transform, parts_tf (Class Caravan), transform (Class Part), has_object (Class CharacterControl).
    */
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Caravan")
        {
            caravan_tf = c.gameObject.transform;

            caravan = caravan_tf.GetComponentInParent<Caravan>();

            if (Input.GetButtonDown(Place_Part_str))
            {
                if (char_control.has_object)
                {
                    AddToCaravan();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (!char_control.has_object)
                {
                    RemoveFromCaravan();
                }
            }
        }
    }

    /*
    Purpose:                Remove object from caravan.
    Effects:                Part's parent is now attach_point of the capsule (capsule is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, parts_tf, caravan, char_control (Class Caravan), transform (Class Part), 
                            has_object (Class CharacterControl)
    */
    private void RemoveFromCaravan()
    {
        Transform part_tf = caravan.FindPartSlot();

        if (part_tf != null)
        {
            part_tf.GetComponent<Part>().Drop();
            caravan.parts_tf.RemoveLast();
        }
        else Debug.Log("No part attached to caravan");
    }

    /*
    Purpose:                Add object to caravan.
    Effects:                Part's parent is now 1 of 12 transforms children of the caravan (caravan is now grandparent!)
    Input/Output:           N/A.
    Global Variables Used:  caravan_tf, part_slot_tf, caravan, char_control (Class InteractWithCaravan), transform, parts_tf (Class Caravan),
                            transform (Class Part), has_object (Class CharacterControl)
    */
    void AddToCaravan()
    {
        Transform part_tf = transform.GetChild(0).GetChild(0);

        part_slot_tf = caravan.FindPartSlot(part_tf);
        part_tf.GetComponent<Part>().AttachTo(part_slot_tf);

        caravan.parts_tf.AddFirst(part_tf);
        char_control.has_object = false;
    }
}
