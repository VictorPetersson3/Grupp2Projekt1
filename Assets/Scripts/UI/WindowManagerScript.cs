using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManagerScript : MonoBehaviour
{

    public Dropdown resolutionDropdown;
    
    Resolution[] resolutions;


    
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = "  " + resolutions[i].width
                + " x " + resolutions[i].height
                /*+ Screen. + " hz"*/;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(resolutionDropdown);
        });
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.width, Screen.fullScreen);
    }
    void DropdownValueChanged(Dropdown someChange)
    {
        int resolutionIndex = someChange.value;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.width, Screen.fullScreen);
        Debug.Log("We got here brah");
    }
}
