using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using XCharts.Runtime;
using UnityEngine.SceneManagement;
public class DeformationJointDisplacement : MonoBehaviour
{

    public LineChart WavelengthValue_LineChart;
    public LineChart physicalQuantity_LineChart;
    public Toggle WavelengthValue_Toggle;
    public Toggle Physical_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        InitLineChart(WavelengthValue_LineChart);
        InitLineChart(physicalQuantity_LineChart);

     //  WavelengthValue_Toggle.onValueChanged.AddListener()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLineChart(LineChart lineChart)
    {
        lineChart.ClearData();
       
        for (int i = 0; i < 25; i++)
        {

            lineChart.AddXAxisData((i * 20).ToString());
            lineChart.AddData(0, UnityEngine.Random.Range(30, 90));
        }
    }
    public void UpdateLineChart(LineChart lineChart)
    {
        lineChart.GetSerie(0).ClearData();

        for (int i = 0; i < 25; i++)
        {

            lineChart.AddXAxisData((i * 20).ToString());
            lineChart.UpdateData(0,i,UnityEngine.Random.Range(30, 90));
        }
    }

    public void ToggleChage()
    {

      

        if (WavelengthValue_Toggle.isOn)
        {

            WavelengthValue_LineChart.gameObject.SetActive(true);
            physicalQuantity_LineChart.gameObject.SetActive(false);
        }
        else
        {
            physicalQuantity_LineChart.gameObject.SetActive(true);
            WavelengthValue_LineChart.gameObject.SetActive(false);
        }

    }
    public void SceneLoad(string Name)
    { 
    
    SceneManager.LoadScene(Name);
    
    }

}
