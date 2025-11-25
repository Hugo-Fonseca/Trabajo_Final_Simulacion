using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private float score;

    public float CurrentScore => score;

    void Update()
    {

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        Debug.Log("ScoreManager encontrado: " + (scoreManager != null));


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            score += 1 * Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        }

        

    }

}
