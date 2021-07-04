using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    #region:Profile signleton
    public static Profile instance;
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
    public class Cloth
    {
        public Sprite Image;
    }
    
    public List<Cloth> ClothList;
    [SerializeField] GameObject ClothUIPrefab;
    [SerializeField] Transform ClothScrollView;
    GameObject g;
    int newSelectedIndex, previousSelectedIndex ;
    [SerializeField] Color ActiveClothColor;
    [SerializeField] Color DefaultClothColor;
    [SerializeField] Image CurrentClothes;

    public AnimatorOverrideController[] playerAnimOverriderController;
    void Start()
    {
        newSelectedIndex = previousSelectedIndex = 0;
        GetAvailableCloths();
    }

    public void GetAvailableCloths()
    {
        for (int i = 0; i < Shop.instance.ShopItemList.Count; i++)
        {
             if(Shop.instance.ShopItemList[i].isPurchased)
            {
                //add purchased items to the clothes list

                AddCloth(Shop.instance.ShopItemList[i].Image);
            }
        }

        SelectCloth(newSelectedIndex);
    }

    public void AddCloth(Sprite img)
    {
        if(ClothList == null)
        {
            ClothList = new List<Cloth>(); 
        }

        Cloth cloth = new Cloth() { Image = img };
        ClothList.Add(cloth);

        //add cloths to the scrollview UI
        g = Instantiate(ClothUIPrefab, ClothScrollView);
        g.transform.GetChild(0).GetComponent<Image>().sprite = cloth.Image;

        //add click event
        g.transform.GetComponent<Button>().AddEventListner(ClothList.Count - 1, OnClothClick);
    }

    void OnClothClick(int clothIndex)
    {
        SelectCloth(clothIndex);
        GameManager.instance.playerAnim.runtimeAnimatorController = playerAnimOverriderController[clothIndex] as RuntimeAnimatorController;
    }
     void SelectCloth(int clothIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = clothIndex;
        ClothScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultClothColor;
        ClothScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveClothColor;

        //changing the current clothes

        CurrentClothes.sprite = ClothList[newSelectedIndex].Image;
        GameManager.instance.profileImg.sprite = ClothList[newSelectedIndex].Image;
    }
}
