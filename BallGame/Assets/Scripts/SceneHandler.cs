using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour 
{

    public GameObject characterHandler;

    public Animator transitionAnim;
    public string sceneName;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(characterHandler.GetComponent<CharacterSel>().redSelected == true && characterHandler.GetComponent<CharacterSel>().blueSelected == true && characterHandler.GetComponent<CharacterSel>().greenSelected == true && characterHandler.GetComponent<CharacterSel>().yellowSelected == true)
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

}
