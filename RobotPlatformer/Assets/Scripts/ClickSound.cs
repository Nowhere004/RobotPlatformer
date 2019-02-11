using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour {

    [SerializeField]
    private GameObject clickSoundEnter;
    [SerializeField]
    private GameObject clickSoundDown;

    public void ClickSoundFonk()
    {
        GameObject clickSoundEffect = Instantiate(clickSoundEnter);
        Destroy(clickSoundEffect,0.5f);  
    }
    public void ClickSoundDownFonk()
    {
        GameObject clickSoundDownEffect = Instantiate(clickSoundDown);
        Destroy(clickSoundDownEffect, 0.5f);
    }

}
