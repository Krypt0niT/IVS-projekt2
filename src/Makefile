PROFILING_EXE := CalculatorApp.Profiling\bin\Debug\net8.0\CalculatorApp.Profiling.exe
CALCULATOR_EXE := WpfApp1\bin\Debug\net8.0-windows\WpfApp1.exe
STDDEV_INPUT := CalculatorApp.Profiling\testInput.txt

.DEFAULT_GOAL := all

all: build run

build:
	@echo Building solution...
	dotnet publish WpfApp1 -c Release -r win-x64 -o WpfApp1\bin\Debug\net8.0-windows

run:
	@echo Starting calculator GUI...
	@"$(CALCULATOR_EXE)"

stddev:
	@echo Running StdDev with test data...
	@$(PROFILING_EXE) < $(STDDEV_INPUT)

clean:
	@echo Removing unnecessary files...
	@for /d /r . %%d in (bin,obj) do @if exist "%%d" rmdir /s /q "%%d"
	@echo Clean complete.

pack:
	@echo Packing project...
	@powershell -Command "Compress-Archive -Path '..\*' -DestinationPath '..\xuradnl00_xkuzmok00_xlancor00.zip' -Force"
	@echo Packing complete.

doc:
	@echo Generating documentation...
	@doxygen "Doxyfile"

help:
	@echo ======= Makefile Help =======
	@echo Available targets:
	@echo -------------------------------
	@echo all       - runs calculator GUI
	@echo run       - runs calculator GUI (builds if needed)
	@echo stddev    - runs standard deviation calculation
	@echo doc       - generates documentation
	@echo pack      - creates project zip package
	@echo clean     - cleans temporary files
	@echo help      - shows this help
	@echo ==================================