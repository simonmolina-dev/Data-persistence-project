using UnityEngine;
using  TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public TMP_InputField inputField;
   // public Button startButton;
    private void Start()
    {
       inputField.Select();
       inputField.ActivateInputField();
       StartCoroutine(ActivateInputNextFrame());
    }
    //public void OnEnable()
    //{
        //startButton = GameObject.Find("Start Button").GetComponent<Button>();
        //startButton.onClick.RemoveAllListeners();
        //startButton.onClick.AddListener(MenuManager.Instance.StartNew);
    //}
    public IEnumerator ActivateInputNextFrame()
    {
        yield return null; // wait 1 frame
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
        inputField.ActivateInputField(); // focus the field
        inputField.Select();              // make TMP think it's selected
    }

}
