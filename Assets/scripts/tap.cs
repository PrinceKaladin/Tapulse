using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tap : MonoBehaviour
{

    public GameObject circle;
    public GameObject circle2;
    public void ontap() {
        circle.SetActive(true);
        circle2.SetActive(true);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        if (GameObject.Find("combojoa")) {
            GameObject.Find("scorojoa").GetComponent<Text>().text = PlayerPrefs.GetInt("Score",0).ToString();
            GameObject.Find("combojoa").GetComponent<Text>().text = "X" +  PlayerPrefs.GetInt("combo",0).ToString();
        }
    }
}
