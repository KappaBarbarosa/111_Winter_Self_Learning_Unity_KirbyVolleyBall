using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearManager : MonoBehaviour
{
   [SerializeField] GameObject[] BearPrefabs;
    int coco = 0;
    public int Count=0;
    float rate = 0.5f;
    float[] Rate = { 0.5f, 0.5f,2f,0.75f };
    int ratecount;
    float nexttime;
    [SerializeField] GameObject Replay;
    [SerializeField] GameObject Back;
    [SerializeField] GameObject Close;
    public void SpawnBear(bool leftwin)
    {
        if (leftwin) coco = 1;
        else coco = 2;
      
        

    }
    private void Update()
    {
        if (coco != 0)
        {
         

            if (Time.time > nexttime)
            {
                nexttime = Time.time + Rate[ratecount++];
                Count++;
                int r = Random.Range(0, BearPrefabs.Length);
                GameObject co = Instantiate(BearPrefabs[r], transform);
                float lr, rr;
                if (coco == 1)
                {
                    lr = -8f;
                    rr = -1f;
                }
                else
                {
                    lr = 1f;
                    rr = 8f;
                }
                co.transform.position = new Vector3(Random.Range(lr, rr), 3f, 0f);
            }
        }
        if (ratecount >= Rate.Length) ratecount = 0;
        if (Count == 12) { 
            coco = 0;
            Replay.SetActive(true);
            Back.SetActive(true);
            Close.SetActive(true);
        } 
    }
}
