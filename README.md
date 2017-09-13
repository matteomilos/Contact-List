# Dodavanje testnih podataka pri prvom pokretanju #
Kako bi se dodali testni podaci u bazu podataka, potrebno je u "Package Manager Console" redom pokrenuti naredbe:

enable-migrations

add-migration InitialCreate (prilikom ove naredbe mozda dode do poruke: "Unable to generate an explicit migration because the following explicit migrations are pending...", u tom slucaju treba je zanemariti i prijeci na sljedecu)

update-database

Nakon toga bit ce dodana dva kontakta u bazu podataka. Ukoliko se ne izvrse navedene naredbe, aplikacija ce i dalje uredno raditi te ce biti omoguceno dodavanje novih kontakata izravno u aplikaciji
