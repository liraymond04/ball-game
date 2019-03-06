using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSel : MonoBehaviour {

    public bool redSelected = false;
    public bool blueSelected = false;
    public bool greenSelected = false;
    public bool yellowSelected = false;

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(redSelected == true && blueSelected == true && greenSelected == true && yellowSelected == true)
        {
            Debug.Log("All ready");
        }
    }

    void PlayerReady()
    {

    }

    void PlayerNotReady()
    {

    }

}
