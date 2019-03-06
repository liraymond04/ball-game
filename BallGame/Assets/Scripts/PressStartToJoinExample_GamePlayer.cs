// Copyright (c) 2017 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Rewired;

[AddComponentMenu("")]

public class PressStartToJoinExample_GamePlayer : MonoBehaviour 
{

    public GameObject globalObject;
    GlobalControl globalControl;

    public Color red;
    public Color blue;
    public Color green;
    public Color yellow;

    public Color alphaRed;
    public Color alphaBlue;
    public Color alphaGreen;
    public Color alphaYellow;

    public GameObject characterSel;
    public GameObject text;

    public int gamePlayerId = 0;

    public int selectedColor;
    public int finalColor;

    private bool selecting = false;
    private bool buttonDown = false;

    public int state = 0;

    private Rewired.Player player { get { return PressStartToJoinExample_Assigner.GetRewiredPlayer(gamePlayerId); } }

    void Start()
    {
        globalControl = globalObject.GetComponent<GlobalControl>();
    }

    void Update() 
    {
        if(!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if(player == null) return;

        if(state == 0)
        {
            ProcessInput();
            SetColor();
        }
        if(state == 1)
        {
            FinalColor();
            ColorSelected();
            GlobalVariable();

            if(player.GetButtonDown("Fire 2"))
            {
                selecting = false;

                state = 0;

                ColorDeselect();
                DeselectGlobalVariable();
            }
        }

        if (selecting == true)
        {
            text.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            text.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    private void ProcessInput() 
    {
        if (player.GetButtonDown("Fire 1") && selecting == false)
        {
            if (selectedColor == 0 && characterSel.GetComponent<CharacterSel>().redSelected == false)
            {
                selecting = true;

                state = 1;
            }
            if (selectedColor == 1 && characterSel.GetComponent<CharacterSel>().blueSelected == false)
            {
                selecting = true;

                state = 1;
            }
            if (selectedColor == 2 && characterSel.GetComponent<CharacterSel>().greenSelected == false)
            {
                selecting = true;

                state = 1;
            }
            if (selectedColor == 3 && characterSel.GetComponent<CharacterSel>().yellowSelected == false)
            {
                selecting = true;

                state = 1;
            }
        }

        if(player.GetAxisRaw("Move Horizontal") > 0.5f)
        {
            if(buttonDown == false)
            {
                Debug.Log("Right");

                if(selectedColor < 3)
                {
                    selectedColor++;
                }

                buttonDown = true;
            }
        }
        if (player.GetAxisRaw("Move Horizontal") < -0.5f)
        {
            if (buttonDown == false)
            {
                Debug.Log("Left");

                if (selectedColor > 0)
                {
                    selectedColor--;
                }

                buttonDown = true;
            }
        }
        if(player.GetAxisRaw("Move Horizontal") == 0)
        {
            buttonDown = false;
        }
    }

    void SetColor()
    {
        if (selectedColor == 0 && characterSel.GetComponent<CharacterSel>().redSelected == false)
        {
            GetComponent<Image>().color = red;
        }
        if (selectedColor == 1 && characterSel.GetComponent<CharacterSel>().blueSelected == false)
        {
            GetComponent<Image>().color = blue;
        }
        if (selectedColor == 2 && characterSel.GetComponent<CharacterSel>().greenSelected == false)
        {
            GetComponent<Image>().color = green;
        }
        if (selectedColor == 3 && characterSel.GetComponent<CharacterSel>().yellowSelected == false)
        {
            GetComponent<Image>().color = yellow;
        }
        if (selectedColor == 0 && characterSel.GetComponent<CharacterSel>().redSelected == true)
        {
            GetComponent<Image>().color = alphaRed;
        }
        if (selectedColor == 1 && characterSel.GetComponent<CharacterSel>().blueSelected == true)
        {
            GetComponent<Image>().color = alphaBlue;
        }
        if (selectedColor == 2 && characterSel.GetComponent<CharacterSel>().greenSelected == true)
        {
            GetComponent<Image>().color = alphaGreen;
        }
        if (selectedColor == 3 && characterSel.GetComponent<CharacterSel>().yellowSelected == true)
        {
            GetComponent<Image>().color = alphaYellow;
        }
    }

    void FinalColor()
    {
        if (selectedColor == 0)
        {
            GetComponent<Image>().color = red;
        }
        if (selectedColor == 1)
        {
            GetComponent<Image>().color = blue;
        }
        if (selectedColor == 2)
        {
            GetComponent<Image>().color = green;
        }
        if (selectedColor == 3)
        {
            GetComponent<Image>().color = yellow;
        }
    }

    void ColorSelected()
    {
        if (selectedColor == 0)
        {
            characterSel.GetComponent<CharacterSel>().redSelected = true;
        }
        if (selectedColor == 1)
        {
            characterSel.GetComponent<CharacterSel>().blueSelected = true;
        }
        if (selectedColor == 2)
        {
            characterSel.GetComponent<CharacterSel>().greenSelected = true;
        }
        if (selectedColor == 3)
        {
            characterSel.GetComponent<CharacterSel>().yellowSelected = true;
        }
    }

    void ColorDeselect()
    {
        if (selectedColor == 0)
        {
            characterSel.GetComponent<CharacterSel>().redSelected = false;
        }
        if (selectedColor == 1)
        {
            characterSel.GetComponent<CharacterSel>().blueSelected = false;
        }
        if (selectedColor == 2)
        {
            characterSel.GetComponent<CharacterSel>().greenSelected = false;
        }
        if (selectedColor == 3)
        {
            characterSel.GetComponent<CharacterSel>().yellowSelected = false;
        }
    }

    void GlobalVariable()
    {
        if (selectedColor == 0)
        {
            globalControl.red = gamePlayerId;
        }
        if (selectedColor == 1)
        {
            globalControl.blue = gamePlayerId;
        }
        if (selectedColor == 2)
        {
            globalControl.green = gamePlayerId;
        }
        if (selectedColor == 3)
        {
            globalControl.yellow = gamePlayerId;
        }
    }

    void DeselectGlobalVariable()
    {
        if (selectedColor == 0)
        {
            globalControl.red = 0;
        }
        if (selectedColor == 1)
        {
            globalControl.blue = 0;
        }
        if (selectedColor == 2)
        {
            globalControl.green = 0;
        }
        if (selectedColor == 3)
        {
            globalControl.yellow = 0;
        }
    }

}
