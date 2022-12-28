using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class PlayerVisibility : MonoBehaviour
{
    public Image backgroundLoseScreen;
    PlayerMovements player;
    ALLMOBCONTROLLER allMob;
    void Start()
    {
        player = GetComponent<PlayerMovements>();
        allMob = FindObjectOfType<ALLMOBCONTROLLER>();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LooseScreen());
        }
    }

    IEnumerator LooseScreen()
    {
        allMob.StopAllEnemy();
        int intensity = 10;
        float maxOpacity = 0.9f;
        float minOpacity = 0f;
        float transitionDelay = 0.25f;

        for (int i = 0; i < intensity; i++)
        {
            backgroundLoseScreen.DOColor(new Color(0.8301887f, 0.8301887f, 0.8301887f, maxOpacity), 0.1f);
            yield return new WaitForSeconds(transitionDelay);
            backgroundLoseScreen.DOColor(new Color(0.8301887f, 0.8301887f, 0.8301887f, minOpacity), 0.1f);
            yield return new WaitForSeconds(0.1f);
            maxOpacity -= 0.09f;
            transitionDelay -= 0.02f;
        }
    }

}