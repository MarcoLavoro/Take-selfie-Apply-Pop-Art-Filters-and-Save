using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnityExample;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComicFilterToImage : MonoBehaviour
{
    /// <summary>
    /// The comic filter.
    /// </summary>
   static ComicFilter comicFilter;
    // Start is called before the first frame update


    public static Texture2D ApplyComicTextureToTexture(Texture2D texturee)
    {
        Texture2D texture = new Texture2D(texturee.width, texturee.height, TextureFormat.Alpha8, false);
        comicFilter = new ComicFilter(60, 120);
        int[] sizes = new int[2];
        sizes[0] = texturee.height;
        sizes[1] = texturee.width;
        Mat rgbaMat = new Mat(sizes, 0);
        Utils.texture2DToMat(texturee, rgbaMat);
        comicFilter.Process(rgbaMat, rgbaMat);
        Utils.fastMatToTexture2D(rgbaMat, texture);
        return texture;
    }
}
