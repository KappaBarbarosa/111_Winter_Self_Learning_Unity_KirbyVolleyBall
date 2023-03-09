using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    [SerializeField] GameObject Anim;
    public void Sstart()
    {
        Debug.Log("start!!!");
        Anim.SetActive(true);
        Anim.GetComponent<Animation>().Play("FadeOut");
    }

}
