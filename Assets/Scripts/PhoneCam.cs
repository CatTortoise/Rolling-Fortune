using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCam : MonoBehaviour
{
    private bool isCamAvailable;
    private WebCamTexture webCam;
    private Texture defaultBackground;
    private int resWidth = 256;
    private int resHeight = 256;
    private Texture2D currentSnapshot;
    private bool showingSnapshot = false;

    public RawImage background;
    public AspectRatioFitter fitter;
    public RawImage playerProfilePicture;
    public GameObject takePictureButton;
    public GameObject savePictureButton;
    public GameObject CancelButton;
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
        CancelButton.gameObject.SetActive(true);
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
        else if (isCamAvailable)
        {
            float ratio = (float)webCam.width / (float)webCam.height;
            fitter.aspectRatio = ratio;

            float yScale = webCam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, yScale, 1f);

            int orientation = -webCam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
        }
    }

    public void CloseCamera()
    {
        webCam.Stop();
        showingSnapshot = false;
        currentSnapshot = null;
        background.gameObject.SetActive(false);
        takePictureButton.gameObject.SetActive(false);
        savePictureButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        profilePicBorders.gameObject.SetActive(false);
    }

    public void TakeSnapshot()
    {
        cameraObject.gameObject.SetActive(true);
        if (cameraObject.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cameraObject.Render(); //snapshot takes the ingame background instead of the camera feed
            RenderTexture.active = cameraObject.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0, false);//change the second set of 0, 0 to the correct position of the bottom left corner of the image border
            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapshotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Snapshot taken");
            cameraObject.gameObject.SetActive(false);
            showingSnapshot = true;
            currentSnapshot = new Texture2D(resWidth,resHeight, TextureFormat.RGB24, false); 
            currentSnapshot.LoadImage(bytes);
            webCam.Stop();
            webCam = null;
            background.texture = null;
            background.texture = currentSnapshot; //Still showing normal camera after snapshot
            takePictureButton.SetActive(false);
            savePictureButton.SetActive(true);
        }
    }

    public void SavePicture()
    {
        playerProfilePicture.texture = currentSnapshot;
    }

    private string SnapshotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.persistentDataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
