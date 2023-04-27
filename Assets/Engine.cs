using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public Transform tr_player;
    public Transform tr_ball;
    public Text points;
    public static int point;
    public GameObject bonus;
    private bool created = false;
    private int new_position = 0;
    private int range = 5;
    private bool change_speed = false;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        int count = 16;
        for (int i = 0; i < count; i++)
        {
            if (new_position == 8)
            {
                new_position = 0;
                range--;
            }
            Debug.Log("new_position: " + new_position+" range: "+range+" count: "+count);
            Instantiate(bonus,new Vector2(-15.75f + (new_position * 4.5f), range*2.0f), Quaternion.identity);
            new_position++;
        }
    }

    public Vector3 direction;
    private Vector3 mousePoint;

    // Update is called once per frame
    void Update()
    {
        tr_ball.position += direction;

        Vector3 player = cam.WorldToScreenPoint(new Vector3(tr_player.position.x, tr_player.position.y, tr_player.position.z));
        mousePoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, player.y, player.z));

        if (tr_player.position.x < -16f || tr_player.position.x > 16f)
        { 
            if (mousePoint.x > -16f && mousePoint.x < 16f)
                tr_player.position = mousePoint;
        }
        else
        {
            tr_player.position = mousePoint;
        }

        if (tr_ball.position.x >= 17f || tr_ball.position.x <= -17f)
        {
            direction.x = (-direction.x);
        }

        if (tr_ball.position.y > 11f)
        {
            direction.y = (-direction.y);
        }

        if (tr_ball.position.y < -11f)
        {
            LoadScene();
        }
    }


    //Gdy wystąpi kolizja obiektów
    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D: "+ col.collider.name);
        if (col.collider.name == "gracz")
        {
            direction.y = (-direction.y);
            point++;
            Faster();
            SetBonus();
        }
        if (col.collider.name=="Bonus(Clone)")
        {
            direction.y = (-direction.y);
            direction.x = (-direction.x);
            point += 10;
            if (new_position == 0)
            {
                range++;
                new_position = 7;
            }
            else
            {
                new_position--;
            }
            
        }
        points.text = "Punkty: " + point.ToString();

    }

    //ładowanie wyniku gry
    public void LoadScene()
    {
        SceneManager.LoadScene("Game over", LoadSceneMode.Single);
    }

    //przeszkoda z dodatkowymi punktami
    public void SetBonus()
    {
        Debug.Log(point+" % 30 = "+point%30);
        if (point % 30 <10 && point > 30 && created == false)
        {
            Debug.Log("Seting bonus . . .");
            if (new_position == 8)
            {
                new_position = 0;
                range--;
            }
            else
            {
                new_position++;
            }
            Instantiate(bonus, new Vector2(-15.75f + (new_position * 4.5f), range), Quaternion.identity);
            created = true;
        }
        else
        {
            created = false;
        }
    }

    //przyspieszanie piłki
    private void Faster()
    {
        if (point > 50)
        {
            if (point % 50 < 15 && change_speed == true)
            {
                direction.x = direction.x * 1.1f;
                direction.y = direction.y * 1.1f;
                change_speed = false;
            }
            if (point % 50 > 15 && change_speed == false)
            {
                change_speed = true;
            }
        }
    }
}
