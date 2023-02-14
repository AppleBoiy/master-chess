using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarodev : HeroUnitBase {
    [SerializeField] private AudioClip _someSound;

    void Start() {
        // Example usage of a static system
        AudioSystem.Instance.PlaySound(_someSound);
    }
    
    public override void ExecuteMove() {
        // Perform tarodev specific animation, do damage, move etc.
        // You'll obviously need to accept the move specifics as an argument to this function. 
        // I go into detail in the Grid Game #2 video
        base.ExecuteMove(); // Call this to clean up the move
    }
}
