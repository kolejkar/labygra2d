using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        int points = Engine.point;
        Debug.Log(points);
        info.text = "Koniec gry\nZdobyte punkty: " + points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //ładowanie menu głównego
    public void LoadScene()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
