using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Lightchanger : MonoBehaviour
{
    public Camera cam;
    public Animator anim;
    public Sprite deadSprite;

    // Use this for initialization
    void Start ()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ground");

        foreach (GameObject go in gameObjectArray)
        {
          go.GetComponent<Renderer>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "kill")
        {
            FindObjectOfType<GameManagement>().LevelFailed();
        }
        if (col.gameObject.tag == "enemy")
        {
            if(col.gameObject.GetComponent<Transform>().position.y < this.GetComponent<Transform>().position.y-1.05)
            {
                Destroy(col.gameObject.GetComponent<BoxCollider2D>());
                Destroy(col.gameObject.GetComponent<Rigidbody2D>());
                Destroy(col.gameObject.GetComponent<EnemyMover>());
                col.gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
                col.gameObject.GetComponent<Animator>().SetTrigger("fade");
                Destroy(col.gameObject, 2f);
                anim.SetTrigger("flash");

                StartCoroutine(ShowWorld());
            }
           else
           {
                FindObjectOfType<GameManagement>().LevelFailed();
           }

        }
        if (col.gameObject.tag == "Finish")
        {
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            FindObjectOfType<GameManagement>().LevelComplete();

        }
    }



    private IEnumerator ShowWorld()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ground");

        foreach (GameObject go in gameObjectArray)
        {
            //go.GetComponent<SpriteRenderer>().sprite = visibleSprite;
            go.GetComponent<Renderer>().enabled = true;
        }

        yield return new WaitForSeconds(0.7f);
        foreach (GameObject go in gameObjectArray)
        {
            //go.GetComponent<SpriteRenderer>().sprite = darkSprite;
            go.GetComponent<Renderer>().enabled = false;

        }
    }
}
