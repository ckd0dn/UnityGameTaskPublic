using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TooltipController : MonoBehaviour
{

    private void OnMouseDown()
    {
        if (Managers.UI.GetPopup<Tooltip>() != null)
        {
            Managers.UI.ClosePopup<Tooltip>();
        }
        else
        {
            ShowTooltip();
        }

    }

    private void ShowTooltip()
    {
        MonsterController monsterController = GetComponent<MonsterController>();
        if (monsterController == null) return;

        var tooltip = Managers.UI.ShowPopup<Tooltip>();

        Debug.Log($"{monsterController.Name}.asset");
        Sprite sprite = Managers.Resource.Load<Sprite>($"{monsterController.Name}.asset");

        tooltip.SetupTooltip(sprite, monsterController.Name, monsterController.Grade, monsterController.Speed.ToString(), monsterController.Health.ToString());
    }


}
