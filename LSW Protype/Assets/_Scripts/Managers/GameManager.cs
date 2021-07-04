using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region:GameManager signleton
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);     
        }else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] TextMeshProUGUI[] allCoinsUIText;
    public int coins;

    [SerializeField] GameObject QuestionPanel;

    public Animator playerAnim;
    public Image profileImg;
    private void Start()
    {
        UpdateAllCoinTextUI();  
    }
    public void UseCoins(int amount)
    {
        coins -= amount;
    }

   
    public bool HasEnoughCoins(int amount)
    {
        return (coins >= amount);
    }

    public void UpdateAllCoinTextUI()
    {
        for (int i = 0; i < allCoinsUIText.Length; i++)
        {
            allCoinsUIText[i].text = coins.ToString();
        }
    } 

    public void OpenQuestionPanel()
    {
        QuestionPanel.SetActive(true);
    }

    public void CloseQuestionPanel()
    {
        QuestionPanel.SetActive(false);
    }




}
