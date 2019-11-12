@echo off

echo ....install

adb -P 5039 install -r arcore_unity.apk

if %errorlevel% neq 0 (
    echo プログラムは異常終了しました。
    exit /b
)

echo;
echo ....ADB Logcat

adb -P 5039 logcat -c
adb -P 5039 logcat -s ARCoreUnity