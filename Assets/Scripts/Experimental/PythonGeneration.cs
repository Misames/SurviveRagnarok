using UnityEngine;
using Python.Runtime;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class PythonGeneration : MonoBehaviour
{
    // Text to convert to Python Code
    public TextAsset textToConvert;

    // All UI to interact with
    public TMP_InputField FirstInputField;
    public TMP_InputField SecondInputField;

    public Button FirstButton;
    public Button SecondButton;

    public RawImage FirstImage;
    public RawImage SecondImage;

    // Url of the generated images
    private string firstImageString = null;
    private string secondImageString = null;

    // PyModule, corresponding to the Python Code
    private PyModule pythonScript;

    // Start is called before the first frame update
    void Start()
    {
        Runtime.PythonDLL = @"python311.dll";
        PythonEngine.Initialize();

        SecondButton.interactable = false;

        using (Py.GIL())
        {
            var stringImported = textToConvert.text;

            pythonScript = PyModule.FromString("PythonScript", stringImported);
        }

    }

    /// <summary>
    /// Generate only one image
    /// </summary>
    public void GenerateImageA()
    {
        if (FirstInputField.text == null || FirstInputField.text == "")
        {
            Debug.LogError("ENTER A VALID PROMPT");
            return;
        }

        var PythonString1 = new PyString(FirstInputField.text);

        using (Py.GIL())
        {
            var result = pythonScript.InvokeMethod("generate_and_save_image", new PyObject[] { PythonString1 });
            firstImageString = Application.dataPath + "/GeneratedImages/" + result.ToString();
        }

        byte[] bytes = File.ReadAllBytes(firstImageString);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        FirstImage.texture = texture;

        SecondButton.interactable = true;
        FirstButton.interactable = false;
    }

    /// <summary>
    /// Generate the second image by passing the first in argument
    /// </summary>
    public void GenerateImageB()
    {
        if (SecondInputField.text == null || SecondInputField.text == "")
        {
            Debug.LogError("ENTER A VALID PROMPT");
            return;
        }

        var PythonString1 = new PyString(firstImageString);
        var PythonString2 = new PyString(SecondInputField.text);

        using (Py.GIL())
        {
            var result = pythonScript.InvokeMethod("generate_and_save_image", new PyObject[] { PythonString2, PythonString1});
            secondImageString = Application.dataPath + "/GeneratedImages/" + result.ToString();
        }

        byte[] bytes = File.ReadAllBytes(secondImageString);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        SecondImage.texture = texture;

        SecondButton.interactable = false;
        FirstButton.interactable = true;
    }

}
