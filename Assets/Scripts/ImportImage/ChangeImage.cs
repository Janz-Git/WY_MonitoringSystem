/****************************************************
    文件：ChangeImage.cs
	作者：Mark
    日期：#CreateTime#
	功能：用于替换图片
*****************************************************/

using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ChangeImage : MonoBehaviour
{
    public Image currentImage;//当前图片
    public Button selectBtn;//选择图片的按钮

    public InputField PathText_;

    private void Start()
    {
        selectBtn.onClick.AddListener(OnSelectBtnOnclick);//监听按是否被按下，按下则执行括号中的方法
    }
    //当按钮被按下时执行该脚本，打开本地文件夹
    private void OnSelectBtnOnclick()
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);
        //可进行修改选择的文件类型
        ofn.filter = "图片文件(*.jpg*.png)\0*.jpg;*.png";
        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;
        string path = Application.streamingAssetsPath;
        path = path.Replace('/', '\\');
        //默认路径
        ofn.initialDir = path;

        ofn.title = "选择需要替换的图片";

        ofn.defExt = "JPG";//显示文件的类型
                           //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

        if (WindowDll.GetOpenFileName(ofn))
        {
            StartCoroutine(LoadTextrue(ofn.file));
        }
    }
    //加载选择的图片并进行替换
    IEnumerator LoadTextrue(string path)
    {
    UnityWebRequest unityWebRequest = new UnityWebRequest("file:///" + path);
    DownloadHandlerTexture handlerTexture = new DownloadHandlerTexture(true);
    unityWebRequest.downloadHandler = handlerTexture;
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            print(unityWebRequest.error);
        }
        else
        {
    Texture2D t = handlerTexture.texture;
    //将选择的图片替换上去
    currentImage.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.one);

           PathText_.text = path;
        }
    }
}
