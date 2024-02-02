Project Web Server II

Votre tâche pour le projet sera d’implémenter l’application Fullstack en créant un.net Core WEB avec SQL et consommez l’API dans un projet Razor Page.
Le projet est ouvert, vous déciderez quel sera le type de l’application, mais
toutes les applications doivent suivre l’exigence minimale au moins. 

Exigences du projet :
•Flux de connexion / d’inscription
o L’application doit avoir une page d’inscription et de connexion. Et les
points de terminaison de connexion et d’inscription de l’API (contrôleur d’utilisateur).
o Toute fonctionnalité à l’intérieur de l’application ne peut être
accessible que par l’utilisateur enregistré.
•Contrôleurs
o L’API doit avoir au moins deux autres contrôleurs (sans compter le
contrôleur utilisateur).
o Tous les contrôleurs doivent avoir au moins un point de
terminaison pour chacun des verbes HTTP suivants : GET, POST, PUT et DELETE.
o Chaque contrôleur correspondra à un modèle et ils doivent avoir
une relation (la relation n’a pas besoin d’être à l’intérieur de SQL).
o Dans le cas de la relation 1-N, si l’élément est supprimé, vous devez
également supprimer tous les N éléments qui lui sont liés.

•Frontend
o Tous les points de terminaison doivent être appelés dans le frontend.
o Cela signifie que l’utilisateur créera, lira, mettra à jour et supprimera
des éléments des deux tables de base de données à l’aide de http Request à partir du frontend.
o Utiliser des cookies pour enregistrer les informations d’identification de
l’utilisatrice dans l’APP.
