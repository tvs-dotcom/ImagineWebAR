using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Imagine.WebAR.Editor
{
    public class PostProcessBuild_WT : MonoBehaviour
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string buildPath)
        {
            string html = File.ReadAllText(buildPath + "/index.html");
            
            var geolocSceneCount = WorldTrackerGlobalSettings.Instance.geolocationScenes.Count;
            var includedSceneCount = 0;
            foreach( var scenePath in WorldTrackerGlobalSettings.Instance.geolocationScenes){
                foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
                {
                    if (scene.path == scenePath)
                    {
                        includedSceneCount++;
                        continue;
                    }
                }
            }

            if(includedSceneCount > 0){
                //we enable GPS in the template
                Debug.Log("Enabling geolocation - used by " + geolocSceneCount + " scenes");
                html = html.Replace("//Call Start GPS here --> StartGPS();", "StartGPS()");
                File.WriteAllText(buildPath + "/index.html", html);
            }
            
        }
    }
}

