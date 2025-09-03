using UnityEngine;
using  TMPro;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField.Select();
        inputField.ActivateInputField();
        MenuManager.Instance.PlayerName.text = inputField.text ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
}
