using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;

public class ImgToImgGenerator : MonoBehaviour
{
    private const string ApiKey = "QnO9X5-D7b6rZEa7JV7QEg";
    private const string BaseUrl = "https://stablehorde.net/api";
    private string generationId;

    public TMP_InputField promptInputField;
    public Button generateButton;
    public RawImage displayImage;
    public Texture2D sourceTexture;

    private void Start()
    {
        generateButton.onClick.AddListener(OnGenerateButtonClick);
    }

    private void OnGenerateButtonClick()
    {
        string prompt = promptInputField.text;
        StartCoroutine(GenerateImage(prompt));
    }

    private IEnumerator GenerateImage(string prompt)
    {
        string url = $"{BaseUrl}/v2/generate/async";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("apiKey", ApiKey);

        byte[] imageBytes = sourceTexture.EncodeToPNG();
        string base64Image = Convert.ToBase64String(imageBytes);
        
        string payload = $"{{\"prompt\":\"{prompt}\", \"source_image\":\"{base64Image}\", \"source_processing\":\"img2img\"}}";
        
        www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(payload));
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error generating image: " + www.error);
            Debug.LogError("Server response: " + www.downloadHandler.text);
            yield break;
        }

        string jsonResponse = www.downloadHandler.text;
        generationId = JsonUtility.FromJson<GenerationResponse>(jsonResponse).id;

        StartCoroutine(CheckGenerationStatus());
    }

    private IEnumerator CheckGenerationStatus()
    {
        while (true)
        {
            string url = $"{BaseUrl}/v2/generate/check/{generationId}";
            Debug.Log("Checking generation status at URL: " + url);
            UnityWebRequest www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking generation status: " + www.error);
                yield break;
            }

            // Parsez la réponse pour vérifier si la génération est terminée
            // Mettez à jour la variable isFinished en conséquence
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("Check status response: " + jsonResponse);
            bool isFinished = JsonUtility.FromJson<GenerationStatusResponse>(jsonResponse).finished;

            if (isFinished)
            {
                StartCoroutine(DownloadImage());
                yield break;
            }

            yield return new WaitForSeconds(10f);
        }
    }


    private IEnumerator DownloadImage()
{
    string statusUrl = $"{BaseUrl}/v2/generate/status/{generationId}";
    Debug.Log("Getting image info from URL: " + statusUrl);
    
    UnityWebRequest statusRequest = UnityWebRequest.Get(statusUrl);
    yield return statusRequest.SendWebRequest();

    if (statusRequest.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error getting image info: " + statusRequest.error);
        Debug.LogError("Server response: " + statusRequest.downloadHandler.text);
        yield break;
    }

    string jsonResponse = statusRequest.downloadHandler.text;
    GenerationStatusResponse generationStatus = JsonUtility.FromJson<GenerationStatusResponse>(jsonResponse);
    string imageUrl = generationStatus.generations[0].img;

    Debug.Log("Downloading image from URL: " + imageUrl);
    
    UnityWebRequest imageRequest = UnityWebRequest.Get(imageUrl);
    imageRequest.downloadHandler = new DownloadHandlerBuffer();
    yield return imageRequest.SendWebRequest();

    if (imageRequest.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Error downloading image: " + imageRequest.error);
        Debug.LogError("Server response: " + imageRequest.downloadHandler.text);
        yield break;
    }

    byte[] webpData = imageRequest.downloadHandler.data;
    
    if (webpData == null || webpData.Length == 0)
    {
        Debug.LogError("WebP data is null or empty.");
        yield break;
    }
    
    // Utilisez la méthode LoadWebpTexture2D du plugin pour convertir les données WebP en Texture2D
    WebpImporter.LoadWebpTexture2D(webpData, (Texture2D texture) =>
    {
        if (texture == null)
        {
            Debug.LogError("Failed to load WebP image.");
            return;
        }

        displayImage.texture = texture;
    });
}

    [System.Serializable]
    public class GenerationResponse
    {
        public string id;
    }

    [System.Serializable]
    public class GenerationStatusResponse
    {
        public bool finished;
        public Generation[] generations;
    }

    [System.Serializable]
    public class Generation
    {
        public string img;
    }
}
