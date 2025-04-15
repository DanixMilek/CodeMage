using UnityEditor;
using UnityEngine;
using System.Diagnostics;

public class RestartEditor : MonoBehaviour
{
    [MenuItem("Tools/Restart Unity #r")] // Shift + R como atajo de reseteo
    private static void RestartUnity()
    {
        if (EditorUtility.DisplayDialog("Reiniciar Unity",
            "¿Estás seguro de que deseas reiniciar Unity? Asegúrate de guardar todos los cambios.",
            "Reiniciar", "Cancelar"))
        {
            // Guarda todos los cambios antes de reiniciar
            AssetDatabase.SaveAssets();

            // Obtén el proyecto actual
            string projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);

            // Obtén la ruta al ejecutable de Unity
            string unityPath = EditorApplication.applicationPath;

            // Ejecuta Unity en el proyecto actual después de cerrar el editor
            Process.Start(new ProcessStartInfo
            {
                FileName = unityPath,
                Arguments = $"-projectPath \"{projectPath}\"",
                UseShellExecute = true
            });

            // Cierra Unity
            EditorApplication.Exit(0);
        }
    }
}
