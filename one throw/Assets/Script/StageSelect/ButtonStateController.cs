using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ButtonStateController : MonoBehaviour
{
    [System.Serializable]
    public class ButtonData
    {
        public Button button;
        public string id;  // ユニークID
        [HideInInspector]
        public Color originalColor;
    }

    public ButtonData[] buttons;
    private Color darkColor = new Color(0.5f, 0.5f, 0.5f);

    void Start()
    {
        foreach (var bd in buttons)
        {
            bd.originalColor = bd.button.image.color;

            // 保存されている状態に応じて色を設定
            bool clicked = IsButtonClicked(bd.id);
            if (clicked)
            {
                bd.button.image.color = darkColor;
            }

            bd.button.onClick.AddListener(() => OnButtonClicked(bd));
        }
    }

    bool IsButtonClicked(string id)
    {
        var state = DataManager.Instance.data.buttonStates.FirstOrDefault(s => s.id == id);
        return state != null && state.clicked;
    }

    void OnButtonClicked(ButtonData bd)
    {
        bd.button.image.color = darkColor;

        // DataManagerのデータを更新して保存
        var states = DataManager.Instance.data.buttonStates;
        var state = states.FirstOrDefault(s => s.id == bd.id);
        if (state == null)
        {
            states.Add(new ButtonState { id = bd.id, clicked = true });
        }
        else
        {
            state.clicked = true;
        }

        DataManager.Instance.Save(DataManager.Instance.data);
    }

    // ゲーム開始時や必要に応じて呼ぶリセット関数
    public void ResetButtonStates()
    {
        foreach (var bd in buttons)
        {
            bd.button.image.color = bd.originalColor;
        }

        DataManager.Instance.data.buttonStates.Clear();
        DataManager.Instance.Save(DataManager.Instance.data);
    }
}
