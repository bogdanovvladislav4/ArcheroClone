using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponButton : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private int typeOfWeapon;

    public void ChangeWeaponClick()
    {
        character.ChangeWeapon(typeOfWeapon);
    }
}
