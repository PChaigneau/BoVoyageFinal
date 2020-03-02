-- Afficher toutes les caractéristiques de la table Destination
--sp_help 'Destination'

-- Creation des 5 continents (niveau 1)

insert Destination(Id, Nom, Niveau, Description) values 
(100, 'Europe', 1, 'Continent Europe'), 
(200, 'Asie', 1, 'Continent Asie'),
(300, 'Afrique', 1, 'Continent Afrique'),
(400, 'Amérique', 1, 'Continent Amérique'),
(500, 'Océanie', 1, 'Continent Océanie')


-- Creation des destinations pays (niveau 2)

insert Destination(Id, IdParente, Nom, Niveau, Description) values
(110, 100, 'France', 2, 'Pays France'),
(210, 200, 'Birmanie', 2, 'La Birmanie est un pays passionnant pour tous ceux qui s’intéressent à l''art, aux civilisations, à l''hindouisme. Ce pays s''ouvre et a conservé toute la richesse de son patrimoine culturel. Visitez les temples, les marchés, ...'),
(410,400, 'Canada', 2, 'Découvrez la nature généreuse et les grandes villes du Canada en toute saison, grâce aux nombreux circuits que nous avons élaborés.')

-- Ajout nouvelles Destinations pays
insert Destination(Id, IdParente, Nom, Niveau, Description) values
(310,300, 'Afrique du Sud', 2, 'L’Afrique du Sud est un de ces « pays monde », un mélange colorée de langues, cultures et de paysages qui se succèdent, mais ne se ressemblent pas.'),
(510,500, 'Australie', 2, 'Des côtes orientales au Territoire du Nord en passant par le cœur de l’Océanie, l’Australie vous embarque d’un extrême à l’autre par la route, les airs, et une petite croisière dans la Grande Barrière de corail.'),
(520,500, 'Indonesie', 2, 'L''Indonésie se définit par sa diversité ethnique. De part et d''autre de la multitude d''îles, les sites à découvrir se multiplient en voyage en Indonésie. Des forêts de Sumatra aux volcans de Java, le dépaysement accompagne les vacances en Indonésie.'),
(320,300, 'Maroc', 2, 'Offrez-vous des vacances au Maroc et visitez le Jardin Majorelle à Marrakech et la Mosquée Hassan II à Casablanca. Vous pourrez aussi faire du golf au Maroc sur des greens de grande qualité. Un voyage au Maroc, c''est un périple entre tradition et modernisme.'),
(120,100, 'Norvege', 2, 'La Norvège est remarquable par bien des aspects : c’est le pays le plus septentrional d’Europe, mais aussi celui dont la densité est la plus faible du continent. C’est enfin le premier pays au classement mondial de l’Indice de Développement Humain.')

-- Creation des destinations region (niveau 3)
insert Destination(Id, IdParente, Nom, Niveau, Description) values
(111, 110, 'Guadeloupe', 3, 'Dans un site exceptionnel,en bordure d''un petit lagon turquoise, tout est réuni pour un séjour paradisiaque. Découvrez les merveilles de grande terre et de basse terre, les joies des plongées dans la réserve naturelle.'),
(112, 110, 'Saint-Barthélémy', 3, 'Imaginez une île où il fait 26 à 28 °C toute l''année. Baignez vous dans une eau turquoise.'),
(113, 110, 'Bretagne', 3, 'Superbe région. Terre de légendes. De nombreux spots pour le surf et le kitesurf.')

-- delete from Destination where Destination.Nom = 'Saint-Barthélémy'

-- Afficher toutes les caractéristiques de la table Voyage

--sp_help 'Voyage'

insert Voyage(IdDestination, DateDepart, DateRetour, PlacesDispo, PrixHT, Reduction, Descriptif) values 
(111, '03/07/2020', '03/14/2020', 10, 789.99, 0.30, 'Séjour d''une semaine en Guadeloupe'),
(112, '03/08/2020', '03/15/2020', 10, 999.99, 0.30, 'Séjour d''une semaine en Saint-Barthélémy'),
(210, '04/08/2020', '04/15/2020', 10, 999.99, 0.30, 'Séjour d''une semaine en Birmanie'),
(410, '04/07/2020', '04/14/2020', 10, 1099.99, 0.30, 'Séjour d''une semaine en Canada'),
(113, '05/08/2020', '05/15/2020', 10, 399.99, 0.30, 'Séjour d''une semaine en Bretagne')

-- Ajout nouveaux voyages
insert Voyage(IdDestination, DateDepart, DateRetour, PlacesDispo, PrixHT, Reduction, Descriptif) values 
(310, '03/07/2020', '03/14/2020', 10, 2099.99, 0.30, 'Séjour d''une semaine en Afrique du Sud'),
(510, '03/21/2020', '03/28/2020', 10, 1599.99, 0.30, 'Séjour d''une semaine en Australie'),
(520, '03/21/2020', '03/28/2020', 10, 1299.99, 0.30, 'Séjour d''une semaine en Indonesie'),
(320, '03/07/2020', '03/14/2020', 10, 1299.99, 0.30, 'Séjour d''une semaine au Maroc'),
(120, '03/07/2020', '03/14/2020', 10, 699.99, 0.30, 'Séjour d''une semaine en Norvege')

-- delete from Voyage where Voyage.IdDestination = 1
--select * from Voyage

-- Afficher toutes les caractéristiques de la table Personne
--sp_help 'Personne'

insert Personne(TypePers, Civilite, Nom, Prenom, Email, Telephone, Datenaissance) values 
(1, 'M', 'Deniro', 'Robert', 'robert.deniro@gmail.com', '0662661236', '08/17/1943'),
(2, 'F', 'Thurman', 'Uma', 'uma.thurman@gmail.com', '0668641276', '04/29/1970')

-- Afficher toutes les caractéristiques de la table Client
--sp_help 'Client'

insert Client(Id)
values (1)
--select * from Client
-- Afficher toutes les caractéristiques de la table Voyageur
--sp_help 'Voyageur'

insert Voyageur(Id, Idvoyage) values 
(1, 2),
(2, 2)

-- Ici on a deux voyageurs qui ont voyagé ensemble pour le voyage  IdVoyage 9 et dont la réservation a été faite par le client  Id 1


-- Afficher toutes les caractéristiques de la table EtatDossier

--sp_help 'EtatDossier'

insert EtatDossier(Id, Libelle) values 
(1, 'En attente'),
(2, 'En cours'),
(3, 'Refusée'),
(4, 'Acceptée')

-- Afficher toutes les caractéristiques de la table DossierResa

--sp_help 'DossierResa'

insert DossierResa (NumeroCB, IdClient, IdEtatDossier, IdVoyage) values 
('4900560236488955', 1, 4, 2)

-- delete from EtatDossier where EtatDossier.Id = 1


-- Afficher toutes les caractéristiques de la table Photo
--sp_help 'Photo'

insert Photo(NomFichier, IdDestination) values 
('Guadeloupe_Photo.jpg', 111),
('Saint_Barthelemy_Photo.jpg', 112),
('Birmanie_Photo.jpg', 210),
('Canada_Photo.jpg', 410),
('Bretagne_Photo.jpg', 113)

-- Ajout nouvelles Photos
insert Photo(NomFichier, IdDestination) values 
('Afrique du Sud_Photo.jpg', 310),
('Australie_Photo.jpg', 510),
('Indonesie_Photo.jpg', 520),
('Maroc_Photo.jpg', 320),
('Norvege_Photo.jpg', 120)

-- drop table Photo
-- delete from Photo where Photo.Id = 11
select Photo.Id from Photo

-- Backup and Restore DataBase
backup database BoVoyage to disk = N'C:\Users\Adminl\Desktop\BoVoyage\BoVoyageFinal\BoVoyageFinal\BDD\BoVoyage.bak' with format

restore database BoVoyage from disk = N'C:\Users\Adminl\Desktop\BoVoyage\BoVoyageFinal\BoVoyageFinal\BDD\BoVoyage.bak'
with
move 'BoVoyage' to N'C:\Users\Adminl\Desktop\BoVoyage\BoVoyageFinal\BoVoyageFinal\BDD\BoVoyage.mdf',
move 'BoVoyage_log' to N'C:\Users\Adminl\Desktop\BoVoyage\BoVoyageFinal\BoVoyageFinal\BDD\BoVoyage.ldf'