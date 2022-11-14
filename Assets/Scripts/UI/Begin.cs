using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Begin : MonoBehaviour
{
    public int SceneNo;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LoadTargetScene); ;
    }
    void Update()
    {

    }

    public void LoadTargetScene()
    {
        SceneManager.LoadScene(SceneNo);
    }
}
