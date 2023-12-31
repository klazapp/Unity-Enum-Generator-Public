using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace com.Klazapp.Editor
{
    public class EnumGenerator : EditorWindow
    {
        #region Textures
        public Texture2D enumGeneratorIcon;
        public Texture2D warningIcon;
        #endregion
        
        private string enumName = "NewEnum";
        private List<string> enumValues = new() { "Value1", "Value2", "Value3" };
        private List<int> enumIntegers = new() { 0, 1, 2 };
        private Vector2 scrollPosition;

        
        [MenuItem("Klazapp/Tools/Enum Generator")]
        private static void ShowWindow()
        {
            //Retrieve inspector window
            var inspectorWindowType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");

            //Dock beside inspector window
            EditorWindow window = GetWindow<EnumGenerator>(inspectorWindowType);

            //Show content
            //Hardcoded strings due to static function
            var icon = AssetDatabase.LoadAssetAtPath<Texture>(
                "Packages/com.klazapp.enumgenerator/Editor/Data/Enum Icon.png");
            var titleContent = new GUIContent("Enum Generator", icon);
            window.titleContent = titleContent;
        }
        
        private void OnEnable()
        {
            enumName = "";
            enumValues = new() { "Value1", "Value2", "Value3" };
            enumIntegers = new() { 0, 1, 2 };
        }

        private void OnGUI()
        {
            //Store default color
            var defaultBgColor = GUI.backgroundColor;

            EditorGUILayout.Space(30f);

            DrawTitle();
            
            EditorGUILayout.Space(10f);
            
            CustomEditorHelper.DrawHorizontalLine();

            EditorGUILayout.Space(30f);

            DrawEnumName();
            
            EditorGUILayout.Space(30f);

            if (IsValidString(enumName) && !IsEmptyString(enumName))
            {
                DrawEnumValues();
            }

            EditorGUILayout.Space(50f);

            if (!HasDuplicates(enumValues) && !IsEmptyString(enumName) && IsValidString(enumName))
            {
                DrawGenerate();
            }
          
            if (!IsValidString(enumName))
            {
                DrawInvalidEnumNameWarning();
            }
            else if (HasDuplicates(enumValues) && !IsEmptyString(enumName))
            {
                DrawInvalidEnumValuesWarning();
            }
            return;

            #region Local Functions
            void DrawTitle()
            {
                var labelFieldStyle = new GUIStyle()
                {
                    fontSize = 30,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                CustomEditorHelper.DrawBox(0, 30, new Color32(155, 155, 155, 0), "Enum Generator", labelFieldStyle, enumGeneratorIcon);
            }

            void DrawEnumName()
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space(20);
                var inputFieldLabelStyle = new GUIStyle()
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                CustomEditorHelper.DrawBox(0, 60, new Color32(155, 155, 155, 0), "Type your enum name here:", inputFieldLabelStyle);
                CustomEditorHelper.DrawSpace(5);
                enumName = EditorGUILayout.TextField("", enumName, GUILayout.Height(20));
                EditorGUILayout.Space(10);
                EditorGUILayout.Space(20);
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
            
            void DrawEnumValues()
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginVertical();
                
                var inputFieldLabelStyle = new GUIStyle()
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                
                CustomEditorHelper.DrawBox(0, 60, new Color32(155, 155, 155, 0), "Type your enum values here:", inputFieldLabelStyle);
                
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
                var toBeDeleted = -1;
                for (var i = 0; i < enumValues.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Height(40));
                    GUILayout.FlexibleSpace();
                    
                    var enumValuesStyle = new GUIStyle(EditorStyles.textField)
                    {
                        fontSize = 15,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleCenter,
                        wordWrap = true,
                        normal =
                        {
                            textColor = Color.white
                        }
                    };
                    
                    enumValues[i] = EditorGUILayout.TextField(enumValues[i], enumValuesStyle, GUILayout.Height(30));
                    enumIntegers[i] = EditorGUILayout.IntField(enumIntegers[i], enumValuesStyle, GUILayout.Height(30));

                    var deductButtonStyle = new GUIStyle()
                    {
                        fontSize = 12,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleCenter,
                        wordWrap = true,
                        normal =
                        {
                            textColor = Color.white
                        }
                    };

                    if (CustomEditorHelper.DrawButton(40, 25, new Color32(155, 45, 95, 255), "-", deductButtonStyle, null, Alignment.None))
                    {
                        toBeDeleted = i;
                    }
                    
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
                
                if (toBeDeleted > -1)
                {
                    enumValues.RemoveAt(toBeDeleted);
                    enumIntegers.RemoveAt(toBeDeleted);
                }
                
                EditorGUILayout.EndScrollView();
                
                CustomEditorHelper.DrawSpace(10);

                var addValueStyle = new GUIStyle()
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
       
                if (CustomEditorHelper.DrawButton(150, 30, new Color32(55, 145, 135, 255), "ADD VALUE", addValueStyle))
                {
                    enumValues.Add("NewValue");
                    enumIntegers.Add(enumValues.Count - 1);
                }
                
                CustomEditorHelper.DrawSpace(10);
                
                EditorGUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            void DrawInvalidEnumNameWarning()
            {
                if (IsEmptyString(enumName))
                    return;

                var warningStyle = new GUIStyle()
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                
                CustomEditorHelper.DrawBox(200, 20, new Color32(155, 155, 155, 0), "Enum name is invalid!", warningStyle, warningIcon);
            }
            
            void DrawGenerate()
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                
                var generateStyle = new GUIStyle()
                {
                    fontSize = 12,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };

                if (CustomEditorHelper.DrawButton(200, 35, new Color32(55, 145, 195, 255), "GENERATE ENUM", generateStyle, null,
                        Alignment.None))
                {
                    ChooseSaveLocation();
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
            
            void DrawInvalidEnumValuesWarning()
            {
                var warningStyle = new GUIStyle()
                {
                    fontSize = 15,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
                    wordWrap = true,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                CustomEditorHelper.DrawBox(300, 20, new Color32(155, 155, 155, 0), "Duplicate enum values found!", warningStyle, warningIcon);
            }
            #endregion
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEmptyString(string input)
        {
            //Check for null, empty, or all whitespace
            return string.IsNullOrWhiteSpace(input);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidString(string input)
        {
            foreach (var c in input)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    //Invalid character found
                    return false; 
                }
            }
            
            //String is valid
            return true; 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasDuplicates(List<string> values)
        {
            var uniqueValues = new HashSet<string>();
            foreach (var value in values)
            {
                //Trim to ensure whitespace doesn't cause false negatives
                if (!uniqueValues.Add(value.Trim())) 
                {
                    //Found duplicate
                    return true; 
                }
            }
            
            //No duplicates
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ChooseSaveLocation()
        {
            var path = EditorUtility.SaveFilePanel("Save Enum Script", "", enumName + ".cs", "cs");
            Debug.Log("path = " + path);
            if (!string.IsNullOrEmpty(path))
            {
                GenerateEnum(path);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GenerateEnum(string path)
        {
            if (string.IsNullOrEmpty(enumName) || enumValues.Count == 0)
            {
                EditorUtility.DisplayDialog("Error", "Enum name and values cannot be empty", "OK");
                return;
            }

            var builder = new StringBuilder();
            builder.AppendLine("public enum " + enumName);
            builder.AppendLine("{");
            for (var i = 0; i < enumValues.Count; i++)
            {
                builder.AppendLine($"    {enumValues[i].Trim()} = {enumIntegers[i]},");
            }
            builder.AppendLine("}");

            File.WriteAllText(path, builder.ToString());
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Enum Generated", "Enum script generated at: " + path, "OK");
        }
    }
}