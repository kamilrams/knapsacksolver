KnapsackProblem solver
======================

Wstęp
-----
Naiwna implementacja algorytmu genetycznego do rozwiązania problemu plecakowego
- [o algorytmach genetycznych](https://pl.wikipedia.org/wiki/Algorytm_genetyczny)
- [o problemie plecakowym](https://pl.wikipedia.org/wiki/Problem_plecakowy)

Projekt składa się z implementacji algorytmu genetycznego oraz aplikacji okienkowej do jego uruchamiania,
wraz z prezentacją wyników zwróconych przez algorytm.

Całość została przygotowana na zaliczenie przedmiotu _Metody numeryczne_.

Wykorzystane technologie
------------------------
- Język C# na platformie .NET 6.0
- [AvaloniaUI](https://avaloniaui.net/) - framework wykorzystany do stworzenia aplikacji desktopowej
- [NUnit](https://nunit.org/) - do testów jednostkowych
- [Microsoft MVVM Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) - gotowa implementacja wzorca MVVM
- [ScottPlot](https://scottplot.net/) - biblioteka do wyświetlania diagramów

Elementy rozwiązania
--------------------
Rozwiązanie składa się z następujących projektów (modułów):
- `KnapsackProblem.Solver` - właściwa implementacja algorytmu genetycznego do rozwiązania problemu plecakowego
- `KnapsackProblem.DesktopApp` - aplikacja okienkowa, punkt wejściowy programu, posiada następujące funkcje:
    - wprowadzanie parametrów dla algorytmu genetycznego
    - wprowadzanie danych wejściowych
    - uruchamianie algorytmu, wraz z prezentacją postępów z jego wykonywania
    - prezentacja wyników
- `KnapsackProblem.Solver.Tests` - testy jednostkowe weryfikujące poprawne działanie zaimplementowanego algorytmu
- `KnapsackProblem.InputGenerator` - generator danych wejściowych dla algorytmu, w formie niezależnej biblioteki
- `KnapsackProblem.InputGenerator.ConsoleApp` - aplikacja konsolowa pozwalająca wygenerować dane wejściowe; korzysta z wymienionej powyżej biblioteki

Opis działania algorytmu
------------------------
1. Algorytm wczytuje zadane parametry oraz dane wejściowe
    - lista parametrów:
        - `ilość generacji`
        - `wielkość populacji początkowej`
        - `jakość populacji początkowej`
        - `prawdopodobieństwo krzyżowania`
        - `prawdopodobieństwo mutacji`
        - `ziarno generatora losowego`
    - dane wejściowe:
        - `pojemność plecaka`
        - `lista przedmiotów`
            - każdy przedmiot zdefiniowany jest poprzez 3 właściwości:
                - `nazwa`
                - `waga`
                - `wartość`
2. Następuje walidacja danych wejściowych
3. Wygenerowanie populacji początkowej chromosomów
    - każdy chromosom zawiera zakodowane rozwiązanie w formie tablicy typu `bool`
        - można je traktować jak _bity_
        - ilość elementów tablicy odpowiada ilości przedmiotów z danych wejściowych
        - indeks elementu to indeks przedmiotu danych wejściowych
        - jeśli element w tablicy o zadanym indeksie jest `true`, to znaczy że ten przedmiot został wybrany
    - wygenerowana populacja początkowa zależy od parametru `jakość populacji początkowej`
        - parametr ten określa, jaki procent populacji początkowej powinien zawierać _prawidłowe rozwiązanie_ (waga całkowita nie przekracza podanej `pojemności plecaka`)
        - *uwaga* - populacja początkowa losowana jest aż do skutku (dopóki `jakość populacji początkowej` nie zostanie spełniona);
        może to być czasochłonne, więc kluczowe jest dobranie rozsądnej wartości dla tego parametru (oraz dla `pojemności plecaka`)
4. Iteracja przez N generacji, wykonując dla każdej generacji:
    - obliczenie przystosowania chromosomu (jak dobre jest reprezentowane przez niego rozwiązanie?)
        - rozwiązanie którego waga całkowita przekracza założoną `pojemność całkowitą` dostaje ocenę `0`
        - dla pozostałych rozwiązań ocena jest sumą wartości wybranych przedmiotów
    - wybranie chromosomów do puli rodzicielskiej (na zasadzie rankingowej - wybierane jest X chromosomów z najlepszą oceną)
    - przeprowadzenie krzyżowania na osobnikach z puli rodzicielskiej
        - w wyniku krzyżowania zawsze powstaną dwa nowe chromosomy
        - wynikowe chromosomy mogą być klonami rodziców (jeśli nie zaszło `prawdopodobieństwo krzyżowania`) lub całkiem nowymi chromosomami
        - krzyżowanie dwóch chromosomów polega na wylosowaniu punktu przecięcia, podziale każdego z nich na dwie części w wylosowanym
        miejscu, a następnie pomieszaniu tych części ze sobą - przykład:
            - chromosom 1: 110001, chromosom 2: 001011
            - wylosowany punkt przecięcia: 3
                - części chromosomu 1: 110 oraz 001
                - części chromosomu 2: 001 oraz 011
            - wynik krzyżowania:
                - wynik 1: 110011
                - wynik 2: 001001
    - przeprowadzenie mutacji chromosomów
        - mutacja jest przeprowadzana na każdym chromosomie
        - polega ona na negacji każdego bitu chromosomu, dla którego zajdzie `prawdopodobieństwo mutacji`
    - znalezienie najlepszego chromosomu w danej generacji, zapamiętanie go, przejście do kolejnej generacji bądź zakończenie działania algorytmu
    - wynikiem algorytmu jest lista przetworzonych generacji z przypisanym najlepszym rozwiązaniem w danej generacji

Zrzuty ekranu
-------------
![okno główne programu](./Attachments/Screenshots/main_window.png | width=500)

![prezentacja wyników w formie diagramu](./Attachments/Screenshots/results_diagram.png | width=500)

![prezentacja wyników w formie tabeli](./Attachments/Screenshots/results_table.png | width=500)


Jak uruchomić
-------------
- do zbudowania solucji wymagane jest Visual Studio 2022
- projekt startowy dla aplikacji okienkowej to `KnapsackProblem.DesktopApp`

Dodatkowe linki
---------------
- [profil autora na LinkedIn](https://www.linkedin.com/in/kamil-rams-99a0571a1/)