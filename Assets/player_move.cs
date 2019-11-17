using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_move : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey("a"))
        {
            rb.AddForce(new Vector2(-200 * Time.deltaTime, 0));
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(new Vector2(200 * Time.deltaTime, 0));
        }

        if (Input.GetKey("w"))
        {
            rb.AddForce(new Vector2(0, 500 * Time.deltaTime));
        }

        rb.AddForce(new Vector2(0, 150 * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D: "+ col.collider.name);
        if (col.collider.name == "Sciana")
            LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Game over", LoadSceneMode.Single);
    }
}
