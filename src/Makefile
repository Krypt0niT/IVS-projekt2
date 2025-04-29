WPF_DIR := ../src/WpfApp1

#STDDEV_EXE := ../src/CalculatorAp/bin/Debug/net8.0/StdDev.exe
#CALCULATOR_EXE := $(WPF_DIR)/bin/Debug/net8.0/WpfApp1.exe

.DEFAULT_GOAL := all

all: run

run:
	@echo Starting calculator GUI...
	dotnet run --project $(WPF_DIR)/WpfApp1.csproj
#@"$(CALCULATOR_EXE)"

stddev:
	@echo Running StdDev with test data...
#@type ../testdata.txt | "$(STDDEV_EXE)"

clean:
	@echo Removing unnecesary files...
#@for /D %%d in (../src\*) do if exist %%d\bin rmdir /S /Q %%d\bin
#@for /D %%d in (../src\*) do if exist %%d\obj rmdir /S /Q %%d\obj
# @if exist $(DOC_DIR) rmdir /S /Q $(DOC_DIR)

pack:
	@echo Packing project...
	@powershell Compress-Archive -Path "..\*" -DestinationPath "..\xuradnl00_xkuzmok00_xlancor00.zip"

doc:
	@echo Generating documentation...
	@doxygen ../src/Doxyfile

help:
	@echo ======= Makefile Help =======
	@echo Available targets:
	@echo -------------------------------
	@echo all       - runs calculator and standard deviation
	@echo run       - runs calculator GUI
	@echo stddev    - runs standard deviation calculation
	@echo test      - runs math library tests
	@echo doc       - generates documentation
	@echo pack      - creates project zip package
	@echo clean     - cleans temporary files
	@echo help      - shows this help
	@echo ==================================