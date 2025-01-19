using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CsvParser 
{
    [SerializeField] private string csvAddress = "SampleMonster.csv"; // Addressable 이름
    public List<string[]> Data { get; private set; } = new List<string[]>();
    public Action OnLoaded;

    // CSV 파일을 Addressable을 통해 비동기적으로 로드하는 함수
    public void LoadCSVData()
    {
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(csvAddress);

        // 로드 완료 후 처리
        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // 로드된 CSV 데이터를 텍스트로 받음
                string csvContent = op.Result.text;

                // CSV 데이터를 파싱하여 처리
                List<string[]> parsedData = ParseCSV(csvContent);
                Data = parsedData;
                OnLoaded?.Invoke();
            }
            else
            {
                Debug.LogError("CSV 파일 로드 실패");
            }
        };
    }

    // CSV 데이터를 파싱하는 함수
    private List<string[]> ParseCSV(string csvContent)
    {
        List<string[]> parsedData = new List<string[]>();

        // 줄 바꿈으로 구분된 행 처리
        string[] rows = csvContent.Split('\n');
        foreach (string row in rows)
        {
            // 각 행을 쉼표로 구분
            string[] columns = row.Split(',');
            parsedData.Add(columns);
        }

        return parsedData;
    }
}
