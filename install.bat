@echo off

echo ....install

adb -P 5038 install -r arcore_unity.apk

if %errorlevel% neq 0 (
    echo プログラムは異常終了しました。
    exit /b
)

echo;
echo ....ADB Logcat

adb -P 5038 logcat -s ARCoreUnity