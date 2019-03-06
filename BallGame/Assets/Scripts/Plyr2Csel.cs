using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plyr2Csel : MonoBehaviour
{

    public GameObject back;
    public GameObject back1;
    public GameObject back2;

    public GameObject readyObject;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject yellow;

    public Color original;
    public Color select;
    public Color redSprite;
    public Color blueSprite;
    public Color greenSprite;
    public Color yellowSprite;

    public bool selectCharacter = false;
    public bool selecting = false;
    public int characterSelected = 1;
    public bool ready = false;
    public bool readyTrue = false;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SelectCharacter();
        ControlInput();
        SpriteControllerControl();
        ArrowController();
        CancelSelect();
        PlayerReady();
    }

    void SelectCharacter()
    {
        if (selectCharacter == true)
        {
            if (characterSelected == 1)
            {
                //image.color = playerRed;

                //Debug.Log("Character" + characterSelected);

                red.GetComponent<Image>().enabled = true;
                blue.GetComponent<Image>().enabled = false;
                green.GetComponent<Image>().enabled = false;
                yellow.GetComponent<Image>().enabled = false;
            }
            if (characterSelected == 2)
            {
                red.GetComponent<Image>().enabled = false;
                blue.GetComponent<Image>().enabled = true;
                green.GetComponent<Image>().enabled = false;
                yellow.GetComponent<Image>().enabled = false;
            }
            if (characterSelected == 3)
            {
                red.GetComponent<Image>().enabled = false;
                blue.GetComponent<Image>().enabled = false;
                green.GetComponent<Image>().enabled = true;
                yellow.GetComponent<Image>().enabled = false;
            }
            if (characterSelected == 4)
            {
                red.GetComponent<Image>().enabled = false;
                blue.GetComponent<Image>().enabled = false;
                green.GetComponent<Image>().enabled = false;
                yellow.GetComponent<Image>().enabled = true;
            }
        }
    }

    void ControlInput()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            back1.GetComponent<Image>().color = select;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            selectCharacter = true;
        }
        if (Input.GetButtonDown("Fire3") && selectCharacter == true)
        {
            ready = true;
        }
        if (Input.GetAxisRaw("Horizontal1") > 0.5f)
        {
            NextCharacter();
        }
        if (Input.GetAxisRaw("Horizontal1") < -0.5f)
        {
            PreviousCharacter();
        }
        if (Input.GetAxisRaw("Horizontal1") == 0f)
        {
            selecting = true;
        }
    }

    void NextCharacter()
    {
        if (selecting == true && characterSelected < 4 && ready == false)
        {
            characterSelected += 1;

            selecting = false;
        }
    }

    void PreviousCharacter()
    {
        if (selecting == true && characterSelected > 1 && ready == false)
        {
            characterSelected -= 1;

            selecting = false;
        }
    }

    void SpriteControllerControl()
    {
        if (red.GetComponent<Image>().enabled == true)
        {
            back.GetComponent<Image>().color = redSprite;
        }
        if (blue.GetComponent<Image>().enabled == true)
        {
            back.GetComponent<Image>().color = blueSprite;
        }
        if (green.GetComponent<Image>().enabled == true)
        {
            back.GetComponent<Image>().color = greenSprite;
        }
        if (yellow.GetComponent<Image>().enabled == true)
        {
            back.GetComponent<Image>().color = yellowSprite;
        }
    }

    void ArrowController()
    {
        if (selectCharacter == false)
        {
            leftArrow.GetComponent<Image>().enabled = false;
            rightArrow.GetComponent<Image>().enabled = false;
        }
        if (characterSelected == 1 && selectCharacter == true)
        {
            leftArrow.GetComponent<Image>().enabled = false;
            rightArrow.GetComponent<Image>().enabled = true;
        }
        if (characterSelected > 1 && characterSelected < 4 && selectCharacter == true)
        {
            leftArrow.GetComponent<Image>().enabled = true;
            rightArrow.GetComponent<Image>().enabled = true;
        }
        if (characterSelected == 4 && selectCharacter == true)
        {
            leftArrow.GetComponent<Image>().enabled = true;
            rightArrow.GetComponent<Image>().enabled = false;
        }
    }

    void CancelSelect()
    {
        if (selectCharacter == true && Input.GetButtonDown("Fire4"))
        {
            back.GetComponent<Image>().color = original;

            leftArrow.GetComponent<Image>().enabled = false;
            rightArrow.GetComponent<Image>().enabled = false;

            red.GetComponent<Image>().enabled = false;
            blue.GetComponent<Image>().enabled = false;
            green.GetComponent<Image>().enabled = false;
            yellow.GetComponent<Image>().enabled = false;

            selectCharacter = false;
            back1.GetComponent<Image>().color = original;
        }
        if (ready == true && Input.GetButtonDown("Fire4"))
        {
            readyObject.GetComponent<Image>().enabled = false;

            ready = false;
        }
    }

    void PlayerReady()
    {
        if (ready == true && Input.GetButtonDown("Fire3"))
        {
            back.GetComponent<Image>().color = select;

            leftArrow.GetComponent<Image>().enabled = false;
            rightArrow.GetComponent<Image>().enabled = false;

            red.GetComponent<Image>().enabled = false;
            blue.GetComponent<Image>().enabled = false;
            green.GetComponent<Image>().enabled = false;
            yellow.GetComponent<Image>().enabled = false;

            readyObject.GetComponent<Image>().enabled = true;

            if (characterSelected == 1)
            {
                GlobalControl.Instance.red = 2;
            }
            if (characterSelected == 2)
            {
                GlobalControl.Instance.blue = 2;
            }
            if (characterSelected == 3)
            {
                GlobalControl.Instance.green = 2;
            }
            if (characterSelected == 4)
            {
                GlobalControl.Instance.yellow = 2;
            }
        }
    }

}
