# Nastavenie defaultného cieľa
.DEFAULT_GOAL := all

# Hlavný projekt, ktorý sa spúšťa (môžeš upraviť podľa potreby)
PROJECT_PATH = CalculatorApp

# Výstupný priečinok, predpokladá sa Debug build
BUILD_DIR = $(PROJECT_PATH)\bin\Debug

# Výstupný súbor (prispôsobiť názvu .exe podľa VS projektu)
EXECUTABLE = $(BUILD_DIR)\$(PROJECT_PATH).exe

# Výstupný archív pre pack
ARCHIVE_NAME = CalculatorApp.zip

## Spustí program (musí byť predtým skompilovaný vo Visual Studiu)
all: run

## Spustí program (musí byť skompilovaný vo VS)
run:
	@if exist "$(EXECUTABLE)" ( \
		echo Spustenie programu... && \
		"$(EXECUTABLE)" \
	) else ( \
		echo [CHYBA] Program nie je skompilovaný. Skompiluj ho najprv vo Visual Studiu. && \
		exit /b 1 \
	)

## Zabalí celý projekt do ZIP archívu na odovzdanie
pack:
	@if exist "$(ARCHIVE_NAME)" del /q "$(ARCHIVE_NAME)"
	@echo Zabalovanie projektu do $(ARCHIVE_NAME)...
	@powershell -Command "Compress-Archive -Path * -DestinationPath '$(ARCHIVE_NAME)' -Force"
	@echo Hotovo.

## Vyčistí všetky výstupné (neodovzdávané) súbory
clean:
	@echo Mazanie súborov, ktoré nemajú byť odovzdané...
	@if exist "$(PROJECT_PATH)\bin" rmdir /s /q "$(PROJECT_PATH)\bin"
	@if exist "$(PROJECT_PATH)\obj" rmdir /s /q "$(PROJECT_PATH)\obj"
	@if exist "$(ARCHIVE_NAME)" del /q "$(ARCHIVE_NAME)"
	@echo Hotovo.

## Spustí program so vstupom pre výpočet smerodajnej odchýlky
stddev:
	@if exist "$(EXECUTABLE)" ( \
		echo Spúšťam test pre smerodajnú odchýlku... && \
		"$(EXECUTABLE)" < test_input.txt \
	) else ( \
		echo [CHYBA] Program nie je skompilovaný. Skompiluj ho najprv vo Visual Studiu. && \
		exit /b 1 \
	)

## Zobrazí nápovedu
help:
	@echo ======= Makefile napoveda =======
	@echo Dostupné ciele:
	@echo   make / make all       - Spustí program (musí byť skompilovaný cez Visual Studio)
	@echo   make run              - To isté ako all
	@echo   make stddev           - Spustí program s testovacím vstupom pre smerodajnú odchýlku
	@echo   make pack             - Zabalí projekt do CalculatorApp.zip pre odovzdanie
	@echo   make clean            - Odstráni bin/obj a ďalšie dočasné súbory
	@echo   make help             - Zobrazí túto nápovedu
	@echo ==================================