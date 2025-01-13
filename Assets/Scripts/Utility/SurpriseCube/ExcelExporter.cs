using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UnityEngine;
using Color = System.Drawing.Color;

public class ExcelExporter : MonoBehaviour
{
    [Header("Utility")]
    private static Action<string, List<Color32>> _ExportExcelHelper;

    private void Awake()
    {
        _ExportExcelHelper += ExportToExcel;
    }

    public static void ExportExcel(string path, List<Color32> topColors)
    {
        _ExportExcelHelper?.Invoke(path, topColors);
    }

    private void ExportToExcel(string path, List<Color32> topColors)
    {
        string filePath = (Directory.Exists(path) ? path : Application.dataPath) + "/ColoresPredominantes.xlsx";

        using (ExcelPackage excel = new ExcelPackage())
        {
            var worksheet = excel.Workbook.Worksheets.Add("Datos de la Imagen");

            List<ExcelRange> formatCells = new List<ExcelRange>();

            for (int i = 0; i < topColors.Count; i++)
            {
                worksheet.Cells[1, i+1].Value = "Color #" + (i+1);

                formatCells.Add(worksheet.Cells[1, i+1]);
            }

            FormatCell(formatCells, ExcelFillStyle.Solid, Color.LightGoldenrodYellow, true);

            formatCells.Clear();

            for (int i = 0; i < topColors.Count; i++)
            {
                worksheet.Cells[2, i + 1].Value = "R: " + topColors.ElementAt(i).r + ", G: " + topColors.ElementAt(i).g + ", B: " + topColors.ElementAt(i).b;
            }

            for (int i = 0; i < topColors.Count; i++)
            {
                worksheet.Cells[3, i + 1].Value = "";

                FormatCell(worksheet.Cells[3, i + 1], ExcelFillStyle.Solid, Color.FromArgb(topColors.ElementAt(i).r, topColors.ElementAt(i).g, topColors.ElementAt(i).b));
            }

            worksheet.Cells.AutoFitColumns();

            File.WriteAllBytes(filePath, excel.GetAsByteArray());
#if UNITY_EDITOR
            Debug.Log("Archivo exportado a: " + filePath);
#endif
        }
    }

    private void FormatCell(ExcelRange cell, ExcelFillStyle fillStyle, Color backgroundColor, bool bold = false)
    {
        cell.Style.Fill.PatternType = fillStyle;
        cell.Style.Fill.BackgroundColor.SetColor(backgroundColor);
        cell.Style.Font.Bold = bold;
    }

    private void FormatCell(List<ExcelRange> cells, ExcelFillStyle fillStyle, Color backgroundColor, bool bold = false)
    {
        foreach (ExcelRange cell in cells) 
        {
            cell.Style.Fill.PatternType = fillStyle;
            cell.Style.Fill.BackgroundColor.SetColor(backgroundColor);
            cell.Style.Font.Bold = bold;
        }
    }
}
