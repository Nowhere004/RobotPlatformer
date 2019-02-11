using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToTheGame : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            LevelChangerSc.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
