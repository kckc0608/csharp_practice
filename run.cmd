:: MSBuild 를 호출하여 빌드하는 배치파일.
:: echo off 로 하단에 작성한 명령어의 배치 프로그램상 출력을 끈다.
:: 명령어를 실행하고, 배치프로그램이 바로 종료되지 않도록 Pause 명령어로 일시중지 시킨다.
@echo off
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild datagrid_practice.csproj

rem rem 은 주석 지시자
:: ::도 주석 지시자

set BUILD_STATUS=%ERRORLEVEL%
if %BUILD_STATUS% == 0 goto end
if not %BUILD_STATUS% == 0 goto fail

:: exit /b 0 에서 exit 은 종료 명령어고, /b 옵션은 cmd가 아니라 배치프로그램을 종료하겠다는 의미, 0 은 명시적으로 넘겨주는 종료값.

:end
start bin\Debug\datagrid_practice.exe
exit /b 0

:fail
echo %BUILD_STATUS%
exit /b 0