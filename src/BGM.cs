using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
   
    // Start is called before the first frame update
    public GameObject CocoBGM;
    GameObject BGMusic = null;
    public bool cocobool;
    private void Start()
    {
        BGMusic = GameObject.FindGameObjectWithTag("BGM");
        if(BGMusic==null)
        {
            BGMusic = Instantiate(CocoBGM);
        }
        cocobool = false;
    }
    private void Update()
    {

    }
}
