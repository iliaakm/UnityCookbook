using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageToLoad : MonoBehaviour
{
    private void Start()
    {
        string textName = "babka.png";
        var tex = LoadTexture(textName);
        if(tex == null)
        {
            return;
        }
        else
        {
            GetComponent<Renderer>().material.mainTexture = tex;
        }

        ScreenCapture.CaptureScreenshot("screen.jpg", 2);
    }

    public Texture2D LoadTexture(string filename)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
        string projectRootPath = directoryInfo.Parent.ToString();
        var imagePath = Path.Combine(projectRootPath,filename);
        if(!File.Exists(imagePath))
        {
            Debug.LogWarning($"No file exits at path {imagePath}");
            return null;
        }

        var fileData = File.ReadAllBytes(imagePath);
        var tex = new Texture2D(2, 2);
        var succes = tex.LoadImage(fileData);

        if(succes)
        {
            return tex;
        }
        else
        {
            Debug.LogWarning($"Failed to load texture at path {imagePath}");
            return null;
        }        
    }
}
