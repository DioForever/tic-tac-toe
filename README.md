# Tic-Tac-Toe

- ## 1. Úvod
  - ### 1.1 Účel
    - Vytvoření aplikace pro hraní piškvorek s možností volby velikosti hracího pole a možností hrát lokálně proti někomu jinému nebo proti umělé inteligenci (botovi).
  - ### 1.2 Konvence dokumentu
    - Tituly jsou veliké podle důležitosti "#", jsou tři velikosti využiti od největšího po nejmenší. Používá se zvýraznění textu pro zvýraznění pojmenování.
  - ### 1.3 Cílová skupina
    - Tento dokument je určený pro vývojáře, kteří chtějí vytvořit piškvorky aplikaci.
  - ### 1.4 Kontakty
    - Mail: vaskodaniel1@gmail.com
- ## 2. Popis
  - ### 2.1 Produkt
    Produkt bude vyvíjen v jazyku C# a bude spouštěn jako .exe soubor.
  - ### 2.2 Funkce
    Hra zahrnuje možnost volby velikosti hracího pole (3x3, 4x4, 5x5) a možnost hraní proti jinému hráči lokálně nebo proti počítači (botovi).
  - ### 2.3 Uživatelské skupiny
    Je jen jedna skupina, **základní uživatelé** budou mít možnost hrát piškvorky jako lokální hráči proti sobě nebo proti počítači (botovi).
  - ### 2.4 Provozní prostředí
    Aplikace je navržena pro desktopové počítače.
  - ### 2.5 Uživatelské prostředí
    Možnost volby velikosti hracího pole, možnost volby mezi hraním proti jinému hráči nebo proti počítači, grafické rozhraní pro hrací pole.
- ## 3. Požadavky na rozhraní
  - ### 3.1 Uživatelské rozhraní
    Desktopová aplikace s grafickým rozhraním pro hrací pole a volbu možností.
- ## 4. Vlastnosti systému
  - ### 4.1 Velikost hracího pole
    - Důležitost: **HIGH**
    - Možnost volby velikosti hracího pole: 3x3, 4x4, 5x5.
  - ### 4.2 Hraní proti jinému hráči
    - Důležitost: **HIGH**
    - Možnost hraní piškvorek proti jinému hráči na jednom zařízení.
  - ### 4.3 Hraní proti počítači
    - Důležitost: **HIGH**
    - Možnost hraní piškvorek proti počítači (bot).
  - ### 4.4 Grafické rozhraní
    - Důležitost: **MIDDLE**
    - Uživatelsky přívětivé grafické rozhraní pro snadné ovládání hry.
- ## 5. Nefunkční požadavky
  - ### 5.1 Výkonnost
    - Odezva a provádění tahů ve hře, s největší odezvou 1s.
  - ### 5.2 Bezpečnost
    - Bezpečnost není primárně důležitá, jelikož se jedná o offline aplikaci a je nám jedno jestli hráči podváději proti svým přítelům či botovi.
  - ### 5.3 Spolehlivost
    - Zajištění funkčnosti aplikace a předejití možných chyb, crash aplikace jednou za 100 000 spuštění.
  - ### 5.4 Projektová dokumentace
    - Aktuální dokument obsahující specifikace systému.
  - ### 5.5 Uživatelská dokumentace
    - Dokumentace pro uživatele včetně instrukcí a popisu funkcionality aplikace.
