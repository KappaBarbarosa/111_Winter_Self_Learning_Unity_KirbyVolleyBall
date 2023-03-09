using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioClip WinBGM;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.iswin)
        {
            GameManager.iswin = false;
            gameObject.GetComponent<AudioSource>().clip = WinBGM;
            gameObject.GetComponent<AudioSource>().PlayOneShot(WinBGM);
        }
        if (GameManager.cocobool)
        {
            GameManager.cocobool = false;
            Destroy(this.gameObject);
        }

    }
}
