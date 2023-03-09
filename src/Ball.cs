using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject effect;
    [SerializeField] GameObject Anim;
    [SerializeField] GameObject FpText, SpText;
    [SerializeField] GameObject PresentManager;

    bool done = false;
    void Start()
    {
        SpText.GetComponent<TMP_Text>().text = "" + GameManager.SecondPlayerScore;
        FpText.GetComponent<TMP_Text>().text = "" + GameManager.FirstPlayerScore;
        Vector3 move = gameObject.transform.position;
        if (GameManager.FormerWinner) move = new Vector3(move.x + 12f, move.y, move.z);
        gameObject.transform.position = move;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEffect(Vector2 point)
    {
         GameObject coco = Instantiate(effect);
        coco.transform.position = point;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Bottom" || collision.gameObject.tag == "bound")
        {
            Vector2 location = this.transform.position; 
            Vector2 colsetpoint = collision.collider.ClosestPoint(location);
            SpawnEffect(colsetpoint);
            if(collision.gameObject.tag == "Bottom" && !done)
            {
                done = true;
                
                GameManager.FormerWinner = colsetpoint.x < 0;
                if (colsetpoint.x < 0)
                {
                    
                    GameManager.SecondPlayerScore++;
                    SpText.GetComponent<TMP_Text>().text = "" + GameManager.SecondPlayerScore;
                }
                else { 
                    GameManager.FirstPlayerScore++;
                    FpText.GetComponent<TMP_Text>().text = "" + GameManager.FirstPlayerScore;
                }
                if (GameManager.FirstPlayerScore == 10 || GameManager.SecondPlayerScore == 10)
                {
                    GameOver();
                }
                else
                {
                    Time.timeScale = 0.25f;
                    Anim.GetComponent<Animation>().Play("FadeOut");
                }
                
                //Invoke("lose", 1f);
            }
            
        }
    }
    void GameOver()
    {
        PresentManager.GetComponent<BearManager>().SpawnBear(GameManager.FirstPlayerScore == 10);
        GameManager.iswin = true;

    }
    public void Restart()
    {
        GameManager.SecondPlayerScore=0;
        GameManager.FirstPlayerScore=0;
        GameManager.FormerWinner = false;
        GameManager.cocobool = true;
        Anim.GetComponent<Animation>().Play("FadeOut");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void back()
    {
        GameManager.SecondPlayerScore = 0;
        GameManager.FirstPlayerScore = 0;
        GameManager.FormerWinner = false;
        GameManager.cocobool = true;
        SceneManager.LoadScene("StartScene");
    }

}
