"# clienttoserveranddatabase" 

-------------
Aplikacja pozwala na wysłanie zapytania bazodanowego typu select do serwera który następnie łączy się do bazy i wynik wysyła do klienta. 
----serializacja i deserializacja---- :)
-------------

-------------
    JAK PRZETESTOWAĆ
-------------

Potrzebny zainstalowany XAMPP z domyślnymi ustawieniami - login root i brak hasła. Na chwilę obecną na sztywno jest ustawione połączenie do bazy mysql na której możemy wykonać zapytanie select * from user;


-------------
    TODO
-------------

-------------
    SERVER
-------------

Dodać wątki do serwera
Poprawić obsługę błędów
Dodać możliwość ustawienia danych połączenia do bazy danych - np. pobieranie konfiguracji z config.xml
Dodać możliwość łączenia się nie tylko do MySql'a - Oracle, MS Server

-------------
    CLIENT
-------------

Dodać możliwość zapisu danych do xml -serializacja xml
Dodać możliwość wczytania danych z xml do DataGridView - deserializacja xml
Poprawić czcionki w textbox sql query na większe i bardziej czytelne

