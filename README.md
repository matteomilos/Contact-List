# Dodavanje testnih podataka pri prvom pokretanju #
Kako bi se dodali testni podaci u bazu podataka, potrebno je u "Package Manager Console" redom pokrenuti naredbe:
enable-migrations
add-migration InitialCreate
update-database
Nakon toga bit �e dodana dva kontakta u bazu podataka. Ukoliko se ne izvr�e navedene naredbe, aplikacija �e i dalje uredno raditi te �e biti omogu�eno dodavanje novih kontakata izravno u aplikaciji
