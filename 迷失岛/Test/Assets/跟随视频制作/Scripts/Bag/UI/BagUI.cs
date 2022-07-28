using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    public Button leftButton, rightButton;
    public int currentIndex;

    public SlotUI slotUI;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    public void OnUpdateUIEvent(PropDetails prop, int index)
    {
        //Debug.LogWarning("OnUpdateUIEvent");
        if (prop == null)
        {
            currentIndex = -1;
            slotUI.SetEmpty();
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetProp(prop);
            leftButton.interactable = index > 0;
            if (index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }

    public void SwitchProp(int delta)
    {
        //Debug.LogWarning("switchprop" + delta);
        int index = currentIndex + delta;
        if (index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if (index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        EventHandler.CallChangeItemEvent(index);
    }
}