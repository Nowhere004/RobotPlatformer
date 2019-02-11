using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChangerSc : MonoBehaviour {
    private static LevelChangerSc instance;
    public static LevelChangerSc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelChangerSc>();
            }
            return instance;
        }
    }


    [SerializeField]
    private Animator animator;
    private int levelToLoad;
        public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("fade");
    }
    public void OnFateComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            FadeToLevel(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
