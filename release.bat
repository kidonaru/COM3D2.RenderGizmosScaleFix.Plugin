@echo off
chcp 65001

call .\source\COM3D2.RenderGizmosScaleFix.Plugin\build.bat
if %ERRORLEVEL% neq 0 (
    echo ビルドに失敗しました
    exit /b 1
)

set VERSION="1.0.0.0"
echo VERSION: %VERSION%

set PLUGIN_NAME=COM3D2.RenderGizmosScaleFix.Plugin

if exist output rmdir /s /q output
md output\%PLUGIN_NAME%

xcopy BepInEx output\%PLUGIN_NAME%\BepInEx /E /I
copy README.md output\%PLUGIN_NAME%\README.md

powershell Compress-Archive -Path "output\%PLUGIN_NAME%" -DestinationPath "output\%PLUGIN_NAME%-v%VERSION%.zip" -Force

rmdir /s /q output\%PLUGIN_NAME%

echo ビルドに成功しました
exit /b 0
