using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public void DisplayText(GameObject canvas,string text)
    {
        TMP_Text text_ = canvas.GetComponentInChildren<TMP_Text>();
        text_.text = text;
    }
}
