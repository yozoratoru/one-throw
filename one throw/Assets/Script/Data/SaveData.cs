using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int TotalCoins = 0;

    public List<ButtonState> buttonStates = new List<ButtonState>();
}

[System.Serializable]
public class ButtonState
{
    public string id;
    public bool clicked;
}
