# Développement Unity de Curve Fever et Implémentation d'Intelligences Artificielles

**Description:**

**Développement du jeu sous Unity :**

  -Navigation (arène, vaisseau, déplacement et gestion de collisions)

  -Bonus (roquette et saut)

  -Multijoueurs (joueur ou ordinateur)

  -Comestibles (modificateurs de vitesse)

  -Interface Utilisateur (HUD - cooldown et score)

  -Hall d’accueil (lobbying - paramétrage de partie)


**Intégration d’intelligences artificielles :**

  -brute (aléatoire, prédéfinie, détection d’obstacle et zone de danger)

  -apprentissage (classification prise de décision)

-------------------------------------------------------------------------------

**Le jeu : Curve Fever**

Curve Fever est un jeu 2D, multijoueur, de type snake, dans lequel vous affrontez 1 à 3 autres joueurs dans une arène. Le but du jeu est d'être le premier à atteindre un certain nombre de points. Pour cela, vous devez survivre le plus longtemps possible en évitant les traînées et les murs. L’attribution des points se fait en fonction du classement des joueurs à chaque manche.

---------------------------------------------------------------------------------

_**Les fonctionnalités**_

Au lancement, un hall d’accueil (lobby) permet de paramétrer le jeu. Une partie peut accueillir jusqu’à 4 joueurs que ce soit en local sur une même machine ou contre des IA.
Une liste déroulante (Dropdown) permet d’affecter le rôle de joueur ou de sélectionner le niveau de difficulté de l’intelligence artificielle. Les pseudos des joueurs sont personnalisables.

Une case à cocher (CheckBox) permet d’activer les pouvoirs comestibles.
Au démarrage, chaque joueur se voit attribuer une couleur de vaisseau et de traînée.
Les vaisseaux avancent automatiquement en laissant une traînée derrière leur corps. Des trous sont créés de manière répétitive et constante afin d’offrir des nouveaux chemins aux joueurs.

Pour le joueur, les touches directionnelles : 

- gauche et droite permettent l’orientation du vaisseau.

- haut permet le lancement d’une roquette détruisant les adversaires

- bas permet d’activer un saut sur une courte distance utile pour traverser les traînées

Une interface joueur (HUD) affiche des informations concernant le score du joueur et le temps restant avant la possibilité d’activation (cooldown) des pouvoirs roquette et saut.

Des bonus comestibles apparaissent aléatoirement dans l’arène. Ces bonus ralentissent ou accélère la vitesse des joueurs. Les verts agissent sur le vaisseau qui a consommé tandis que les oranges agissent sur tous les autres.

Un bouton “restart” permet le retour au lobby pour choisir des options différentes de partie.
A la fin de chaque manche, le HUD des points s’affiche pour présenter le gain de chacun ainsi que leur score total.

-----------------------------------------------------------------------------------------

**Architecture logicielle**

*Le choix de l’environnement Unity*

Unity est un moteur de jeu multiplateforme connu pour sa rapidité aux prototypages. Au travers de Scènes, vous placez des éléments “GameObjects” : environnements, obstacles et décorations. Le comportements des GameObject est défini par le biais de scripts C# .

*Décomposition logicielle*

Notre jeu possède deux scènes : MenuScene correspond au hall d’accueil (lobby) pour la paramétrisation des parties et MainScene où se déroule notre partie. La connexion entre les deux scènes se fait via une classe statique appelée GameSettings

La classe ManagePlayers manage les joueurs durant la partie. Cela concerne : leur positionnement, leur couleur, leurs pouvoirs et leur gain de manche et leur score total.

Les scripts associés au vaisseau sont : la classe de définition (Player), le mouvement (Ship Movement), la traînée qu’il laisse derrière (Snake), ses bonus (Powers).

Les scripts Controller permettent de transmettre les choix de directions aux autres scripts. Les décisions peuvent être écoutées au contact des touches du clavier pour un humain, ou calculées par rapport à l’environnement au travers des algorithmes d’intelligence artificielle.

----------------------------------------------------------------------------------------------

**IA programmées algorithmiquement**

*IA Aléatoire “Easy”*

Cette IA décide aléatoirement de tourner à gauche, droite ou rester tout droit. Cette IA peu intelligente nous a permis de réaliser des tests et d’intégrer le système d’IA à notre architecture d’application. 

*IA à trajectoire prédéfinie*

Notre objectif, au travers de cette IA à trajectoire prédéfinie, est de rester le plus longtemps en vie sans s'intéresser aux interactions avec l’environnement. Nous avons fait le choix d’une trajectoire en spirale.

*IA à détection d’obstacle*

Cette IA analyse son environnement proche pour prendre des décisions. Elle utilise des rayons de collision (raycast) pour détecter les objets environnants. 
Une vingtaine de raycast permet de parcourir un maximum de terrain. 
L’information de l’obstacle le plus proche permet de définir la direction à prendre. 
Si l’obstacle est considéré comme “loin” (défini par une valeur de distance arbitraire), alors le vaisseau continue tout droit. 
Sinon on calcule le signe de l’angle formé par le vecteur RAYCAST et le vecteur RAYCAST + NORMALE OBSTACLE. 
Si l’angle est positif, le vaisseau tourne à gauche, sinon il tourne à droite.

*IA à détection de zone de danger*

L’IA à détection de zone de danger utilise un nouvel algorithme qui prend en compte chaque données reçues par les raycasts. 
Son principe est de lancer plusieurs dizaines de raycasts, chacun d’eux est associé à une zone autour du vaisseau (droite, gauche ou devant). 
Le danger d’un raycast est calculé à l’aide d’une fonction qui se base sur la distance à l’objet détecté. 
Ensuite chaque zone fait la somme des dangers calculés pour chacun de ses raycasts, et le vaisseau prend la décision d’aller vers la zone la moins dangereuse.
En rose l’IA à détection de zone de danger, en vert l’ia à détection d’obstacles. 
Le demi-cercle vert représente les raycasts utilisé pour calculer le danger des zones autour du vaisseaux.
L’ordonnée de la courbe avec le point rouge est utilisée pour calculer le danger d’un raycast présent dans la zone droite ou gauche. 
L’abscisse représente la distance du premier obstacle détecté par le raycast.
La seconde courbe est utilisée pour calculer le danger représenté par un raycast associé à la zone devant le vaisseau.

**IA par apprentissage supervisé**

L’objectif de cette IA est la prise de décision par l’ordinateur avec un comportement humain. 
Nous avons entraîné un algorithme de classification puis importer le modèle dans unity. L’algorithme se décompose en 4 étapes :

- La création du jeu de données : Un algorithme permet de capturer les informations d’environnement du joueur. Les données sont labellisées avec la décision du joueur. Elles sont enregistrées dans un fichier excel pour faciliter la lecture.

- L’import et la mise en forme des données : Les données nécessitent un prétraitement pour être utilisées. Une normalisation est réalisé avec les dimensions du terrain pour obtenir une échelle commune entre 0 et 1.

- L’entrainement d’un modèle : À l’aide de la bibliothèque Scikit Learn, nous créons un classifieur de type Random Forest (ensemble d’arbres décisionnels). Afin d’obtenir les meilleurs paramètres, la méthode GridSearchCV les optimise par une recherche croisée sur une grille de paramètres.

- L’utilisation du modèle dans Unity : L’export du modèle se fait à l’aide du module Pickle au format pkl. Ce modèle est appelé dans Unity au travers d’un fichier python qui prend en entrée les données capturées non labellisées et renvoie la classification -1, 0 ou 1. 

---------------------------------------------------------------------------------------

Credits: Frank Bigand, Emile Levast, Alex Gautier
