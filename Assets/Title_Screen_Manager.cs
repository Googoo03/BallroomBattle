using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Screen_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    void Awake()
    {
        animator = this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {
        animator.SetTrigger("Open");
    }

    public void exitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
