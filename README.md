# COM3D2.RenderGizmosScaleFix.Plugin

v1.0.0.0

- [COM3D2.RenderGizmosScaleFix.Plugin](#com3d2rendergizmosscalefixplugin)
  - [概要](#概要)
  - [インストール方法](#インストール方法)
  - [コンフィグファイル](#コンフィグファイル)
  - [変更履歴](#変更履歴)
    - [2025/05/24 v1.0.0.0](#20250524-v1000)
  - [規約](#規約)
    - [MOD規約](#mod規約)


## 概要

視野角を変更するとギズモのサイズがバグるのを修正するPluginです。

※BepInEx専用です。

https://github.com/user-attachments/assets/ec4c7656-e7e5-42b1-9ae0-580eb1b97765



## インストール方法

[Releases](https://github.com/kidonaru/COM3D2.RenderGizmosScaleFix.Plugin/releases)
から最新の`COM3D2.RenderGizmosScaleFix.Plugin-vX.X.X.zip`をダウンロードします。

zip解凍後、`BepInEx\plugins`フォルダの中身を、COM3D2をインストールしたフォルダの`BepInEx\plugins`に配置してください。

各ファイルの説明:
- `COM3D2.RenderGizmosScaleFix.Plugin.dll`
  - プラグインの本体。

COM3D2 Ver.2.38.0で動作確認済みです。


## コンフィグファイル

インストール後にCOM3D2を起動すると`BepInEx\config`フォルダに`COM3D2.RenderGizmosScaleFix.Plugin.cfg`が生成されます。

このコンフィグファイルをテキストエディタで編集すると、ギズモのサイズを調整することができます。

```ini
## Gizmoのスケール倍率（デフォルト: 0.25 = 1/4）
# Setting type: Single
# Default value: 0.25
ScaleMultiplier = 0.25
```


## 変更履歴


### 2025/05/24 v1.0.0.0

- 公開版リリース


## 規約

### MOD規約

※MODはKISSサポート対象外です。
※MODを利用するに当たり、問題が発生してもKISSは一切の責任を負いかねます。
※「カスタムメイド3D2」か「カスタムオーダーメイド3D2」か「CR EditSystem」を購入されている方のみが利用できます。
※「カスタムメイド3D2」か「カスタムオーダーメイド3D2」か「CR EditSystem」上で表示する目的以外の利用は禁止します。
※これらの事項は http://kisskiss.tv/kiss/diary.php?no=558 を優先します。
