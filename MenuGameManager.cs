using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pushPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void pushOption()
    {

    }

    public void pushExit()
    {
        Application.Quit();
    }
}
