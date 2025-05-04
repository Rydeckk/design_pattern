# Robot Factory

Ce projet implémente une usine de fabrication de robots en utilisant des concepts de programmation orientée objet et des modèles de conception. L'application permet de gérer un inventaire de pièces, de vérifier la disponibilité des stocks, de produire des robots, et de générer des instructions d'assemblage.

# Release 1.0

## Fonctionnalités

- **Gestion de l'inventaire** : Ajout, affichage et mise à jour des stocks de pièces.
- **Vérification des commandes** : Vérifie si les pièces nécessaires pour produire des robots sont disponibles.
- **Production de robots** : Génère des robots en fonction des commandes et met à jour les stocks.
- **Instructions d'assemblage** : Génère les étapes nécessaires pour assembler les robots.

## Robots disponibles

- RobotI
- RobotII
- RobotIII

## Commandes disponibles

Voici les commandes que vous pouvez utiliser dans l'application :

### 1. `STOCKS`
Affiche l'inventaire actuel.

**Exemple :**
`STOCKS`

### 2. `NEEDED_STOCKS`
Affiche les pièces nécessaires pour produire une quantité donnée de robots.

**Exemple :**
`NEEDED_STOCKS 2 RobotI, 3 RobotII`

### 3. `INSTRUCTIONS`
Génère les instructions d'assemblage pour une quantité donnée de robots.

**Exemple :**
`INSTRUCTIONS 2 RobotI, 3 RobotII`

### 4. `VERIFY`
Vérifie si les pièces nécessaires pour produire une quantité donnée de robots sont disponibles.

**Exemple :**
`VERIFY 2 RobotI, 3 RobotII`

### 5. `PRODUCE`
Produit les robots en fonction des quantités spécifiées et met à jour les stocks.

**Exemple :**
`PRODUCE 2 RobotI, 3 RobotII`

### 6. `EXIT`
Quitte l'application.