using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float pos;
    public GameObject heart;
    int once = 1;
    HeartManager parent;
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position.x;
        moveSpeed = 10f;
        parent = transform.parent.GetComponent<HeartManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = parent.HearSpeed;
        pos = gameObject.transform.position.x;
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        if (pos >= 3.4f && once==1)
        {
            once--;         
            parent.SpawnHeart();
        }
        if (pos > 10f) { 
            Destroy(gameObject);        
        } 
    }

}
