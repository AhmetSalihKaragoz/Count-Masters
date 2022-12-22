using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    [SerializeField] private TextMeshPro _doorText;
    public int textValue;
    public char textSymbol;
    private void Start()
    {
        _doorText.text = textSymbol + textValue.ToString();
    }
}
