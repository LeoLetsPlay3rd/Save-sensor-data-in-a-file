using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CSVWriter : MonoBehaviour
{
    public Transform targetTransform; // the transform to track
    public Button recordButton; // the button to start recording

    private string csvPath;
    private StreamWriter writer;
    private float timer;
    private bool isRecording;
    private int numColumns;
    private const int MAX_ROWS = 60; // the maximum number of rows to record per round

    void Start()
    {
        recordButton.onClick.AddListener(StartRecording); // add the StartRecording method to the button's onClick event
    }

    void Update()
    {
        if (isRecording && numColumns < MAX_ROWS)
        {
            // write the current position to the CSV file
            writer.WriteLine(targetTransform.position.x + ",," + targetTransform.position.y + "," + targetTransform.position.z);

            numColumns++;
        }
        else if (isRecording)
        {
            // close the CSV file when the script is done
            writer.Close();
            isRecording = false;
        }
    }

    public void StartRecording()
    {
        if (!isRecording)
        {
            // set up the CSV file
            string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            csvPath = Application.dataPath + "/output_" + timeStamp + ".csv";
            numColumns = 0;

            writer = new StreamWriter(csvPath, false);
            writer.WriteLine("X,Y,Z"); // write column headers

            isRecording = true;
        }
    }

    void OnDestroy()
    {
        // close the CSV file when the script is destroyed
        if (isRecording)
        {
            writer.Close();
        }
    }
}