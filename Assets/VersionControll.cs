using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionControll : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = "V " + Application.version;
    }
}
