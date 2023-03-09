using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField] GameObject[]  HeartPrefabs;
    [SerializeField] GameObject Anim;
    public float HearSpeed;
    private void Start()
    {
        HearSpeed = 10f;
    }
    public void SpawnHeart()
    {
        int r = Random.Range(0, HeartPrefabs.Length);
        GameObject heart = Instantiate(HeartPrefabs[r], transform);
        heart.transform.position = new Vector3(-10f, 1.2f, 0f);
    }
    public void Faster()
    {
        if (HearSpeed < 25f) HearSpeed++;
    }
    public void Slower()
    {
        if (HearSpeed > 1f) HearSpeed--;
    }


}
