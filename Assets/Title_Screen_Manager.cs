using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Screen_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public AK.Wwise.Event button_press;
    public AK.Wwise.Event start_fight;
    void Awake()
    {
        animator = this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {
        button_press.Post(gameObject);
        start_fight.Post(gameObject);
        animator.SetTrigger("Open");
    }

    public void exitGame() {
        button_press.Post(null);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
