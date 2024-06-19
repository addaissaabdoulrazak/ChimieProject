# Application d'Échange de Produits Chimiques entre Laboratoires

## Objectif

Le développement d’une version basique d’une application permettant d’échanger des produits chimiques entre laboratoires de recherche. Cette application distingue deux types d’utilisateurs : les administrateurs et les utilisateurs (laboratoires de recherche).

## Table des Matières

- [Use Cases](#use-cases)
  - [Utilisateur](#utilisateur)
  - [Administrateur](#administrateur)
- [Processus d'Inscription dans la Plateforme](#processus-dinscription-dans-la-plateforme)
  - [Demande d'inscription](#demande-dinscription)
  - [Actions de l’administrateur](#actions-de-ladministrateur)
- [Langages et Technologies](#langages-et-technologies)

## Use Cases

### Utilisateur

- Faire une demande d’inscription sur la plateforme.
- Éditer les données de son compte une fois inscrit.
- Gérer un produit à échanger.
- Rechercher des produits échangés par les autres laboratoires.

### Administrateur

- Gérer les demandes d’inscription faites par les utilisateurs :
  - Accepter les demandes.
  - Décliner les demandes.
  - Désactiver un compte.
- Gérer les publications d’échange faites par les laboratoires.

## Processus d'Inscription dans la Plateforme

### Demande d'inscription

1. Un laboratoire remplit une demande d’inscription (stockée dans la table `Laboratoire` avec le statut "Demande").

### Actions de l’administrateur

- **Accepter la demande** : Le statut du laboratoire change de "Demande" à "Actif".
- **Décliner la demande** : L’enregistrement de la demande est supprimé de la table `Laboratoire`.

## Langages et Technologies

- **ASP.NET**
- **ADO.NET**
- **C#**
- **SQL Server**


