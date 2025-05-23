using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RenderGizmosScaleFix
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginName = "RenderGizmosScaleFix";
        public const string PluginGuid = "COM3D2.RenderGizmosScaleFix.Plugin";
        public const string PluginVersion = "1.0.0.0";

        static Plugin()
        {
        }

        private static BepInEx.Configuration.ConfigEntry<float> scaleMultiplier;

        private void Awake()
        {
            // Configの設定を追加
            Config.SaveOnConfigSet = true;
            scaleMultiplier = Config.Bind(
                "GizmoSettings",
                "ScaleMultiplier",
                0.25f,
                "Gizmoのスケール倍率（デフォルト: 0.25 = 1/4）"
            );

            Harmony.CreateAndPatchAll(typeof(Plugin), null);
        }

        [HarmonyPatch(typeof(GizmoRender), "RenderGizmos")]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            UnityEngine.Debug.Log($"[RenderGizmosScaleFix] Transpiler called for GizmoRender.RenderGizmos.");

            var codes = new List<CodeInstruction>(instructions);

            // this.generalLens = -2f * Mathf.Tan(0.5f * Camera.main.fieldOfView) * magnitude / 50f;
            // this.generalLens = 2f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * magnitude / x;

            for (int i = 0; i < codes.Count; i++)
            {
                // 元の計算式のパターンを探す
                if (codes[i].opcode == OpCodes.Ldc_R4 &&
                    codes[i].operand is float value && value == -2f &&
                    i + 4 < codes.Count &&
                    codes[i + 1].opcode == OpCodes.Ldc_R4 &&
                    codes[i + 1].operand is float value2 && value2 == 0.5f &&
                    codes[i + 2].opcode == OpCodes.Call &&
                    codes[i + 2].operand.ToString().Contains("get_main") &&
                    codes[i + 3].opcode == OpCodes.Callvirt &&
                    codes[i + 3].operand.ToString().Contains("get_fieldOfView"))
                {
                    // 新しい計算式に置き換え
                    codes[i] = new CodeInstruction(OpCodes.Ldc_R4, 2f); // -2f を 2f に変更
                    codes[i + 1] = new CodeInstruction(OpCodes.Ldc_R4, 0.5f); // 0.5f はそのまま

                    codes.Insert(i + 4, new CodeInstruction(OpCodes.Ldc_R4, 0.017453292f)); // Mathf.Deg2Rad
                    codes.Insert(i + 5, new CodeInstruction(OpCodes.Mul));

                    UnityEngine.Debug.Log($"[RenderGizmosScaleFix] Replaced calculation in GizmoRender.RenderGizmos at index {i}.");
                }

                // 50で割っている箇所を変更
                if (codes[i].opcode == OpCodes.Ldc_R4 &&
                    codes[i].operand is float divValue && divValue == 50f)
                {
                    codes[i] = new CodeInstruction(OpCodes.Ldc_R4, 1f / scaleMultiplier.Value);
                    UnityEngine.Debug.Log($"[RenderGizmosScaleFix] Changed division by 50 to multiplication by {scaleMultiplier.Value} at index {i}.");
                }
            }

            return codes;
        }
    }
}