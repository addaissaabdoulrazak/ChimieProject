Objectif :
Le développement d’une version basique d’une application permettant d’échanger des produits chimiques entre laboratoires de recherche. Cette application distinguera deux types d’utilisateurs : les administrateurs et les utilisateurs (laboratoires de recherche)

Use Case Utilisateur

Faire une demande d’inscription dans la plateforme.
Éditer les données de son compte une fois inscrit.
Gérer un produit à échanger.
Rechercher des produits échangés par les autres laboratoires.

Use Case Administrateur

Gérer les demandes d’inscription faites par les utilisateurs :
Accepter les demandes.
Décliner les demandes.
Désactiver un compte.
Gérer les publications d’échange faites par les laboratoires.
﻿
Processus d'Inscription dans la Plateforme

Demande d'inscription :
Un laboratoire remplit une demande d’inscription (stockée dans la table Laboratoire avec le statut "Demande").
Actions de l’administrateur :
Accepter la demande : Le statut du laboratoire change de "Demande" à "Actif".
Décliner la demande : L’enregistrement de la demande est supprimé de la table Laboratoire.

Langages et Technologies : Asp.net, Ado.net, C#, SQL Server
