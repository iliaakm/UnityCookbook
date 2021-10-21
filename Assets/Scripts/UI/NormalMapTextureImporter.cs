using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class NormalMapTextureImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        var filename = System.IO.Path.GetFileNameWithoutExtension(assetPath);
        var normalMapSuffixes = new[] { "_n", "_normal", "_nrm" , "_NORMAL", "_Normal" };

        foreach (var suffix in normalMapSuffixes)
        {
            if(filename.EndsWith(suffix))
            {
                TextureImporter textureImporter = assetImporter as TextureImporter;
                textureImporter.textureType = TextureImporterType.NormalMap;

                return;
            }
        }
    }
}
#endif