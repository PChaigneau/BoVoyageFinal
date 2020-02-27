-- Afficher toutes les caractéristiques de la table Destination
sp_help 'Destination'

insert Destination(IdParente,
Nom,
Niveau, 
Description)
values (1, 'Guadeloupe', 3, 'Dans un site exceptionnel,en bordure d''un petit lagon turquoise, tout est réuni pour un séjour paradisiaque. Découvrez les merveilles de grande terre et de basse terre, les joies des plongées dans la réserve naturelle.')

-- delete from Destination where Destination.Nom = 'Saint-Barthélémy'

insert Destination(IdParente,
Nom,
Niveau, 
Description)
values (1, 'Saint-Barthélémy', 3, 'Imaginez une île où il fait 26 à 28 °C toute l''année. Baignez vous dans une eau turquoise.')

insert Destination(IdParente,
Nom,
Niveau, 
Description)
values (2, 'Birmanie', 2, 'La Birmanie est un pays passionnant pour tous ceux qui s’intéressent à l''art, aux civilisations, à l''hindouisme. Ce pays s''ouvre et a conservé toute la richesse de son patrimoine culturel. Visitez les temples, les marchés, ...')

insert Destination(IdParente,
Nom,
Niveau, 
Description)
values (3, 'Canada', 2, 'Découvrez la nature généreuse et les grandes villes du Canada en toute saison, grâce aux nombreux circuits que nous avons élaborés.')

insert Destination(IdParente,
Nom,
Niveau, 
Description)
values (1, 'Bretagne', 3, 'Superbe région. Terre de légendes. De nombreux spots pour le surf et le kitesurf.')


-- Afficher toutes les caractéristiques de la table Voyage

sp_help 'Voyage'

insert Voyage(IdDestination,
DateDepart,
DateRetour,
PlacesDispo,
PrixHT,
Reduction,
Descriptif)
values (1, '03/07/2020', '03/14/2020', 10, 789.99, 0.30, 'Séjour d''une semaine en Guadeloupe')

-- delete from Voyage where Voyage.IdDestination = 1

insert Voyage(IdDestination,
DateDepart,
DateRetour,
PlacesDispo,
PrixHT,
Reduction,
Descriptif)
values (2, '03/08/2020', '03/15/2020', 10, 999.99, 0.30, 'Séjour d''une semaine en Saint-Barthélémy')

-- delete from Voyage where Voyage.IdDestination = 2

insert Voyage(IdDestination,
DateDepart,
DateRetour,
PlacesDispo,
PrixHT,
Reduction,
Descriptif)
values (3, '04/08/2020', '04/15/2020', 10, 999.99, 0.30, 'Séjour d''une semaine en Birmanie')

-- delete from Voyage where Voyage.IdDestination = 3

insert Voyage(IdDestination,
DateDepart,
DateRetour,
PlacesDispo,
PrixHT,
Reduction,
Descriptif)
values (4, '04/07/2020', '04/14/2020', 10, 1099.99, 0.30, 'Séjour d''une semaine en Canada')

-- delete from Voyage where Voyage.IdDestination = 4

insert Voyage(IdDestination,
DateDepart,
DateRetour,
PlacesDispo,
PrixHT,
Reduction,
Descriptif)
values (5, '05/08/2020', '05/15/2020', 10, 399.99, 0.30, 'Séjour d''une semaine en Bretagne')

-- delete from Voyage where Voyage.IdDestination = 5


-- Afficher toutes les caractéristiques de la table Personne
sp_help 'Personne'

insert Personne(
TypePers,
Civilite,
Nom,
Prenom,
Email,
Telephone,
Datenaissance)
values (1, 'M', 'Deniro', 'Robert', 'robert.deniro@gmail.com', '0662661236', '08/17/1943')

insert Personne(
TypePers,
Civilite,
Nom,
Prenom,
Email,
Telephone,
Datenaissance)
values (2, 'F', 'Thurman', 'Uma', 'uma.thurman@gmail.com', '0668641276', '04/29/1970')


-- Afficher toutes les caractéristiques de la table Client
sp_help 'Client'

insert Client(Id)
values (1)

-- Afficher toutes les caractéristiques de la table Voyageur
sp_help 'Voyageur'

insert Voyageur(Id,
Idvoyage)
values (1, 9)

insert Voyageur(Id,
Idvoyage)
values (2, 9)

-- Ici on a deux voyageurs qui ont voyagé ensemble pour le voyage  IdVoyage 9 et dont la réservation a été faite par le client  Id 1


-- Afficher toutes les caractéristiques de la table DossierResa

sp_help 'DossierResa'

insert DossierResa (NumeroCB,
IdClient,
IdEtatDossier,
IdVoyage)
values ('4900560236488955', 1, 4, 9)

-- Afficher toutes les caractéristiques de la table EtatDossier

sp_help 'EtatDossier'

insert EtatDossier(Id,
Libelle)
values (1, 'En attente')

insert EtatDossier(Id,
Libelle)
values (2, 'En cours')

insert EtatDossier(Id,
Libelle)
values (3, 'Refusée')

insert EtatDossier(Id,
Libelle)
values (4, 'Acceptée')

-- delete from EtatDossier where EtatDossier.Id = 1


-- Afficher toutes les caractéristiques de la table Photo
sp_help 'Photo'

insert Photo(NomFichier,
			 IdDestination)
values ('Guadeloupe_Photo.jpg', 1)

insert Photo(NomFichier,
			 IdDestination)
values ('Saint_Barthelemy_Photo.jpg', 2)

insert Photo(NomFichier,
			 IdDestination)
values ('Birmanie_Photo.jpg', 3)

insert Photo(NomFichier,
			 IdDestination)
values ('Canada_Photo.jpg', 4)

insert Photo(NomFichier,
			 IdDestination)
values ('Bretagne_Photo.jpg', 5)

-- drop table Photo
-- delete from Photo where Photo.Id = 11
select Photo.Id from Photo

-- Backup and Restore DataBase
backup database BoVoyage to disk = N'C:\Users\Adminl\Desktop\BoVoyage\BDD\BoVoyage.bak' with format

restore database BoVoyage from disk = N'C:\Users\Adminl\Desktop\BoVoyage\BDD\BoVoyage.bak'
with
move 'BoVoyage' to N'C:\Users\Adminl\Desktop\BoVoyage\BDD\BoVoyage.mdf',
move 'BoVoyage_log' to N'C:\Users\Adminl\Desktop\BoVoyage\BDD\BoVoyage.ldf'