using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public ItemData itemInMouse;
    public int itemCountinMouse;
    public GameObject CopyingSlot;
    private int selectedIndex = -1;
    private int lastSelected = 0;

    private SlotComponent[, ] SlotArray = new SlotComponent[8, 8];
    [SerializeField]
    private SlotComponent[] ItemBar = new SlotComponent[8];
    public SlotComponent selectedSlot;

    //Нужно будет снять галочки, чтобы инвентарь не был открыт сразу
    public bool isOpen = true;

    void Start()
    {
        InitInventory();
    }

   
    private void InitInventory()
    {
        Canvas.ForceUpdateCanvases();
        float size = 33.625f; 
        GameObject ItemsPanel = gameObject.transform.GetChild(0).gameObject; 
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                GameObject NewSlot = Instantiate(CopyingSlot, ItemsPanel.transform);
                RectTransform rectTransform = NewSlot.GetComponent<RectTransform>();
                rectTransform.anchorMax = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, i * size, size);
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, j * size, size);
                SlotArray[i, j] = NewSlot.GetComponent<SlotComponent>();
            }
        }
    }

    //Находит свободный слот инвентаря
    public SlotComponent FindFreeSlot()
    {
        //Сначала ищем свободный слот на панели быстрого доступа
        for(int k = 0; k < 8; k++)
        {
            if (ItemBar[k].getItemSlot() == null)
                return ItemBar[k];
        }
        //Потом в инвентаре
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (SlotArray[i, j].getItemSlot() == null)
                    return SlotArray[i, j];
            }
        }
        return null;
    }

    //Находит слот инвентаря с указанным типом предмета и неполным стаком
    public SlotComponent FindUnfilledSlot(ItemData item)
    {
        for (int k = 0; k < 8; k++)
        {
            if (ItemBar[k].getItemSlot() == item && ItemBar[k].itemCount < item.maxStackSize)
                return ItemBar[k];
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (SlotArray[i, j].getItemSlot() == item && SlotArray[i, j].itemCount < SlotArray[i,j].getItemSlot().maxStackSize)
                    return SlotArray[i, j];
            }
        }
        return null;
    }

    //Находит заполненный слот с указанным типом предмета
    public SlotComponent FindFilledSlot(ItemData item)
    {
        for (int k = 0; k < 8; k++)
        {
            if (ItemBar[k].getItemSlot() == item && ItemBar[k].itemCount == item.maxStackSize)
                return ItemBar[k];
        }
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(SlotArray[i, j].getItemSlot() == item && SlotArray[i, j].itemCount == item.maxStackSize)
                    return SlotArray[i, j];
            }
        }
        return null;
    }
    
    public void AddToInventory(ItemData item, int count)
	{
        int a = 0;
		while(count > 0)
        {
            SlotComponent slot;
            if((slot = FindUnfilledSlot(item))!=null)
            {
                if(item.maxStackSize - slot.itemCount >= count)
                {
                    slot.setItemCount(slot.itemCount + count);
                    count = 0;
                }
                else
                {
                    count = count - (item.maxStackSize - slot.itemCount);
                    slot.setItemCount(item.maxStackSize);
                }
            }else if((slot = FindFreeSlot()))
            {
                if(count <= item.maxStackSize)
                {
                    slot.setItemSlot(item, count);
                    count = 0;
                }else
                {
                    slot.setItemSlot(item, item.maxStackSize);
                    count = count - item.maxStackSize;
                }
            }
            a++;
            if(a > 10)
            {
                Debug.Log("Много итераций");
                break;
            }
        }
	}
    
	public void DeleteFromInventory(ItemData item, int count)
	{
		while(count > 0)
        {
            SlotComponent slot;
            if((slot = FindUnfilledSlot(item)) != null)
            {
                if(slot.itemCount >= count)
                {
                    slot.itemCount = slot.itemCount - count;
                    if (slot.itemCount == 0) slot.clearItemSlot();
                }else
                {
                    count = count - slot.itemCount;
                    slot.clearItemSlot();
                }
            }else if((slot = FindFilledSlot(item)) != null)
            {
                if(item.maxStackSize >= count)
                {
                    slot.itemCount = slot.itemCount - count;
                    if (slot.itemCount == 0) slot.clearItemSlot();
                }else
                {
                    slot.clearItemSlot();
                    count = count - item.maxStackSize;
                }
            }
        }
	}

    public void SetSelectedSlot(float scrollDir)
    {

        if (scrollDir < 0)
            selectedIndex++;
        else if (scrollDir > 0)
            selectedIndex--;
        if (selectedIndex < -1)
            selectedIndex = 7;
        else if (selectedIndex > 7)
            selectedIndex = -1;
        if (selectedIndex != -1)
        {
            ItemBar[selectedIndex].transform.localScale = new Vector2(1.15f, 1.15f);
            //if (selectedIndex < 8) ItemBar[selectedIndex].GetComponentInChildren<Text>().text = ItemBar[selectedIndex].getItemSlot().name;
            ItemBar[lastSelected].transform.localScale = new Vector2(1.0f, 1.0f);
            lastSelected = selectedIndex;
        }
        else
            ItemBar[lastSelected].transform.localScale = new Vector2(1.0f, 1.0f);
    }

    public SlotComponent getSelectedSlot()
    {
        return ItemBar[selectedIndex];
    }
    
    public void HideInventory()
    {
        var Images = gameObject.GetComponentsInChildren<Image>();
        var Buttons = gameObject.GetComponentsInChildren<Button>();
        foreach(var el in Images)
        {
            el.enabled = false;
        }
        foreach(var el in Buttons)
        {
            el.enabled = false;
        }
        isOpen = false;
    }

    public void OpenInventory()
    {
        var Images = gameObject.GetComponentsInChildren<Image>();
        var Buttons = gameObject.GetComponentsInChildren<Button>();
        foreach(var el in Images)
        {
            el.enabled = true;
        }
        foreach(var el in Buttons)
        {
            el.enabled = true;
        }
        isOpen = true;
    }
}

