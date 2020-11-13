# Développement Unity de Curve Fever et Implémentation d'Intelligences Artificielles

## Description

UV-Projet-P2 est un projet étudiant consistant en la réalisation d'un jeu du type Curve Fever sur Unity, ainsi que la création de différentes intelligences artificielles pouvant y jouer.

## Installation

Le projet a été développé et testé sur Windows 10. Nous ne pouvons pas garantir qu'il fonctionnera sur un autre système d'exploitation.

1. Télécharger Unity au lien suivant : [Unity](https://unity3d.com/fr/get-unity/download)
Nous avons testé le jeu avec la version 2019.4.11f1 de Unity. L'utilisation de Unity Hub est recommandée.

1. Cloner le projet sur votre machine
`git clone https://github.com/fbigand/UV-Projet-P2.git`

1. Ajouter le dossier cloné en tant que projet depuis Unity ou Unity Hub.

1. Dans le fichier `Assets/Scripts/ShipController/ControllerIALearning.cs`, modifier la valeur de la variable `pathPythonExe` par le chemin d'accès de l'exécutable Python de votre machine. Nous n'avons pas encore modifié le fichier pour accéder à une variable d'environnement.

1. Ouvrir le projet depuis Unity et charger la scène MainMenu (`Assets/Scenes/MainMenu.unity`)

1. Lancer le jeu

L'IA utilisant le modèle d'apprentissage n'est à ce jour que très peu performant (30 secondes de délai entre chaque frame). Nous déconseillons de l'utiliser, hormis par curiosité.

<hr>

Credits: Frank Bigand, Emile Levast, Alex Gautier
