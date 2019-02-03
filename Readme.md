Ce projet est utilis� pour experimenter les differents tests qu'un d�veloppeur peut r�aliser.
* Unit tests
* Integrated tests (narrow & broad)
* End to end tests

Suivre ces �tapes si vous voulez cr�er une base de donn�es :
https://docs.microsoft.com/fr-fr/ef/core/get-started/netcore/new-db-sqlite

**Au cas o� le lien n'est pas acc�ssible, voici les informations � retenir :**

Installez Microsoft.EntityFrameworkCore.Sqlite et Microsoft.EntityFrameworkCore.Design :

```dotnet add package Microsoft.EntityFrameworkCore.Sqlite``` 
et 
```dotnet add package Microsoft.EntityFrameworkCore.Design```

Cr�ez le mod�le : Le DbContext et les entit�s

Une fois que vous avez un mod�le, vous utilisez des migrations pour cr�er une base de donn�es.
Ex�cutez 
```
dotnet ef migrations add InitialCreate 
```
pour g�n�rer automatiquement un mod�le de migration et cr�er l�ensemble initial de tables du mod�le.
Ex�cutez 
```
dotnet ef database update 
```
pour appliquer la nouvelle migration � la base de donn�es. Cette commande cr�e la base de donn�es avant d�appliquer des migrations.

Pour plus d'info sur les migrations :
https://docs.microsoft.com/fr-fr/ef/core/managing-schemas/migrations/index

Modification du mod�le :
Si vous apportez des modifications au mod�le, vous pouvez utiliser la commande 
```
dotnet ef migrations add 
```
pour g�n�rer automatiquement une nouvelle migration. 
Apr�s avoir v�rifi� le code de mod�le g�n�r� automatiquement (et effectu� toutes les modifications n�cessaires), vous pouvez utiliser la commande 
```
dotnet ef database update 
```
pour appliquer les modifications de sch�ma � la base de donn�es.

EF Core utilise une table ```__EFMigrationsHistory``` dans la base de donn�es pour effectuer le suivi des migrations qui ont d�j� �t� appliqu�es � la base de donn�es.
Le moteur de base de donn�es SQLite ne prend pas en charge certaines modifications de sch�ma qui sont prises en charge par la plupart des autres bases de donn�es relationnelles. 
Par exemple, l�op�ration ```DropColumn``` n�est pas prise en charge. 

Les migrations EF Core g�n�rent du code pour ces op�rations, mais si vous tentez de les appliquer � une base de donn�es ou de g�n�rer un script, EF Core l�ve des exceptions. 
Consultez Limitations de SQLite. 
https://docs.microsoft.com/fr-fr/ef/core/providers/sqlite/limitations

Pour tout nouveau d�veloppement, il est pr�f�rable de supprimer la base de donn�es et d�en cr�er une nouvelle plut�t que d�utiliser des migrations quand le mod�le change.