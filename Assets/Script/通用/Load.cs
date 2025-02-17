using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator loadAnim;
    public GameObject loadingConetext;
    public Image loadImg;


    public void LoadLevel(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        //觸發轉場動畫
        loadAnim.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        //打開讀取進度，並讀取
        loadingConetext.SetActive(true);

        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;
        while (scene.progress < 0.9f)
        {
            loadImg.fillAmount = Mathf.MoveTowards(loadImg.fillAmount, scene.progress, 3 * Time.deltaTime);
            yield return null;
        }

        //讀取好後會先延遲1秒完成讀取，並切換關卡
        loadImg.fillAmount = 1;
        yield return new WaitForSeconds(1);
        loadingConetext.SetActive(false);
        scene.allowSceneActivation = true;
    }
}
