# Migrator

Der Migrator dient als neuer Konverter und kann auch Tabellen Strukturen einfach und sicher ändern, ohne alle Daten vorher in eine neue Tabelle zu schreiben.
Der Migrator durchläuft, die Dateien von oben nach unten die in dem Ordner "Databases/Migrations" in diesem Projekt befinden.
Aus diesem Grund ist es *wichtig das die neuen SQL Statement (Dateien) immer mit dem korrekten Datum gespeichert werden*.
Es kann auch gern die letzte Datei aus dem Ordner genommen, kopiert und abgewandelt werden.

Im Folgenden werden einige SQL Statements erklärt, um die Tabellenstruktur in eine MySQL-Datenbank zu ändern.

Bei weiteren Fragen stehe ich (@Thomas Jünemann) gern zur Verfügung bzw. https://www.w3schools.com/sql/ hilf auch sehr beim Erstellen von SQL Statements.

## Fundort der Dateien
Im Ordner Databases/Migrations im csproj 'Migrator' findet ihr alle SQL Statements.
Hier sind Kleinigkeiten zu beachten: 

Beispiel Filename:
```
<jahr_monat_tag>_<entwickler>_<mode>_<tabellenname>.sql
```
Beispiel Attribute:
- Jahr: 2023
- Monat: 11
- Tag: 04
- Entwickler: thj
- Mode: 
  - CREATE (anlegen)
  - ADD (hinzufügen) 
  - RENAME (umbennen)
  - MODIFY (änderung)
  - DROP (löschen)
- Tabellenname: adressen

Ergibt dann eine Datei die beispielsweise so ausehen könnte.

```
2023_03_04_thj_create_adressen.sql
```
## Benutzungen der Statements

1) Eine neue Tabelle erstellen:

```sql
CREATE TABLE IF NOT EXISTS customers (
    id INT(11) NOT NULL AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    age INT(11),
    PRIMARY KEY (id)
    );
```
2) Eine Spalte zu einer vorhandenen Tabelle hinzufügen:
```sql
ALTER TABLE customers ADD phone VARCHAR(15) AFTER email;
```
3) Eine Spalte einer vorhandenen Tabelle umbenennen:
```sql
ALTER TABLE customers RENAME COLUMN phone TO mobile;
```
4) Den Datentyp einer Spalte ändern:
```sql
ALTER TABLE customers MODIFY age INT(11) UNSIGNED;
```
5) Eine Spalte aus einer vorhandenen Tabelle löschen:
```sql
ALTER TABLE customers DROP COLUMN age;
```
6) Eine Index-Spalte zu einer vorhandenen Tabelle hinzufügen:
```sql
ALTER TABLE customers ADD INDEX idx_name_email (name, email);
```
7) Eine FOREIGN KEY-Beziehung zwischen zwei Tabellen erstellen:
```sql
CREATE TABLE IF NOT EXISTS orders (
   id INT(11) NOT NULL AUTO_INCREMENT,
   customer_id INT(11) NOT NULL,
   product VARCHAR(50) NOT NULL,
   quantity INT(11),
   PRIMARY KEY (id),
   FOREIGN KEY (customer_id) REFERENCES customers(id)
);
```
8) Eine FOREIGN KEY-Beziehung aus einer Tabelle löschen:
```sql
ALTER TABLE orders DROP FOREIGN KEY orders_customer_id_fk;
```
9) Eine gesamte Tabelle löschen:
```sql
DROP TABLE customers;
```

## Multi Statements
Es können auch mehrer Statement mit einer einzigen Datei ausgeführt werden.

Beispiel:
```sql
ALTER TABLE customers ADD phone VARCHAR(15) AFTER email;
ALTER TABLE customers RENAME COLUMN email TO mail_address;
```
oder auch ein Update einer bestimmten Column (das WHERE ist hierbei optional) nach dem eine Tabelle modifiziert wurde:

```sql
ALTER TABLE customers RENAME COLUMN mail_address TO mail;
UPDATE customers SET product = "richtig cooles Produkt" WHERE quantity > 60
```
