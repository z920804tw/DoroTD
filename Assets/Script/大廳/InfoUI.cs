using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] infoPage;
    public GameObject[] assestUsePages;
    public GameObject prevBtn;
    public GameObject nextBtn;
    [SerializeField] int currnetAssestIndex;

    public GameObject[] monsterInfoPages;
    public GameObject prevBtn1;
    public GameObject nextBtn1;
    [SerializeField] int currnetInfoIndex;
    void OnEnable()
    {
        CloseAllPage(infoPage);
        infoPage[0].SetActive(true);
    }

    //右側按鈕用
    public void OpenInfoPage(int i)
    {
        CloseAllPage(infoPage);
        ResetPage();
        infoPage[i].SetActive(true);
    }

    public void SelectAssestPage(int i)
    {
        CloseAllPage(assestUsePages);
        if (i == 0)
        {
            currnetAssestIndex--;
        }
        else
        {
            currnetAssestIndex++;
        }

        if (currnetAssestIndex == 0)
        {
            prevBtn.SetActive(false);
        }
        else if (currnetAssestIndex < assestUsePages.Length - 1)
        {
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
        }
        else
        {
            nextBtn.SetActive(false);
        }
        assestUsePages[currnetAssestIndex].SetActive(true);
    }

    public void SelectMonsterInfoPage(int i)
    {
        CloseAllPage(monsterInfoPages);
        if (i == 0)
        {
            currnetInfoIndex--;
        }
        else
        {
            currnetInfoIndex++;
        }

        if (currnetInfoIndex == 0)
        {
            prevBtn1.SetActive(false);
        }
        else if (currnetInfoIndex < monsterInfoPages.Length - 1)
        {
            prevBtn1.SetActive(true);
            nextBtn1.SetActive(true);
        }
        else
        {
            nextBtn1.SetActive(false);
        }
        monsterInfoPages[currnetInfoIndex].SetActive(true);
    }

    void CloseAllPage(GameObject[] pages)
    {
        foreach (GameObject n in pages)
        {
            n.SetActive(false);
        }
    }
    public void ResetPage()
    {
        CloseAllPage(assestUsePages);
        CloseAllPage(monsterInfoPages);

        assestUsePages[0].SetActive(true);
        monsterInfoPages[0].SetActive(true);

        currnetAssestIndex=0;
        currnetInfoIndex=0;

        prevBtn.SetActive(false);
        prevBtn1.SetActive(false);

        nextBtn.SetActive(true);
        nextBtn1.SetActive(true);
    }


}
