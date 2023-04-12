# Migrator

Hier sind einige SQL Statements um die Tabellenstruktur in eine MySQL Datenbank zu ändern.

## Fundort
Im Ordner Databases/Migrations im csproj 'Migrator' findet ihr alle SQL Statements.
Hier sind Kleinigkeiten zu beachten: 

Beispiel Filename:
```
<jahr_monat_tag>_<entwickler>_<mode>_<tabellenname>.sql
```

Beispiel Attribute:
- jahr: 2023
- monat: 11
- tag: 04
- entwickler: thj
- mode: 
  - CREATE (anlegen)
  - ADD (hinzufügen) 
  - RENAME (umbennen)
  - MODIFY (änderung)
  - DROP (löschen)
- tabellenname: adressen

Ergibt im dann eine Datei die beispielsweise so ausehen könnte.

```
2023_03_04_thj_create_adressen.sql
```
+
Es kann auch gern die letzte genommen, kopiert und abgewandelt werdenm, *wichtig ist nur das die Dateien die am neusten sind auch ganz unten stehen*, der Migrator geht diesen Ordner nämlich von Oben nach Unten durch.

## Benutzungen

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

## Multistatements
Es können auch mehrer Statement mit einer einzigen Datei ausgeführt werden.

Beispiel:
```sql
ALTER TABLE customers ADD phone VARCHAR(15) AFTER email;
ALTER TABLE customers RENAME COLUMN email TO mail_address;
```

