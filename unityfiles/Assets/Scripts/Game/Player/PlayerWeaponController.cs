using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> availableWeapons;

    private GameObject currentWeapon;
    private int currentWeaponIndex;

    private GunController activeGunController;

    // -- properties -- //
    public List<GameObject> AvailableWeapons {
        get => availableWeapons;
        set => availableWeapons = value;
    }

    public GunController ActiveGunController {
        get => activeGunController;
        internal set => this.activeGunController = value;
    }

    // -- public -- //

    /// <summary>
    /// changes to the next weapon in the weapon list 
    /// if the end of the list is reached the index loops around
    /// </summary>
    /// <returns>The weapon controller of the new active weapon</returns>
    public GunController ChangeToNextWeapon(){return this.ChangeWeapon(1);}


    /// <summary>
    /// changes to the prev weapon in the weapon list 
    /// if the end of the list is reached the index loops around
    /// </summary>
    /// <returns>The weapon controller of the new active weapon</returns>
    public GunController ChangeToPrevWeapon(){return this.ChangeWeapon(-1);}
    // -- private -- //

    /// <summary>
    /// Changes the current weapon the given number of posissions
    /// If the current weapon passes one of the other indexes it loops around
    /// </summary>
    /// <param name="numChanges">the number of places in the weapon list to change the active weapon</param>
    /// <returns>the new active weapon controller</returns>
    private GunController ChangeWeapon(int numChanges){
        int newIndex = (this.AvailableWeapons.Count + numChanges) % (this.availableWeapons.Count -1);
        if (newIndex < 0){newIndex=this.availableWeapons.Count -1;}

        if (newIndex != this.currentWeaponIndex){
            this.currentWeaponIndex = newIndex;
            this.ActiveGunController = WUGameObjects.GetChildWithTagLike(this.availableWeapons[newIndex], "gun")[0].GetComponent<GunController>();
        }
        
        return this.activeGunController;
    }


}