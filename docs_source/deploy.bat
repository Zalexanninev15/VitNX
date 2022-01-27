@echo off
copy html\*.* ..\docs\*.* /Y
rd html /S /Q
rd latex /S /Q