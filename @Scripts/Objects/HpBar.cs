using UnityEngine;
using UnityEngine.UI;
using static Define;

public class HpBar : BaseController
{
    private Slider slider;
    
    public override void Init()
    {
        base.Init();

        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        var worldCanvas = GameObject.Find("WorldCanvas");
        if (worldCanvas != null)
            transform.parent.SetParent(worldCanvas.transform, false);
    }

    private void OnEnable()
    {
        slider.value = 1;
    }

    public void UpdateHpBar(int MaxHp, int Hp)
    {
        if (MaxHp == 0) return;
        slider.value = (float)Hp / MaxHp;
    }

    public void UpdatePosition(Transform target)
    {
        transform.position = target.position + new Vector3(0, 1, 0); 
    }
}
