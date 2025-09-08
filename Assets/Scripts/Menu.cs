using UnityEngine;
using  TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class Menu : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField.Select();
        inputField.ActivateInputField();
     }
    
}
