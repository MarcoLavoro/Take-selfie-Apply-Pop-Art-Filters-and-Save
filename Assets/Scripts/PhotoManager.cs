

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnityExample;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotoManager : MonoBehaviour
{
  
    Texture2D photoTexture;
    WebCamTexture webcamTexture;

    public Color[] FilterColors;
    public Color[] BackgoroundFilterColors;


    public GameObject[] Panels;
    public GameObject UIConteinerShootInterface;
    public GameObject UIConteinerSaveImage;
    public GameObject UIInteractionPhotoSaved;
    public RawImage rawImageTakePhoto;
    public RawImage[] rawImageEffect;
    public Image[] backgoroundRawImageEffect;

    // Start is called before the first frame update
    void Start()
    {
        webcamTexture = new WebCamTexture();
        rawImageTakePhoto.texture = webcamTexture;
        webcamTexture.Play();
        float value = (float)webcamTexture.width / (float)webcamTexture.height;
        rawImageTakePhoto.GetComponent<AspectRatioFitter>().aspectRatio = value;
    }
    //public
    public void TakeScreenshot()
    {
        StartCoroutine(HideUIAndTakeScreenshot());
    }
    public void SaveImage()
    {
        StartCoroutine(HideUIAndSavePhoto());
    }

    //private

    IEnumerator HideUIAndTakeScreenshot()
    {
        UIConteinerShootInterface.SetActive(false);
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();
        Texture2D tx2d = ScreenCapture.CaptureScreenshotAsTexture();
        yield return new WaitForEndOfFrame();
        WebcamTextureToEffect(tx2d);
        UIConteinerShootInterface.SetActive(true);
    }

    public void WebcamTextureToEffect(Texture2D texturee)
    {
        photoTexture = ComicFilterToImage.ApplyComicTextureToTexture(texturee);
        for (int i = 0; i < rawImageEffect.Length; i++)
        {
            rawImageEffect[i].color = FilterColors[i];
            rawImageEffect[i].texture = photoTexture;

            backgoroundRawImageEffect[i].color = BackgoroundFilterColors[i];

        }
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
    }

 

    IEnumerator HideUIAndSavePhoto()
    {
        UIConteinerSaveImage.SetActive(false);
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();
        Texture2D tx2d = ScreenCapture.CaptureScreenshotAsTexture();
        yield return new WaitForEndOfFrame();


        byte[] bytes = tx2d.EncodeToPNG();
        var dirPath = Application.dataPath + "/../SavedImages/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + "Image" + ".png", bytes);

        Debug.Log("saved on:" + dirPath + "Image" + ".png");
        yield return new WaitForEndOfFrame();

        UIConteinerSaveImage.SetActive(true);
        UIInteractionPhotoSaved.SetActive(true);
    }
}
