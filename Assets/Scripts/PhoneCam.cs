using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PhoneCam : MonoBehaviour
{
    private bool isCamAvailable;
    private WebCamTexture webCam;
    private Texture defaultBackground;
    private int resWidth = 256;
    private int resHeight = 256;
    private Texture2D currentSnapshot;
    private string currentSnapshotPath;

    public RawImage background;
    public AspectRatioFitter fitter;
    public RawImage playerProfilePicture;
    public SpriteRenderer PFPShowcaser;
    public GameObject takePictureButton;
    public GameObject savePictureButton;
    public GameObject cancelButton;
    public GameObject profilePicBorders;
    public Camera cameraObject;

    private void Awake()
    {
        cameraObject = GetComponent<Camera>();
        if (cameraObject.targetTexture == null)
        {
            cameraObject.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = cameraObject.targetTexture.width;
            resHeight = cameraObject.targetTexture.height;
        }
        cameraObject.gameObject.SetActive(false);
    }

    public void OpenCamera()
    {
        defaultBackground = background.texture;
        background.gameObject.SetActive(true);
        takePictureButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        profilePicBorders.gameObject.SetActive(true);
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera found.");
            isCamAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                    webCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        isCamAvailable = true;
        webCam.Play();
        background.texture = webCam;
    }

    private void Update()
    {
        if (!isCamAvailable) { return; }

        float ratio = (float)webCam.width / (float)webCam.height;
        fitter.aspectRatio = ratio;

        float yScale = webCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, yScale, 1f);

        int orientation = -webCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void CloseCamera()
    {
        webCam = null;
        isCamAvailable = false;
        currentSnapshot = null;
        currentSnapshotPath = null;
        background.gameObject.SetActive(false);
        takePictureButton.gameObject.SetActive(false);
        savePictureButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        profilePicBorders.gameObject.SetActive(false);
        PFPShowcaser.gameObject.SetActive(false);
    }

    public void TakeSnapshot()
    {
        cameraObject.gameObject.SetActive(true);
        if (cameraObject.gameObject.activeInHierarchy)
        {
            StartCoroutine(SimpleCapture());
            Debug.Log("Snapshot taken");
            currentSnapshot = GetScreenshot(currentSnapshotPath);//Figure out why it can't find the file
            webCam.Stop();
            webCam = null;
            background.texture = null;
            Sprite sp = Sprite.Create(currentSnapshot, new Rect(0, 0, currentSnapshot.width, currentSnapshot.height),
                new Vector2(0.5f, 0.5f));
            CutScreenshot();//Finish that function
            PFPShowcaser.sprite = sp;
            PFPShowcaser.gameObject.SetActive(true);
            takePictureButton.SetActive(false);
            savePictureButton.SetActive(true);
        }
        cameraObject.gameObject.SetActive(false);
    }

    public void SavePicture()
    {
        playerProfilePicture.texture = currentSnapshot;
        CloseCamera();
    }

    private string SnapshotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    private IEnumerator SimpleCapture()
    {
        string fileName = SnapshotName();
        currentSnapshotPath = fileName;
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
    }

    private Texture2D GetScreenshot(string filePath)
    {
        Texture2D texture = null;
        if (File.Exists(filePath))
        {
            byte[] data = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(data);
        }
        else { Debug.Log("Could not find file at path: " + filePath); }
        return texture;
    }

    private void CutScreenshot()
    {
        //Cut the screenshot to fit inside PFP Showcaser
    }
}
