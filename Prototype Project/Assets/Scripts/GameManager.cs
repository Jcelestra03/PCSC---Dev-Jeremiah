using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject.Find("Timer").GetComponent<Text>().text = "Time: " + Mathf.Round(Time.time);

    }
    public void loadAlley()
    {
        StartCoroutine(levelLoader("Alley", 1));
    }
    public void loadLevel2()
    {
        StartCoroutine(levelLoader("Level2", 2));
    }
    public void loadMenu()
    {
        StartCoroutine(levelLoader("MainMenu", 1));
    }

    IEnumerator levelLoader(string levelName, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(levelName);

    }











}
