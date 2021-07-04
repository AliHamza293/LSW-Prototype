using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    #region:Shop signleton
    public static Shop instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    [System.Serializable]
    public class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool isPurchased = false;
    }

    public List<ShopItem> ShopItemList;
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Animator NoCoinAnim;
    
    Button buyBtn;
    void Start()
    {
        
        int length = ShopItemList.Count;
        for (int i = 0; i < length; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            if(ShopItemList[i].isPurchased)
            {
                DisableBuyButton();
            }
            
            buyBtn.AddEventListner(i, OnShopItemBtnClicked);

        }

        
        
    }

    void DisableBuyButton()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
    }
    void OnShopItemBtnClicked(int itemIndex)
    {
        if(GameManager.instance.HasEnoughCoins(ShopItemList[itemIndex].Price))
        {
            GameManager.instance.UseCoins(ShopItemList[itemIndex].Price);
            //purchase item
            ShopItemList[itemIndex].isPurchased = true;

            //disable the button
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyButton();
            


            //change UICoinsText
            GameManager.instance.UpdateAllCoinTextUI();
            //add cloths
            Profile.instance.AddCloth(ShopItemList[itemIndex].Image);
        }
        else
        {
            NoCoinAnim.SetTrigger("NoCoins");
            Debug.Log("You don't have enough money");
        }
        
    }


    //for coins UI

    


    public void OpenShop()
    {
        ShopPanel.SetActive(true);

    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }
}
