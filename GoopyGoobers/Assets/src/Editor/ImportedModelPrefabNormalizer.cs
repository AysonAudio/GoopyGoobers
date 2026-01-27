using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
namespace Editor {

    /// <summary>
    /// Normalizes default prefab settings when importing 3D models.
    /// Exported model file units should match Unity units 1:1.
    /// </summary>
    public class ImportedModelPrefabNormalizer : AssetPostprocessor {

        /// <summary>
        /// Unchecks import setting: "Convert Units 1cm (File) to 0.01m (Unity)".
        /// Imports as 1FileUnit=1UnityUnit.
        /// </summary>
        void OnPreprocessModel() {
            var importer = assetImporter as ModelImporter;
            Assert.IsNotNull(importer);
            importer.useFileScale = false;
            importer.useFileUnits = true;
        }

        /// <summary>
        /// Resets prefab transform scale from incorrect model file unit conversion.
        /// Moves prefab transform to scene origin.
        /// </summary>
        void OnPostprocessMeshHierarchy(GameObject root) {
            root.transform.localPosition = Vector3.zero;
            root.transform.localScale = Vector3.one;
            root.transform.localRotation = Quaternion.identity;
        }
    }
}
