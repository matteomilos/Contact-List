# Dodavanje testnih podataka pri prvom pokretanju #
Kako bi se dodali testni podaci u bazu podataka, potrebno je u "Package Manager Console" redom pokrenuti naredbe:
enable-migrations
add-migration InitialCreate
update-database
Nakon toga bit æe dodana dva kontakta u bazu podataka. Ukoliko se ne izvr¹e navedene naredbe, aplikacija æe i dalje uredno raditi te æe biti omoguæeno dodavanje novih kontakata izravno u aplikaciji
